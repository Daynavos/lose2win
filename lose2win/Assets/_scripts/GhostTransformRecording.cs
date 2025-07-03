using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GhostTransformRecording", menuName = "Ghost Transform Recording")]
public class GhostTransformRecording : ScriptableObject {
    public enum game_states{
        start,
        first_run,
        second_run
    }
    public game_states game_state = game_states.start;
    public bool canMove = false;
    public bool canRecord = false;
    public List<TransformSnapshot> frames = new List<TransformSnapshot>();
    public bool isRecording = false;
    public void ClearRecording() {
        frames.Clear();
        isRecording = false;
    }
    public void StopRecording() {
        isRecording = false;
    }
    public void StartRecording() {
        if (canRecord)
        {
            isRecording = true;
        }
        
    }
}

[System.Serializable]
public class TransformSnapshot {
    public float timestamp;
    public Vector3 position;
    public Quaternion rotation;
}
