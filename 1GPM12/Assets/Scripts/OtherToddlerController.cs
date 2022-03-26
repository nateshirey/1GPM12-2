using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherToddlerController : MonoBehaviour
{
    public Animator anim;
    public float blend;
    public Transform playerTransform;

    private void Update()
    {
        Vector3 dir = playerTransform.position - this.transform.position;
        blend = Vector3.Dot(dir.normalized, this.transform.right);
        anim.SetFloat("LookDir", blend);
    }
}
