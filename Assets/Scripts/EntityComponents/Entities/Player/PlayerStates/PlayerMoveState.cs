
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.Move;
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

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.IsWallDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateMachine.ChangeState(player.RunState);
        }
    }
}
