#region Copyright (c) 2015 KEngine / Kelly <http://github.com/mr-kelly>, All rights reserved.

// KEngine - Toolset and framework for Unity3D
// ===================================
// 
// Filename: SettingModuleEditor.cs
// Date:     2015/12/03
// Author:  Kelly
// Email: 23110388@qq.com
// Github: https://github.com/mr-kelly/KEngine
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 3.0 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library.

#endregion

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class LuaOutlet : MonoBehaviour
{
    /// Outlet info, serialize
    /// </summary>
    [System.Serializable]
    public class OutletInfo
    {
        /// <summary>
        /// Lua Property Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Component type 's full p_name (with namespace)
        /// </summary>
        public string ComponentType;

        /// <summary>
        /// UI Control Object
        /// </summary>
        public UnityEngine.Object Object;
    }
    /// <summary>
    /// Serialized outlet infos
    /// </summary>
    public List<OutletInfo> OutletInfos = new List<OutletInfo>();

    public LuaTable m_LuaTable { get; private set; }

    public string m_LuaName;

    private void BindLuaInternal()
    {
        LuaHelper.CallStaticFunction(m_LuaName + ".BindLua", this);
    }

    public void BindLua(string p_name)
    {
        m_LuaName = p_name;
        BindLuaInternal();
    }

    /// <summary>
    /// Do not call this in cs.
    /// </summary>
    /// <param name="table"></param>
    public void BindFromLua(LuaTable table)
    {
        m_LuaTable = table;

        foreach (var item in OutletInfos)
        {
            m_LuaTable[item.Name] = ((GameObject)item.Object).GetComponent(item.ComponentType);
        }
    }

    void OnDestroy()
    {
        if (CanExeLuaMono && m_LuaTable.Exist("OnDestroy") && m_LuaTable["Start"].GetType() == typeof(LuaFunction))
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":OnDestroy");
        }
    }

    void OnEnable()
    {
        if (CanExeLuaEnable)
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":OnEnable");
        }
    }

    void OnDisable()
    {
        if (CanExeLuaDisable)
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":OnDisable");
        }
    }

    void Update()
    {
        if (CanExeLuaUpdate)
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":Update");
        }
    }

    void Start()
    {
        if (CanExeLuaMono && m_LuaTable.Exist("Start") && m_LuaTable["Start"].GetType() == typeof(LuaFunction))
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":Start");
        }
    }

    private bool CanExeLuaMono = false;
    private bool CanExeLuaUpdate = false;
    private bool CanExeLuaEnable = false;
    private bool CanExeLuaDisable = false;

    void Awake()
    {
        if (!string.IsNullOrEmpty(m_LuaName))
        {
            BindLuaInternal();
        }

        CanExeLuaMono = !string.IsNullOrEmpty(m_LuaName) && m_LuaTable != null;
        CanExeLuaUpdate = CanExeLuaMono && m_LuaTable.Exist("Update") && m_LuaTable["Update"].GetType() == typeof(LuaFunction);
        CanExeLuaEnable = CanExeLuaMono && m_LuaTable.Exist("OnEnable") && m_LuaTable["OnEnable"].GetType() == typeof(LuaFunction);
        CanExeLuaDisable = CanExeLuaMono && m_LuaTable.Exist("OnDisable") && m_LuaTable["OnDisable"].GetType() == typeof(LuaFunction);

        if (CanExeLuaMono && m_LuaTable.Exist("Awake") && m_LuaTable["Awake"].GetType() == typeof(LuaFunction))
        {
            LuaHelper.CallFunction(m_LuaTable, m_LuaName + ":Awake");
        }
    }
}