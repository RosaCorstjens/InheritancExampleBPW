using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private float fleeUntilDist = 5f;
    [SerializeField] private float fleeStartDist = 2f;

    protected override void Start()
    {
        base.Start();

        name = "Chicken";
        legs = 4;

        // add states and subscribe to listeners
        fsm.AddState(StateType.Flee, new AnimalFlee());
        fsm.GetState(StateType.Flee).onEnter.AddListener(ToggleRun);
        fsm.GetState(StateType.Flee).onExit.AddListener(ToggleRun);
        fsm.AddState(StateType.Idle, new AnimalIdle());

        // start in idle
        fsm.GotoState(StateType.Idle);
    }

    protected override void Update()
    {
        base.Update();

        // calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, CameraController.myTransform.position);

        // based on the current state
        // react on that distance
        switch (fsm.CurrentState)
        {
            case StateType.Idle:
                // player too close? go to flee
                if(distanceToPlayer < fleeStartDist)
                {
                    fsm.GotoState(StateType.Flee);
                }
                break;
            case StateType.Flee:
                // player far enough? go to idle
                if (distanceToPlayer >= fleeUntilDist)
                {
                    fsm.GotoState(StateType.Idle);
                }
                break;
        }
    }

    protected override void ReactToClick()
    {
        base.ReactToClick();
        MakeSound();
    }

    protected override void MakeSound()
    {
        Debug.Log("Pok pok pok");
    }
}