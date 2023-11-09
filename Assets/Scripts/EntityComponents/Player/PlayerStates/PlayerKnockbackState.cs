public class PlayerKnockbackState : PlayerState
{
    public PlayerKnockbackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.knockbackPower = 1;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsKnocked)
        {
            stateMachine.ChangeState(player.AirState);
        }
    }
}
