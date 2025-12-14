//using UnityEngine;

//[CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data")]
//public class ObstacleData : ScriptableObject
//{
//    public GameObject prefab;      // The obstacle prefab
//    public Vector3 size = Vector3.one;   // Optional: override size
//    public float speed = 0f;       // For moving obstacles
//    [Range(0f, 1f)] public float spawnChance = 0.5f; // Probability of spawning
//    public int lane = 0;           // Optional: lane index
//}
//using UnityEngine;

//[CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data")]
//public class ObstacleData : ScriptableObject
//{
//    public GameObject prefab;
//    public Vector3 size = Vector3.one;
//    public float speed = 0f;
//    [Range(0f, 1f)] public float spawnChance = 0.5f;


//    [Header("Runtime Options")]
//    public string tagName = "Obstacle"; // Tag to assign when spawned
//}
//using UnityEngine;

//public enum ObstacleMovementType
//{
//    None,
//    Rolling
//}

//[CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data")]
//public class ObstacleData : ScriptableObject
//{
//    public GameObject prefab;
//    public Vector3 size = Vector3.one;

//    [Range(0f, 1f)]
//    public float spawnChance = 0.5f;

//    [Header("Runtime Options")]
//    public string tagName = "Obstacle";

//    [Header("Movement")]
//    public ObstacleMovementType movementType = ObstacleMovementType.None;
//    public float speed = 0f;
//}
using UnityEngine;

public enum ObstacleMovementType
{
    None,
    Rolling
}

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Obstacles/Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public GameObject prefab;
    public Vector3 size = Vector3.one;

    [Range(0f, 1f)]
    public float spawnChance = 0.5f;

    [Header("Runtime Options")]
    public string tagName = "Obstacle";


    [Header("Movement")]
    public ObstacleMovementType movementType = ObstacleMovementType.None;
    public float speed = 0f;
}
