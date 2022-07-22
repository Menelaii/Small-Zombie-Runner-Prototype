using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class AggroZone : MonoBehaviour
{
    [SerializeField] private float _exitTriggerDelay;
    [SerializeField] private float _defaultRadius;
    [SerializeField] private float _increasedRadius;
    [SerializeField] private float _rollbackDelay;
    [SerializeField] private CircleCollider2D _collider;

    private bool _targetInZone;

    public event Action<Human> TriggeredEnter;
    public event Action TriggeredExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Human human))
        {
            _targetInZone = true;
            TriggeredEnter?.Invoke(human);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Human human))
        {
            _targetInZone = false;
            Invoke(nameof(TryTriggerExit), _exitTriggerDelay);
        }
    }

    public void IncreaseRadius()
    {
        _collider.radius = _increasedRadius;
        Invoke(nameof(SetDefaultRadius), _rollbackDelay);
    }

    private void SetDefaultRadius()
    {
        _collider.radius = _defaultRadius;
    }

    private void TryTriggerExit()
    {
        if (_targetInZone)
            return;

        TriggeredExit?.Invoke();
    }
}
