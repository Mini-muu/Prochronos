public class PlayerChargedAttack : PlayerState
{
    public PlayerChargedAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
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

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
