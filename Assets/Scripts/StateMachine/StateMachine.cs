using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class StateMachine
{
    public PlayerStates PlayerStates { private set; get; }
    public State CurrentState { private set; get; }
    public State PreviousState { private set; get; }
    public void Initialize(State StartingState, PlayerStates PlayerStates)
    {
        this.CurrentState = StartingState;
        this.PlayerStates = PlayerStates;
        StartingState.Enter();
    }
    public void ChangeState(State newState)
    {
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }
}
