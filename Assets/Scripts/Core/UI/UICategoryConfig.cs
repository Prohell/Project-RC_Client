using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LitJson;

[Serializable]
public class UICategoryConfig
{
    public static readonly string BundleConfigPath = "Assets/Resources/Build/Load_PreLoad/G_Config/Config";
    public static readonly string BundleConfigName = "UICategoryConfig";
    public static readonly string ResConfigPath = "Resources/UIConfig";
    public static readonly string ResConfigName = "UICategoryConfig";

    public string Name;
    public int StartDepth;
    public int EndDepth;
    public string MultiPolicy;
    public int CacheNum;
    public int ShowingMax;

    public static Dictionary<string, UICategoryConfig> LoadConfig()
    {
        try
        {
#if UNITY_EDITOR
            return ScriptableObject.CreateInstance<UICategoryConfigFile>().UICategoryList.ToDictionary(item => item.Name);
#else
            return Configs.UICategoryConfigFile.UICategoryList.ToDictionary(item => item.Name);
#endif
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in load UICategoryConfig, {0}\n{1}", e.Message, e.StackTrace);
            return null;
        }
    }

    public static Dictionary<string, UICategoryConfig> LoadFromRes()
    {
        string resFile = ResConfigPath + "/" + ResConfigName;
        var textAsset = Resources.Load(resFile) as TextAsset;

        if (textAsset == null)
        {
            return null;
        }

        var txt = textAsset.text;
        var json = JsonMapper.ToObject<UICategoryConfigFile>(txt);
        var m_uiCategoryConfigMap = json.UICategoryList.ToDictionary(item => item.Name);

        return m_uiCategoryConfigMap;
    }
}
