using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("User Parameters")]
    [Range(10, 100)]
    public float lookSpeed = 10f;
    public float lookOffset = 1f;
    public Vector2 rotationLimits;

    [Header("Components")]
    public Transform eyeTarget;
    public Animator toddlerAnim;

    [Header("Debug No Touch")]
    private Vector2 lookInput;
    public Vector2 currentLookAngle;

    public GameEvent addFriend;

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnAddFriend()
    {
        if(addFriend != null)
        {
            addFriend.Raise();
        }
    }

    private void Awake()
    {
        currentLookAngle = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetLookAngle();
        MoveEyeTarget();
    }

    private void SetLookAngle()
    {
        currentLookAngle += lookInput * lookSpeed * Time.deltaTime;
        currentLookAngle.x = Mathf.Clamp(currentLookAngle.x, -rotationLimits.x, rotationLimits.x);
        currentLookAngle.y = Mathf.Clamp(currentLookAngle.y, -rotationLimits.y, rotationLimits.y);
        if(toddlerAnim != null)
        {
            float valueX = currentLookAngle.x / rotationLimits.x;
            toddlerAnim.SetFloat("LookBlend", valueX);
        }
    }

    private void MoveEyeTarget()
    {
        eyeTarget.position = this.transform.position;
        eyeTarget.rotation = this.transform.rotation;

        Quaternion rotationX = Quaternion.AngleAxis(currentLookAngle.x, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(currentLookAngle.y, -Vector3.right);
        Quaternion rotation = rotationX * rotationY;

        eyeTarget.localRotation = rotation;
        eyeTarget.position += eyeTarget.forward * lookOffset;
    }
}