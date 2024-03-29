﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : Animal
{
    [SerializeField] private float chaseUntilDist = 5f;
    [SerializeField] private float chaseStartDist = 10f;

    public override void Start()
    {
        // call start from Animal
        base.Start();

        // set my variables
        name = "Dog";
        legs = 4;
    }

    protected override void SetupFSM()
    {
        // call base
        base.SetupFSM();

        // add idle state
        fsm.AddState(AnimalState.Type.Idle, new Idle(fsm));

        // add chase state
        fsm.AddState(AnimalState.Type.Chase, new Chase(fsm));
        ((Chase)fsm.GetState(AnimalState.Type.Chase)).SetChaseTarget(Camera.main.transform);

        // goto idle state for starters
        fsm.ChangeState(AnimalState.Type.Idle);
    }

    public override void Update()
    {
        // call base
        base.Update();

        // manage state transitions
        switch (fsm.currentState)
        {
            case AnimalState.Type.Idle:
                // player close enough? chase him!
                if (distToPlayer > chaseStartDist)
                    fsm.ChangeState(AnimalState.Type.Chase);
                break;
            case AnimalState.Type.Chase:
                // player reached? go to idle
                if (distToPlayer < chaseUntilDist)
                    fsm.ChangeState(AnimalState.Type.Idle);
                break;
        }
    }

    protected override void MakeSound()
    {
        Debug.Log("Woef! Woef!");
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        if (leftMB)
        {
            LookAt(Camera.main.transform.position);
            MakeSound();
        }
    }
}
