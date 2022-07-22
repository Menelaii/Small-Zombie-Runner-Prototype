using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private int _damage;
    [SerializeField] private LayerMask _damageables;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _stunTime;

    private Collider2D[] _hits = new Collider2D[3];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ShootPoint.position, _radius);
    }

    public override void OnAttackAnimation()
    {
        Physics2D.OverlapCircleNonAlloc(ShootPoint.position, _radius, _hits, _damageables);
        foreach (Collider2D collider in _hits)
        {
            if (collider != null && collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);

                if(collider.TryGetComponent(out Zombie zombie))
                {
                    zombie.Stun(_stunTime);
                    zombie.TossAway(_force);
                }
            }
        }
    }
}
