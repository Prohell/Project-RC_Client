using System;
using System.Collections.Generic;
/// <summary>
/// 事件管理器
/// by TT
/// 2016-07-06
/// </summary>
public class EventManager : Singleton<EventManager>, IReset
{
    public delegate void EventHandler(object param);
    private static Dictionary<string, List<EventHandler>> eventHandlers = new Dictionary<string, List<EventHandler>>();
    private static object mLock = new object();

    public void OnReset()
    {
        lock(mLock)
        {
            eventHandlers.Clear();
        }
    }

    public void AddEventListener(string eventId, EventHandler handler)
    {
        if(handler == null)
        {
            throw new Exception("Event handler cannot be null");
        }
        lock(mLock)
        {
            if (!eventHandlers.ContainsKey(eventId))
            {
                eventHandlers.Add(eventId, new List<EventHandler>());
            }
            eventHandlers[eventId].Add(handler);
        }
    }

    public void RemoveEventListener(string eventId, EventHandler handler)
    {
        if (handler == null)
        {
            throw new Exception("Event handler cannot be null");
        }
        lock (mLock)
        {
            if (eventHandlers.ContainsKey(eventId) && eventHandlers[eventId].Contains(handler))
            {
                eventHandlers[eventId].Remove(handler);
            }
        }
    }

    public void SendEvent(string eventId, object param = null)
    {
        lock(mLock)
        {
            if(eventHandlers.ContainsKey(eventId) && eventHandlers[eventId].Count > 0)
            {
                for(int i = 0, len = eventHandlers[eventId].Count; i < len; i++)
                {
                    eventHandlers[eventId][i].Invoke(param);
                }
            }
        }
    }
}
