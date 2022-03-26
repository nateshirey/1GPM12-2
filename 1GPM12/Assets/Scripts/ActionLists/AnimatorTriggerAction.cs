using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AnimatorTriggerAction : ActionNode
{
    public Animator animator;
    public string triggerName;

    public AnimatorTriggerAction()
    {
        name = "Animator Trigger";
    }
    public AnimatorTriggerAction(string trigger = "")
    {
        name = "Animator Trigger";
        triggerName = trigger;
    }

    public override void Start()
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerName);
        }
    }
}
