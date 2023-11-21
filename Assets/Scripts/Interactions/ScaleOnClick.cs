using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnClick : MonoBehaviour, IClickable
{
    public void Clicked(bool leftMB, bool rightMB)
    {
        if (leftMB)
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (rightMB)
            transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
}
