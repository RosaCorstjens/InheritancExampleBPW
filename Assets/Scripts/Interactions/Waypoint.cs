using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.waypoints.Add(gameObject);       
    }

    private void OnDestroy()
    {
        GameManager.Instance.waypoints.Remove(gameObject);
    }
}
