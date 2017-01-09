using UnityEngine;
using System.Collections;
/// <summary>
/// Basic type of the state
/// </summary>
/// <typeparam name="enetityType"></typeparam>
public class State <enetityType>
{
    public enetityType Target;
    /// <summary>
    /// Enter State
    /// </summary>
    /// <param name="entityType"></param>
    public virtual void Enter(enetityType obj)
    {

    }
    /// <summary>
    /// During the state
    /// </summary>
    /// <param name="entityType"></param>
    public virtual void Execute(enetityType obj)
    {
        
    }
    /// <summary>
    /// When exit the state
    /// </summary>
    /// <param name="obj"></param>
    public virtual void Exit(enetityType obj)
    {

    }
}

