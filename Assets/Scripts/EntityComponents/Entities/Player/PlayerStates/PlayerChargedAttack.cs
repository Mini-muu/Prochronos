public class PlayerChargedAttack : PlayerState
{
    public PlayerChargedAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        action = PlayerAction.StrongAttack;
    }

    //TODO
    //Attack info
    //Damage = 3
    //ChargeDuration -> CheckFrame (hypothetically around 3sec)
    //Player can move & change face direction while charging
    //Connect Animation -> Animator

    public override void Enter()
    {
        base.Enter();

        stateTimer = .2f;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
