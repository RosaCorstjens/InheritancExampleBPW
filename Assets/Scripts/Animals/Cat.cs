using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Animal
{
    [SerializeField] private float chaseUntilDist = 2f;

    private Chicken target;

    public override void Start()
    {
        // call base
        base.Start();

        // set my vars
        name = "Cat";
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

        // goto idle state for starters
        fsm.ChangeState(AnimalState.Type.Idle);
    }

    public override void Update()
    {
        // call base
        base.Update();

        // check for target update
        if (target == null)
            target = GameManager.Instance.GetRandomChicken();

        // manage state transitions
        switch (fsm.currentState)
        {
            case AnimalState.Type.Idle:
                // go to chase if we have a target
                if (target != null)
                    fsm.ChangeState(AnimalState.Type.Chase);
                break;
            case AnimalState.Type.Chase:
                // set target
                ((Chase)fsm.GetState(AnimalState.Type.Chase)).SetChaseTarget(target.transform);

                // reached target? kill it and transition to idle
                if (Vector3.Distance(transform.position, target.transform.position) < chaseUntilDist)
                {
                    target.Die();
                    target = null;
                    fsm.ChangeState(AnimalState.Type.Idle);
                }
                break;
        }
    }

    protected override void MakeSound()
    {
        Debug.Log("Miaaauw");
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        // cats do not care if they are clicked, duh
    }
}