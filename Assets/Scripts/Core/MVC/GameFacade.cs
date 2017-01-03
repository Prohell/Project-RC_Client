using System;
using UnityEngine;
/// <summary>
/// MVC主管理器
/// by TT
/// 2016-07-06
/// </summary>
public class GameFacade :Singleton<GameFacade>, IInit, IReset, IDestroy
{
    public static MediatorManager Mediator { get { return MediatorManager.GetInstance(); } }
    public static ProxyManager Proxy { get { return ProxyManager.GetInstance(); } }
    public static EventManager Event { get { return EventManager.GetInstance(); } }

    public static void AddMediator<T>(Mediator<T> m, GameObject go) where T : MonoBehaviour
    {
        m.view = go.GetComponent<T>();
        Mediator.Add(m);
    }

    public static void RemoveMediator(Type t)
    {
        Mediator.Remove(t);
    }

    public static T GetMediator<T>() where T : IMediator
    {
        return Mediator.Get<T>();
    }

    public static T GetProxy<T>() where T : IProxy
    {
        return Proxy.Get<T>();
    }

    public static void AddEventListener(string eventId, EventManager.EventHandler handler)
    {
        Event.AddEventListener(eventId, handler);
    }

    public static void RemoveEventListener(string eventId, EventManager.EventHandler handler)
    {
        Event.RemoveEventListener(eventId, handler);
    }

    public static void SendEvent(string eventId, object param = null)
    {
        Event.SendEvent(eventId, param);
    }

    public void OnDestroy()
    {
        OnReset();
    }

    public void OnInit()
    {
        Proxy.OnInit();
    }

    public void OnReset()
    {
        Proxy.OnReset();
        Event.OnReset();
        Mediator.OnReset();
    }
}