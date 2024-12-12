using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusEnemy : Fungus
{
    public CharacterStats characterStats;

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        PlayerStats pStats = collider.gameObject.GetComponent<PlayerStats>();
        if (pStats != null)
        {
            HurtPlayer(pStats, collider);
        }
    }

    private void HurtPlayer(PlayerStats pStats, Collider2D collider)
    {
        Player p = collider.GetComponent<Player>();

        if (p == null) return;

        if (pStats.TryTakeDamage(damage)) return;
    }
}
