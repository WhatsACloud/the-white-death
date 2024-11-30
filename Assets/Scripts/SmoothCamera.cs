using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform player;          // The player's Transform
    public float smoothSpeed = 0.125f; // How smooth the camera follows
    public Vector3 offset;            // Offset from the player's position

    void FixedUpdate()
    {
        //// Desired camera position based on the player's position + offset
        //Vector3 desiredPosition = player.position + offset;

        //// Lock the Z position to the camera's original Z position
        //desiredPosition.z = transform.position.z;

        //// Smoothly interpolate the camera's position using Lerp
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the new position
        //transform.position = smoothedPosition;
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.z = transform.position.z; // Lock Z position
        transform.position = desiredPosition;    // Snap to position
    }
}

