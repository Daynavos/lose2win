using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopManager : MonoBehaviour {
    public GameObject playerFirstRun;
    public GameObject playerSecondRun;
    public GameObject ghost;

    public GhostInputRecording recordingSO;

    public void StartSecondRun() {
        // Option 1: Reload scene and use a GameManager to switch states
        // Option 2: Just swap active objects
        recordingSO.StopRecording();
        Debug.Log("Start Second Run");
        playerFirstRun.SetActive(false);
        ghost.SetActive(true);
        playerSecondRun.SetActive(true);
        
        
    }
}