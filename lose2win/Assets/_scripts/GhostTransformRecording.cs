using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GhostTransformRecording", menuName = "Ghost Transform Recording")]
public class GhostTransformRecording : ScriptableObject {
    public List<TransformSnapshot> frames = new List<TransformSnapshot>();
    public bool isRecording = false;
    public bool hasFirstRunCompleted = false;
    public void ClearRecording() {
        frames.Clear();
        isRecording = false;
    }
    public void StopRecording() {
        isRecording = false;
    }
    public void StartRecording() {
        isRecording = true;
    }
}

[System.Serializable]
public class TransformSnapshot {
    public float timestamp;
    public Vector3 position;
    public Quaternion rotation;
}
