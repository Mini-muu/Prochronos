public class DummyHeavyHitState : DummyState
{
    public DummyHeavyHitState(Dummy _dummy, DummyStateMachine _stateMachine, string _animBoolName) : base(_dummy, _stateMachine, _animBoolName)
    {
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

        if (triggerCalled)
        {
            stateMachine.ChangeState(dummy.IdleState);
        }
    }
}