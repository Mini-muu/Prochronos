using UnityEngine;

public class Enemy_1_GroundedState : EnemyState
{
    protected Enemy_1 enemy;

    protected Transform playerTransform;

    protected Player player;

    public Enemy_1_GroundedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_1 _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
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

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, playerTransform.position) < 2)
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }
}
