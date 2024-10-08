
public class DummyBrokenState : DummyState
{
    public DummyBrokenState(Dummy _dummy, DummyStateMachine _stateMachine, string _animBoolName) : base(_dummy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dummy.Stats.MakeInvincible(true);
    }

    public override void Exit()
    {
        base.Exit();
        dummy.Stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(dummy.IdleState);
            dummy.Destroy();
        }
    }
}

