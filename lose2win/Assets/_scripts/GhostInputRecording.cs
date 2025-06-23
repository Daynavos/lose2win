using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GhostInputRecording", menuName = "Ghost Input Recording")]
public class GhostInputRecording : ScriptableObject {
    public List<InputSnapshot> inputFrames = new List<InputSnapshot>();
    public bool isRecordingActive = true;

    public void ClearRecording() {
        inputFrames.Clear();
        isRecordingActive = true;
    }

    public void StopRecording() {
        isRecordingActive = false;
    }
}

[System.Serializable]
public class InputSnapshot {
    public float timestamp;
    public Vector2 moveInput;
    public bool jumpPressed;
}