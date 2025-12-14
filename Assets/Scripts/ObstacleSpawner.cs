////based on player 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;    // List of obstacle types
//    public Transform player;            // Reference to player
//    public float spawnDistance = 30f;   // Distance in front of player
//    public float spawnIntervalMin = 1f;
//    public float spawnIntervalMax = 3f;
//    public float laneWidth = 2f;        // Width of a lane

//    private void Start()
//    {
//        StartCoroutine(SpawnRoutine());
//    }

//    private IEnumerator SpawnRoutine()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

//            // Pick a random obstacle based on spawn chance
//            ObstacleData selected = PickObstacle();
//            if (selected != null)
//            {
//                SpawnObstacle(selected);
//            }
//        }
//    }

//    private ObstacleData PickObstacle()
//    {
//        List<ObstacleData> candidates = new List<ObstacleData>();
//        foreach (var obs in obstacles)
//        {
//            if (Random.value <= obs.spawnChance)
//                candidates.Add(obs);
//        }

//        if (candidates.Count == 0) return null;

//        return candidates[Random.Range(0, candidates.Count)];
//    }

//    private void SpawnObstacle(ObstacleData data)
//    {
//        // Random lane offset
//        float laneOffset = Random.Range(-1, 2) * laneWidth; // -1, 0, 1 lanes

//        Vector3 spawnPos = player.position + player.forward * spawnDistance;
//        spawnPos.z += laneOffset; // adjust for Z-axis lanes

//        GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

//        // Optional: apply size variation
//        obj.transform.localScale = data.size;

//        // Optional: assign moving speed if obstacle moves
//        MovingObstacle mover = obj.GetComponent<MovingObstacle>();
//        if (mover != null)
//            mover.speed = data.speed;
//    }
//}

//bsed on spawne rin segennt
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;         // List of obstacle types
//    public int maxObstaclesPerSegment = 3;   // Max obstacles per segment
//    public float segmentWidth = 2f;          // Z-axis width
//    public float segmentLength = 10f;        // X-axis length
//    public float minSpawnDelay = 1f;         // Min time between obstacle spawns
//    public float maxSpawnDelay = 3f;         // Max time between obstacle spawns

//    private void Start()
//    {
//        StartCoroutine(SpawnObstaclesCoroutine());
//    }

//    private IEnumerator SpawnObstaclesCoroutine()
//    {
//        int spawnedCount = 0;

//        while (spawnedCount < maxObstaclesPerSegment)
//        {
//            // Pick an obstacle
//            ObstacleData selected = PickObstacle();
//            if (selected != null)
//            {
//                // Random position inside segment
//                float xPos = Random.Range(0f, segmentLength);
//                float zPos = Random.Range(-segmentWidth / 2f, segmentWidth / 2f);
//                Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);

//                // Instantiate obstacle as child of the segment
//                GameObject obj = Instantiate(selected.prefab, spawnPos, Quaternion.identity, transform);
//                obj.transform.localScale = selected.size;

//                // Optional: assign moving speed if obstacle moves
//                MovingObstacle mover = obj.GetComponent<MovingObstacle>();
//                if (mover != null)
//                    mover.speed = selected.speed;

//                spawnedCount++;
//            }

//            // Wait for a random interval before spawning next obstacle
//            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
//            yield return new WaitForSeconds(delay);
//        }
//    }

//    private ObstacleData PickObstacle()
//    {
//        List<ObstacleData> candidates = new List<ObstacleData>();
//        foreach (var obs in obstacles)
//        {
//            if (Random.value <= obs.spawnChance)
//                candidates.Add(obs);
//        }

//        if (candidates.Count == 0) return null;

//        return candidates[Random.Range(0, candidates.Count)];
//    }
//}

////another try based on segemnt 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SegmentObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;       // List of obstacle types
//    public int maxObstaclesPerSegment = 3; // Max obstacles in this segment
//    //public float segmentWidth = 2f;        // Z-axis width of segment
//    //public float segmentLength = 10f;      // X-axis length of segment
//    public float minX = -50f;
//    public float maxX = 50f;
//    public float minZ = -7f;
//    public float maxZ = 7f;


//    private void Start()
//    {
//        SpawnObstaclesInSegment();
//    }

//    private void SpawnObstaclesInSegment()
//    {
//        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerSegment + 1);

//        for (int i = 0; i < obstaclesToSpawn; i++)
//        {
//            ObstacleData selected = PickObstacle();
//            if (selected == null) continue;

//            // Random position inside the segment
//            //float xPos = Random.Range(0f, segmentLength);
//            //float zPos = Random.Range(-segmentWidth / 2f, segmentWidth / 2f);
//            //Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);
//            float xPos = Random.Range(minX, maxX);
//            float zPos = Random.Range(minZ, maxZ);
//            Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);


//            // Instantiate obstacle as child of segment
//            GameObject obj = Instantiate(selected.prefab, spawnPos, Quaternion.identity, transform);
//            obj.transform.localScale = selected.size;

//            // Optional: assign moving speed if obstacle moves
//            MovingObstacle mover = obj.GetComponent<MovingObstacle>();
//            if (mover != null)
//                mover.speed = selected.speed;
//        }
//    }

