using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrigger : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    public float moveSpeed = 1;
    public Vector3 triggerPosition;
    public Vector3 bounds = Vector3.one;

    public LayerMask validColliders;

    public ActionExecutor actionExecutor;
    private bool actionsStarted = false;

    private Collider[] cols;
    private bool trigger;

    private void Awake()
    {
        startPosition = this.transform.position;
    }

    private void Update()
    {
        cols = Physics.OverlapBox(triggerPosition, bounds / 2, Quaternion.identity, validColliders);
        trigger = cols.Length > 0 ? true : false;
    }

    private void FixedUpdate()
    {
        if (trigger)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
            StartActions();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void StartActions()
    {
        if (!actionsStarted && actionExecutor != null)
        {
            actionsStarted = true;
            actionExecutor.Play();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(triggerPosition, bounds);
    }
}
