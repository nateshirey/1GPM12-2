using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSocket : MonoBehaviour
{
    public Transform socketTransform;
    public bool useScale = false;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = socketTransform.position;
        this.transform.rotation = socketTransform.rotation;
        if (useScale)
        {
            this.transform.localScale = socketTransform.localScale;
        }
    }
}
