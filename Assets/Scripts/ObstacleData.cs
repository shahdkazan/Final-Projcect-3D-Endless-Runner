
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

   
    public string tagName = "Obstacle";


    public ObstacleMovementType movementType = ObstacleMovementType.None;
    public float speed = 0f;
}
