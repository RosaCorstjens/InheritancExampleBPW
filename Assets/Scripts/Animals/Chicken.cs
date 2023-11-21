using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private float fleeUntilDist = 5f;
    [SerializeField] private float fleeStartDist = 2f;

    public override void Start()
    {
        // call start from Animal
        base.Start();

        // set my variables
        name = "Chicken";
        legs = 2;
    }

    protected override void SetupFSM()
    {
        // call base
        base.SetupFSM();

        // add wander state
        fsm.AddState(AnimalState.Type.Wander, new Wander(fsm));

        // add flee state
        fsm.AddState(AnimalState.Type.Flee, new Flee(fsm));

        // start in wander
        fsm.ChangeState(AnimalState.Type.Wander);
    }

    public override void Update()
    {
        // call base
        base.Update();

        // manage state transitions
        switch (fsm.currentState)
        {
            case AnimalState.Type.Wander:
                // player too close? flee!
                if (distToPlayer < fleeStartDist)
                    fsm.ChangeState(AnimalState.Type.Flee);
                break;
            case AnimalState.Type.Flee:
                // player far enough? go wander
                if (distToPlayer >= fleeUntilDist)
                    fsm.ChangeState(AnimalState.Type.Wander);
                break;
        }
    }

    protected override void MakeSound()
    {
        Debug.Log("Pok pok pok");
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        if (leftMB)
            MakeSound();
        else if (rightMB)
            Die();
    }
}