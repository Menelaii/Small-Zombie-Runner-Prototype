using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private float _force;
    [SerializeField] private Projectile _ammoType;
    [SerializeField] private int _magCapacity;
    [SerializeField] private int _ammo;

    private int _currentMag;

    private void Start()
    {
        _currentMag = _magCapacity;
    }

    public override void Use()
    {
        if(_currentMag > 0)
        {
            base.Use();
        }
        else
        {
            TryReload();
        }
    }

    public void OnReload()
    {
        if(_ammo >= _magCapacity)
        {
            _currentMag = _magCapacity;
            _ammo -= _magCapacity;
        }
        else if(_ammo > 0)
        {
            _currentMag += _ammo;
            _ammo = 0;
        }
    }

    public override void OnAttackAnimation()
    {
        _currentMag--;
        Shoot(_ammoType, ShootPoint.transform.up);
    }

    protected virtual void Shoot(Projectile bullet, Vector2 direction)
    {
        var projectile = Instantiate(bullet, ShootPoint.position, Quaternion.identity);
        projectile.AddForce(ShootPoint.transform.up, _force);
    }

    private void TryReload()
    {
        if (_ammo <= 0)
            return;

        StartReload();
    }

    private void StartReload()
    {
        Switchable = false;
        Animator.SetTrigger("Reload");
    }
}
