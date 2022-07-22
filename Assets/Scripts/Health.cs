using System;
using UnityEngine;

[Serializable]
public class Health : IDamageable
{
    [SerializeField] private int _value;
    [SerializeField] private int _maxValue;

    public event Action Died;

    public void TakeDamage(int damage)
    {
        _value = Mathf.Clamp(_value - damage, 0, _value);
        if(_value <= 0)
        {
            Died?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        _value = Mathf.Clamp(_value + amount, 0, _maxValue);
    }
}
