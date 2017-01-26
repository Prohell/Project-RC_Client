using UnityEngine;
using System.Collections.Generic;

public class UIItemConfigFile : ScriptableObject
{
    public List<UIItemConfig> UIItemList = new List<UIItemConfig>()
    {
        new UIItemConfig()
        {
            MediatorType = "",
            Name = "MessageBox",
            BundleName = "load_preload$s_ui$messagebox.assetbundle",
            AssetName = "MessageBox",
            Type = "Popup",
            ViewName = "MessageBoxView"
        },
        new UIItemConfig()
        {
            MediatorType = "MainUIMediator",
            Name = "MainUI",
            BundleName = "load_preload$s_ui$mainui.assetbundle",
            AssetName = "MainUI",
            Type = "FullScreen",
            ViewName = "MainUIView"
        },
        new UIItemConfig()
        {
            MediatorType = "WorldMediator",
            Name = "World",
            BundleName = "load_preload$s_ui$worldui.assetbundle",
            AssetName = "WorldUI",
            Type = "FullScreen",
            ViewName = "WorldView"
        },
        new UIItemConfig()
        {
            MediatorType = "MainResourceMediator",
            Name = "MainResource",
            BundleName = "load_preload$s_ui$mainresource.assetbundle",
            AssetName = "MainResource",
            Type = "FullScreen",
            ViewName = "MainResourceView"
        },
        new UIItemConfig()
        {
            MediatorType = "LoginMediator",
            Name = "Login",
            BundleName = "load_preload$s_ui$login.assetbundle",
            AssetName = "Login",
            Type = "Window",
            ViewName = "LoginView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator",
            Name = "LuaTest1",
            BundleName = "load_preload$g_luatest$ui.assetbundle",
            AssetName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator2",
            Name = "LuaTest2",
            BundleName = "load_preload$g_luatest$ui.assetbundle",
            AssetName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator3",
            Name = "LuaTest3",
            BundleName = "load_preload$g_luatest$ui.assetbundle",
            AssetName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "LuaTestMediator4",
            Name = "LuaTest4",
            BundleName = "load_preload$g_luatest$ui.assetbundle",
            AssetName = "LuaTest",
            Type = "Popup",
            ViewName = "LuaTestView"
        },
        new UIItemConfig()
        {
            MediatorType = "CityUIMediator",
            Name = "CityUI",
			BundleName = "load_preload$s_ui$cityui.assetbundle",
            AssetName = "CityUI",
            Type = "FullScreen",
			ViewName = "CityUI"
        },
		new UIItemConfig()
		{
			MediatorType = "TroopTrainUIMediator",
			Name = "TroopTrainUI",
			BundleName = "load_preload$s_ui$trooptrainui.assetbundle",
			AssetName = "TroopTrainUI",
			Type = "Window",
			ViewName = "TroopTrainUI"
		},
		new UIItemConfig()
		{
			MediatorType = "CityInfoUIMediator",
			Name = "CityInfoUI",
			BundleName = "load_preload$s_ui$cityinfoui.assetbundle",
			AssetName = "CityInfoUI",
			Type = "Window",
			ViewName = "CityInfoUI"
		},
		new UIItemConfig()
		{
			MediatorType = "ResearchUIMediator",
			Name = "ResearchUI",
			BundleName = "load_preload$s_ui$researchui.assetbundle",
			AssetName = "ResearchUI",
			Type = "Window",
			ViewName = "ResearchUI"
		},
    };
}
