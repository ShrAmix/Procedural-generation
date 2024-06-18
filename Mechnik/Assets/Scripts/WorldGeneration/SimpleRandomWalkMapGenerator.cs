using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SimpleRandomWalkMapGenerator : MonoBehaviour
{

    [SerializeField]  protected Vector2Int startPosition=Vector2Int.zero;

    [SerializeField] private int chunkWhith = 384;
    [SerializeField] private int chunkHeight = 3840;

    [SerializeField]  private int iteration = 10;
    [SerializeField] public int walkLenght = 10;
    [SerializeField] public bool startRandomlyEachIteration = true;


    [SerializeField] private TilemapVisualizer tilemapVisualizer;

    [SerializeField] private NoiseGeneration noise;
    private HashSet<Vector2Int> floorPosition;

    private void Start()
    {
        noise.size=new Vector2Int(chunkWhith, chunkHeight);
        noise.tm = tilemapVisualizer.flootTilemap;
    }
    public void RemoveProblemGeneration()
    {
        floorPosition = ProveduralGenerationAlgorithms.SmoothPath(floorPosition, 1, true);
        tilemapVisualizer.Clear();


        tilemapVisualizer.PaintFloorTiles(floorPosition);
    }
    public void PlusBoxGeneration()
    {
        floorPosition = ProveduralGenerationAlgorithms.SmoothPath(floorPosition, 1, false);
        tilemapVisualizer.Clear();


        tilemapVisualizer.PaintFloorTiles(floorPosition);
    }

    public void RunProcedularGeneration()
    {
        floorPosition = RunRandomWalk();

        tilemapVisualizer.Clear();

        
        tilemapVisualizer.PaintFloorTiles(floorPosition);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> florPosition = new HashSet<Vector2Int>();

         var path = ProveduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLenght, chunkWhith, chunkHeight, iteration,noise);
         florPosition.UnionWith(path);
            
        
        return florPosition;
    }
}
