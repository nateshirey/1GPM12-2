using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCaptures : MonoBehaviour
{
    public DataTextureDrawer dataTextureDrawer;

    public Camera cam;

    public void OnCaptureImage()
    {
        dataTextureDrawer.ScheduleDraw(cam, 15);
    }
}
