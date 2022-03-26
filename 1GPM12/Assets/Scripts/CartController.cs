using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    [Range(0f, 10f)]
    public float cartSpeed = 1f;
    [Range(1, 10)]
    public float turnRate = 1f;
    public Waypoint startWayPoint;

    private Waypoint previousWaypoint;
    private Waypoint nextWaypoint;

    private Coroutine turnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        previousWaypoint = startWayPoint;
        nextWaypoint = startWayPoint.nextWayPoint;

        this.transform.position = previousWaypoint.transform.position;
    }

    private void SetNewWaypoint()
    {
        if(nextWaypoint.nextWayPoint != null)
        {
            previousWaypoint = nextWaypoint;
            nextWaypoint = previousWaypoint.nextWayPoint;

            if(turnCoroutine != null)
            {
                StopCoroutine(turnCoroutine);
            }
            turnCoroutine = StartCoroutine(RotateCartCoroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToWaypointWaypoint();
    }

    private void MoveToWaypointWaypoint()
    {
        float currentDist = Vector3.Distance(nextWaypoint.transform.position, this.transform.position);

        //we made it to the next waypoint
        if(currentDist < 0.01f)
        {
            SetNewWaypoint();
        }

        else
        {
            this.transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, cartSpeed * Time.deltaTime);
        }
    }

    private IEnumerator RotateCartCoroutine()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = previousWaypoint.transform.rotation;

        float timer = 0;
        while(timer < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer);
            timer += turnRate * Time.deltaTime;
            yield return null;
        }

        yield break;
    }

}
