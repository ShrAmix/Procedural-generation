using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ProveduralGenerationAlgorithms
{ 
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLenght, int chunkWhith, int chunkHeight, int iteration, NoiseGeneration noise)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        
        //path.Add(startPosition);
        var previosPosition = startPosition;

        
            for (int x = chunkWhith / 2 * -1; x <= chunkWhith / 2; x++)
            {
                for (int y = chunkHeight / 2 * -1; y <= chunkHeight / 2; y++)
                {
                    Vector2Int newPosition = new Vector2Int(x, y);
                    path.Add(newPosition);
                }
            }




        int radius = 0;

        for (int u = 0; u < iteration; u++)
        {

            for (int i = 0; i < walkLenght; i++)
            {
                var newPosition = previosPosition + Direction2D.GetRandomCardinalDurection2();

                Vector2Int pos = newPosition;

                radius = 6;
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        var offset = new Vector2Int(x, y);
                        if (offset.magnitude <= radius)
                        {
                            path.Remove(pos + offset);
                        }
                    }
                }

                previosPosition = newPosition;

            }
            previosPosition = new Vector2Int(Random.Range(chunkWhith / 2 * -1, chunkWhith / 2), Random.Range(chunkHeight / 2 * -1, chunkHeight / 2));
            //previosPosition = new Vector2Int(0, 0);
            Debug.Log(previosPosition);
            for (int j = 0; j < walkLenght; j++)
            {
                var newPosition = previosPosition + Direction2D.GetRandomCardinalDurection();

                Vector2Int pos = newPosition;

                radius = 4;
                for (int x = -radius; x <= radius; x++)
                {
                    for (int y = -radius; y <= radius; y++)
                    {
                        var offset = new Vector2Int(x, y);
                        if (offset.magnitude <= radius)
                        {
                            path.Remove(pos + offset);
                        }
                    }
                }
              
                previosPosition = newPosition;
            }

        }
        noise.genTileCave(startPosition, path, 1);

       // noise.genTileCave(startPosition, path, 2);
       // noise.genTileCave(startPosition, path, 3);

       for(int i = 0; i < 8; i++)
            path = SmoothPath(path, 1,true); // Згладжування шляху
        
            path = SmoothPath(path, 1, false); 

        return path;
    }

    public static HashSet<Vector2Int> SmoothPath(HashSet<Vector2Int> path, int smoothIterations, bool typeRemove)
    {
        HashSet<Vector2Int> smoothedPath = new HashSet<Vector2Int>(path);

        for (int iter = 0; iter < smoothIterations; iter++)
        {
            List<Vector2Int> newPath = new List<Vector2Int>(smoothedPath);

            foreach (Vector2Int pos in path)
            {
                int neighborCount = CountNeighbors(path, pos);
                if (neighborCount < 4 && !typeRemove)
                {
                    // Додайте перевірку для верхньої діагоналі тут
                    if (!path.Contains(pos + new Vector2Int(0, 1)) && path.Contains(pos + new Vector2Int(1, 1)) && path.Contains(pos + new Vector2Int(-1, 1)))
                        newPath.Add(pos + new Vector2Int(0, 1));
                    if (!path.Contains(pos + new Vector2Int(1, 0)) && path.Contains(pos + new Vector2Int(1, 1)) && path.Contains(pos + new Vector2Int(1, -1)))
                        newPath.Add(pos + new Vector2Int(1, 0));
                    if (!path.Contains(pos + new Vector2Int(0, -1)) && path.Contains(pos + new Vector2Int(1, -1)) && path.Contains(pos + new Vector2Int(-1, -1)))
                        newPath.Add(pos + new Vector2Int(0, -1));
                    if (!path.Contains(pos + new Vector2Int(-1, 0)) && path.Contains(pos + new Vector2Int(-1, -1)) && path.Contains(pos + new Vector2Int(-1, 1)))
                        newPath.Add(pos + new Vector2Int(-1, 0));
                    // Кінець перевірки верхньої діагоналі
                }

                
                // If a cell has only one neighbor in any cardinal direction, remove it
                if (neighborCount <= 1 && typeRemove)
                    newPath.Remove(pos);
            }

            smoothedPath = new HashSet<Vector2Int>(newPath);
        }

        return smoothedPath;
    }

    private static int CountNeighbors(HashSet<Vector2Int> path, Vector2Int pos)
    {
        int count = 0;
        foreach (Vector2Int dir in Direction2D.cardinalDirectionList)
        {
            Vector2Int neighborPos = pos + dir;
            // Check if the neighbor position is within the path
            if (path.Contains(neighborPos))
                count++;
        }
        return count;
    }



}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1),       //UP
        new Vector2Int(1,0),    //RIGHT
        new Vector2Int(0,-1),   //DOWN
        new Vector2Int(-1,0)    //LEFT
    };

    public static Vector2Int GetRandomCardinalDurection()
    {
        Vector2Int s = cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)], g = new Vector2Int(-1, 0),g2= new Vector2Int(1, 0);

        if (s == g)
            s = new Vector2Int(-2, 0);
        else if(s==g2)
            s = new Vector2Int(2, 0);


        return s;
    }

    public static Vector2Int GetRandomCardinalDurection2()
    {
        Vector2Int s= cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)],g= new Vector2Int(0, -1);

        if (s == g)
            s = new Vector2Int(0, -2);
        return s;
    }
}

