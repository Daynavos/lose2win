using UnityEngine;

public class TransformGhost : MonoBehaviour {
    public GhostTransformRecording recordingSO;
    private int currentIndex = 0;

    void FixedUpdate() {
        if (currentIndex >= recordingSO.frames.Count) return;

        var frame = recordingSO.frames[currentIndex];
        transform.position = frame.position;
        transform.rotation = frame.rotation;

        currentIndex++;
    }
}