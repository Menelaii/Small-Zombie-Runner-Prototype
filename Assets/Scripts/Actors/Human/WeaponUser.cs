using System.Collections.Generic;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    [SerializeField] private float _switchCooldown;
    [SerializeField] private Transform _objectsParent;
    [SerializeField] private List<Weapon> _weapons;

    private Weapon _currentWeapon;
    private float _weaponCooldownTimer;
    private float _switchCooldownTimer;
    private int _index;

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i] = Instantiate(_weapons[i]);
        }

        Equip(_weapons[0]);
    }

    private void Update() 
    {
        _weaponCooldownTimer -= Time.deltaTime;
        if(_weaponCooldownTimer <= 0 && Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Use();
            _weaponCooldownTimer = _currentWeapon.AttackCooldown;
        }

        _switchCooldownTimer -= Time.deltaTime;
        if (_switchCooldownTimer <= 0 && Input.GetMouseButton(1))
        {
            TrySwitchWeapon();
            _switchCooldownTimer = _switchCooldown;
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        if (_weapons.Contains(weapon))
            return;

        Weapon weaponObject = Instantiate(weapon);

        _weapons.Add(weaponObject);
    }

    private void TrySwitchWeapon()
    {
        if (_weapons.Count == 1 || _currentWeapon.Switchable == false)
            return;

        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        _currentWeapon.gameObject.SetActive(false);

        _index++;
        if (_index >= _weapons.Count)
            _index = 0;

        Equip(_weapons[_index]);
    }

    private void Equip(Weapon weapon)
    {
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
    }

    private Weapon Instantiate(Weapon weapon)
    {
        Weapon weaponObject = Instantiate(weapon, _objectsParent);
        weaponObject.gameObject.SetActive(false);
        return weaponObject;
    }
}