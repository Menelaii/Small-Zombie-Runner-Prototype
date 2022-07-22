using UnityEngine;

public class Human : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;

    [Header("Components")]
    [SerializeField] private TopDownMovement _movement;
    [SerializeField] private WeaponUser _weaponUser;
    [SerializeField] private TargetLock _targetLock;

    private void Awake()
    {
        _health.Died += OnDied;
        _movement.Init(new PlayerMovementInput());
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
    }

    public void PickUp(Weapon weapon)
    {
        _weaponUser.AddWeapon(weapon);
    }

    public void DisableMovement() => _movement.enabled = false;

    public void EnableMovement() => _movement.enabled = true;

    private void OnDied()
    {
        _health.Died -= OnDied;
        Destroy(gameObject);
    }
}
