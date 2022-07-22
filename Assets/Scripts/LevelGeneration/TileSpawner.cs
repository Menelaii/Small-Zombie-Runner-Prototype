using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private float _repeatsAffection;
    [SerializeField] private float _randomAffection;
    [SerializeField] private Vector2 _origin = Vector2.zero;
    [SerializeField] private float _offset = 30f;
    [SerializeField] private Tile[] _tiles;

    private int _count = 1;

    public GameObject Spawn()
    {
        Tile tile = GetNext();
        Vector2 newPosition = _origin + Vector2.up * _count * _offset;
        _count++;

        return Instantiate(tile.GetPrefab(), newPosition, Quaternion.identity);
    }

    public void Place(Trigger tileEndTrigger)
    {
        tileEndTrigger.transform.position += Vector3.up * _offset;
    }

    private Tile GetNext()
    {
        Tile next = null;
        float weight;
        float minWeight = float.MaxValue;
        foreach (Tile tile in _tiles)
        {
            weight = tile.GetWeight(_repeatsAffection, _randomAffection);
            if (weight < minWeight)
            {
                minWeight = weight;
                next = tile;
            }
        }

        return next;
    }
}
