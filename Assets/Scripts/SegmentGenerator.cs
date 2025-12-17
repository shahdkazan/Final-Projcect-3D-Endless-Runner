
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    // Prefab for a single segment
    public GameObject segmentPrefab;

    // List of currently active segments
    private List<GameObject> activeSegments = new List<GameObject>();

    // Generation counter used to calculate spawn positions
    private int generationCount = 1;

    // Time interval between spawning new segments
    private float spawnInterval = 20f;

    private void Start()
    {
        // Start the repeating segment generation coroutine
        StartCoroutine(SpawnSegmentRoutine());
    }

    // Coroutine to spawn segments periodically
    private IEnumerator SpawnSegmentRoutine()
    {
        while (true)
        {
            // Calculate spawn position based on generation count
            Vector3 spawnPos = new Vector3(-100f * generationCount, 0f, 0f);

            // Instantiate new segment and add it to the active list
            GameObject newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
            activeSegments.Add(newSegment);

            // Increment generation counter for next segment
            generationCount++;

            // Keep only the last 3 segments active, destroy oldest if needed
            if (activeSegments.Count > 3)
            {
                GameObject oldSegment = activeSegments[0];
                activeSegments.RemoveAt(0);
                Destroy(oldSegment);
            }

            // Wait before spawning the next segment
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
