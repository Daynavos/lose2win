using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour {
    public GameObject playerFirstRun;
    public GameObject playerSecondRun;
    public GameObject ghost;
    //public GhostInputRecording recordingSO;
    public GhostTransformRecording recordingSO;
    public GameObject LosePanel;
    
    void Start() {
        
        switch (recordingSO.game_state)
        {
            case GhostTransformRecording.game_states.start:
                Time.timeScale = 1;
                recordingSO.ClearRecording();
                recordingSO.canMove = false;
                playerFirstRun.SetActive(true);
                ghost.SetActive(false);
                playerSecondRun.SetActive(false);
                break;
            case GhostTransformRecording.game_states.first_run:
                recordingSO.game_state = GhostTransformRecording.game_states.start;
                recordingSO.canMove = false;
                break;
            case GhostTransformRecording.game_states.second_run:
                playerFirstRun.SetActive(false);
                ghost.SetActive(true);
                playerSecondRun.SetActive(true);
                break;
        }
    }
    
    public void RecordStopButton() {
        switch (recordingSO.game_state)
        {
            case GhostTransformRecording.game_states.start:
                recordingSO.game_state = GhostTransformRecording.game_states.first_run;
                recordingSO.canMove = true;
                recordingSO.StartRecording();
                break;
            case GhostTransformRecording.game_states.first_run:
                recordingSO.game_state = GhostTransformRecording.game_states.second_run;
                recordingSO.StopRecording();
                ResetSceneForReplay(); 
                break;
            case GhostTransformRecording.game_states.second_run:
                recordingSO.game_state = GhostTransformRecording.game_states.start;
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