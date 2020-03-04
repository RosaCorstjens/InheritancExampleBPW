using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    protected override void Start()
    {
        base.Start();

        name = "Dog";
        legs = 4;
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
}
