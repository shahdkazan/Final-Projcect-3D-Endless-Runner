
using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    // Prefab to spawn as a collectable
    public GameObject collectablePrefab;

    // Minimum and maximum number of collectables per row
    public int minRowCount = 3;
    public int maxRowCount = 6;

    // Distance between collectables in the same row
    public float spacing = 1f;

    // X bounds of the segment where collectables can spawn
    private float segmentStartX = -50f;
    private float segmentEndX = 50f;

    // Random gap range between rows
    public float minRowGap = 3f;
    public float maxRowGap = 6f;

    // Possible Z positions (lanes)
    public float[] lanes = { -3f, -1f, 1f, 3f };

    // Tracks the last spawned X position to avoid overlap
    private float lastX;

    private void Start()
    {
        // Initialize lastX at the end of the segment
        lastX = segmentEndX;

        // Start spawning rows of collectables
        StartCoroutine(SpawnRowsRoutine());
    }

    private IEnumerator SpawnRowsRoutine()
    {
        // Continuously spawn rows until the segment start is reached
        while (true)
        {
            // Random number of collectables in this row
            int rowCount = Random.Range(minRowCount, maxRowCount + 1);

            // Calculate the starting X position for this row
            float rowStartX = lastX - Random.Range(minRowGap, maxRowGap);

            // Stop spawning if we reach the start of the segment
            if (rowStartX < segmentStartX)
                yield break;

            // Choose a random lane (Z position)
            float laneZ = lanes[Random.Range(0, lanes.Length)];

            // Spawn collectables in a row
            for (int i = 0; i < rowCount; i++)
            {
                float x = rowStartX - i * spacing;
                Vector3 pos = transform.position + new Vector3(x, 0.5f, laneZ);
                Instantiate(collectablePrefab, pos, Quaternion.identity, transform);
            }

            // Update lastX to the end of the newly spawned row
            lastX = rowStartX - (rowCount - 1) * spacing;

            // Wait one frame before spawning the next row
            yield return null;
        }
    }
}

