using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Animal
{
    private Chicken target;
    [SerializeField] private float chaseUntilDist = 2f;

    protected override void Initialize()
    {
        base.Initialize();

        name = "Cat";
        legs = 4;

        // add idle
        fsm.AddState(AnimalStateType.Idle, new AnimalIdle());

        // add chase state
        fsm.AddState(AnimalStateType.Chase, new AnimalChase());

        // start in idle
        fsm.GotoState(AnimalStateType.Idle);
    }

    public override void DoUpdate()
    {
        base.DoUpdate();

        if(target == null)
        {
            target = GameManager.Instance.GetRandomChicken();

            if(target != null)
            {
                ((AnimalChase)fsm.GetState(AnimalStateType.Chase)).SetChaseTransform(target.transform);

                if (fsm.CurrentState != AnimalStateType.Chase)
                {
                    fsm.GotoState(AnimalStateType.Chase);
                }
            }
            else if(fsm.CurrentState != AnimalStateType.Idle)
            {
                fsm.GotoState(AnimalStateType.Idle);
            }
        }

        if(fsm.CurrentState == AnimalStateType.Chase)
        {
            float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if(distanceToTarget < chaseUntilDist)
            {
                target.Die();
                target = null;
                fsm.GotoState(AnimalStateType.Idle);
            }
        }
    }

    protected override void ReactToClick(bool leftMB, bool rightMB)
    {
        base.ReactToClick(leftMB, rightMB);
    }

    protected override void MakeSound()
    {
        Debug.Log("Miaaauw");
    }
}