﻿using System;
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

    private static void LoadFileByName(string funName)
    {
        int index = funName.LastIndexOf(".");
        if (index >= 0)
        {
            funName = funName.Substring(0, index);
        }
        LoadFile(funName);
    }

    public static object[] CallStaticFunction(string funName, params object[] args)
    {
        LoadFileByName(funName);

        var fun = LuaManager.Instance.LuaState.GetFunction(funName);
        var returnObjects = fun.Call(args);
        fun.Dispose();

        return returnObjects;
    }

    public static object[] CallFunction(LuaTable instance, string funName, params object[] args)
    {
        funName = funName.Replace(":", ".");

        LoadFileByName(funName);

        var fun = LuaManager.Instance.LuaState.GetFunction(funName);
        var returnObjects = fun.Call(instance, args);
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
    public static LuaTable GetComponent(GameObject gb, string name)
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
    public static List<LuaTable> GetComponents(GameObject gb, string name)
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
        return CallStaticFunction("LuaScriptHelper.NewTable")[0] as LuaTable;
    }

    #endregion

    public static void LoadBundleGB(string assetBundleName, string assetName, LuaFunction function)
    {
        CM_Dispatcher.instance.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(assetBundleName, assetName,
            DelegateFactory.Callback_UnityEngine_GameObject(function, null, false) as Callback<GameObject>));
    }

    public static void LoadBundleGB(string assetBundleName, string assetName, LuaTable instance, LuaFunction function)
    {
        CM_Dispatcher.instance.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(assetBundleName, assetName,
            DelegateFactory.Callback_UnityEngine_GameObject(function, instance, true) as Callback<GameObject>));
    }

    public static void AddListener(DelegateUtil.VoidDelegate source, object dest)
    {
        source += (DelegateUtil.VoidDelegate)dest;
    }

    public static void AddListener(DelegateUtil.IntDelegate source, object dest)
    {
        source += (DelegateUtil.IntDelegate)dest;
    }

    public static void AddListener(DelegateUtil.FloatDelegate source, object dest)
    {
        source += (DelegateUtil.FloatDelegate)dest;
    }

    public static void AddListener(DelegateUtil.StringDelegate source, object dest)
    {
        source += (DelegateUtil.StringDelegate)dest;
    }

    public static void AddListener(DelegateUtil.TableDelegate source, object dest)
    {
        source += (DelegateUtil.TableDelegate)dest;
    }
}