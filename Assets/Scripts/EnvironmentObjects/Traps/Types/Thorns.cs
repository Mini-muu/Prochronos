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
                if (doesKnockback && pStats.TryTakeDamage(damage))
                {
                    if (p.Stats.currentHealth > 0)
                    {
                        p.StateMachine.ChangeState(p.KnockbackState);
                    }
                }
            }
            else
            {
                EnemyStats eStats = collider.gameObject.GetComponent<EnemyStats>();
                eStats.TryTakeDamage(eStats.currentHealth);
            }
        }
    }
}
