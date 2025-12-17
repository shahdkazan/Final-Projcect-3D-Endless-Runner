using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Array of obstacle data (prefabs and properties)
    public ObstacleData[] obstacles;

    // Maximum number of obstacles per segment
    public int maxObstaclesPerSegment = 3;

    // Minimum distance from collectables to avoid overlap
    public float minDistanceFromCollectable = 2.5f;
    public string collectableTag = "Collectable";

    // Segment boundaries
    private float segmentStartX = -50f;  // Segment start X position
    private float segmentEndX = 50f;     // Segment end X position

    // Obstacle spacing and spawn timing
    public float minSpacing ;        // Minimum distance between obstacles
    public float maxSpacing ;       // Maximum distance between obstacles
    public float minDelay ;          // Minimum seconds between spawns
    public float maxDelay ;          // Maximum seconds between spawns

    // Possible lanes for obstacles (Z positions)
    private float[] lanes = { -3f, -1f, 1f, 3f };

    // Tracks last X position of spawned obstacle
    private float lastX;

    private void Start()
    {
        lastX = segmentEndX;                     // Initialize starting point at segment end
        StartCoroutine(SpawnObstaclesRoutine()); // Start obstacle spawning coroutine
    }

    // Coroutine to spawn obstacles in the segment
    private IEnumerator SpawnObstaclesRoutine()
    {
        int obstaclesToSpawn = Random.Range(30, maxObstaclesPerSegment + 1);

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            ObstacleData selected = PickObstacle(); // Randomly pick an obstacle
            if (selected != null)
            {
                // Calculate next X position backward within segment
                float xPos = lastX - Random.Range(minSpacing, maxSpacing);

                // Stop spawning if past the segment start
                if (xPos < segmentStartX)
                    yield break;

                // Pick a random lane
                float zPos = lanes[Random.Range(0, lanes.Length)];

                // Compute spawn position
                Vector3 spawnPos = transform.position + new Vector3(xPos, selected.prefab.transform.position.y, zPos);

                // Check if spawn position is safe from collectables
                if (!IsPositionSafeFromCollectables(spawnPos))
                {
                    lastX = xPos; // Move forward for next obstacle
                    continue;
                }

                // Instantiate obstacle
                GameObject obj = Instantiate(
                    selected.prefab,
                    spawnPos,
                    selected.prefab.transform.rotation,
                    transform
                );

                // Set obstacle properties
                obj.transform.localScale = selected.size;
                obj.tag = selected.tagName;

                // Add rolling movement if needed
                if (selected.movementType == ObstacleMovementType.Rolling)
                {
                    Rolling rock = obj.AddComponent<Rolling>();
                    rock.moveSpeed = selected.speed;
                }

                // Update lastX for next obstacle
                lastX = xPos;
            }

            // Wait a random delay before spawning next obstacle
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Randomly selects an obstacle based on spawn chance
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

    // Checks if the spawn position is far enough from collectables
    private bool IsPositionSafeFromCollectables(Vector3 pos)
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag(collectableTag);

        foreach (GameObject c in collectables)
        {
            // Same lane check
            if (Mathf.Abs(c.transform.position.z - pos.z) < 0.1f)
            {
                // X distance check
                if (Mathf.Abs(c.transform.position.x - pos.x) < minDistanceFromCollectable)
                    return false; // Too close, unsafe
            }
        }
        return true; // Safe to spawn
    }
}
