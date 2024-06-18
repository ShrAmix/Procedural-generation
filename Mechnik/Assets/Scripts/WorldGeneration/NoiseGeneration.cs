using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseGeneration : MonoBehaviour
{
    [HideInInspector] public Vector2Int size;
    public float zoom;

    public Vector2Int offset;

    [HideInInspector] public Tilemap tm;

    public float intensivity;
    public float cutPlane;

    public float vignetteIntensivity;




    public void genTileCave(Vector2Int startPosition, HashSet<Vector2Int> path, float num)
    {
        //tm.ClearAllTiles();
        offset = new Vector2Int(Random.Range(-10000, 10000), Random.Range(-10000, 10000));
        for (int x = size.x/2*-1; x <= size.x/2; x++)
        {
            for (int y = size.y / 2 * -1; y <= size.y/2; y++)
            {
                var p = Mathf.PerlinNoise((x + offset.x) / (zoom*num), (y + offset.y) / (zoom*num));
                var v =1f- new Vector2(size.x/2-x,size.y/2-y).magnitude*(vignetteIntensivity/10000);
                var gr = p*v;

                if (gr < cutPlane/100)
                {
                    //tm.SetTile(new Vector3Int(x, y,0), t);
                    var newPosition = new Vector2Int(x,y);
                    Vector2Int pos = newPosition;
                    path.Remove(pos);
                }
                
            }
        }
    }


}
