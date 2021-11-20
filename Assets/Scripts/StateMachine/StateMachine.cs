using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class StateMachine
{
    public PlayerStates PlayerStates { set; get; }
    public State CurrentState { set; get; }
    public State PreviousState { set; get; }
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
