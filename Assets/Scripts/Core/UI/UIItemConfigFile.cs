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
            Type = "Window",
			ViewName = "CityUI"
        },
		new UIItemConfig()
		{
			MediatorType = "TrainingUIMediator",
			Name = "TrainingUI",
			BundleName = "load_preload$s_ui$trainingui.assetbundle",
			AssetName = "TrainingUI",
			Type = "Window",
			ViewName = "TrainingUI"
		},
		new UIItemConfig()
		{
			MediatorType = "DetailsUIMediator",
			Name = "DetailsUI",
			BundleName = "load_preload$s_ui$detailsui.assetbundle",
			AssetName = "DetailsUI",
			Type = "Window",
			ViewName = "DetailsUI"
		}
    };
}
