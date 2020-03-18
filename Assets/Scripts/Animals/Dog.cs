using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    private float timer;

    protected override void Start()
    {
        base.Start();

        name = "Dog";
        legs = 4;

        // add idle state
        // subscribe on enter
        fsm.AddState(StateType.Idle, new AnimalIdle());
        fsm.GetState(StateType.Idle).onEnter.AddListener(SetTimer);

        // add the wander state
        fsm.AddState(StateType.Wander, new AnimalWander());
        fsm.GetState(StateType.Wander).onEnter.AddListener(SetTimer);

        // start in the wander state
        fsm.GotoState(StateType.Wander);
    }

    protected override void Update()
    {
        base.Update();

        // check whether we should leave the idle state
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            switch (fsm.CurrentState)
            {
                case StateType.Idle:
                    fsm.GotoState(StateType.Wander);
                    break;
                case StateType.Wander:
                    fsm.GotoState(StateType.Idle);
                    break;
            }
        }
    }

    protected override void ReactToClick()
    {
        base.ReactToClick();

        LookAtPlayer();
        MakeSound();
    }

    protected override void MakeSound()
    {
        Debug.Log("Woef woef");
    }

    public void SetTimer()
    {
        switch (fsm.CurrentState)
        {
            case StateType.Idle:
                timer = Random.Range(1, 7);
                break;
            case StateType.Wander:
                timer = Random.Range(20, 60);
                break;
        }
    }
}
