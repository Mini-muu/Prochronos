public class DummyIdleState : DummyState
{
    public DummyIdleState(Dummy _dummy, DummyStateMachine _stateMachine, string _animBoolName) : base(_dummy, _stateMachine, _animBoolName)
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
    }
}