public class PlayerKnockbackState : PlayerState
{
    public PlayerKnockbackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.StartCoroutine("BusyFor", player.knockbackDuration);
        stateTimer = .015f;
    }

    public override void Exit()
    {
        base.Exit();
        player.knockbackPower = 1;
        player.knockbackDuration = player.OriginalKnockbackDuration;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsKnocked)
        {
            stateMachine.ChangeState(player.AirState);
        }

        if(stateTimer < 0 && (player.IsWallDetected()  || player.IsGroundDetected())) 
        {
            player.StopKnockback();
        }
    }
}
