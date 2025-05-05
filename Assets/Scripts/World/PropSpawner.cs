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
        int cellRange = Mathf.CeilToInt(spawnRadius / spacing);

        for (int x = -cellRange; x <= cellRange; x++)
        {
            for (int y = -cellRange; y <= cellRange; y++)
            {
                Vector2Int gridPos = lastPlayerCell + new Vector2Int(x, y);
                Vector3 worldPos = GridToWorld(gridPos);

                if (Vector3.Distance(playerPos, worldPos) <= spawnRadius &&
                    !IsPointVisible(worldPos) &&
                    !spawnedProps.ContainsKey(gridPos) &&
                    Random.value < spawnChance)
                {
                    GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
                    GameObject obj = Instantiate(prefab, worldPos, Quaternion.identity);
                    spawnedProps[gridPos] = obj;
                }
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