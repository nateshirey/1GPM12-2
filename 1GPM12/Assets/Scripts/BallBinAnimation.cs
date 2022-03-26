using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBinAnimation : MonoBehaviour
{
    public List<GameObject> balls = new List<GameObject>();
    public List<float> targets;
    private List<float> startPositions;
    public float frequency = 5f;
    public float maxTarget = 0.2f;
    public AnimationCurve moveCurve;
    public float animLength;

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<float>();
        NewTargets();

        startPositions = new List<float>();
        for (int i = 0; i < balls.Count; i++)
        {
            startPositions.Add(balls[i].transform.position.y);
        }

        StartCoroutine(Wait());

    }

    private IEnumerator Wait()
    {
        float timer = 0f;
        while(timer < frequency)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        NewTargets();
        StartCoroutine(Move());
        yield break;
    }

    private IEnumerator Move()
    {
        float timer = 0f;
        while(timer < animLength)
        {
            timer += Time.deltaTime;
            SetAnimPos(timer / animLength);
            yield return null;
        }
        StartCoroutine(Wait());
        yield break;
    }

    private void SetAnimPos(float animTime)
    {
        for (int i = 0; i < balls.Count; i++)
        {
            Vector3 temp = balls[i].transform.position;
            temp.y = Mathf.Lerp(startPositions[i], startPositions[i] + targets[i], moveCurve.Evaluate(animTime));
            balls[i].transform.position = temp;
        }
    }

    void NewTargets()
    {
        targets.Clear();
        for (int i = 0; i < balls.Count; i++)
        {
            targets.Add(Random.Range(0, maxTarget));
        }
    }
}
