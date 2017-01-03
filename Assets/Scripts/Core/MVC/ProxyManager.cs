using System;
using System.Collections.Generic;
/// <summary>
/// 数据层
/// by TT
/// 2016-07-06
/// </summary>
public class ProxyManager : Singleton<ProxyManager>, IInit, IReset
{
    private Dictionary<Type, IProxy> mProxies = new Dictionary<Type, IProxy>();
    private static object mLock = new object();
    public bool IsInit = false;

    public void Add(IProxy p)
    {
        lock (mLock)
        {
            Type type = p.GetType();
            if (mProxies.ContainsKey(type))
            {
                throw new Exception(type + "already exists in ProxyManager.");
            }
            p.OnInit();
            mProxies.Add(type, p);
        }
    }

    public void Remove(Type t)
    {
        lock (mLock)
        {
            IProxy p;
            if (!mProxies.TryGetValue(t, out p))
            {
                return;
            }
            mProxies.Remove(t);
            p.OnDestroy();
        }
    }

    public T Get<T>() where T : IProxy
    {
        IProxy p;
        if (mProxies.TryGetValue(typeof(T), out p))
        {
            return (T)p;
        }
        return (T)p;
    }

    public object Get(string typeName)
    {
        IProxy p;
        if (mProxies.TryGetValue(Type.GetType(typeName), out p))
        {
            return Convert.ChangeType(p, Type.GetType(typeName));
        }
        return Convert.ChangeType(p, Type.GetType(typeName));
    }

    public void OnReset()
    {
        lock (mLock)
        {
            foreach (IProxy p in mProxies.Values)
            {
                p.OnDestroy();
            }
            mProxies.Clear();
        }
    }

    public void OnInit()
    {
        // add new proxy here.
		Add(new PlayerProxy());
        Add(new CityProxy());
        Add(new BattleProxy());
        //Add(new MapProxy());
        IsInit = true;
    }
}