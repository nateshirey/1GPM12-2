using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCard : MonoBehaviour
{
    public Material transitionMat;
    public GameEvent screenBlackEvent;
    private string fadeVal = "_CutoffValue";
    public float transitionTime = 1f;
    public float waitTime = 1f;
    public bool fadeOnStart = true;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(transitionMat != null)
        {
            Graphics.Blit(source, destination, transitionMat);
        }
    }

    private void Awake()
    {
        if(transitionMat != null)
        {
            transitionMat.SetFloat(fadeVal, 0);
        }
        if (fadeOnStart)
        {
            StartCoroutine(FadeInCoroutine(transitionTime));
        }
    }

    public void Transition()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(transitionTime));
    }

    public void Transition(float length)
    {
        StartCoroutine(FadeOutCoroutine(length));
    }

    private IEnumerator FadeOutCoroutine(float length)
    {
        transitionMat.SetFloat(fadeVal, 0);

        float timer = 0;
        float val = 0;

        while(timer < length)
        {
            timer += Time.deltaTime;
            val = timer / length;
            transitionMat.SetFloat(fadeVal, val);
            yield return null;
        }

        transitionMat.SetFloat(fadeVal, 1);
        yield return new WaitForSeconds(waitTime * 0.5f);

        if(screenBlackEvent != null)
        {
            screenBlackEvent.Raise();
        }

        StartCoroutine(FadeInCoroutine(length));
        yield break;
    }

    private IEnumerator FadeInCoroutine(float length)
    {
        yield return new WaitForSeconds(waitTime * 0.5f);

        transitionMat.SetFloat(fadeVal, 1);

        float timer = 0;
        float val = 1;

        while (timer < length)
        {
            timer += Time.deltaTime;
            val = length - timer / length;
            transitionMat.SetFloat(fadeVal, val);

            yield return null;
        }
        transitionMat.SetFloat(fadeVal, 0);
        yield break;
    }

}
