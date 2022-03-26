using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game EventInt", menuName = "Game Events/Game Event Int", order = 51)]
public class GameEventInt : ScriptableObject
{
    private List<GameEventIntListener> listeners = new List<GameEventIntListener>();

    public void Raise(int x)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(x);
        }
    }

    public void RegisterListener(GameEventIntListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventIntListener listener)
    {
        listeners.Remove(listener);
    }
}
