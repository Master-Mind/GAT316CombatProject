using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The global event system
public static class EventSystem
{
    public delegate void EventReciever<T>(T e)
        where T : Assets.Scripts.EventSystem.IEvent;
    
    private static Dictionary<int, List<Delegate>> _subscribers;

    //constructs the whole system
    static EventSystem()
    {
        _subscribers = new Dictionary<int, List<Delegate>>();
    }

    //Dispatch the message with a specific dispatcher, can be used to dispatch messages
    //to the components of a gameobject
    public static void Dispatch<EventType>(EventDispatcher dispatcher, EventType e)
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        var subs = _subscribers[typeof(EventType).GetHashCode()];
        DispatchInternal(e, ref subs);
    }

    //Dispatch the given message globally
    public static void Dispatch<EventType>(EventType e)
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        List<Delegate> subs = _subscribers[typeof(EventType).GetHashCode()];
        DispatchInternal(e, ref subs);
    }

    //private no touchy
    private static void DispatchInternal<EventType>(EventType e, ref List<Delegate> subs)
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        List<int> markedForDeletion = new List<int>();
        for (int i = 0; i < subs.Count; i++)
        {
            if (subs[i] == null)
            {
                markedForDeletion.Add(i);
            }
            else
            {
                ((EventReciever<EventType>)subs[i])(e);
            }
        }

        foreach (int i in markedForDeletion)
        {
            subs.RemoveAt(i);
        }
    }

    //Subscribe to the event type. The given type must inherit from IEvent. If the event has not
    //been registered, it will register now automatically
    public static void Subscribe<EventType>(EventReciever<EventType> eventRecieverReciever)
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        if (!_subscribers.ContainsKey(typeof(EventType).GetHashCode()))
        {
            RegisterEvent<EventType>();
        }
        _subscribers[typeof(EventType).GetHashCode()].Add(eventRecieverReciever);
    }

    //Subscribe to the even using the given dispatcher.
    public static void Subscribe<EventType>(EventDispatcher dispatcher, EventReciever<EventType> eventRecieverReciever)
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        if (!_subscribers.ContainsKey(typeof(EventType).GetHashCode()))
        {
            RegisterEvent<EventType>();
        }
        dispatcher.Subscribers[typeof(EventType).GetHashCode()].Add(eventRecieverReciever);
    }

    //Register the given event type
    public static void RegisterEvent<EventType>()
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        _subscribers[typeof(EventType).GetHashCode()] = new List<Delegate>();
    }

    //Register the given event type only if it hasn't already been registered.
    public static void TryRegisterEvent<EventType>()
        where EventType : Assets.Scripts.EventSystem.IEvent
    {
        if (!_subscribers.ContainsKey(typeof(EventType).GetHashCode()))
        {
            _subscribers[typeof(EventType).GetHashCode()] = new List<Delegate>();
        }
    }
}
