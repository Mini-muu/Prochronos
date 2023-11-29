
public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState _startState)
    {
        CurrentState = _startState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        if (!_newState.IsUnlocked()) return;

        CurrentState.Exit();
        CurrentState = _newState;
        CurrentState.Enter();
    }
}
