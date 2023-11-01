using UnityEngine;

public class Enemy_1_StunnedState : EnemyState
{
    private Enemy_1 enemy;

    public Enemy_1_StunnedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_1 _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.FX.InvokeRepeating("RedColorBlink", 0, .1f);

        stateTimer = enemy.stunnedDuration;

        rb.velocity = new Vector2(-enemy.FacingDir * enemy.stunnedDir.x, enemy.stunnedDir.y);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.FX.Invoke("CancelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.IdleState);
        }
    }
}
