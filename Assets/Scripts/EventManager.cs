using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary<string, Action<IEventParam>> eventDictionary;
    private static EventManager eventManager;

    /**
     * Initialize the event dictionay
     * It's a map that binds an event name to a an Action<EventParam> callback function)
     */
    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<IEventParam>>();
        }
    }

    /**
     * Start listening to the event.
     * If the event can't be found in dictionay, adds it.
     */
    public static void StartListening(string eventName, Action<IEventParam> listener)
    {
        Action<IEventParam> e = null;
        if (instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e += listener;
            instance.eventDictionary[eventName] = e;
        }
        else
        {
            e += listener;
            instance.eventDictionary.Add(eventName, e);
        }
    }

    /**
     * Stop listening to the event.
     */
    public static void StopListening(string eventName, Action<IEventParam> listener)
    {
        if (eventManager == null) return;
        Action<IEventParam> e = null;
        if(instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e -= listener;
            instance.eventDictionary[eventName] = e;
        }
    }

    /**
     * Trigger the event with params
     */
    public static void TriggerEvent(string eventName, IEventParam parameters)
    {
        Action<IEventParam> e = null;
        if(instance.eventDictionary.TryGetValue(eventName, out e))
        {
            e.Invoke(parameters);
        }
    }

    /**
     * Singleton instance getter
     */
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in the scene, gros jambon !");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }
}
