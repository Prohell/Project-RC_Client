using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Net;

public class DevelopUtility {

	//创建Unity 二进制文件
	static public T CreateScriptableObject<T>(string path,string name) where T : ScriptableObject{
		if(!Directory.Exists (path)){
			Directory.CreateDirectory (path);
		}

	    var file = path + "/" + name + ".asset";
	    if (File.Exists(file))
	    {
	        File.Delete(file);
	    }

        var stObj = ScriptableObject.CreateInstance<T>();

		AssetDatabase.CreateAsset (stObj, path + "/" + name + ".asset");
		//AssetDatabase.SaveAssets ();
		//AssetDatabase.Refresh ();
		return (T)stObj;
	}

	static public void CreateJson(object obj, string filePath,string fileName){
		if(!Directory.Exists(filePath)){
			Directory.CreateDirectory (filePath);
		}
		string json = JsonUtility.ToJson (obj);
		File.WriteAllText(filePath + "/" + fileName + ".txt", json);
	}


/// <summary>
/// Setting 文件指的是只在编辑模式时使用的配置文件，并不会被加载到游戏客户端并热更
/// </summary>

	static private string settingPath = "Assets/Editor Default Resources/Settings";

	static private BundleBuildSetting _bundleSetting;
	static public BundleBuildSetting bundleSetting{
		get{
			if(_bundleSetting == null){
				if (!Directory.Exists (settingPath)){
					Directory.CreateDirectory(settingPath);
				}
				var obj = AssetDatabase.LoadAssetAtPath (settingPath + "/" + "BundleBuildSetting.asset", typeof(BundleBuildSetting));
				if (obj != null) {
					_bundleSetting = (BundleBuildSetting)obj;
				} else {
					_bundleSetting = CreateScriptableObject<BundleBuildSetting> (settingPath, "BundleBuildSetting");
				}
				obj = null;
			}
			return _bundleSetting;
		}
	}

/// <summary>
/// Config 文件指的是被加载到游戏客户端并热更的游戏运行时所需的配置文件
/// </summary>

	static private string buildPath = "Assets/Resources/Build";
	static private string configPath = "Assets/Resources/Build/Load_Preload/G_Config/Config";
	static private string clientConfigPath = "Assets/Resources/Configs";

	static private ClientConfig _clientConfig;
	static public ClientConfig clientConfig{
		get{
			if(_clientConfig == null){
				if (!Directory.Exists (clientConfigPath)){
					Directory.CreateDirectory(clientConfigPath);
				}
				var obj = AssetDatabase.LoadAssetAtPath (clientConfigPath + "/" + "ClientConfig.asset", typeof(ClientConfig));
				if (obj != null) {
					_clientConfig = (ClientConfig)obj;
				} else {
					_clientConfig = CreateScriptableObject<ClientConfig> (clientConfigPath, "ClientConfig");
				}
				obj = null;
			}
			return _clientConfig;
		}
	}


	static private CustomConfig _customConfig;
	static public CustomConfig customConfig{
		get{
			if(_customConfig == null){
				if (!Directory.Exists (configPath)){
					Directory.CreateDirectory(configPath);
				}
				var obj = AssetDatabase.LoadAssetAtPath (configPath + "/" + "CustomConfig.asset", typeof(CustomConfig));
				if (obj != null) {
					_customConfig = (CustomConfig)obj;
				} else {
					_customConfig = CreateScriptableObject<CustomConfig> (configPath, "CustomConfig");
				}
				obj = null;
			}
			return _customConfig;
		}
	}

	static private LoadPolicys _loadPolicys;
	static public LoadPolicys loadPolicys{
		get{
			if(_loadPolicys == null){
				if (!Directory.Exists (buildPath)){
					Directory.CreateDirectory(buildPath);
				}
				var obj = AssetDatabase.LoadAssetAtPath (buildPath + "/" + "LoadPolicys.asset", typeof(LoadPolicys));
				if (obj != null) {
					_loadPolicys = (LoadPolicys)obj;
				} else {
					_loadPolicys = CreateScriptableObject<LoadPolicys> (buildPath, "LoadPolicys");
				}
				obj = null;
			}
			return _loadPolicys;
		}
	}


}
