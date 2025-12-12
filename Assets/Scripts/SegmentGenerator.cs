
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SegmentGenerator : MonoBehaviour
//{
//    public GameObject segmentPrefab;     // Single segment prefab
//    private List<GameObject> activeSegments = new List<GameObject>();

//    private int generationCount = 1;     // 's' in formula
//    private float spawnInterval = 15f;   // Spawn every 15 seconds

//    private void Start()
//    {
//        // Start the repeating segment generation
//        StartCoroutine(SpawnSegmentRoutine());
//    }

//    private IEnumerator SpawnSegmentRoutine()
//    {
//        while (true)
//        {
//            // Calculate new segment position
//            Vector3 spawnPos = new Vector3(-100f * generationCount, 0f, 0f);

//            // Instantiate segment and add to active list
//            GameObject newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
//            activeSegments.Add(newSegment);

//            // Increment generation counter
//            generationCount++;

//            // If more than 2 segments exist, delete the oldest
//            if (activeSegments.Count > 2)
//            {
//                GameObject oldSegment = activeSegments[0];
//                activeSegments.RemoveAt(0);
//                Destroy(oldSegment);
//            }

//            // Wait for 15 seconds before spawning next segment
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SegmentGenerator : MonoBehaviour
//{
//    public GameObject segmentPrefab;     // Single segment prefab
//    private List<GameObject> activeSegments = new List<GameObject>();

//    private int generationCount = 1;     // 's' in formula
//    private float spawnInterval = 15f;   // Spawn every 15 seconds
//    private float initialDelay = 10f;    // Wait before first spawn

//    private void Start()
//    {
//        // Assume one segment already exists in the scene and add it to the list
//        GameObject initialSegment = segmentPrefab;
//        activeSegments.Add(initialSegment);

//        // Start the repeating segment generation with initial delay
//        StartCoroutine(SpawnSegmentRoutine());
//    }

//    private IEnumerator SpawnSegmentRoutine()
//    {
//        // Wait before first spawn
//        yield return new WaitForSeconds(initialDelay);

//        while (true)
//        {
//            // Calculate new segment position
//            Vector3 spawnPos = new Vector3(-100f * generationCount, 0f, 0f);

//            // Instantiate segment and add to active list
//            GameObject newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
//            activeSegments.Add(newSegment);

//            // Increment generation counter
//            generationCount++;

//            // If more than 2 segments exist, delete the oldest
//            if (activeSegments.Count > 4)
//            {
//                GameObject oldSegment = activeSegments[0];
//                activeSegments.RemoveAt(0);
//                Destroy(oldSegment);
//            }

//            // Wait for 15 seconds before spawning next segment
//            yield return new WaitForSeconds(spawnInterval);
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public GameObject segmentPrefab;
    private List<GameObject> activeSegments = new List<GameObject>();

    public Transform player;             // Reference to player
    private int generationCount = 1;
    private float spawnInterval = 15f;
    private float initialDelay = 10f;
    private float segmentLength = 100f;  // Length used in X-position

    private void Start()
    {
        // Assume one segment already exists
        activeSegments.Add(segmentPrefab);
        StartCoroutine(SpawnSegmentRoutine());
    }

    private IEnumerator SpawnSegmentRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            // Spawn new segment
            Vector3 spawnPos = new Vector3(-segmentLength * generationCount, 0f, 0f);
            GameObject newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
            activeSegments.Add(newSegment);

            generationCount++;

            // Delete segments that are fully behind the player
            if (activeSegments.Count > 2)
            {
                GameObject firstSegment = activeSegments[0];
                if (player.position.x > firstSegment.transform.position.x + segmentLength)
                {
                    activeSegments.RemoveAt(0);
                    Destroy(firstSegment);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
