using UnityEngine;

[CreateAssetMenu(fileName ="New Effect Data", menuName = "Data/Effect/Rinvigoration Effect")]
public class Rinvigoration_Effect : ItemEffect
{
    [SerializeField] private int rinvigorateAmount;

    public override void ExecuteEffect()
    {
        base.ExecuteEffect();

        PlayerStats playerStats = PlayerManager.instance.playerStats;

        playerStats.maxHealth.AddModifier(rinvigorateAmount);

        playerStats.onHealthChanged();
    }
}
