using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private TileSpawner _tileSpawner;
    [SerializeField] private Trigger _tileEndTrigger;
    [SerializeField] private Horde _horde;
    [SerializeField] private GameObject _defaultTile;

    private GarbageRemover _garbageRemover;

    private void Awake()
    {
        _garbageRemover = new GarbageRemover();
        _garbageRemover.OnTileSpawned(_defaultTile);

        _horde.TilePassed += _garbageRemover.OnTilePassed;
    }

    private void OnDestroy()
    {
        _horde.TilePassed -= _garbageRemover.OnTilePassed;
    }

    public void OnTilesEnd()
    {
        GameObject obstacles = _tileSpawner.Spawn();
        _garbageRemover.OnTileSpawned(obstacles);
        _tileSpawner.Place(_tileEndTrigger);
    }
}
