using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventIntListener : MonoBehaviour
{
    [SerializeField] private GameEventInt gameEventInt;
    [SerializeField] private UnityEvent<int> response;

    private void OnEnable()
    {
        gameEventInt.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEventInt.UnregisterListener(this);
    }

    public void OnEventRaised(int x)
    {
        response.Invoke(x);
    }
}
