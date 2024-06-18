using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public int chunkSizeX = 16;
    public int chunkSizeY = 16;

    void Start()
    {
        GenerateChunks();
    }

    void GenerateChunks()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Vector3Int min = bounds.min;
        Vector3Int max = bounds.max;

        for (int x = min.x; x < max.x; x += chunkSizeX)
        {
            for (int y = min.y; y < max.y; y += chunkSizeY)
            {
                Vector3Int chunkPosition = new Vector3Int(x, y, 0);
                CreateChunk(chunkPosition);
            }
        }
    }

    void CreateChunk(Vector3Int position)
    {
        GameObject chunkObject = new GameObject("Chunk_" + position);
        chunkObject.transform.position = tilemap.CellToWorld(position);

        Tilemap chunkTilemap = chunkObject.AddComponent<Tilemap>();
        chunkTilemap.tileAnchor = new Vector3(0.5f, 0.5f, 0f);

        TilemapRenderer chunkRenderer = chunkObject.AddComponent<TilemapRenderer>();
        chunkRenderer.sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder;

        TilemapCollider2D chunkCollider = chunkObject.AddComponent<TilemapCollider2D>();
        chunkCollider.usedByComposite = true;

        TileBase[] tiles = tilemap.GetTilesBlock(new BoundsInt(position, new Vector3Int(chunkSizeX, chunkSizeY, 1)));

        chunkTilemap.SetTilesBlock(new BoundsInt(Vector3Int.zero, new Vector3Int(chunkSizeX, chunkSizeY, 1)), tiles);
    }
}