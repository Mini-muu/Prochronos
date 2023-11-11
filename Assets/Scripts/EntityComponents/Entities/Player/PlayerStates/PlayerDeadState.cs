public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.gameObject.layer = 0;
        player.SetDeadVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected() /*|| player.IsWallDetected()*/)
            player.SetZeroVelocity();

        /*if (triggerCalled && player.IsGroundDetected())
        {
            player.SetZeroVelocity();
        }*/
        
        //GameOver part
    }
}
