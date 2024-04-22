using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.WallSlide;
    }

    public override void Enter()
    {
        base.Enter();
        PlayerInputManager.instance.jump.performed += JumpPerformed;
    }

    public override void Exit()
    {
        base.Exit();
        PlayerInputManager.instance.jump.performed -= JumpPerformed;
    }

    public override void Update()
    {
        base.Update();

        if(!player.IsWallDetected()) 
        {
            stateMachine.ChangeState(player.AirState);
        }

        /*if (PlayerInputManager.instance.jump.IsPressed())
        {
            stateMachine.ChangeState(player.WallJumpState);
            return;
        }*/
        if (stateMachine.CurrentState.Equals(player.WallJumpState)) return;

        if (xInput != 0 && player.FacingDir != xInput)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * .8f);
        }


        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(player.WallJumpState);
    }
}
