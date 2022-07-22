using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    [SerializeField] private float _switchCooldown;
    [SerializeField] private float _priorityAffection;
    [SerializeField] private LayerMask _obstaclesMask;
    [SerializeField] private TowardsTargetRotator _rotator;

    private List<IAttentionThing> _potentialTargets = new List<IAttentionThing>();
    private float _time;

    public IAttentionThing Target { get; private set; }

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time >= _switchCooldown)
        {
            _time = 0;
            SwitchTarget();
        }
    }

    public void AddPotentialTarget(IAttentionThing thing)
    {
        _potentialTargets.Add(thing);

        if(Target == null && IsVisible(thing.Transform))
        {
            SwitchTarget(thing);
        }
    }

    public void RemovePotentialTarget(IAttentionThing thing)
    {
        _potentialTargets.Remove(thing);

        if(Target == thing)
        {
            SwitchTarget(null);
        }
    }

    public void SwitchTarget()
    {
        if (_potentialTargets.Count == 0)
        {
            SwitchTarget(null);
            return;
        }

        SwitchTarget(GetNearestWithMaxWeight());
    }

    public void SwitchTarget(IAttentionThing thing)
    {
        Target = thing;
        _rotator.SetTarget(thing?.Transform);
    }

    private IAttentionThing GetNearestWithMaxWeight()
    {
        IAttentionThing target = null;
        float maxWeight = float.MinValue;
        float weight;
        float sqrDistance;
        foreach (var potentialTarget in _potentialTargets)
        {
            if (potentialTarget == null)
            {
                _potentialTargets.Remove(potentialTarget);
                continue;
            }

            sqrDistance = (transform.position - potentialTarget.Transform.position).sqrMagnitude;
            weight = (int)potentialTarget.Priority * _priorityAffection - sqrDistance;

            if (weight > maxWeight && IsVisible(potentialTarget.Transform))
            {
                maxWeight = weight;
                target = potentialTarget;
            }
        }

        return target;
    }

    private bool IsVisible(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        return Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude + 0.001f, _obstaclesMask) == false;
    }
}
