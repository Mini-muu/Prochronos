using UnityEngine;

public class Enemy_1_IdleState : Enemy_1_GroundedState
{
    public Enemy_1_IdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_1 _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName, _enemy)
    { 
    
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }

        if (enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
