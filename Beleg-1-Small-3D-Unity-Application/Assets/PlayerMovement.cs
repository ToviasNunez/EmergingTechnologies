using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;     // Speed of horizontal movement
    public float jumpForce = 7f;     // Force of the jump
    private Rigidbody rb;
    private bool isGrounded;         // Check if player is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Reference to the Rigidbody component
    }

    void Update()
    {
        // Get horizontal and vertical input
        float moveInput = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        // Create a vector for movement in 3D space
        Vector3 move = new Vector3(moveInput, 0, moveInputVertical) * moveSpeed * Time.deltaTime;

        // Apply movement
        rb.MovePosition(transform.position + move);

        // Jumping logic
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
