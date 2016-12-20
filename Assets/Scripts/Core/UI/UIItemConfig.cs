using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class UIItemConfig : ICloneable
{
    public static readonly string BundleConfigPath = "Assets/Resources/Build/Load_PreLoad/G_Config/Config";
    public static readonly string BundleConfigName = "UIItemConfig";
    public static readonly string ResConfigPath = "Resources/UIConfig";
    public static readonly string ResConfigName = "UIItemConfig";

    public string Type;
    public string Name;
    public string ViewName;
    public string MediatorType;
    public string BundlePath;
    public string BundleName;

    public static Dictionary<string, UIItemConfig> LoadConfig()
    {
        try
        {
#if UNITY_EDITOR
            return new UIItemConfigFile().UIItemList.ToDictionary(item => item.Name);
#else
            return Configs.UIItemConfigFile.UIItemList.ToDictionary(item => item.Name);
#endif
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in load UIItemConfig, {0}\n{1}", e.Message, e.StackTrace);
            return null;
        }
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
