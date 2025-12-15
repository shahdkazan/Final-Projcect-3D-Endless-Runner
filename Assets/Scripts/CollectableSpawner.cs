//using System.Collections.Generic;
//using UnityEngine;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minCount = 3;
//    public int maxCount = 6;
//    public float spacing = 1.5f;

//    [Header("Spawn Area")]
//    public float minX = -50f;
//    public float maxX = 50f;
//    public float minZ = -4f;
//    public float maxZ = 4f;

//    [Header("Avoid Obstacles")]
//    public float minDistanceFromObstacle = 3f;
//    public string obstacleTag = "Obstacle";

//    public int maxAttempts = 20;

//    void Start()
//    {
//        SpawnCollectableRow();
//    }

//    void SpawnCollectableRow()
//    {
//        int count = Random.Range(minCount, maxCount + 1);

//        for (int attempt = 0; attempt < maxAttempts; attempt++)
//        {
//            float x = Random.Range(minX, maxX);
//            float z = Random.Range(minZ, maxZ);

//            Vector3 startPos = transform.position + new Vector3(x, 0.5f, z);

//            if (IsPositionSafe(startPos, count))
//            {
//                for (int i = 0; i < count; i++)
//                {
//                    Vector3 pos = startPos + Vector3.right * spacing * i;
//                    Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//                }
//                return;
//            }
//        }
//    }

//    bool IsPositionSafe(Vector3 startPos, int count)
//    {
//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 checkPos = startPos + Vector3.right * spacing * i;

//            foreach (var obs in obstacles)
//            {
//                if (Vector3.Distance(checkPos, obs.transform.position) < minDistanceFromObstacle)
//                    return false;
//            }
//        }
//        return true;
//    }
//}
//using UnityEngine;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minCount = 3;
//    public int maxCount = 6;
//    public float spacing = 1.5f;

//    [Header("Spawn Area")]
//    public float minX = -50f;
//    public float maxX = 50f;
//    public float minZ = -4f;
//    public float maxZ = 4f;

//    [Header("Avoid Obstacles")]
//    public float minDistanceFromObstacle = 3f;
//    public string obstacleTag = "Obstacle";

//    void Start()
//    {
//        SpawnCollectableRow();
//    }

//    void SpawnCollectableRow()
//    {
//        int count = Random.Range(minCount, maxCount + 1);

//        float x = Random.Range(minX, maxX);
//        float z = Random.Range(minZ, maxZ);

//        Vector3 startPos = transform.position + new Vector3(x, 0.5f, z);

//        if (!IsPositionSafe(startPos, count))
//            return; // skip spawning completely

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 pos = startPos + Vector3.right * spacing * i;
//            Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//        }
//    }

//    bool IsPositionSafe(Vector3 startPos, int count)
//    {
//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 checkPos = startPos + Vector3.right * spacing * i;

//            foreach (GameObject obs in obstacles)
//            {
//                if (Vector3.Distance(checkPos, obs.transform.position) < minDistanceFromObstacle)
//                    return false;
//            }
//        }
//        return true;
//    }
//}

//lanes unopit
//using UnityEngine;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minCount = 3;
//    public int maxCount = 6;
//    public float spacing = 1.5f;

//    [Header("Spawn Area")]
//    public float minX = -50f;
//    public float maxX = 50f;

//    [Header("Lanes (Z from -4 to 4, width = 2)")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    [Header("Avoid Obstacles")]
//    public float minDistanceFromObstacle = 3f;
//    public string obstacleTag = "Obstacle";

//    void Start()
//    {
//        SpawnCollectableRow();
//    }

//    void SpawnCollectableRow()
//    {
//        int count = Random.Range(minCount, maxCount + 1);

//        float x = Random.Range(minX, maxX);
//        float z = lanes[Random.Range(0, lanes.Length)];

//        Vector3 startPos = transform.position + new Vector3(x, 0.5f, z);

//        if (!IsPositionSafe(startPos, count))
//            return;

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 pos = startPos + Vector3.right * spacing * i;
//            Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//        }
//    }

//    bool IsPositionSafe(Vector3 startPos, int count)
//    {
//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 checkPos = startPos + Vector3.right * spacing * i;

//            foreach (GameObject obs in obstacles)
//            {
//                if (Vector3.Distance(checkPos, obs.transform.position) < minDistanceFromObstacle)
//                    return false;
//            }
//        }
//        return true;
//    }
//}

//lanes opti 
//using UnityEngine;
//using System.Collections.Generic;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minCount = 3;
//    public int maxCount = 6;
//    public float spacing = 1.5f;

//    [Header("Spawn Area")]
//    public float minX = -50f;
//    public float maxX = 50f;

//    [Header("Lanes (Z from -4 to 4, width = 2)")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    [Header("Avoid Obstacles")]
//    public string obstacleTag = "Obstacle";

//    void Start()
//    {
//        SpawnCollectableRow();
//    }

//    void SpawnCollectableRow()
//    {
//        // Find which lanes are free
//        List<float> freeLanes = new List<float>(lanes);

//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);
//        foreach (GameObject obs in obstacles)
//        {
//            // Remove lane if an obstacle is present in it (approximation)
//            foreach (float laneZ in lanes)
//            {
//                if (Mathf.Abs(obs.transform.position.z - laneZ) < 0.5f)
//                {
//                    freeLanes.Remove(laneZ);
//                }
//            }
//        }

//        if (freeLanes.Count == 0)
//            return; // No free lane, skip spawning

//        // Determine how many collectables to spawn
//        int count = Random.Range(minCount, maxCount + 1);
//        count = Mathf.Min(count, freeLanes.Count); // Don't exceed free lanes

