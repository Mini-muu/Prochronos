using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.Jump;
    }

    public override void Enter()
    {
        base.Enter();


        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            player.canDoubleJump = false;
            stateMachine.ChangeState(player.JumpState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.AirState);
        }
    }

    private bool IsDoubleJumpUnlocked()
    {
        return PlayerManager.instance.unlockedActions.Contains(PlayerAction.DoubleJump);
    }
}
