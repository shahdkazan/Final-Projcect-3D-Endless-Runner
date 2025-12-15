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

//best
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
//                Vector3 spawnPosWithPrefabY = spawnPos;
//                spawnPosWithPrefabY.y = selected.prefab.transform.position.y;

//                GameObject obj = Instantiate(
//                    selected.prefab,
//                    spawnPosWithPrefabY,
//                    selected.prefab.transform.rotation,
//                    transform
//                );
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
//best with lanes 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;
//    public int maxObstaclesPerSegment = 3;

//    public float minX = -50f;
//    public float maxX = 50f;

//    [Header("Lanes")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    public float minSpawnInterval = 1f;
//    public float maxSpawnInterval = 3f;

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
//                float xPos = Random.Range(minX, maxX);
//                float zPos = lanes[Random.Range(0, lanes.Length)];

//                Vector3 spawnPos = transform.position + new Vector3(xPos, 0f, zPos);
//                spawnPos.y = selected.prefab.transform.position.y;

//                GameObject obj = Instantiate(
//                    selected.prefab,
//                    spawnPos,
//                    selected.prefab.transform.rotation,
//                    transform
//                );

//                obj.transform.localScale = selected.size;
//                obj.tag = selected.tagName;

//                if (selected.movementType == ObstacleMovementType.Rolling)
//                {
//                    RollingRock rock = obj.AddComponent<RollingRock>();
//                    rock.moveSpeed = selected.speed;
//                }
//            }

//            yield return new WaitForSeconds(
//                Random.Range(minSpawnInterval, maxSpawnInterval)
//            );
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

//sequential
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;
//    public int maxObstaclesPerSegment = 3;

//    public float startX = -50f;      // starting X position
//    public float minSpacing = 5f;    // minimum X distance between obstacles
//    public float maxSpacing = 10f;   // maximum X distance between obstacles

//    [Header("Lanes")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    private void Start()
//    {
//        SpawnObstacleSequence();
//    }

//    private void SpawnObstacleSequence()
//    {
//        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerSegment + 1);
//        float currentX = startX;

//        for (int i = 0; i < obstaclesToSpawn; i++)
//        {
//            ObstacleData selected = PickObstacle();
//            if (selected == null) continue;

//            // Choose lane randomly
//            float zPos = lanes[Random.Range(0, lanes.Length)];

//            Vector3 spawnPos = transform.position + new Vector3(currentX, selected.prefab.transform.position.y, zPos);

//            GameObject obj = Instantiate(
//                selected.prefab,
//                spawnPos,
//                selected.prefab.transform.rotation,
//                transform
//            );

//            obj.transform.localScale = selected.size;
//            obj.tag = selected.tagName;

//            if (selected.movementType == ObstacleMovementType.Rolling)
//            {
//                RollingRock rock = obj.AddComponent<RollingRock>();
//                rock.moveSpeed = selected.speed;
//            }

//            // Increment X for the next obstacle
//            currentX += Random.Range(minSpacing, maxSpacing);
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
//sequnatikl no backward but using timing too 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ObstacleSpawner : MonoBehaviour
//{
//    public ObstacleData[] obstacles;
//    public int maxObstaclesPerSegment = 3;

//    [Header("Spawn Area")]
//    public float startX = 50f;         // starting X position (farther)
//    public float minSpacing = 5f;      // minimum distance backward
//    public float maxSpacing = 10f;     // maximum distance backward

//    [Header("Lanes")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    [Header("Spawn Timing")]
//    public float minSpawnInterval = 1f;
//    public float maxSpawnInterval = 3f;

//    private void Start()
//    {
//        StartCoroutine(SpawnObstaclesRoutine());
//    }

//    private IEnumerator SpawnObstaclesRoutine()
//    {
//        int obstaclesToSpawn = Random.Range(1, maxObstaclesPerSegment + 1);
//        float lastX = startX;

//        for (int i = 0; i < obstaclesToSpawn; i++)
//        {
//            ObstacleData selected = PickObstacle();
//            if (selected != null)
//            {
//                // Random lane
//                float zPos = lanes[Random.Range(0, lanes.Length)];

//                // Random X backward from last obstacle
//                float xOffset = Random.Range(minSpacing, maxSpacing);
//                lastX -= xOffset; // move backward

//                Vector3 spawnPos = transform.position + new Vector3(lastX, selected.prefab.transform.position.y, zPos);

//                GameObject obj = Instantiate(
//                    selected.prefab,
//                    spawnPos,
//                    selected.prefab.transform.rotation,
//                    transform
//                );

//                obj.transform.localScale = selected.size;
//                obj.tag = selected.tagName;

//                if (selected.movementType == ObstacleMovementType.Rolling)
//                {
//                    RollingRock rock = obj.AddComponent<RollingRock>();
//                    rock.moveSpeed = selected.speed;
//                }
//            }

//            // Wait 1–3 seconds before spawning the next obstacle
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

//best obstcales code untill now 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObstacleData[] obstacles;
    public int maxObstaclesPerSegment = 3;

    [Header("Avoid Collectables")]
    public float minDistanceFromCollectable = 2.5f;
    public string collectableTag = "Collectable";

    [Header("Spawn Settings")]
    public float segmentStartX = -50f;  // segment start
    public float segmentEndX = 50f;     // segment end
    public float minSpacing = 5f;       // minimum distance between obstacles
    public float maxSpacing = 10f;      // maximum distance between obstacles
    public float minDelay = 0f;         // minimum seconds between spawns
    public float maxDelay = 2f;         // maximum seconds between spawns

    [Header("Lanes")]
    public float[] lanes = { -3f, -1f, 1f, 3f };

    private float lastX;  // track last X position

    private void Start()
    {
        lastX = segmentEndX; // start from the end
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
                // Calculate next X position backward
                float xPos = lastX - Random.Range(minSpacing, maxSpacing);

                // Stop if we go past the start of the segment
                if (xPos < segmentStartX)
                    yield break;

                // Random lane
                float zPos = lanes[Random.Range(0, lanes.Length)];

                //Vector3 spawnPos = transform.position + new Vector3(xPos, selected.prefab.transform.position.y, zPos);

                //GameObject obj = Instantiate(
                //    selected.prefab,
                //    spawnPos,
                //    selected.prefab.transform.rotation,
                //    transform
                //);
                Vector3 spawnPos = transform.position + new Vector3(xPos, selected.prefab.transform.position.y, zPos);

                // ❗ Check collectables BEFORE spawning
                if (!IsPositionSafeFromCollectables(spawnPos))
                {
                    // still move forward in the segment
                    lastX = xPos;
                    continue;
                }

                GameObject obj = Instantiate(
                    selected.prefab,
                    spawnPos,
                    selected.prefab.transform.rotation,
                    transform
                );


                obj.transform.localScale = selected.size;
                obj.tag = selected.tagName;

                if (selected.movementType == ObstacleMovementType.Rolling)
                {
                    RollingRock rock = obj.AddComponent<RollingRock>();
                    rock.moveSpeed = selected.speed;
                }

                // Update lastX for next obstacle
                lastX = xPos;
            }

            // Wait before spawning next
            float waitTime = Random.Range(minDelay, maxDelay);
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

    private bool IsPositionSafeFromCollectables(Vector3 pos)
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag(collectableTag);

        foreach (GameObject c in collectables)
        {
            // same lane
            if (Mathf.Abs(c.transform.position.z - pos.z) < 0.1f)
            {
                if (Mathf.Abs(c.transform.position.x - pos.x) < minDistanceFromCollectable)
                    return false;
            }
        }
        return true;
    }

}
