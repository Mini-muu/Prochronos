using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Data", menuName = "Data/Effect/Heal Effect")]
public class Heal_Effect : ItemEffect
{
    [SerializeField] private int healAmount;

    public override void ExecuteEffect()
    {
        base.ExecuteEffect();

        PlayerStats playerStats = PlayerManager.instance.playerStats;

        playerStats.IncreaseHealthBy(healAmount);

        playerStats.onHealthChanged();
    }
}
