using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    [SerializeField] private float chaseUntilDist = 5f;
    [SerializeField] private float chaseStartDist = 10f;

    protected override void Initialize()
    {
        base.Initialize();

        name = "Dog";
        legs = 4;

        // add idle state
        fsm.AddState(AnimalStateType.Idle, new AnimalIdle());

        // add chase state
        fsm.AddState(AnimalStateType.Chase, new AnimalChase());

        // start in the chase state
        fsm.GotoState(AnimalStateType.Chase);
    }

    public override void DoUpdate()
    {
        base.DoUpdate();

        // calculate distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.controller.transform.position);

        // based on the current state
        // react on that distance
        switch (fsm.CurrentState)
        {
            case AnimalStateType.Idle:
                // player far enough? go to flee
                if (distanceToPlayer > chaseStartDist)
                {
                    fsm.GotoState(AnimalStateType.Chase);
                }
                break;
            case AnimalStateType.Chase:
                // player reached? go to idle
                if (distanceToPlayer < chaseUntilDist)
                {
                    fsm.GotoState(AnimalStateType.Idle);
                }
                break;
        }
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        base.ReactToClick(leftMB, rightMB);

        if (leftMB)
        {
            LookAtPlayer();
            MakeSound();
        }
    }

    protected override void MakeSound()
    {
        Debug.Log("Woef woef");
    }
}
