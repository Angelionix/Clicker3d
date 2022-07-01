using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameEvent", menuName = "SO/GameEvent", order = 52)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Register(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnRegister(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void EventAction(GameObject go)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnOccurs(go);
        }
    }
}
