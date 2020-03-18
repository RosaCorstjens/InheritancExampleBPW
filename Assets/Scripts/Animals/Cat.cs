using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Animal
{
    protected override void Start()
    {
        base.Start();

        name = "Cat";
        legs = 4;

        // start in idle
        fsm.GotoState(StateType.Idle);
    }

    protected override void ReactToClick()
    {
        base.ReactToClick();
    }

    protected override void MakeSound()
    {
        Debug.Log("Miaaauw");
    }
}