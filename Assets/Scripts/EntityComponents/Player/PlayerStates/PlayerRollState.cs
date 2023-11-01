public class PlayerRollState : PlayerState
{

    //Check Immunity Frame
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //player.skill.Roll = null;

        stateTimer = player.rollDuration;

        player.ToggleImmunity();
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);

        player.ToggleImmunity();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.WallSlideState);
            return;
        }

        player.SetVelocity(player.rollSpeed * player.RollDir, 0);

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
    }
}