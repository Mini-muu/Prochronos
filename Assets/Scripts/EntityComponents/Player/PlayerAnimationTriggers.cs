using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        PerformAttack(false);
    }

    private void ChargedAttackTrigger()
    {
        PerformAttack(true);
    }

    private void PerformAttack(bool isCharged)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats target = hit.GetComponent<EnemyStats>();
                if(isCharged)
                    player.Stats.DoStrongDamage(target);
                else
                    player.Stats.DoDamage(target);
            }
        }
    }
}
