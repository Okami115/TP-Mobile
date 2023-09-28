using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State currentState;
    private Dictionary<Type, State> states;


    public StateMachine()
    {
        states = new Dictionary<Type, State>();
    }

    public void AddState<T>(State state) where T : State
    {
        states.Add(typeof(T), state);
    }

    public void Update()
    {
        currentState.Update();
    }

    public void ChangeState<T>() where T : State
    {
        currentState?.Exit();

        Type nextStateType = typeof(T);

        if (states.TryGetValue(nextStateType, out var state)) 
        { 
            currentState = state;
            currentState.Enter();
        }
    }
}

public abstract class State
{
    protected StateMachine machine;
    protected Dictionary<Type, Condition> conditions = new Dictionary<Type, Condition> ();

    protected State(StateMachine machine)
    {
        this.machine = machine;
    }

    public bool CheckCondition<T>() where T : Condition
    {
        bool ret = false;
        Type conditionType = typeof(T);

        if (conditions.TryGetValue(conditionType, out var condition))
            ret = condition.Check();
        
        return ret;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class Condition
{
    public virtual bool Check() => true;
}
