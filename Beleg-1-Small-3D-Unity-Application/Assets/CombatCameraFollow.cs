using UnityEngine;

public class CombatCameraFollow : MonoBehaviour
{
    public Transform playerTransform;   // Reference to the player
    public Vector3 offset = new Vector3(0, 5, -10);  // Default offset to position the camera behind the player
    public float smoothSpeed = 0.125f;  // Smoothness of the camera's follow movement
    public float forwardOffset = 3f;    // How far ahead of the player the camera looks
    public float rotationSpeed = 5f;    // Speed at which the camera rotates around the player
    public float zoomSpeed = 4f;        // Speed of zooming in/out with scroll
    public float minZoom = 5f;          // Minimum zoom distance
    public float maxZoom = 15f;         // Maximum zoom distance

    private float currentZoom = 0f;     // Current zoom level

    void Start()
    {
        // Ensure playerTransform is set
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void LateUpdate()
    {
        // Handle zoom in/out using scroll wheel
        HandleZoom();

        // Calculate the player's forward direction (the direction the player is moving in)
        Vector3 playerForward = playerTransform.forward;

        // Calculate where the camera should be (slightly ahead in the direction the player is moving)
        Vector3 desiredPosition = playerTransform.position - (playerForward * forwardOffset) + offset + (Vector3.up * offset.y);

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Smoothly rotate the camera to look towards the player
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Zoom functionality using scroll wheel input
    void HandleZoom()
    {
        // Scroll wheel input for zooming in/out
        currentZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        // Clamp zoom to avoid going too far or too close
        currentZoom = Mathf.Clamp(currentZoom, -maxZoom, -minZoom);

        // Adjust the offset Z position for zooming
        offset.z = currentZoom;
    }
}
