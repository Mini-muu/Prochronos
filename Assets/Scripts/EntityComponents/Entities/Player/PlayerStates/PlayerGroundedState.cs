﻿using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.ParryState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.NormalAttrackState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            stateMachine.ChangeState(player.ChargedAttackState);
        }

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.AirState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.JumpState);
            player.canDoubleJump = PlayerManager.instance.unlockedActions.Contains(PlayerAction.DoubleJump);
        }
    }
}
