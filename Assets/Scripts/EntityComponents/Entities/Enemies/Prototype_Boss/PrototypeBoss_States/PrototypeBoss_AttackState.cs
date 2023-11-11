
using UnityEngine;

public class PrototypeBoss_AttackState : EnemyState
{
    private PrototypeBoss boss;

    public PrototypeBoss_AttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, PrototypeBoss _boss) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        boss.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        boss.SetZeroVelocity();

        if (triggerCalled)
        {
            stateMachine.ChangeState(boss.TeleportState);
        }
    }
}