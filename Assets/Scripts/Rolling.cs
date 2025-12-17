using UnityEngine;

public class Rolling : MonoBehaviour
{
    // Forward movement speed
    public float moveSpeed;

    // Rotation speed (degrees per second)
    public float rollSpeed = 180f;

    void Update()
    {
        // Move obstacle forward along world X-axis
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);

        // Rotate obstacle around its own Y-axis
        transform.Rotate(Vector3.up, rollSpeed * Time.deltaTime, Space.Self);
    }
}
