using System;
using UnityEngine;

public class Horde : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _tileHeight = 30f;

    private int _tilesPassed;
    private float _startPositionY;

    public event Action TilePassed;

    private void Start()
    {
        _startPositionY = transform.position.y;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up * _speed, _speed * Time.deltaTime);
        if (IsNextTilePassed())
        {
            _tilesPassed++;
            TilePassed?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(int.MaxValue);
        }
    }

    private bool IsNextTilePassed()
    {
        return (int)((transform.position.y - _startPositionY) / _tileHeight) > _tilesPassed;
    }
}
