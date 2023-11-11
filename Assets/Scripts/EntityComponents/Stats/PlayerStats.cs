using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    float timeElapsed;

    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override bool TryTakeDamage(int _damage)
    {
        return base.TryTakeDamage(_damage);
    }

    protected override void Die()
    {
        player.Die();
        base.Die();
    }

    protected override void Update()
    {
        base.Update();
        timeElapsed += Time.deltaTime;

        player.allowStaminaRecovery = AllowStaminaRecovery();

        TryRecoverStamina();
    }

    #region Stamina

    private void TryRecoverStamina()
    {
        if (!CanStaminaRecover()) return;

        if (currentStamina < GetMaxStaminaValue())
        {
            IncreaseStaminaBy(staminaPerSec / 60);
        }
        else
        {
            SetStaminaTo(GetMaxStaminaValue());
            player.allowStaminaRecovery = false;
        }

        timeElapsed = 0;
    }

    private bool CanStaminaRecover()
    {
        return timeElapsed >= 0.01f && player.allowStaminaRecovery;
    }

    private bool AllowStaminaRecovery()
    {
        if(player.allowStaminaRecovery) return true;

        return timeElapsed >= staminaWaitingRecovery;
    }

    public void DecreaseStaminaBy(float value)
    {
        currentStamina -= value;

        player.allowStaminaRecovery = false;

        if (onStaminaChanged != null)
            onStaminaChanged();
    }

    private void SetStaminaTo(float value)
    {
        currentStamina = value;

        if (onStaminaChanged != null)
            onStaminaChanged();
    }

    public void IncreaseStaminaBy(float value)
    {
        currentStamina += value;

        if (onStaminaChanged != null)
            onStaminaChanged();
    }

    #endregion
}
