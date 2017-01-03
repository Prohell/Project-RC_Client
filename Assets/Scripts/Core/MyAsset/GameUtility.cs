using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;

public class GameUtility {
	static public string resURL = @"file://E:/Resources/";

	static public string buildPath = "Build";



	static private string _streamingHotfixPathApp;
	static public string StreamingHotfixPathApp{
		get{ 
			if(string.IsNullOrEmpty(_streamingHotfixPathApp)){
				StringBuilder streamingPathStr = new StringBuilder ();
				streamingPathStr.Append (Application.streamingAssetsPath).Append ("/").Append (GameUtility.GetPlatformName()).Append ("/").Append (Configs.clientConfig.hotfixFile);
				_streamingHotfixPathApp = streamingPathStr.ToString ();
				streamingPathStr = null;
			}
			return _streamingHotfixPathApp;
		}
	}

	static private string _streamingHotfixPathFull;
	static public string StreamingHotfixPathFull{
		get{ 
			if(string.IsNullOrEmpty(_streamingHotfixPathFull)){
				StringBuilder streamingPathStr = new StringBuilder ();
				streamingPathStr.Append (WWWStreamPath).Append ("/").Append (GameUtility.GetPlatformName()).Append ("/").Append (Configs.clientConfig.hotfixFile);
				_streamingHotfixPathFull = streamingPathStr.ToString ();
				streamingPathStr = null;
			}
			return _streamingHotfixPathFull;
		}
	}

	static private string _persistentHotfixPath;
	static public string PersistentHotfixPath{
		get{ 
			if(string.IsNullOrEmpty(_persistentHotfixPath)){
				StringBuilder persistentPathStr = new StringBuilder ();
				persistentPathStr.Append (Application.persistentDataPath).Append ("/").Append (GameUtility.GetPlatformName()).Append ("/").Append (Configs.clientConfig.hotfixFile);
				_persistentHotfixPath = persistentPathStr.ToString ();
				persistentPathStr = null;
			}
			return _persistentHotfixPath;
		}
	}

	static public string WWWStreamPath{
		get{
			string path = Application.streamingAssetsPath;
			if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor){
				path = @"file://" + Application.streamingAssetsPath;
			}else if(Application.platform == RuntimePlatform.Android){
				path =	"jar:file://" + Application.dataPath + "!/assets";
			}else if(Application.platform == RuntimePlatform.IPhonePlayer){
				path = @"file://" + Application.dataPath +"/Raw";
			}
			return path;
		}
	}

	static public string GetPlatformName()
	{
		#if UNITY_EDITOR
		return GetPlatformForAssetBundles(UnityEditor.EditorUserBuildSettings.activeBuildTarget);
		#else
		return GetPlatformForAssetBundles(Application.platform);
		#endif
	}

	#if UNITY_EDITOR
	static private string GetPlatformForAssetBundles(UnityEditor.BuildTarget target)
	{
		switch(target)
		{
		case UnityEditor.BuildTarget.Android:
			return "Android";
		case UnityEditor.BuildTarget.iOS:
			return "iOS";
		case UnityEditor.BuildTarget.WebGL:
			return "WebGL";
		case UnityEditor.BuildTarget.StandaloneWindows:
		case UnityEditor.BuildTarget.StandaloneWindows64:
			return "Windows";
		case UnityEditor.BuildTarget.StandaloneOSXIntel:
		case UnityEditor.BuildTarget.StandaloneOSXIntel64:
		case UnityEditor.BuildTarget.StandaloneOSXUniversal:
			return "OSX";
			// Add more build targets for your own.
			// If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
		default:
			return null;
		}
	}
	#endif

	static private string GetPlatformForAssetBundles(RuntimePlatform platform)
	{
		switch(platform)
		{
		case RuntimePlatform.Android:
			return "Android";
		case RuntimePlatform.IPhonePlayer:
			return "iOS";
		case RuntimePlatform.WebGLPlayer:
			return "WebGL";
		case RuntimePlatform.WindowsPlayer:
			return "Windows";
		case RuntimePlatform.OSXPlayer:
			return "OSX";
			// Add more build targets for your own.
			// If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
		default:
			return null;
		}
	}


	static public AssetsInfo assetsInfo;
	static public IEnumerator LoadAssetsInfo(Action<AssetsInfo> callback = null){
		if (assetsInfo == null) {
			yield return LoadFromJson<AssetsInfo> (GameUtility.StreamingHotfixPathFull, "AssetsInfo", (config) => {
				assetsInfo = config;
				if (callback != null) {
					callback (assetsInfo);
				}
			});
		}
	}

	static public BundlesInfo bundlesInfo;
	static public IEnumerator LoadBundlesInfo(Action<BundlesInfo> callback = null){
		if (bundlesInfo == null) {
			yield return LoadFromJson<BundlesInfo> (GameUtility.StreamingHotfixPathFull, "BundlesInfo", (config) => {
				bundlesInfo = config;
				if (callback != null) {
					callback (bundlesInfo);
				}
			});
		}
	}

	static public IEnumerator LoadFromJson<T>(string path, string name, Action<T> callback = null){
		yield return GameLoader.LoadText (path + "/" + name + ".txt", (str) => {
			T config = JsonUtility.FromJson<T> (str);
			if (callback != null) {
				callback (config);
			}
		});
	}
}
