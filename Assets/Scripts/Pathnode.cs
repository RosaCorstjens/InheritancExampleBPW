using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.pathNodes.Add(this.gameObject);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance.pathNodes.Contains(this.gameObject))
            GameManager.Instance.pathNodes.Remove(this.gameObject);
    }
}
