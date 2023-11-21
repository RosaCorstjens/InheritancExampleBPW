using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalFSM 
{
    public Animal owner;

    private Dictionary<AnimalState.Type, AnimalState> states;
    public AnimalState.Type currentState;

    public AnimalFSM(Animal owner)
    {
        // save the owner
        this.owner = owner;

        // setup the dictionary for the states to be added
        states = new Dictionary<AnimalState.Type, AnimalState>();

        // start in no state
        currentState = AnimalState.Type.None;
    }

    public void Update()
    {
        if (states.ContainsKey(currentState))
            states[currentState].Execute();
    }

    public void AddState(AnimalState.Type type, AnimalState state)
    {
        // check if state already exists
        // return if so
        if (states.ContainsKey(type))
            return;

        // add the new state
        states.Add(type, state);
    }

    public void ChangeState(AnimalState.Type type)
    {
        // if this state doesn't exist, return
        if (!states.ContainsKey(type))
            return;

        // exit old state
        // set and enter new state
        if(states.ContainsKey(currentState))
            states[currentState].Exit();
        
        currentState = type;

        if (states.ContainsKey(currentState))
            states[currentState]?.Enter();
    }

    public AnimalState GetState(AnimalState.Type type)
    {
        // returns state if it exists, else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }
}
