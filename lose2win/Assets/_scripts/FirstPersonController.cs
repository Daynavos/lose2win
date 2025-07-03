using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {
    public Transform cameraTransform;
    public float moveSpeed = 6f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    float verticalLookRotation = 0f;
    
    public float interactRange = 3f;
    public LayerMask interactLayer; // Optional: filter what can be hit


    public GhostTransformRecording recordingSO;

    void Start() {
        controller = GetComponent<CharacterController>();
        if (!cameraTransform) cameraTransform = Camera.main.transform;

        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        if (!recordingSO.canMove) return;

        // --- MOUSE LOOK ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera (pitch)
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraTransform.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);

        // --- MOVEMENT ---
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // --- JUMPING ---
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // --- GRAVITY ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }
}