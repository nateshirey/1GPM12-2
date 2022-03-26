using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


//This is a base class for running through lists in editor.
//Create actions in a new script and inherit from ActionNode
//Removing an actionNode derived class from the project will break
//any lists that used that actionType class
public class ActionList : MonoBehaviour
{
    [SerializeReference]
    public List<ActionNode> Actions;
}

[Serializable]
public abstract class ActionNode
{
    public string name;
    public float time = 1f;

    public virtual void Start()
    {

    }
    public virtual void Act(float timer)
    {

    }
    public virtual void End()
    {

    }
}