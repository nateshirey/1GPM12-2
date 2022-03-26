using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventVector3Listener : MonoBehaviour
{
    [SerializeField] private GameEventVector3 gameEventVector3;
    [SerializeField] private UnityEvent<float, float, float, GameObject> response;

    private void OnEnable()
    {
        gameEventVector3.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEventVector3.UnregisterListener(this);
    }

    public void OnEventRaised(Vector3 vector, GameObject gameObject)
    {
        response.Invoke(vector.x, vector.y, vector.z, gameObject);
    }
}
