using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogFSM 
{
    public Dog owner;

    private Dictionary<Type, DogState> states;
    private DogState currentState;

    public DogFSM(Dog owner)
    {
        // save the owner
        this.owner = owner;

        // setup the dictionary for the states to be added
        states = new Dictionary<Type, DogState>();

        // start in no state
        currentState = null;
    }

    public void Update()
    {
        Debug.Log(currentState);
        currentState?.Execute();
    }

    public void AddState(Type type, DogState state)
    {
        // check if state already exists
        // return if so
        if (states.ContainsKey(type))
            return;

        // add the new state
        states.Add(type, state);
    }

    public void ChangeState(Type type)
    {
        // if this state doesn't exist, return
        if (!states.ContainsKey(type))
            return;

        // exit old state
        // set and enter new state
        currentState?.Exit();
        currentState = states[type];
        currentState?.Enter();
    }

    public DogState GetState(Type type)
    {
        // returns state if it exists, else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }
}