//    private ObstacleData PickObstacle()
//    {
//        List<ObstacleData> candidates = new List<ObstacleData>();
//        foreach (var obs in obstacles)
//        {
//            if (Random.value <= obs.spawnChance)
//                candidates.Add(obs);
//        }

//        if (candidates.Count == 0) return null;
//        return candidates[Random.Range(0, candidates.Count)];
//    }
//}

//another way to spawn in segment but with time intevral 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;       // List of obstacle types
//    public int maxObstaclesPerSegment = 3; // Max obstacles in this segment
//    public float minX = -50f;
//    public float maxX = 50f;
//    public float minZ = -7f;
//    public float maxZ = 7f;
//    public float minSpawnInterval = 1f;    // Min delay between obstacles
//    public float maxSpawnInterval = 3f;    // Max delay between obstacles

//    private void Start()
//    {
//        StartCoroutine(SpawnObstaclesRoutine());
//    }

//    private IEnumerator SpawnObstaclesRoutine()
//    {
//        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerSegment + 1);

//        for (int i = 0; i < obstaclesToSpawn; i++)
//        {
//            ObstacleData selected = PickObstacle();
//            if (selected != null)
//            {
//                // Random position inside the segment
//                float xPos = Random.Range(minX, maxX);
//                float zPos = Random.Range(minZ, maxZ);
//                Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);

//                // Instantiate obstacle as child of segment
//                //GameObject obj = Instantiate(selected.prefab, spawnPos, Quaternion.identity, transform);

//                Vector3 spawnPosWithPrefabY = spawnPos;
//                spawnPosWithPrefabY.y = selected.prefab.transform.position.y;

//                GameObject obj = Instantiate(
//                    selected.prefab,
//                    spawnPosWithPrefabY,
//                    selected.prefab.transform.rotation,
//                    transform
//                );
//                //GameObject obj = Instantiate(selected.prefab, spawnPos, selected.prefab.transform.rotation, transform);
//                obj.transform.localScale = selected.size;

//                // Assign tag from ScriptableObject
//                obj.tag = selected.tagName;
//                //    // Optional: assign moving speed if obstacle moves
//                //    MovingObstacle mover = obj.GetComponent<MovingObstacle>();
//                //    if (mover != null)
//                //        mover.speed = selected.speed;
//                // Add movement script if obstacle type requires it
//                if (selected.movementType == ObstacleMovementType.Rolling)
//                {
//                    RollingRock rock = obj.AddComponent<RollingRock>();

//                    rock.moveSpeed = selected.speed;
//                }
//            }

//            // Wait a random interval before spawning next obstacle
//            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
//            yield return new WaitForSeconds(waitTime);
//        }
//    }

//    private ObstacleData PickObstacle()
//    {
//        List<ObstacleData> candidates = new List<ObstacleData>();
//        foreach (var obs in obstacles)
//        {
//            if (Random.value <= obs.spawnChance)
//                candidates.Add(obs);
//        }

//        if (candidates.Count == 0) return null;
//        return candidates[Random.Range(0, candidates.Count)];
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleData[] obstacles;       // List of obstacle types
    public int maxObstaclesPerSegment = 3; // Max obstacles in this segment
    public float minX = -50f;
    public float maxX = 50f;
    public float minZ = -7f;
    public float maxZ = 7f;
    public float minSpawnInterval = 1f;    // Min delay between obstacles
    public float maxSpawnInterval = 3f;    // Max delay between obstacles

    private void Start()
    {
        StartCoroutine(SpawnObstaclesRoutine());
    }

    private IEnumerator SpawnObstaclesRoutine()
    {
        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerSegment + 1);

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            ObstacleData selected = PickObstacle();
            if (selected != null)
            {
                // Random position inside the segment
                float xPos = Random.Range(minX, maxX);
                float zPos = Random.Range(minZ, maxZ);
                Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);

                // Instantiate obstacle as child of segment
                Vector3 spawnPosWithPrefabY = spawnPos;
                spawnPosWithPrefabY.y = selected.prefab.transform.position.y;

                GameObject obj = Instantiate(
                    selected.prefab,
                    spawnPosWithPrefabY,
                    selected.prefab.transform.rotation,
                    transform
                );
                obj.transform.localScale = selected.size;

                // Assign tag from ScriptableObject
                obj.tag = selected.tagName;
                //    // Optional: assign moving speed if obstacle moves
                //    MovingObstacle mover = obj.GetComponent<MovingObstacle>();
                //    if (mover != null)
                //        mover.speed = selected.speed;
                // Add movement script if obstacle type requires it
                if (selected.movementType == ObstacleMovementType.Rolling)
                {
                    RollingRock rock = obj.AddComponent<RollingRock>();

                    rock.moveSpeed = selected.speed;
                }
            }

            // Wait a random interval before spawning next obstacle
            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private ObstacleData PickObstacle()
    {
        List<ObstacleData> candidates = new List<ObstacleData>();
        foreach (var obs in obstacles)
        {
            if (Random.value <= obs.spawnChance)
                candidates.Add(obs);
        }

        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }
}
