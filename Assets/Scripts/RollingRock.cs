using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public float moveSpeed;
    public float rollSpeed = 180f;

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, rollSpeed * Time.deltaTime, Space.Self);
    }
}
//using UnityEngine;

//public class RollingRock : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float rotationSpeed = 360f; // degrees per second

//    void Update()
//    {
//        // Move on X axis
//        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

//        // Rotate around X axis
//        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
//    }
//}

//using UnityEngine;

//public class RollingRock : MonoBehaviour
//{
//    public float moveSpeed = 5f;    // Movement speed along X
//    public float radius = 0.5f;     // Approximate radius of the log

//    void Update()
//    {
//        // Move along positive X
//        float distance = moveSpeed * Time.deltaTime;
//        transform.position += Vector3.right * distance;

//        // Rotate around Z axis to roll on ground
//        float rotationAngle = (distance / (2 * Mathf.PI * radius)) * 360f;
//        transform.Rotate(Vector3.forward, rotationAngle, Space.Self);
//    }
//}

//good rolls and moves
//using UnityEngine;

//public class RollingRock : MonoBehaviour
//{
//    public float moveSpeed = 5f;   // Linear movement speed
//    private float radius = 0.5f;   // Approximate radius of the rock

//    void Start()
//    {
//        // If the rock has a collider, automatically get radius from bounds
//        Collider col = GetComponent<Collider>();
//        if (col != null)
//        {
//            radius = col.bounds.extents.y;
//        }
//    }

//    void Update()
//    {
//        // Move along positive X
//        float moveDelta = moveSpeed * Time.deltaTime;
//        transform.Translate(Vector3.right * moveDelta, Space.World);

//        // Rotate around Z axis based on distance moved (rolling on ground)
//        float rotationDegrees = (moveDelta / (2 * Mathf.PI * radius)) * 360f;
//        transform.Rotate(Vector3.forward, -rotationDegrees, Space.Self);
//    }
//}
//using UnityEngine;

//public class RollingRock : MonoBehaviour
//{
//    public float rotationSpeed = 360f; // degrees per second

//    void Update()
//    {
//        // Rotate around X axis only
//        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
//    }
//}
