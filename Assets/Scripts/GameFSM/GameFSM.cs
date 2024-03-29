﻿using System;
using System.Collections.Generic;

public class GameFSM 
{
    private Dictionary<Type, GameState> states;
    public GameState currentState;

    public GameFSM()
    {
        // setup the dictionary for the states to be added
        states = new Dictionary<Type, GameState>();

        // start in no state
        currentState = null;
    }

    public void Update()
    {
        currentState?.Execute();
    }

    public void AddState(Type type, GameState state)
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

    public GameState GetState(Type type)
    {
        // returns state if it exists, else null
        if (states.ContainsKey(type))
            return states[type];
        else
            return null;
    }
}
