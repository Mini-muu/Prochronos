using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //stateTimer = .2f; -> DoubleJump Delay

        PlayerInputManager.instance.jump.performed += DoubleJumpPerformed;
    }

    public override void Exit()
    {
        base.Exit();

        PlayerInputManager.instance.jump.performed -= DoubleJumpPerformed;
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }
    }

    private void DoubleJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (player.canDoubleJump)// && stateTimer < 0)
        {
            player.canDoubleJump = false;
            stateMachine.ChangeState(player.JumpState);
        }
    }
}
