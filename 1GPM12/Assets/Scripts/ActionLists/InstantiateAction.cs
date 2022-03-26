using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InstantiateAction : ActionNode
{
    public GameObject gameObject;
    public Transform parent;
    public Vector3 position;
    public Vector3 rotation;
    public InstantiateAction()
    {
        name = "Instantiate";
    }

    public override void Start()
    {
        GameObject.Instantiate(gameObject,position, Quaternion.Euler(rotation.x, rotation.y, rotation.z), parent);
    }
}
