using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private bool _destroyOnTouch;
    [SerializeField] private Vector2 _defaultDirection;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
        }

        if(_destroyOnTouch)
            Destroy(gameObject);
    }

    public void AddForce(Vector2 direction, float magnitude)
    {
        transform.up = direction;
        _rigidbody.AddForce(direction * magnitude, ForceMode2D.Impulse);
    }

    public void AddForce(float magnitude)
    {
        transform.up = _defaultDirection;
        _rigidbody.AddForce(_defaultDirection * magnitude, ForceMode2D.Impulse);
    }
}
