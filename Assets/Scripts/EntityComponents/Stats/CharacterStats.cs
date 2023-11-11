using System.Runtime.CompilerServices;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

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
    public System.Action onMaxStaminaChanged;

    public bool IsDead { get; private set; }
    public bool IsInvicible {  get; private set; }

    protected virtual void Start()
    {
        currentHealth = GetMaxHealthValue();
        currentStamina = GetMaxStaminaValue();

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update() { }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        int totalDamage = damage.GetValue();

        bool hasBeenDamaged = Damage(totalDamage, _targetStats);

        if(hasBeenDamaged && _targetStats.currentHealth > 0)
        {
            Entity entity = _targetStats.gameObject.GetComponent<Entity>();
            if(entity != null)
            {
                entity.OnLightHit();
            }
        }
    }

    public virtual void DoStrongDamage(CharacterStats _targetStats)
    {
        int totalDamage = strongDamage.GetValue();

        bool hasBeenDamaged = Damage(totalDamage, _targetStats);

        if (hasBeenDamaged && _targetStats.currentHealth > 0)
        {
            Entity entity = _targetStats.gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                entity.OnHeavyHit();
            }
        }
    }

    private bool Damage(int totalDamage, CharacterStats _targetStats)
    {
        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        return _targetStats.TryTakeDamage(totalDamage);
    }

    private int CheckTargetArmor(CharacterStats _targetStats, int _totalDamage)
    {
        _totalDamage -= armor.GetValue();
        _totalDamage = Mathf.Clamp(_totalDamage, 0, int.MaxValue);

        return _totalDamage;
    }

    public virtual bool TryTakeDamage(int _damage)
    {
        if (IsInvicible) return false;

        DecreaseHealthBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth <= 0)
        {
            Die();
        }

        return true;
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
        IsDead = true;
    }

    /*public void KillEntity()
    {
        if (!IsDead)
            Die();
    }*/

    public void Resurrect()
    {
        if (IsDead)
        {
            IsDead = false;
            currentHealth = GetMaxHealthValue();
        }
    }

    public void MakeInvincible(bool _invincible) => IsInvicible = _invincible;

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue();
    }

    public float GetMaxStaminaValue()
    {
        return maxStamina.GetValue();
    }
}
