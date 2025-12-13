using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    // Minimum distance to trigger smoothing
    public float deadZone = 0.01f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;

            // If we're very close to the desired position, just snap to it
            if (Vector3.Distance(transform.position, desiredPosition) < deadZone)
            {
                transform.position = desiredPosition;
            }
            else
            {
                // Otherwise, smoothly interpolate
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    desiredPosition,
                    ref velocity,
                    smoothSpeed
                );
            }

            transform.LookAt(player);
        }

    }
}
