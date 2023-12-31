﻿using System.Collections;
using UnityEngine;

//Check PJudge
public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;
    public float parryDuration = .2f;

    public bool IsBusy { get; private set; }
    [HideInInspector] public bool allowStaminaRecovery = true;

    [Header("Move Info")]
    public float moveSpeed = 6f;
    public float runSpeed = 8f;
    public float runConsumptionPerSec = 0.2f;
    public float jumpForce = 12f;
    [HideInInspector] public bool canDoubleJump = true;

    [Header("Roll Info")]
    public float rollSpeed = 25f;
    public float rollDuration = 2.5f;
    public float rollConsumption = 2;
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

        CheckForRollInput();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemData item = Inventory.instance.inventoryItemsAlt[0].Key;
            if (item != null)
            {
                item.ExecuteItemEffects();
                Inventory.instance.RemoveItem(item);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ItemData item = Inventory.instance.inventoryItemsAlt[1].Key;
            if (item != null)
            {
                item.ExecuteItemEffects();
                Inventory.instance.RemoveItem(item);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ItemData item = Inventory.instance.inventoryItemsAlt[2].Key;
            if (item != null)
            {
                item.ExecuteItemEffects();
                Inventory.instance.RemoveItem(item);
            }
        }
    }

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void CheckForRollInput()
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

        IsBusy = true;
    }

    public void SetDeadVelocity()
    {
        RB.velocity = new Vector2(4*(-FacingDir), RB.velocityY);
    }

    public override void OnHeavyHit()
    {
        base.OnHeavyHit();
        StateMachine.ChangeState(KnockbackState);
    }
}