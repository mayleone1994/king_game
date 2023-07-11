using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KingGame;

public class EventsMediator : MonoBehaviour
{
    private Dictionary<EventKey, List<Action<object>>> _eventHandlers;

    private void Awake()
    {
        InitDict();
    }

    private void InitDict()
    {
        _eventHandlers = new();
    }

    public void Subscribe(EventKey key, Action<object> handler)
    {
        if(!_eventHandlers.ContainsKey(key))
        {
            _eventHandlers[key] = new List<Action<object>>();
        }

        _eventHandlers[key].Add(handler);
    }

    public void Unsubscribe(EventKey key, Action<object> handler) 
    {
        if (!_eventHandlers.ContainsKey(key))
        {
            Debug.LogWarning($"An error occurred while trying to unsubscribe the required event, due {key} event key was not found");
            return;
        }

        _eventHandlers[key].Remove(handler);
    }

    public void InvokeEvent(EventKey key, object arg)
    {
        if(!_eventHandlers.ContainsKey(key)) 
        {
            Debug.LogWarning($"An error occurred while trying to invoke the required event, due {key} event key was not found");
            return;
        }

        foreach(var handler in _eventHandlers[key]) 
        {
            handler?.Invoke(arg);
        }
    }
}
