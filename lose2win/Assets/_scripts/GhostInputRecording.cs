using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GhostInputRecording", menuName = "Ghost Input Recording")]
public class GhostInputRecording : ScriptableObject {
    public List<InputSnapshot> inputFrames = new List<InputSnapshot>();
    public bool isRecordingActive = false;

    public void ClearRecording() {
        inputFrames.Clear();
        isRecordingActive = false;
    }

    public void StopRecording() {
        isRecordingActive = false;
    }
    
    public void StartRecording() {
        isRecordingActive = true;
    }
}

[System.Serializable]
public class InputSnapshot {
    public float timestamp;
    public Vector2 moveInput;
    public bool jumpPressed;
}