using System;
using UnityEngine;

/// <summary>
/// 平台相关
/// by TT
/// 2016-07-04
/// </summary>
public class PlatformHelper
{
    public static readonly string SDKParentFolder = "Assets/Platform/SDKs";
    public static readonly string SDKModuleParentFolder = "Assets/Platform/SDKModules";
    public static readonly string PluginsFolder = "Assets/Plugins";

    public static readonly string BundlePath = "Assets/BundleAssets";
    public static readonly string ResourcesPath = "Assets/Resources";
    public static readonly string StreamingAssetsPath = "Assets/StreamingAssets";
    public static readonly string PersistentAssetsPath = Application.persistentDataPath + "/ResData";

    public static readonly string AssetListForResources = "AssetsListForResources.txt";

    public static bool IsEnableDebugMode()
    {
        return true;
    }

    public static string GetCurrentTime()
    {
        DateTime now = DateTime.Now;
        return now.ToString("yyyy-MM-dd HH:mm:SS");
    }
}
