using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM 
{
    public GameObject owner { get; private set; }

    public Transform Transform { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    private Dictionary<StateType, State> states;

    public StateType CurrentState { get; private set; }
    private State currState;
    private State prevState;

    public void Initialize(GameObject owner)
    {
        this.owner = owner;

        Transform = owner.transform;
        Animator = owner.GetComponent<Animator>();
        NavMeshAgent = owner.GetComponent<NavMeshAgent>();

        states = new Dictionary<StateType, State>();
    }

    public void AddState(StateType newType, State newState)
    {
        states.Add(newType, newState);
        states[newType].Initialize(this);
    }

    public State GetState(StateType type)
    {
        return states[type];
    }

    public void UpdateState()
    {
        if(currState != null)
            currState.Update();
    }

    public void GotoState(StateType key)
    {
        if (!states.ContainsKey(key))
            return;

        if(currState != null)
            currState.Exit();

        prevState = currState;
        CurrentState = key;
        currState = states[CurrentState];
        
        currState.Enter();
    }
}
