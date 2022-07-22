using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private ZombieAnimator _animator;

    private float _elapsedTime;

    public Human Target { get; private set; }
    public Catcher Catcher { get; private set; } = new Catcher();

    public void Update()
    {
        if (Target == null)
            return;

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= _cooldown && TargetInRange())
        {
            _elapsedTime = 0;
            StartAttack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

    public void SetTarget(Human human)
    {
        Target = human;
    }

    public void ReleaseTarget()
    {
        Catcher.Release(Target);
    }

    private void StartAttack()
    {
        _animator.Attack();
    }

    private void OnAttack()
    {
        if (Target != null && TargetInRange())
        {
            Catcher.Catch(Target);
            Target.TakeDamage(_damage);
        }
    }

    private bool TargetInRange()
    {
        return Vector3.SqrMagnitude(Target.transform.position - transform.position) <= _range * _range;
    }
}
