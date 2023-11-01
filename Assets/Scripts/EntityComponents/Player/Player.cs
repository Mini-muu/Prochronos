using System.Collections;
using UnityEngine;

//Check PJudge
public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float parryDuration = .2f;

    public bool IsBusy { get; private set; }

    [Header("Move Info")]
    public float moveSpeed = 6f;
    public float runSpeed = 8f;
    public float jumpForce = 12f;

    [Header("Dash Info")]
    public float rollSpeed = 25f;
    public float rollDuration = 2.5f;
    public float RollDir { get; private set; }

    [HideInInspector] public SkillManager skill;

    #region States
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerRunState RunState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerAirState AirState { get; private set; }

    public PlayerWallSlideState WallSlideState { get; private set; }

    public PlayerWallJumpState WallJumpState { get; private set; }

    public PlayerRollState RollState { get; private set; }

    public PlayerNormalAttack NormalAttrackState { get; private set; }

    public PlayerChargedAttack ChargedAttackState { get; private set; }

    public PlayerParryState ParryState { get; private set; }

    public PlayerKnockbackState KnockbackState { get; private set; }

    public PlayerDeadState DeadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        RunState = new PlayerRunState(this, StateMachine, "Run");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        RollState = new PlayerRollState(this, StateMachine, "Roll");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        NormalAttrackState = new PlayerNormalAttack(this, StateMachine, "NormalAttack");
        ChargedAttackState = new PlayerChargedAttack(this, StateMachine, "ChargedAttack");
        ParryState = new PlayerParryState(this, StateMachine, "Parry");
        KnockbackState = new PlayerKnockbackState(this, StateMachine, "Hurt");
        //TODO - Fix Dead State
        DeadState = new PlayerDeadState(this, StateMachine, "Die");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
        skill = SkillManager.instance;
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.Roll.CanUseSkill())
        {

            RollDir = Input.GetAxisRaw("Horizontal");

            if (RollDir == 0)
            {
                RollDir = FacingDir;
            }

            StateMachine.ChangeState(RollState);
        }
    }

    public override void Die()
    {
        base.Die();

        StateMachine.ChangeState(DeadState);
    }

}