//        // Pick random X position for the row
//        float x = Random.Range(minX, maxX);

//        // Shuffle free lanes and spawn
//        for (int i = 0; i < count; i++)
//        {
//            float z = freeLanes[i];
//            Vector3 pos = transform.position + new Vector3(x + i * spacing, 0.5f, z);
//            Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//        }
//    }
//}
////multi in a lane but onece too 
//using UnityEngine;
//using System.Collections.Generic;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minCount = 3;
//    public int maxCount = 6;
//    public float spacing = 1.5f; // spacing between collectibles along X

//    [Header("Spawn Area")]
//    public float minX = -50f;
//    public float maxX = 50f;

//    [Header("Lanes (Z from -4 to 4, width = 2)")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    [Header("Avoid Obstacles")]
//    public string obstacleTag = "Obstacle";

//    void Start()
//    {
//        SpawnCollectableRow();
//    }

//    void SpawnCollectableRow()
//    {
//        // Find lanes without obstacles
//        List<float> freeLanes = new List<float>(lanes);

//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);
//        foreach (GameObject obs in obstacles)
//        {
//            foreach (float laneZ in lanes)
//            {
//                if (Mathf.Abs(obs.transform.position.z - laneZ) < 0.5f)
//                {
//                    freeLanes.Remove(laneZ);
//                }
//            }
//        }

//        if (freeLanes.Count == 0)
//            return; // no free lanes available

//        // Determine how many collectibles to spawn
//        int count = Random.Range(minCount, maxCount + 1);

//        // Pick a random free lane
//        float z = freeLanes[Random.Range(0, freeLanes.Count)];

//        // Pick starting X for the row
//        float startX = Random.Range(minX, maxX);

//        // Spawn the row along X in the selected lane
//        for (int i = 0; i < count; i++)
//        {
//            Vector3 pos = transform.position + new Vector3(startX + i * spacing, 0.5f, z);
//            Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//        }
//    }
//}
//best collectable code until now 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CollectableSpawner : MonoBehaviour
//{
//    public GameObject collectablePrefab;

//    [Header("Row Settings")]
//    public int minRowCount = 3;
//    public int maxRowCount = 6;
//    public float spacing = 1f; // distance between collectables in a row

//    [Header("Spawn Settings")]
//    public float segmentStartX = -50f;
//    public float segmentEndX = 50f;
//    public float minRowGap = 3f;   // distance between rows
//    public float maxRowGap = 6f;

//    [Header("Avoid Obstacles")]
//    public float minDistanceFromObstacle = 3f;
//    public string obstacleTag = "Obstacle";

//    [Header("Lanes")]
//    public float[] lanes = { -3f, -1f, 1f, 3f };

//    private float lastX;

//    private void Start()
//    {
//        lastX = segmentEndX;
//        StartCoroutine(SpawnRowsRoutine());
//    }

//    private IEnumerator SpawnRowsRoutine()
//    {
//        while (true)
//        {
//            int rowCount = Random.Range(minRowCount, maxRowCount + 1);

//            // Move backward for next row start
//            float rowStartX = lastX - Random.Range(minRowGap, maxRowGap);
//            if (rowStartX < segmentStartX)
//                yield break;

//            float laneZ = lanes[Random.Range(0, lanes.Length)];

//            // Check entire row against obstacles
//            if (IsRowSafe(rowStartX, rowCount, laneZ))
//            {
//                for (int i = 0; i < rowCount; i++)
//                {
//                    float x = rowStartX - i * spacing;
//                    Vector3 pos = transform.position + new Vector3(x, 0.5f, laneZ);
//                    Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
//                }

//                // Update lastX AFTER row is placed
//                lastX = rowStartX - (rowCount - 1) * spacing;
//            }
//            else
//            {
//                // Skip a bit and retry
//                lastX -= minRowGap;
//                continue;
//            }

//            yield return null;
//        }
//    }

//    private bool IsRowSafe(float startX, int count, float z)
//    {
//        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(obstacleTag);

//        for (int i = 0; i < count; i++)
//        {
//            float x = startX - i * spacing;

//            foreach (GameObject obs in obstacles)
//            {
//                // Same lane
//                if (Mathf.Abs(obs.transform.position.z - z) < 0.1f)
//                {
//                    if (Mathf.Abs(obs.transform.position.x - x) < minDistanceFromObstacle)
//                        return false;
//                }
//            }
//        }
//        return true;
//    }
//}

//noe chcek 
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject collectablePrefab;

    [Header("Row Settings")]
    public int minRowCount = 3;
    public int maxRowCount = 6;
    public float spacing = 1f;

    [Header("Spawn Settings")]
    public float segmentStartX = -50f;
    public float segmentEndX = 50f;
    public float minRowGap = 3f;
    public float maxRowGap = 6f;

    [Header("Lanes")]
    public float[] lanes = { -3f, -1f, 1f, 3f };

    private float lastX;

    private void Start()
    {
        lastX = segmentEndX;
        StartCoroutine(SpawnRowsRoutine());
    }

    private IEnumerator SpawnRowsRoutine()
    {
        while (true)
        {
            int rowCount = Random.Range(minRowCount, maxRowCount + 1);

            float rowStartX = lastX - Random.Range(minRowGap, maxRowGap);
            if (rowStartX < segmentStartX)
                yield break;

            float laneZ = lanes[Random.Range(0, lanes.Length)];

            for (int i = 0; i < rowCount; i++)
            {
                float x = rowStartX - i * spacing;
                Vector3 pos = transform.position + new Vector3(x, 0.5f, laneZ);
                Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
            }

            lastX = rowStartX - (rowCount - 1) * spacing;
            yield return null;
        }
    }
}
