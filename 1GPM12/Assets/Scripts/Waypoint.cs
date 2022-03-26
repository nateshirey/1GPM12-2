using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWayPoint;
    public float pauseTime = 0f;

    private void Awake()
    {
        if(nextWayPoint != null)
        {
            transform.LookAt(nextWayPoint.transform.position, Vector3.up);
        }
    }
}
