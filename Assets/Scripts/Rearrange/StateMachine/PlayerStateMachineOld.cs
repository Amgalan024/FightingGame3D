public class PlayerStateMachineOld
{
    public PlayerStates PlayerStates { private set; get; }
    public State CurrentState { private set; get; }
    public State PreviousState { private set; get; }

    public void Initialize(State startingState, PlayerStates playerStates)
    {
        CurrentState = startingState;
        PlayerStates = playerStates;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}