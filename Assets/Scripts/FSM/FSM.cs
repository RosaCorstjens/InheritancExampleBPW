﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM 
{
    // owner of this FSM
    public GameObject owner { get; private set; }

    // some references to parts of the owner
    public Transform Transform { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    // all states in this FSM
    private Dictionary<StateType, State> states;

    // references to the current and previous state
    public StateType CurrentState { get; private set; }
    private State currState;
    private State prevState;

    public void Initialize(GameObject owner)
    {
        // get a reference to owner
        this.owner = owner;

        // get references to important parts of the owner
        Transform = owner.transform;
        Animator = owner.GetComponent<Animator>();
        NavMeshAgent = owner.GetComponent<NavMeshAgent>();

        // setup the dictionary for the states to be added
        states = new Dictionary<StateType, State>();
    }

    public void AddState(StateType newType, State newState)
    {
        // add the new state with the matching key
        states.Add(newType, newState);

        // initialize the new state, passing this FSM
        states[newType].Initialize(this);
    }

    public State GetState(StateType type)
    {
        // returns state if it exists, 
        // else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }

    public void UpdateState()
    {
        // if the current state exists, 
        // update it
        if(currState != null)
            currState.Update();
    }

    public void GotoState(StateType key)
    {
        // if this state doesn't exist, return
        if (!states.ContainsKey(key))
            return;

        // if there is a current state, 
        // end the state properly by calling exit
        if(currState != null)
            currState.Exit();

        // remember the previous state
        // and set the current state
        prevState = currState;
        CurrentState = key;
        currState = states[CurrentState];
        
        // start the new current state 
        // by entering it
        currState.Enter();
    }
}
