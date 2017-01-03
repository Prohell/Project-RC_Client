﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class SceneIdWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(SceneId));
		L.RegVar("Loading", get_Loading, null);
		L.RegVar("Start", get_Start, null);
		L.RegVar("MapEditor", get_MapEditor, null);
		L.RegVar("Map", get_Map, null);
		L.RegVar("Castle", get_Castle, null);
		L.RegVar("LuaTest", get_LuaTest, null);
		L.RegVar("BattleTest", get_BattleTest, null);
		L.RegVar("NetworkTest", get_NetworkTest, null);
		L.RegFunction("IntToEnum", IntToEnum);
		L.EndEnum();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Loading(IntPtr L)
	{
		ToLua.Push(L, SceneId.Loading);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Start(IntPtr L)
	{
		ToLua.Push(L, SceneId.Start);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MapEditor(IntPtr L)
	{
		ToLua.Push(L, SceneId.MapEditor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Map(IntPtr L)
	{
		ToLua.Push(L, SceneId.Map);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Castle(IntPtr L)
	{
		ToLua.Push(L, SceneId.Castle);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaTest(IntPtr L)
	{
		ToLua.Push(L, SceneId.LuaTest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_BattleTest(IntPtr L)
	{
		ToLua.Push(L, SceneId.BattleTest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_NetworkTest(IntPtr L)
	{
		ToLua.Push(L, SceneId.NetworkTest);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		SceneId o = (SceneId)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}
