using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavePhotosAction : ActionNode
{
    public SavePhotos savePhotos;

    public SavePhotosAction()
    {
        name = "Save Photos Action";
    }

    public override void Start()
    {
        if(savePhotos != null)
        {
            savePhotos.Save();
        }
    }
}
