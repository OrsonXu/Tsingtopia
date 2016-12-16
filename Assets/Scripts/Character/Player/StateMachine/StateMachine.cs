using UnityEngine;
using System.Collections;

public class StateMachine <entityType> {
    /// <summary>
    /// The object of this state
    /// </summary>
    entityType Owner;

    /// <summary>
    /// The global state of the owner, empty tmporarily
    /// </summary>
    State<entityType> GlobalState;

    /// <summary>
    /// The current state of the owner
    /// </summary>
    State<entityType> CurrentState;

    /// <summary>
    /// The previous state of the owner
    /// </summary>
    State<entityType> PreviousState;

    public StateMachine(entityType owner)
    {
        Owner = owner;
        GlobalState = null;
        CurrentState = null;
        PreviousState = null;
    }

    public void SetGlobalState(State<entityType> globalState)
    {
        GlobalState = globalState;
        GlobalState.Target = Owner;
        GlobalState.Enter(Owner);
    }

    public void SetCurrentState(State<entityType> currentState)
    {
        CurrentState = currentState;
        CurrentState.Target = Owner;
        CurrentState.Enter(Owner);
    }

    public void ChangeState(State<entityType> newState)
    {

        if (newState == null)
        {
            Debug.LogError("Cannot find this state");
        }

        CurrentState.Exit(Owner);
        PreviousState = CurrentState;
        PreviousState.Target = Owner;

        CurrentState = newState;
        CurrentState.Target = Owner;
        CurrentState.Enter(Owner);
    }

    public void RevertPreviousState()
    {
        ChangeState(PreviousState);
    }

    public void SMUpdate()
    {
        //if (GlobalState != null)
        //{
        //    GlobalState.Execute(Owner);
        //}

        if (CurrentState != null)
        {
            CurrentState.Execute(Owner);
        }
    }

    public State<entityType> GetGlobalState()
    {
        return GlobalState;
    }

    public State<entityType> GetCurrentState()
    {
        return CurrentState;
    }

    public State<entityType> GetPreviousState()
    {
        return PreviousState;
    }
    
}
