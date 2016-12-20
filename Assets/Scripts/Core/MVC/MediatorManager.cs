using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mediator管理者
/// by TT
/// 2016-07-07
/// </summary>
public class MediatorManager : Singleton<MediatorManager>, IReset, IDestroy
{
    private Dictionary<Type, IMediator> mMediators = new Dictionary<Type, IMediator>();
    private object mLock = new object();

    public void Add(IMediator m)
    {
        lock(mLock)
        {
            Type t = m.GetType();
            if(!mMediators.ContainsKey(t))
            {
                mMediators.Add(t, m);
                m.OnInit();
            }
        }
    }

    public void Remove(Type t)
    {
        lock(mLock)
        {
            IMediator m;
            if(mMediators.TryGetValue(t, out m))
            {
                m.OnDestroy();
                mMediators.Remove(t);
            }
        }
    }

    public T Get<T>() where T : IMediator
    {
        IMediator m;
        if (mMediators.TryGetValue(typeof(T), out m))
        {
            return (T)m;
        }
        return (T)m;
    }

    public object Get(string name)
    {
        IMediator m;
        if (mMediators.TryGetValue(Type.GetType(name), out m))
        {
            return Convert.ChangeType(m, Type.GetType(name));
        }
        return Convert.ChangeType(m, Type.GetType(name));
    }

    public void OnDestroy()
    {
        OnReset();
    }

    public void OnReset()
    {
        lock (mLock)
        {
            foreach (IMediator m in mMediators.Values)
            {
                m.OnDestroy();
            }
            mMediators.Clear();
        }
    }
}