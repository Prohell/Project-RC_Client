using UnityEngine;
using System.Collections.Generic;

public class GameObjectPool : Singleton<GameObjectPool>
{
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
    private GameObject mRoot;
    public GameObject root
    {
        set
        {
            mRoot = value;
            mRoot.SetActive(false);
        }
    }

    private Queue<GameObject> GetPool(string prefabName)
    {
        Queue<GameObject> ret = null;
        if (!poolDict.ContainsKey(prefabName))
        {
            ret = new Queue<GameObject>();
            poolDict.Add(prefabName, ret);
        }
        else
        {
            ret = poolDict[prefabName];
        }
        return ret;
    }

    public GameObject SpawnGo(GameObject prefab)
    {
        Queue<GameObject> pool = GetPool(prefab.name);
        GameObject ret = null;
        if (0 == pool.Count)
        {
            ret = Object.Instantiate(prefab) as GameObject;
        }
        else
        {
            ret = pool.Dequeue();
        }
        return ret;
    }

    public void RecycleGo(string name, GameObject go)
    {
        if (go == null) return;
        Queue<GameObject> pool = GetPool(name);
        pool.Enqueue(go);
        go.transform.parent = mRoot.transform;
    }

    public GameObject WithdrawGo(string name)
    {
        Queue<GameObject> pool = GetPool(name);
        if (0 == pool.Count)
        {
            return null;
        }
        else
        {
            var ret = pool.Dequeue();
            ret.transform.parent = null;
            return ret;
        }
    }

    public void ClearPool(string name)
    {
        if (!poolDict.ContainsKey(name))
        {
            return;
        }
        GetPool(name).Clear();
    }

    public void ClearAll()
    {
        foreach (KeyValuePair<string, Queue<GameObject>> kvp in poolDict)
        {
            Queue<GameObject> q = kvp.Value;
            while (q.Count > 0)
            {
                GameObject go = q.Dequeue();
                NGUITools.Destroy(go);
            }
        }
        poolDict.Clear();
    }
}

