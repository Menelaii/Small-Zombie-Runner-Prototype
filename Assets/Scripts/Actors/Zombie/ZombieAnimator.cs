using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    private static readonly int _attack = Animator.StringToHash("Attack");

    [SerializeField] private Animator _animator;

    public void Attack()
    {
        _animator.SetTrigger(_attack);
    }
}
