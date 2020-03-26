using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    [SerializeField] private float fleeUntilDist = 5f;
    [SerializeField] private float fleeStartDist = 2f;

    public override void Start()
    {
        base.Start();

        GameManager.Instance.chickens.Add(this);
    }

    protected override void Initialize()
    {
        base.Initialize();

        name = "Chicken";
        legs = 4;

        // add states and subscribe to listeners
        fsm.AddState(AnimalStateType.Flee, new AnimalFlee());
        fsm.GetState(AnimalStateType.Flee).onEnter.AddListener(ToggleRun);
        fsm.GetState(AnimalStateType.Flee).onExit.AddListener(ToggleRun);
        fsm.AddState(AnimalStateType.Wander, new AnimalWander());

        // start in idle
        fsm.GotoState(AnimalStateType.Wander);
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
            case AnimalStateType.Wander:
                // player too close? go to flee
                if(distanceToPlayer < fleeStartDist)
                {
                    fsm.GotoState(AnimalStateType.Flee);
                }
                break;
            case AnimalStateType.Flee:
                // player far enough? go to idle
                if (distanceToPlayer >= fleeUntilDist)
                {
                    fsm.GotoState(AnimalStateType.Wander);
                }
                break;
        }
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        base.ReactToClick(leftMB, rightMB);

        if (leftMB)
            MakeSound();
        else if (rightMB)
            Die();
    }

    protected override void MakeSound()
    {
        Debug.Log("Pok pok pok");
    }

    public override void Destroy()
    {
        GameManager.Instance.chickens.Remove(this);

        base.Destroy();
    }

}