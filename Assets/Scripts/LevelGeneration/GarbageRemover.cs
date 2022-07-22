
using UnityEngine;
using System.Collections.Generic;

public class GarbageRemover
{
    private Queue<GameObject> _obstacles;

    public GarbageRemover()
    {
        _obstacles = new Queue<GameObject>();
    }

    public void OnTileSpawned(GameObject obstacles)
    {
        _obstacles.Enqueue(obstacles);
    }

    public void OnTilePassed()
    {
        Object.Destroy(_obstacles.Dequeue());
    }
}
