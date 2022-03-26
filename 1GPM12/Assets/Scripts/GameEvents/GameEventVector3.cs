using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event Vector3", menuName = "Game Events/Game Event Vector3", order = 51)]
public class GameEventVector3 : ScriptableObject
{
    private List<GameEventVector3Listener> listeners = new List<GameEventVector3Listener>();

    public void Raise(Vector3 vector, GameObject gameObject)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(vector, gameObject);
        }
    }

    public void RegisterListener(GameEventVector3Listener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventVector3Listener listener)
    {
        listeners.Remove(listener);
    }
}
