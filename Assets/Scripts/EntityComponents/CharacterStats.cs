using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Stats")]
    public Stat maxHealth;
    public Stat maxStamina;
    public Stat armor;
    public Stat damage;
    public Stat strongDamage;

    [Space(20)]

    public int currentHealth;
    public float currentStamina;
    public float staminaPerSec;
    public float staminaWaitingRecovery = 1f;

    public System.Action onHealthChanged;
    //TODO - Apply
    public System.Action onMaxHealthChanged;

    public System.Action onStaminaChanged;
    //TODO - Apply
    public System.Action onMaxStaminChanged;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        currentStamina = GetMaxStaminaValue();
    }

    protected virtual void Update()
    {

    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue();

        Damage(totalDamage, _targetStats);
    }

    public virtual void DoStrongDamage(CharacterStats _targetStats)
    {
        int totalDamage = strongDamage.GetValue();

        Damage(totalDamage, _targetStats);
    }

    private void Damage(int totalDamage, CharacterStats _targetStats)
    {
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        _targetStats.TryTakeDamage(totalDamage);
    }

    private int CheckTargetArmor(CharacterStats _targetStats, int _totalDamage)
    {
        _totalDamage -= armor.GetValue();
        _totalDamage = Mathf.Clamp(_totalDamage, 0, int.MaxValue);

        return _totalDamage;
    }

    public virtual bool TryTakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if (currentHealth <= 0)
        {
            Die();
        }

        return true;
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

    public float GetMaxStaminaValue()
    {
        return maxStamina.GetValue();
    }
}
