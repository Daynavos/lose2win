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
    
    void Start() {
        playerFirstRun.SetActive(true);
        ghost.SetActive(false);
        playerSecondRun.SetActive(false);
        recordingSO.game_state = GhostTransformRecording.game_states.start;
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
            ghost.SetActive(true);
            recordingSO.canMove = true;

            buttonImage.sprite = stopSprite;
            buttonLabel.text = "Stop";
            break;

        case GhostTransformRecording.game_states.second_run:
            // --- Reset to Initial State ---
            recordingSO.ClearRecording();
            recordingSO.canMove = false;
            recordingSO.game_state = GhostTransformRecording.game_states.start;

            Destroy(ghost); // Or reset it manually if you plan to reuse
            // playerFirstRun.SetActive(true);
            // playerSecondRun.SetActive(false);
            // ghost.SetActive(false); // not needed if destroyed

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