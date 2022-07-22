using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _obstaclesParent;

    private float _repeats;

    private void Start()
    {
        _repeats = 0;
    }

    public float GetWeight(float repeatsAffection, float randomAffection)
    { 
        return (_repeats * repeatsAffection) - (Random.Range(0f, 1f) * randomAffection);
    }

    public GameObject GetPrefab()
    {
        _repeats++;
        return _obstaclesParent;
    }
}
