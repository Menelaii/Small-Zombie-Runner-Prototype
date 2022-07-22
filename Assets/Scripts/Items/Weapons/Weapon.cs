using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private float _attackCooldown;
    [SerializeField] protected Transform ShootPoint;
    [SerializeField] protected Animator Animator;

    public float AttackCooldown => _attackCooldown;
    public bool Switchable { get; protected set; } = true;

    public virtual void Use()
    {
        Switchable = false;
        Animator.SetTrigger("Attack");
    }

    public abstract void OnAttackAnimation();

    public void OnAnimationEnd()
    {
        Switchable = true;
    }
}
