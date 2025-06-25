using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GhostController : MonoBehaviour {
    public GhostInputRecording recordingSO;
    private CharacterController controller;
    private int currentIndex;
    private Vector3 velocity;
    private bool isGrounded;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        if (currentIndex >= recordingSO.inputFrames.Count) return;

        var frame = recordingSO.inputFrames[currentIndex];
        Vector3 move = new Vector3(frame.moveInput.x, 0, frame.moveInput.y);
        
        controller.Move(transform.TransformDirection(move.normalized) * speed * Time.fixedDeltaTime);

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // Jumping
        if (frame.jumpPressed && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravity
        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);

        currentIndex++;
    }
}