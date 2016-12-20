using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEditor;

public class UIConfigEditor
{
    [MenuItem("ProjectRC/Config/ExportUIJsonToBundle", false, 3)]
    public static void JsonToBundle()
    {
        ExportJson(UICategoryConfig.BundleConfigPath, UICategoryConfig.BundleConfigName, UIItemConfig.BundleConfigPath, UIItemConfig.BundleConfigName);
    }

    [MenuItem("ProjectRC/Config/ExportUIJsonToRes", false, 4)]
    public static void JsonToRes()
    {
        ExportJson(UICategoryConfig.ResConfigPath, UICategoryConfig.ResConfigName, UIItemConfig.ResConfigPath, UIItemConfig.ResConfigName);
    }

    [MenuItem("ProjectRC/Config/ExportUISOToBundle", false, 1)]
    public static void SOToBundle()
    {
        ExportSO(UICategoryConfig.BundleConfigPath, UICategoryConfig.BundleConfigName, UIItemConfig.BundleConfigPath, UIItemConfig.BundleConfigName);
    }

    [MenuItem("ProjectRC/Config/ExportUISOToRes", false, 2)]
    public static void SOToRes()
    {
        ExportSO(UICategoryConfig.ResConfigPath, UICategoryConfig.ResConfigName, UIItemConfig.ResConfigPath, UIItemConfig.ResConfigName);
    }

    private static void ExportJson(string categoryPath, string categoryName, string itemPath, string itemName)
    {
        string categoryFile = categoryPath + "/" + categoryName + ".txt";
        if (File.Exists(categoryFile))
        {
            File.Delete(categoryFile);
        }

        var txt = JsonMapper.ToJson(new UICategoryConfigFile());

        using (StreamWriter sw = new StreamWriter(categoryFile))
        {
            sw.Write(txt);
        }

        Debug.Log("Export UI Category Config done.");

        string itemFile = itemPath + "/" + itemName + ".txt";
        if (File.Exists(itemFile))
        {
            File.Delete(itemFile);
        }

        var txt2 = JsonMapper.ToJson(new UIItemConfigFile());

        using (StreamWriter sw = new StreamWriter(itemFile))
        {
            sw.Write(txt2);
        }

        Debug.Log("Export UI Item Config done.");

        AssetDatabase.Refresh();
    }

    private static void ExportSO(string categoryPath, string categoryName, string itemPath, string itemName)
    {
        DevelopUtility.CreateScriptableObject<UICategoryConfigFile>(categoryPath, categoryName);
        Debug.Log("Export UI Category Config done.");
        DevelopUtility.CreateScriptableObject<UIItemConfigFile>(itemPath, itemName);
        Debug.Log("Export UI Item Config done.");
    }
}
