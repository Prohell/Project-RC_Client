using UnityEngine;
using System.IO;
using System.Collections;

/// <summary>
/// 客户端运行时所需的配置文件的管理中心
/// </summary>
public class Configs {


/// <summary>
/// 
/// </summary>
	static private ClientConfig _clientConfig;
	static public ClientConfig clientConfig{
		get{ 
			if(_clientConfig == null){
				_clientConfig = Resources.Load<ClientConfig> ("Configs/ClientConfig");
			}
			return _clientConfig;
		}
	}





/// <summary>
/// 需要热更新的配置文件
/// </summary>


	static private CustomConfig _customConfig;
	static public CustomConfig customConfig{
		get{ 
			if(_customConfig == null){
				_customConfig = Resources.Load<CustomConfig> (GameUtility.buildPath + "/CustomConfig");
			}
			return _customConfig;
		}
		set{ 
			_customConfig = value;
		}
	}

    public static UICategoryConfigFile UICategoryConfigFile { get; private set; }
    public static UIItemConfigFile UIItemConfigFile { get; private set; }


    static private LoadPolicys _loadPolicys;
	static public LoadPolicys loadPolicys{
		get{ 
			if(_loadPolicys == null){
				_loadPolicys = Resources.Load<LoadPolicys> (GameUtility.buildPath + "/LoadPolicys");
			}
			return _loadPolicys;
		}
		set{ 
			_loadPolicys = value;
		}
	}

	static public IEnumerator LoadAllConfigs(){

		yield return AssetLoadManager.LoadAssetAsync<CustomConfig> ("load_preload$g_config$config.assetbundle", "CustomConfig", (config) => {
			_customConfig = config;
		});

		yield return AssetLoadManager.LoadAssetAsync<UICategoryConfigFile> ("load_preload$g_config$config.assetbundle", UICategoryConfig.BundleConfigName, (config) => {
            UICategoryConfigFile = config;
		});

		yield return AssetLoadManager.LoadAssetAsync<UIItemConfigFile> ("load_preload$g_config$config.assetbundle", UIItemConfig.BundleConfigName, (config) => {
            UIItemConfigFile = config;
		});

//		AssetLoadManager.Unload ("",true);
		yield break;
	}

}
