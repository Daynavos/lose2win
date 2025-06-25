using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour {
    public GameObject playerFirstRun;
    public GameObject playerSecondRun;
    public GameObject ghost;
    //public GhostInputRecording recordingSO;
    public GhostTransformRecording recordingSO;
    
    void Start() {
        if (recordingSO.hasFirstRunCompleted) {
            // Second run: show ghost + second player
            playerFirstRun.SetActive(false);
            ghost.SetActive(true);
            playerSecondRun.SetActive(true);
        } else {
            // First run: start clean
            playerFirstRun.SetActive(true);
            ghost.SetActive(false);
            playerSecondRun.SetActive(false);
        }
    }
    
    public void StartSecondRun() {
        if (!recordingSO.hasFirstRunCompleted) {
            //recordingSO.ClearRecording();
            recordingSO.StartRecording();
            recordingSO.hasFirstRunCompleted = true;
        } else {
            recordingSO.StopRecording();
            recordingSO.hasFirstRunCompleted = true;
            ResetSceneForReplay(); 
        }
    }

    void ResetSceneForReplay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}