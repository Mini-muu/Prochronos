using UnityEngine;

public class Thorns : Trap
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        PlayerStats pStats = collider.gameObject.GetComponent<PlayerStats>();
        if (pStats != null)
        {
            HurtPlayer(pStats, collider);
        }
        else
        {
            KillEnemy(collider);
        }
    }

    private void HurtPlayer(PlayerStats pStats, Collider2D collider)
    {
        Player p = collider.GetComponent<Player>();

        if (p == null) return;

        p.knockbackDir = knockback;
        if (doesKnockback && pStats.TryTakeDamage(damage))
        {
            if (p.Stats.currentHealth > 0)
            {
                p.StateMachine.ChangeState(p.KnockbackState);
            }
        }
    }

    private void KillEnemy(Collider2D collider)
    {
        EnemyStats eStats = collider.gameObject.GetComponent<EnemyStats>();
        if (eStats == null) return;
        eStats.TryTakeDamage(eStats.currentHealth);
    }
}
