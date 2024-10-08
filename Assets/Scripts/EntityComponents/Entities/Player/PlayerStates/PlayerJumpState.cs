using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.Jump;
    }

    public override void Enter()
    {
        base.Enter();
        //stateTimer = .2f; -> DoubleJump Delay

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        PlayerInputManager.instance.jump.performed += DoubleJumpPerformed;
    }

    public override void Exit()
    {
        base.Exit();

        PlayerInputManager.instance.jump.performed -= DoubleJumpPerformed;
    }

    public override void Update()
    {
        //Debug.Log(player.canDoubleJump);

        base.Update();

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }
    }

    private void DoubleJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (player.canDoubleJump) //&& stateTimer < 0)
        {
            player.canDoubleJump = false;
            stateMachine.ChangeState(player.JumpState);
        }
    }
}
