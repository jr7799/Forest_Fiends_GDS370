using UnityEngine;
using System.Collections.Generic;

public class PropSpawner : MonoBehaviour
{
    public Transform player;
    public Camera mainCamera;
    public GameObject[] spawnPrefabs;

    public float spacing = 5f;
    public float spawnRadius = 25f;
    public float despawnRadius = 35f;
    public float spawnChance = 0.3f;

    private Dictionary<Vector2Int, GameObject> spawnedProps = new Dictionary<Vector2Int, GameObject>();
    private Vector2Int lastPlayerCell;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        lastPlayerCell = WorldToGrid(player.position);
        SpawnOffscreenProps();
    }

    void Update()
    {
        Vector2Int currentCell = WorldToGrid(player.position);

        if (currentCell != lastPlayerCell)
        {
            lastPlayerCell = currentCell;
            SpawnOffscreenProps();
            DespawnFarProps();
        }
    }

    void SpawnOffscreenProps()
    {
        Vector3 playerPos = player.position;
        int attempts = 100; // Number of attempts per frame to spawn something

        for (int i = 0; i < attempts; i++)
        {
            // Pick a random angle and distance from the player
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(spawnRadius * 0.5f, spawnRadius);
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * distance;
            Vector3 spawnPos = playerPos + offset;

            // Skip if visible
            if (IsPointVisible(spawnPos))
                continue;

            // Convert to grid for tracking, but allow random offset
            Vector2Int gridPos = WorldToGrid(spawnPos);
            if (spawnedProps.ContainsKey(gridPos))
                continue;

            // Only spawn based on chance
            if (Random.value < spawnChance)
            {
                GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

                // Add some jitter to avoid a perfect grid appearance
                Vector3 jitter = new Vector3(
                    Random.Range(-spacing * 0.3f, spacing * 0.3f),
                    Random.Range(-spacing * 0.3f, spacing * 0.3f),
                    0f
                );

                GameObject obj = Instantiate(prefab, spawnPos + jitter, Quaternion.identity);
                spawnedProps[gridPos] = obj;
            }
        }
    }

    void DespawnFarProps()
    {
        Vector3 center = player.position;
        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var pair in spawnedProps)
        {
            if (Vector3.Distance(center, pair.Value.transform.position) > despawnRadius)
            {
                Destroy(pair.Value);
                toRemove.Add(pair.Key);
            }
        }

        foreach (var key in toRemove)
        {
            spawnedProps.Remove(key);
        }
    }

    bool IsPointVisible(Vector3 worldPos)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(worldPos);
        return viewportPoint.z > 0 && viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }

    Vector2Int WorldToGrid(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.RoundToInt(pos.x / spacing),
            Mathf.RoundToInt(pos.y / spacing)
        );
    }

    Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * spacing, gridPos.y * spacing, 0f);
    }
}