﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class BagWindowWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(BagWindow), typeof(System.Object));
		L.RegFunction("Init", Init);
		L.RegFunction("New", _CreateBagWindow);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("m_BagWindowObject", get_m_BagWindowObject, set_m_BagWindowObject);
		L.RegVar("m_BagModel", get_m_BagModel, set_m_BagModel);
		L.RegVar("m_BagController", get_m_BagController, set_m_BagController);
		L.RegVar("m_BagView", get_m_BagView, set_m_BagView);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBagWindow(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				BagWindow obj = new BagWindow();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: BagWindow.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			BagWindow obj = (BagWindow)ToLua.CheckObject(L, 1, typeof(BagWindow));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_BagWindowObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			UnityEngine.GameObject ret = obj.m_BagWindowObject;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagWindowObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_BagModel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			BagModel ret = obj.m_BagModel;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagModel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_BagController(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			LuaInterface.LuaTable ret = obj.m_BagController;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagController on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_BagView(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			LuaInterface.LuaTable ret = obj.m_BagView;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagView on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_BagWindowObject(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			UnityEngine.GameObject arg0 = (UnityEngine.GameObject)ToLua.CheckUnityObject(L, 2, typeof(UnityEngine.GameObject));
			obj.m_BagWindowObject = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagWindowObject on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_BagModel(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			BagModel arg0 = (BagModel)ToLua.CheckObject(L, 2, typeof(BagModel));
			obj.m_BagModel = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagModel on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_BagController(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			LuaTable arg0 = ToLua.CheckLuaTable(L, 2);
			obj.m_BagController = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagController on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_BagView(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			BagWindow obj = (BagWindow)o;
			LuaTable arg0 = ToLua.CheckLuaTable(L, 2);
			obj.m_BagView = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index m_BagView on a nil value" : e.Message);
		}
	}
}

