using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public Stat maxHealth;
    public Stat stamina;
    public Stat armor;
    public Stat damage;

    public int currentHealth;

    public System.Action onHealthChanged;
    //TODO - Apply
    public System.Action onMaxHealthChanged;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();

    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue();

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TakeDamage(totalDamage);
    }

    private int CheckTargetArmor(CharacterStats _targetStats, int _totalDamage)
    {
        _totalDamage -= armor.GetValue();
        _totalDamage = Mathf.Clamp(_totalDamage, 0, int.MaxValue);

        return _totalDamage;
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {

    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }
}
