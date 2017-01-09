using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;

public class LuaHelper
{
    #region Lua Communication

    /// <summary>
    /// Load lua file to memory, will not load same file twice.
    /// </summary>
    /// <param name="path"></param>
    public static void LoadFile(string path)
    {
        LuaManager.Instance.LuaState.Require(path);
    }

    public static void LoadFileByFuncName(string funName)
    {
        int index = funName.LastIndexOf(".");
        if (index >= 0)
        {
            funName = funName.Substring(0, index);
        }
        LoadFile(funName);
    }

    public static object[] CallFunction(string funName, params object[] args)
    {
        LoadFileByFuncName(funName);

        var fun = LuaManager.Instance.LuaState.GetFunction(funName);
        var returnObjects = fun.Call(args);
        fun.Dispose();

        return returnObjects;
    }

    public static object[] CallFunctionWithSelf(LuaTable self, string funName, params object[] args)
    {
        funName = funName.Replace(":", ".");

        LoadFileByFuncName(funName);

        var fun = LuaManager.Instance.LuaState.GetFunction(funName);
        var returnObjects = fun.Call(self, args);
        fun.Dispose();

        return returnObjects;
    }

    public static GameObject LoadGB(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    /// <summary>
    /// [WARNING] This cost too much performance, use it after think.
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    public static Type GetTypeInAll(string p_name)
    {
        foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type t = currentassembly.GetType(p_name, false, false);
            if (t != null) { return t; }
        }

        return null;
    }

    public static Type GetTypeInUnityEngine(string p_name)
    {
        return Type.GetType(string.Format("UnityEngine.{0}, UnityEngine", p_name));
    }

    public static Type GetTypeInNGUI(string p_name)
    {
        return Type.GetType(string.Format("{0}, Assembly-CSharp-firstpass", p_name));
    }

    /// <summary>
    /// Return the first lua table find in gameobject with specific name.
    /// </summary>
    /// <param name="gb"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static LuaTable GetLuaComponent(GameObject gb, string name)
    {
        LuaOutlet[] facade = gb.GetComponents<LuaOutlet>();
        if (facade.Length == 0)
        {
            return null;
        }
        else
        {
            foreach (var item in facade)
            {
                if (item.m_LuaName == name)
                {
                    return item.m_LuaTable;
                }
            }

            return null;
        }
    }

    public static LuaOutlet GetOutletComponent(GameObject gb, string name)
    {
        LuaOutlet[] facade = gb.GetComponents<LuaOutlet>();
        if (facade.Length == 0)
        {
            return null;
        }
        else
        {
            foreach (var item in facade)
            {
                if (item.m_LuaName == name)
                {
                    return item;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Return the lua table list find in gameobject with specific name.
    /// </summary>
    /// <param name="gb"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static List<LuaTable> GetLuaComponents(GameObject gb, string name)
    {
        LuaOutlet[] facade = gb.GetComponents<LuaOutlet>();
        if (facade.Length == 0)
        {
            return null;
        }
        else
        {
            List<LuaTable> returnList = new List<LuaTable>();

            foreach (var item in facade)
            {
                if (item.m_LuaName == name)
                {
                    returnList.Add(item.m_LuaTable);
                }
            }

            return returnList.Count == 0 ? null : returnList;
        }
    }

    #endregion

    #region Lua Data Structure

    public static LuaTable ConvertToTable<T>(List<T> source)
    {
        var table = GetNewTable();
        for (int i = 0; i < source.Count; i++)
        {
            table[i] = source[i];
        }

        return table;
    }

    public static LuaTable GetNewTable()
    {
        return CallFunction("LuaScriptHelper.NewTable")[0] as LuaTable;
    }

    #endregion

    public static void LoadBundleGB(string assetBundleName, string assetName, Action<GameObject> OnLoadDone)
    {
        Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(assetBundleName, assetName, OnLoadDone));
    }

	public static void LoadBundleGB(string assetBundleName, string assetName, LuaTable instance, Action<GameObject> OnLoadDone)
    {
        Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(assetBundleName, assetName, OnLoadDone));
    }

    public static void AddListener(DelegateHelper.VoidDelegate source, object dest)
    {
        source += (DelegateHelper.VoidDelegate)dest;
    }

    public static void AddListener(DelegateHelper.IntDelegate source, object dest)
    {
        source += (DelegateHelper.IntDelegate)dest;
    }

    public static void AddListener(DelegateHelper.FloatDelegate source, object dest)
    {
        source += (DelegateHelper.FloatDelegate)dest;
    }

    public static void AddListener(DelegateHelper.StringDelegate source, object dest)
    {
        source += (DelegateHelper.StringDelegate)dest;
    }

    public static void AddListener(DelegateHelper.TableDelegate source, object dest)
    {
        source += (DelegateHelper.TableDelegate)dest;
    }
}
