using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour {
    public Transform cameraTransform;
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start() {
        controller = GetComponent<CharacterController>();
        if (!cameraTransform) cameraTransform = Camera.main.transform;
    }

    void Update() {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(h, 0, v).normalized;

        if (inputDirection.magnitude >= 0.1f) {
            // Calculate move direction relative to camera
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
