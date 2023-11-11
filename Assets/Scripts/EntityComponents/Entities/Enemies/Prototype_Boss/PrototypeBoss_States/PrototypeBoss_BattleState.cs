using UnityEngine;

public class PrototypeBoss_BattleState : EnemyState
{
    private PrototypeBoss boss;
    protected Transform playerTransform;
    private Player player;
    private int moveDir;

    public PrototypeBoss_BattleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, PrototypeBoss _boss) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        boss = _boss;
    }
    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player;
        playerTransform = player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.Stats.IsDead)
            stateMachine.ChangeState(boss.IdleState);

        if (boss.IsPlayerDetected())
        {
            stateTimer = boss.battleTime;

            if (boss.IsPlayerDetected().distance < boss.attackDistance)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(boss.AttackState);
                }
            }
        }

        if (playerTransform.position.x > boss.transform.position.x)
        {
            moveDir = 1;

        }
        else if (playerTransform.position.x < boss.transform.position.x)
        {
            moveDir = -1;
        }

        boss.SetVelocity(boss.moveSpeed * moveDir, rb.velocity.y);


        if (Vector2.Distance(playerTransform.transform.position, boss.transform.position) > 10)
        {
            stateMachine.ChangeState(boss.TeleportState);
        }
    }

    private bool CanAttack()
    {
        if (Time.time >= boss.lastTimeAttack + boss.attackCooldown)
        {
            boss.lastTimeAttack = Time.time;
            return true;
        }

        return false;
    }
}
