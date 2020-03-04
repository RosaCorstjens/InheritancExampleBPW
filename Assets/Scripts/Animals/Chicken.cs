using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Animal
{
    protected override void Start()
    {
        base.Start();

        name = "Chicken";
        legs = 4;
    }

    protected override void ReactToClick()
    {
        base.ReactToClick();

        Jump();
        LookAtPlayer();
        MakeSound();
    }

    protected override void MakeSound()
    {
        Debug.Log("Pok pok pok");
    }
}