﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class DestroyOnClick : MonoBehaviour, IClickable
{
    public void Clicked(bool leftMB, bool rightMB)
    {
        Destroy(gameObject);
    }
}