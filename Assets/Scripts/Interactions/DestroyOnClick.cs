using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnClick : MonoBehaviour, IClickable
{
    public void Clicked(bool leftMB, bool rightMB)
    {
        if (leftMB)
            Destroy(gameObject);
    }
}
