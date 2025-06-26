using UnityEngine;

public class TransformRecorder : MonoBehaviour {
    public GhostTransformRecording recordingSO;
    private float currentTime;

    void Start() {
        // if (recordingSO.isRecording)
        //     recordingSO.ClearRecording();

        currentTime = 0;
    }

    void FixedUpdate() {
        if (!recordingSO.isRecording) return;

        recordingSO.frames.Add(new TransformSnapshot {
            timestamp = currentTime,
            position = transform.position,
            rotation = transform.rotation
        });

        currentTime += Time.fixedDeltaTime;
    }
}