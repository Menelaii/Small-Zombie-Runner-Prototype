using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Follower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _angleOffset;
    [SerializeField] private Transform _target;

    [Header("Components")]
    [SerializeField] private TowardsTargetRotator _rotator;
    [SerializeField] private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        _rotator.enabled = true;
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector2.zero;
        _rotator.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        Vector3 directionToTarget = (_target.position - transform.position).normalized;
        Vector3 newPosition = transform.position + directionToTarget * _speed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(newPosition);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _rotator.SetTarget(target);
        enabled = target != null;
    }
}
