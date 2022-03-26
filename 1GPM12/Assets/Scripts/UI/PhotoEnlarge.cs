using System.Collections;
using UnityEngine;

public class PhotoEnlarge : MonoBehaviour
{
    public GameObject enlargeImage;
    private RectTransform rectTransform;
    public Transform enlargeParent;
    public Transform defaultParent;
    public Vector2 enlargeSize;
    public float enlargeSpeed = 1f;


    private void Awake()
    {
        defaultParent = this.transform;
        rectTransform = enlargeImage.GetComponent<RectTransform>();
    }

    public void Enlarge()
    {
        enlargeImage.SetActive(true);
        StopAllCoroutines();
        rectTransform.SetParent(enlargeParent, true);
        StartCoroutine(OffsetCoroutine(false));
    }

    public void Shrink()
    {
        StopAllCoroutines();
        rectTransform.SetParent(defaultParent, true);
        StartCoroutine(OffsetCoroutine(true));
    }

    private IEnumerator OffsetCoroutine(bool shrink)
    {
        float timer = 0f;
        Vector2 startMin = rectTransform.offsetMin;
        Vector2 startMax = rectTransform.offsetMax;

        while(timer < 1)
        {
            timer += Time.deltaTime * enlargeSpeed;
            timer = Mathf.Clamp01(timer);
            rectTransform.offsetMin = Vector2.Lerp(startMin, Vector2.zero, timer);
            rectTransform.offsetMax = Vector2.Lerp(startMax, Vector2.zero, timer);
            yield return null;
        }

        if (shrink)
        {
            enlargeImage.SetActive(false);
        }

        yield break;
    }
}
