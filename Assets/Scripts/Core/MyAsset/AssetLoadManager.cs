using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class AssetLoadManager{
	static private IAssetAsyncLoader _loader;
	static private IAssetAsyncLoader loader{
		get{ 
			if(_loader == null){
				if(Configs.clientConfig.isReleaseBundle){
					_loader = new ReleaseLoader();
				}else{
					_loader = new ResourceLoader();
				}
			}
			return _loader;
		}
	}

	static public IEnumerator Init(){
		AddListeners ();

		yield return loader.Init ();
	}

	static public void AddListeners(){
		Messenger.AddListener<string> (LoadEvent.BundleLoadFailed, (str) => {
			#if UNITY_EDITOR
			Debug.LogError(str);
			#endif
		});
	}

	//加载Bundle并缓存
	static public IEnumerator LoadAssetBundle(string assetBundleName, Action<AssetBundle> callback = null){
		yield return loader.LoadAssetBundle (assetBundleName, callback);
	}
	//加载Bundle缓存 并加载资源
	static public IEnumerator LoadAssetAsync<T>(string assetBundleName, string assetName, Action<T> callback)where T : UnityEngine.Object{
		yield return loader.LoadAssetAsync<T> (assetBundleName, assetName, callback);
	}

	static public void AddBundleRef(string bundleName){
		loader.AddBundleReference (bundleName);
	}

	static public void SubBundleRef(string bundleName){
		loader.SubBundleReference (bundleName);
	}

	static public void UnloadAll(bool unloadAllLoadedObject = true){
		loader.UnloadAll (unloadAllLoadedObject);
	}

	static public void Unload(string bundleName, bool unloadAllLoadedObject = true){
		loader.Unload (bundleName, unloadAllLoadedObject);
	}

	static public long GetBundleSize(string assetBundleName){
		return loader.GetBundleSize (assetBundleName);
	}
		
	static public void GetBundleNeedLoads(string bundleName, List<string> needLoad){
		loader.GetBundleNeedLoads (bundleName, needLoad);
	}

	static public IEnumerator LoadFromResource<T>(string assetPath, Action<T> callback = null)where T : UnityEngine.Object{
		ResourceRequest request = Resources.LoadAsync(assetPath,typeof(T));
		yield return request;
		if(request.isDone){
			if(callback != null){
				callback((T)request.asset);
			}
		}
	}

	//开发测试时 可以直接调用此方法从Resource目录加载资源 最后统一替换
	static public IEnumerator LoadFromResource(string assetPath, Action<UnityEngine.Object> callback = null){
		ResourceRequest request = Resources.LoadAsync(assetPath);
		yield return request;
		if(request.isDone){
			if(callback != null){
				callback(request.asset);
			}
		}
	}
}
