using UnityEngine;

public class InputRecorder : MonoBehaviour {
    public GhostInputRecording recordingSO;
    private float currentTime;

    void Start() {
        currentTime = 0;
    }
    void FixedUpdate() {
        if (!recordingSO.isRecordingActive) return;

        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool jump = Input.GetButton("Jump");

        recordingSO.inputFrames.Add(new InputSnapshot {
            timestamp = currentTime,
            moveInput = move,
            jumpPressed = jump
        });

        currentTime += Time.fixedDeltaTime;
    }

}