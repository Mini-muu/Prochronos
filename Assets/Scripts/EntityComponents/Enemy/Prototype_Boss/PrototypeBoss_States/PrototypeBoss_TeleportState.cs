using System.Collections;
using UnityEngine;

public class PrototypeBoss_TeleportState : EnemyState
{
    private PrototypeBoss boss;

    public PrototypeBoss_TeleportState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, PrototypeBoss _boss) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        boss = _boss;
    }

    public override void Enter()
    {
        base.Enter();

        boss.Stats.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();

        boss.Stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            stateMachine.ChangeState(boss.BattleState);
        }
    }
}