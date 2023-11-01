using System;
using UnityEngine;

public class Thorns : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        PlayerStats pStats = collider.gameObject.GetComponent<PlayerStats>();
        if (pStats != null)
        {
            Player p = collider.GetComponent<Player>();
            if (p != null)
            {
                pStats.TakeDamage(damage);
                p.StateMachine.ChangeState(p.KnockbackState);
            }
            else
            {
                EnemyStats eStats = collider.gameObject.GetComponent<EnemyStats>();
                eStats.TakeDamage(eStats.currentHealth);
            }
        }

        Debug.Log("Triggered");
    }
}
