
using UnityEngine;


public class PrototypeBoss_AnimationTriggers : MonoBehaviour
{
    private PrototypeBoss enemy => GetComponentInParent<PrototypeBoss>();

private void AnimationTrigger()
{
    enemy.AnimationFinishTrigger();
}

private void AttackTrigger()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

    foreach (var hit in colliders)
    {
        if (hit.GetComponent<Player>() != null)
        {
            hit.GetComponent<Player>().knockbackPower = 3;
            PlayerStats target = hit.GetComponent<PlayerStats>();
            enemy.Stats.DoStrongDamage(target);
        }
    }
}
    private PrototypeBoss prototypeBoss => GetComponentInParent<PrototypeBoss>();

    private void Relocate() => prototypeBoss.FindPosition();

    private void MakeInvisible() => prototypeBoss.FX.MakeTransparent(true);

    private void MakeVisible() => prototypeBoss.FX.MakeTransparent(false);
}
