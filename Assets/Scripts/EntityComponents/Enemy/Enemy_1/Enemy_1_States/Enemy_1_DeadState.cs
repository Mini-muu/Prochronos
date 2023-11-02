using UnityEngine;

public class Enemy_1_DeadState : EnemyState
{
    Enemy_1 enemy;
    bool executed;

    public Enemy_1_DeadState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_1 _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        executed = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0 && executed)
        {
            rb.velocity = new Vector2(0, 8);
        }

        if(triggerCalled && !executed)
        {
            enemy.Anim.SetBool(enemy.LastAnimBolName, true);
            enemy.Anim.speed = 0;
            enemy.CD.enabled = false;
            stateTimer = .15f;
            enemy.gameObject.layer = 0;
            executed = true;
        }
    }
}
