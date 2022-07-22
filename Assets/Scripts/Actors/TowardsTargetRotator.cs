using UnityEngine;

public class TowardsTargetRotator : MonoBehaviour
{
    [SerializeField] private float _angleOffset = -90;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private float _minDeltaForLerp;
    [SerializeField] private Transform _defaultTarget;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidBody;

    private Transform _target;


    private void Update()
    {
        if (_target == null)
            return;

        Vector3 direction = _target.position - transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + _angleOffset;
        if(Mathf.Abs(targetAngle - _rigidBody.rotation) > _minDeltaForLerp)
        {
            targetAngle = Mathf.LerpAngle(_rigidBody.rotation, targetAngle, _lerpSpeed * Time.fixedDeltaTime);
        }

        _rigidBody.rotation = targetAngle;
    }

    public void SetTarget(Transform target)
    {
        _target = target ?? _defaultTarget;
        enabled = _target != null;
    }
}
