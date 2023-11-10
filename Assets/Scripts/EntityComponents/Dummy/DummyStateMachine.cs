public class DummyStateMachine
{

    public DummyState CurrentState { get; private set; }

    public void Initialize(DummyState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(DummyState _newState)
    {
        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
