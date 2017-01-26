using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BuildManager
{
    #region Build Logic

    private static string ReleaseFolder = "release";

    [MenuItem("Build/EXE")]
    public static void BuildEXE()
    {
        LogModule.DebugLog("[BUILD] Switch to exe.");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);

        PlayerSettings.companyName = "CYOU";
        PlayerSettings.productName = "RC";

        LogModule.DebugLog("[BUILD] Clear folder.");
        FileUtil.DeleteFileOrDirectory(ReleaseFolder + "/Exe");
        Directory.CreateDirectory(ReleaseFolder + "/Exe");

        LogModule.DebugLog("[BUILD] Build parameters:");
        string version = Environment.GetEnvironmentVariable("Version");
        bool isAssetBundle = Environment.GetEnvironmentVariable("IsAssetBundle").Equals("true");

        if (string.IsNullOrEmpty(version))
        {
            throw new Exception("[BUILD] Null parameter, build aborted.");
        }

        try
        {
            ProcessSDK(null);
            ProcessBundle(isAssetBundle);
        }
        catch (Exception e)
        {
            throw e;
        }

        LogModule.DebugLog("[BUILD] Start building.");

        var res = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes.Select(item => item.path).ToArray(), ReleaseFolder + "/Exe/" + GetAccurateTime() + ".exe", BuildTarget.StandaloneWindows,
            BuildOptions.None);

        //Cancel throw exception here.
        if (res.Length > 0)
        {
            LogModule.DebugLog("[BUILD] BuildPlayer error: " + res);
        }

        if (!Directory.GetFileSystemEntries(ReleaseFolder + "/Exe").Any())
        {
            throw new Exception("[BUILD] Build failure, no building output.");
        }

        LogModule.DebugLog("[BUILD] Build exe ends.");
    }

    [MenuItem("Build/iOS")]
    public static void BuildiOS()
    {
        LogModule.DebugLog("[BUILD] Switch to iOS.");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.iOS);

        PlayerSettings.companyName = "CYOU";
        PlayerSettings.productName = "RC";

        PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
        PlayerSettings.iOS.targetOSVersion = iOSTargetOSVersion.iOS_6_0;
        PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;

        LogModule.DebugLog("[BUILD] Clear folder.");
        FileUtil.DeleteFileOrDirectory(ReleaseFolder + "/iOS");
        Directory.CreateDirectory(ReleaseFolder + "/iOS");

        LogModule.DebugLog("[BUILD] Build parameters:");
        string version = Environment.GetEnvironmentVariable("Version");
        string sdkName = Environment.GetEnvironmentVariable("Channel");
        bool isAssetBundle = Environment.GetEnvironmentVariable("IsAssetBundle").Equals("true");

        if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(sdkName))
        {
            throw new Exception("[BUILD] Null parameter, build aborted.");
        }

        LogModule.DebugLog(version);
        LogModule.DebugLog(sdkName);
        LogModule.DebugLog(isAssetBundle.ToString());

        try
        {
            ProcessSDK(sdkName);
            ProcessBundle(isAssetBundle);
        }
        catch (Exception e)
        {
            throw e;
        }

        LogModule.DebugLog("[BUILD] Start building.");

        var res = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes.Select(item => item.path).ToArray(), ReleaseFolder + "/iOS", BuildTarget.iOS,
    BuildOptions.None);

        //Cancel throw exception here.
        if (res.Length > 0)
        {
            LogModule.DebugLog("[BUILD] BuildPlayer error: " + res);
        }

        if (!Directory.GetFileSystemEntries(ReleaseFolder + "/iOS").Any())
        {
            throw new Exception("[BUILD] Build failure, no building output.");
        }

        LogModule.DebugLog("[BUILD] Build iOS ends.");
    }

    [MenuItem("Build/Android")]
    public static void BuildAndroid()
    {
        LogModule.DebugLog("[BUILD] Switch to Android.");
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        PlayerSettings.companyName = "CYOU";
        PlayerSettings.productName = "RC";

        LogModule.DebugLog("[BUILD] Clear folder.");
        FileUtil.DeleteFileOrDirectory(ReleaseFolder + "/Android");
        Directory.CreateDirectory(ReleaseFolder + "/Android");

        LogModule.DebugLog("[BUILD] Build parameters:");
        string version = Environment.GetEnvironmentVariable("Version");
        string sdkName = Environment.GetEnvironmentVariable("Channel");
        bool isAssetBundle = Environment.GetEnvironmentVariable("IsAssetBundle").Equals("true");

        if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(sdkName))
        {
            throw new Exception("[BUILD] Null parameter, build aborted.");
        }

        LogModule.DebugLog(version);
        LogModule.DebugLog(sdkName);
        LogModule.DebugLog(isAssetBundle.ToString());

        try
        {
            ProcessSDK(sdkName);
            ProcessBundle(isAssetBundle);
        }
        catch (Exception e)
        {
            throw e;
        }

        LogModule.DebugLog("[BUILD] Start building.");

        var res = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes.Select(item => item.path).ToArray(), ReleaseFolder + "/Android/" + GetAccurateTime() + "+" + sdkName + ".apk", BuildTarget.Android,
    BuildOptions.None);

        //Cancel throw exception here.
        if (res.Length > 0)
        {
            LogModule.DebugLog("[BUILD] BuildPlayer error: " + res);
        }

        if (!Directory.GetFileSystemEntries(ReleaseFolder + "/Android").Any())
        {
            throw new Exception("[BUILD] Build failure, no building output.");
        }

        LogModule.DebugLog("[BUILD] Build Android ends.");
    }

    #endregion

    private static string GetAccurateTime()
    {
        var d = DateTime.Now;
        return d.Year + "-" + d.Month + "-" + d.Day + "+" + d.Hour + "-" + d.Minute;
    }

    #region Asset Process

    private static void ProcessSDK(string sdkName)
    {
        switch (EditorUserBuildSettings.activeBuildTarget)
        {
            case BuildTarget.StandaloneWindows:
                break;
            case BuildTarget.Android:
                {
                    //Set sdk.
                    LogModule.DebugLog("[BUILD] Set sdk.");
                    FileUtil.DeleteFileOrDirectory(PlatformHelper.PluginsFolder + "/Android/" + sdkName);
                    Directory.CreateDirectory(PlatformHelper.PluginsFolder + "/Android/" + sdkName);

                    if (!Directory.Exists(PlatformHelper.SDKParentFolder + "/" + sdkName))
                    {
                        throw new Exception("[BUILD] Cannot find sdk: " + sdkName + ", build aborted.");
                    }

                    BuildConfigHelper.CopyAllSDKs(PlatformHelper.SDKParentFolder + "/" + sdkName + "/" + "SDKList.json", PlatformHelper.PluginsFolder + "/Android/" + sdkName);

                    break;
                }
            case BuildTarget.iOS:
                {
                    LogModule.DebugLog("[BUILD] Set sdk.");
                    FileUtil.DeleteFileOrDirectory(PlatformHelper.PluginsFolder + "/iOS/" + sdkName);
                    Directory.CreateDirectory(PlatformHelper.PluginsFolder + "/iOS/" + sdkName);

                    if (!Directory.Exists(PlatformHelper.SDKParentFolder + "/" + sdkName))
                    {
                        throw new Exception("[BUILD] Cannot find sdk: " + sdkName + ", build aborted.");
                    }

                    BuildConfigHelper.CopyAllSDKs(PlatformHelper.SDKParentFolder + "/" + sdkName + "/" + "SDKList.json", PlatformHelper.PluginsFolder + "/iOS/" + sdkName);

                    break;
                }
        }
    }

    private static void ProcessBundle(bool isUsingBundle)
    {
        //assets bundle.
		Configs.clientConfig.isReleaseBundle = isUsingBundle;
        AssetDatabase.SaveAssets();

        if (isUsingBundle)
        {
            ToLuaMenu.CopyLuaFilesToPrepareBundle();
            UIConfigEditor.SOToBundle();

            BundleBuildWindow.Build_Release();
        }
        else
        {
            //ToLuaMenu.CopyLuaFilesToRes();
            ToLuaMenu.CopyLuaFilesToPrepareBundle();
            UIConfigEditor.SOToBundle();

            BundleBuildWindow.Build_Develop();
        }
    }

    #endregion
}