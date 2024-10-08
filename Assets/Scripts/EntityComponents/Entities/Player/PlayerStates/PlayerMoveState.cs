using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.Move;
    }

    public override void Enter()
    {
        base.Enter();

        PlayerInputManager.instance.run.performed += RunPerformed;
    }

    public override void Exit()
    {
        base.Exit();

        PlayerInputManager.instance.run.performed -= RunPerformed;
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0 || player.IsWallDetected())
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private void RunPerformed(InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(player.RunState);
    }
}
