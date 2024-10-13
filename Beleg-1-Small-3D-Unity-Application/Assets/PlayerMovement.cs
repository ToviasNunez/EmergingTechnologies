using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed of horizontal movement
    public float jumpForce = 7f;         // Force of the jump
    public Transform cameraTransform;    // Reference to the camera
    private Rigidbody rb;
    private bool isGrounded;             // Check if player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Reference to the Rigidbody component

        // If cameraTransform is not assigned, find the main camera
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Get input for forward/backward and left/right movement
        float moveInputVertical = Input.GetAxis("Vertical");   // Forward/backward movement
        float moveInputHorizontal = Input.GetAxis("Horizontal"); // Left/right movement (optional)

        // Get the camera's forward and right directions for relative movement
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Ignore vertical movement from the camera
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize to prevent faster diagonal movement
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Combine inputs with camera direction to move the player relative to the camera
        Vector3 moveDirection = (cameraForward * moveInputVertical + cameraRight * moveInputHorizontal).normalized;

        // Move the player in the direction of the camera's forward and right
        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;

        // Apply movement
        rb.MovePosition(transform.position + move);

        // Rotate the player to face the direction of movement
        if (moveDirection != Vector3.zero)
        {
            // Rotate 180 degrees to compensate for the backward model orientation
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection) * Quaternion.Euler(0, 180f, 0);
            transform.rotation = targetRotation;
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if player is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player has landed on the ground");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if player has left the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
