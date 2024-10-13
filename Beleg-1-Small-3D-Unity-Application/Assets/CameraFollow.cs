using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;  // Reference to the player's transform
    public Vector3 offset;             // Offset distance between the camera and the player
    public float smoothSpeed = 0.125f; // Smoothness of the camera's movement

    void Start()
    {
        // Optionally, you can initialize the offset based on the initial camera and player positions
        if (playerTransform != null)
        {
            offset = transform.position - playerTransform.position;
        }
    }

    void LateUpdate()
    {
        // Target camera position based on the player's position and the desired offset
        Vector3 desiredPosition = playerTransform.position + offset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optionally, keep the camera looking at the player
        transform.LookAt(playerTransform);
    }
}
