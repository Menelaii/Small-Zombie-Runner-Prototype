using System.Collections;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamageable, IAttentionThing
{
    [SerializeField] private Health _health;
    [SerializeField] private AttentionPriority _attentionPriority;

    [Header("Components")]
    [SerializeField] private Attacker _attacker;
    [SerializeField] private Follower _follower;
    [SerializeField] private AggroZone _aggroZone;
    [SerializeField] private Rigidbody2D _rigidbody;

    private Coroutine _task;

    public AttentionPriority Priority => _attentionPriority;
    public Transform Transform => transform;

    private void Awake()
    {
        _health.Died += OnDied;

        _attacker.Catcher.TargetCatched += OnTargetCatched;
        _attacker.Catcher.TargetReleased += OnTargetReleased;

        _aggroZone.TriggeredEnter += OnAggroZoneTriggeredEnter;
        _aggroZone.TriggeredExit += OnAggroZoneTriggeredExit;
    }

    private void OnDestroy()
    {
        _health.Died -= OnDied;

        _attacker.Catcher.TargetCatched -= OnTargetCatched;
        _attacker.Catcher.TargetReleased -= OnTargetReleased;

        _aggroZone.TriggeredEnter -= OnAggroZoneTriggeredEnter;
        _aggroZone.TriggeredExit -= OnAggroZoneTriggeredExit;
    }

    public void TakeDamage(int damage)
    {
        _attacker.ReleaseTarget();
        _health.TakeDamage(damage);

        if(_attacker.Target == null)
        {
            Seek();
        }
    }

    private void Seek()
    {
        _aggroZone.IncreaseRadius();
    }

    public void Stun(float time)
    {
        _follower.enabled = false;

        if (_task != null)
            StopCoroutine(_task);

        _task = StartCoroutine(UnstunAfter(time));
    }

    public void TossAway(float force)
    {
        _follower.enabled = false;
        _rigidbody.AddForce(transform.up * -1 * force);
    }

    public void SetTarget(Human human)
    {
        Transform targetTransform = human == null ? null : human.transform;
        _follower.SetTarget(targetTransform);
        _attacker.SetTarget(human);
    }

    private IEnumerator UnstunAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        _follower.enabled = true;
    }

    private void OnTargetCatched()
    {
        _follower.enabled = false;
    }

    private void OnTargetReleased()
    {
        _follower.enabled = true;
    }

    private void OnAggroZoneTriggeredEnter(Human human)
    {
        SetTarget(human);
    }

    private void OnAggroZoneTriggeredExit()
    {
        SetTarget(null);
    }

    private void OnDied()
    {
        _attacker.ReleaseTarget();
        Destroy(gameObject);
    }
}
