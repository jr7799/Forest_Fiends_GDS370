using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGeneration : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase groundTile;
    public Transform player;

    public int radius = 10;          // How far to generate tiles around the player
    public int edgeBuffer = 3;       // Trigger expansion when within this distance of edge

    private HashSet<Vector3Int> drawnTiles = new HashSet<Vector3Int>();
    private BoundsInt drawnBounds;

    void Start()
    {
        Vector3Int startCell = tilemap.WorldToCell(player.position);
        ExpandTilesAround(startCell);
    }

    void Update()
    {
        Vector3Int playerCell = tilemap.WorldToCell(player.position);

        if (!IsWithinBounds(playerCell, drawnBounds, edgeBuffer))
        {
            ExpandTilesAround(playerCell);
        }
    }
    public int tileSpaceMult = 5;
    void ExpandTilesAround(Vector3Int center)
    {
        int minX = center.x - radius;
        int maxX = center.x + radius;
        int minY = center.y - radius;
        int maxY = center.y + radius;

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x * tileSpaceMult, y * tileSpaceMult, 0);
                if (!drawnTiles.Contains(pos))
                {
                    tilemap.SetTile(pos, groundTile);
                    drawnTiles.Add(pos);
                }
            }
        }

        // Update the drawn bounds
        drawnBounds = new BoundsInt(minX, minY, 0, maxX - minX + 1, maxY - minY + 1, 1);
    }

    bool IsWithinBounds(Vector3Int pos, BoundsInt bounds, int buffer)
    {
        return
            pos.x <= bounds.xMin + buffer &&
            pos.x >= bounds.xMax - buffer &&
            pos.y <= bounds.yMin + buffer &&
            pos.y >= bounds.yMax - buffer;
    }
}
