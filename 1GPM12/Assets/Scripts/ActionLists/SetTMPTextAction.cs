using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class SetTMPTextAction : ActionNode
{
    public TMP_Text tMPText;
    public string newText;

    public SetTMPTextAction()
    {
        name = "Set TMP Text";
    }

    public override void Start()
    {
        tMPText.text = newText;
    }
}
