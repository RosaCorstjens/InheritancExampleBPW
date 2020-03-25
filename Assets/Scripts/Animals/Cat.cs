using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Animal
{
    protected override void Initialize()
    {
        base.Initialize();

        name = "Cat";
        legs = 4;

        // add idle
        fsm.AddState(AnimalStateType.Idle, new AnimalIdle());

        // start in idle
        fsm.GotoState(AnimalStateType.Idle);
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