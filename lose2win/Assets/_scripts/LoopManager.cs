using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoopManager : MonoBehaviour {
    public GameObject playerFirstRun;
    public GameObject playerSecondRun;
    public GameObject ghost;
    //public GhostInputRecording recordingSO;
    public GhostTransformRecording recordingSO;
    public GameObject LosePanel;
    public UnityEngine.UI.Image buttonImage;
    public TMP_Text buttonLabel;
    public Sprite recordSprite;
    public Sprite replaySprite;
    public Sprite stopSprite;

    public Transform originPad;
    public Transform nextPhase;
    
    public GhostController ghostController;
    
    void Start() {
        playerFirstRun.SetActive(true);
        ghost.SetActive(false);
        playerSecondRun.SetActive(false);
        recordingSO.game_state = GhostTransformRecording.game_states.start;
        recordingSO.ClearRecording();
        recordingSO.canMove = false;

        ghostController = ghost.GetComponent<GhostController>(); // ✅ safe here
    }

    
public SceneStateRecorder stateRecorder; // Assign this in the Inspector

public void RecordStopButton()
{
    switch (recordingSO.game_state)
    {
        case GhostTransformRecording.game_states.start:
            // --- Begin First Run ---
            stateRecorder.TakeSnapshot(); // Capture current state before run
            recordingSO.StartRecording();
            recordingSO.canMove = true;
            recordingSO.game_state = GhostTransformRecording.game_states.first_run;

            playerFirstRun.SetActive(true);
            playerSecondRun.SetActive(false);
            ghost.SetActive(false);

            buttonImage.sprite = replaySprite;
            buttonLabel.text = "Replay";
            break;

        case GhostTransformRecording.game_states.first_run:
            // --- End First Run / Begin Second Run ---
            recordingSO.StopRecording();
            recordingSO.game_state = GhostTransformRecording.game_states.second_run;

            stateRecorder.RestoreSnapshot(); // Restore scene to start-of-run state

            playerFirstRun.SetActive(false);
            playerSecondRun.SetActive(true);
            var cc = playerSecondRun.GetComponent<CharacterController>();
// Handle second player movement
            if (cc != null) cc.enabled = false;
            playerSecondRun.transform.position = originPad.position;
            if (cc != null) cc.enabled = true;

// Handle ghost movement
            ghost.SetActive(true);
            var ghostCC = ghost.GetComponent<CharacterController>();
            if (ghostCC != null) ghostCC.enabled = false;
            ghost.transform.position = originPad.position;
            if (ghostCC != null) ghostCC.enabled = true;

            

            
            recordingSO.canMove = true;

            buttonImage.sprite = stopSprite;
            buttonLabel.text = "Stop";
            break;

        case GhostTransformRecording.game_states.second_run:
            // --- Reset to Initial State ---
            recordingSO.ClearRecording();
            recordingSO.game_state = GhostTransformRecording.game_states.start;
            if (ghostController != null)
            {
                ghostController.currentIndex = 0;
            }
            else
            {
                Debug.LogWarning("GhostController was null when trying to reset playback.");
            }

            // Disable ghost and second player
            ghost.SetActive(false);
            playerSecondRun.SetActive(false);

            // Move first player to where second player ended
            var cc1 = playerFirstRun.GetComponent<CharacterController>();
            if (cc1 != null) cc1.enabled = false;
            playerFirstRun.transform.position = playerSecondRun.transform.position;
            playerFirstRun.transform.rotation = playerSecondRun.transform.rotation;
            if (cc1 != null) cc1.enabled = true;

            playerFirstRun.SetActive(true);
            recordingSO.canMove = true;
            recordingSO.canRecord = false;

            // Update UI
            buttonImage.sprite = recordSprite;
            buttonLabel.text = "Record";
            break;
    }
}


    public void LoseLevel()
    {
        recordingSO.game_state = GhostTransformRecording.game_states.start;
        Time.timeScale = 0;
        LosePanel.SetActive(true);
    }
    void ResetSceneForReplay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}