using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameEventAction : ActionNode
{
    public GameEvent gameEvent;

    public GameEventAction()
    {
        name = "Game Event";
    }

    public override void Start()
    {
        gameEvent.Raise();
    }
}
