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