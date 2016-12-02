using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class BuildConfigHelper
{
    public class SDKConfig
    {
        public string Platform;
        public string[] SDKList;
    }

    public static void CopyAllSDKs(string p_sdkConfigFile,string p_pluginsFolder)
    {
        var config = File.ReadAllText(p_sdkConfigFile);
        
        var sdkConfig = JsonUtility.FromJson<SDKConfig>(config);

        foreach (var item in sdkConfig.SDKList)
        {
            if (!Directory.Exists(PlatformHelper.SDKModuleParentFolder + "/" + item))
            {
                throw new Exception("[BUILD] Cannot find sdk module: " + item + ", build aborted.");
            }

            Utils.CopyAll(PlatformHelper.SDKModuleParentFolder + "/" + item, PlatformHelper.PluginsFolder + "/Android");
        }
    }
}
