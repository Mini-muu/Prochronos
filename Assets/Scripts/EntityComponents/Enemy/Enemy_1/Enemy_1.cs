
using UnityEngine;

// PLACEHOLDER
public class Enemy_1 : Enemy
{
    #region States

    public Enemy_1_IdleState IdleState { get; private set; }
    public Enemy_1_MoveState MoveState { get; private set; }
    public Enemy_1_BattleState BattleState { get; private set; }
    public Enemy_1_AttackState AttackState { get; private set; }
    public Enemy_1_StunnedState StunnedState { get; private set; }
    public Enemy_1_DeadState DeadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        IdleState = new Enemy_1_IdleState(this, StateMachine, "Idle", this);
        MoveState = new Enemy_1_MoveState(this, StateMachine, "Move", this);
        BattleState = new Enemy_1_BattleState(this, StateMachine, "Move", this);
        AttackState = new Enemy_1_AttackState(this, StateMachine, "Attack", this);
        StunnedState = new Enemy_1_StunnedState(this, StateMachine, "Stunned", this);
        DeadState = new Enemy_1_DeadState(this, StateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {
            StateMachine.ChangeState(StunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {

            StateMachine.ChangeState(StunnedState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(DeadState);
    }
}
