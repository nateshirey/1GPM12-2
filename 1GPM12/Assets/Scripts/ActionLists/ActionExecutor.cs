using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionExecutor : MonoBehaviour
{
    public ActionList actionList;
    private Vector3 startPosition;
    private Vector3 startRotation;
    private Vector3 startScale;

    private int actionIndex = 0;
    private float timer = 0;

    [SerializeField] private bool loop;
    [SerializeField] private bool playOnStart = true;
    private bool isPlaying = false;

    private void Awake()
    {
        actionIndex = 0;
        timer = 0;

        startPosition = transform.localPosition;
        startRotation = transform.localRotation.eulerAngles;
        startScale = transform.localScale;
    }

    private void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (actionIndex < actionList.Actions.Count)
            {
                timer += Time.deltaTime;
                var currentNode = actionList.Actions[actionIndex];
                //go to next node
                if (timer >= currentNode.time)
                {
                    actionIndex++;
                    timer = 0f;

                    startPosition = transform.localPosition;
                    startRotation = transform.localRotation.eulerAngles;
                    startScale = transform.localScale;

                    //finish up the stuff on this node and start the next one
                    currentNode.End();
                    if (actionIndex < actionList.Actions.Count)
                    {
                        actionList.Actions[actionIndex].Start();
                    }
                }
                //do node stuff
                else
                {
                    currentNode.Act(timer);
                }
            }
            //when we have gotten through each action, either restart or stop
            else
            {
                if (loop)
                {
                    actionIndex = 0;
                }
                else
                {
                    isPlaying = false;
                }
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }
}
