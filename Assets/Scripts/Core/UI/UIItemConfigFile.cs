using UnityEngine;
using System.Collections.Generic;

public class UIItemConfigFile : ScriptableObject
{
    public List<UIItemConfig> UIItemList = new List<UIItemConfig>()
    {
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator",
            Name = "LuaTest1",
            BundlePath = "load_preload$g_luatest$ui.assetbundle",
            BundleName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator2",
            Name = "LuaTest2",
            BundlePath = "load_preload$g_luatest$ui.assetbundle",
            BundleName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator3",
            Name = "LuaTest3",
            BundlePath = "load_preload$g_luatest$ui.assetbundle",
            BundleName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator4",
            Name = "LuaTest4",
            BundlePath = "load_preload$g_luatest$ui.assetbundle",
            BundleName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
		new UIItemConfig()
		{
			MediatorType = "CityUIMediator",
			Name = "CityView",
			BundlePath = "load_preload$g_luatest$ui.assetbundle",
			BundleName = "LuaTest",
			Type = "Window",
			ViewName = "CityLogic"
		},
		new UIItemConfig()
		{
			MediatorType = "",
			Name = "MessageBox",
			BundlePath = "load_preload$s_ui$messagebox.assetbundle",
			BundleName = "MessageBox",
			Type = "Popup",
			ViewName = "MessageBoxView"
		}
    };
}
