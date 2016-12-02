using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class AssetLoadManager{
	static private IAssetAsyncLoader _loader;
	static private IAssetAsyncLoader loader{
		get{ 
			if(_loader == null){
				if(GameUtility.bundleConfig.isReleaseBundle){
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

		yield return GameUtility.LoadAssetsInfo ();

		if(GameUtility.bundleConfig.isReleaseBundle){
			yield return GameUtility.LoadBundlesInfo ();
		}
	}

	static public void AddListeners(){
		Messenger.AddListener<string> (LoadEvent.BundleLoadFailed, (str) => {
			#if UNITY_EDITOR
			Debug.LogError(str);
			#endif
		});
	}

	//加载Bundle并缓存
	static public IEnumerator LoadAssetBundle(string assetBundleName, Callback<AssetBundle> callback = null){
		yield return loader.LoadAssetBundle (assetBundleName, callback);
	}
	//加载Bundle缓存 并加载资源
	static public IEnumerator LoadAssetAsync<T>(string assetBundleName, string assetName, Callback<T> callback)where T : Object{
		yield return loader.LoadAssetAsync<T> (assetBundleName, assetName, callback);
	}
		
	//反向查找资源并缓存加载
	static public IEnumerator LoadAssetAsyncByFileName<T>(string fileName, Callback<T> callback)where T : Object{
		List<AssetInfo> infos = new List<AssetInfo>();
		yield return GameUtility.LoadAssetsInfo ((info)=>{
			StringBuilder strBuilder = new StringBuilder();
			for(int i = 0;i < info.assetList.Count;i++){
				strBuilder.Remove(0,strBuilder.Length);
				strBuilder.Append(info.assetList[i].name);
				strBuilder.Append(".");
				strBuilder.Append(info.assetList[i].suffix);
				if(strBuilder.ToString() == fileName){
					//冗余资源的反向查找未支持,找到一个Bundle就直接Break
					infos.Add(info.assetList[i]);
					fileName = info.assetList[i].name;
					if(!info.assetList[i].multiple){
						break;
					}
				}
			}
		});

		if(infos.Count == 0){
			#if UNITY_EDITOR
			Debug.LogError("Not find file from AssetsInfo ：" + fileName);
			#endif
		} else if (infos.Count == 1){
			yield return AssetLoadManager.LoadAssetAsync<T> (infos[0].bundleName, fileName, callback);
		} else if (infos.Count > 1){
			#if UNITY_EDITOR
			Debug.LogError("冗余资源的反向查找未支持!");
			#endif
		}
		yield break;
	}

	static public void UnloadBundleRef(string bundleName){
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

	static public IEnumerator LoadFromResource<T>(string assetPath, Callback<T> callback = null)where T : Object{
		ResourceRequest request = Resources.LoadAsync(assetPath,typeof(T));
		yield return request;
		if(request.isDone){
			if(callback != null){
				callback((T)request.asset);
			}
		}
	}

	//开发测试时 可以直接调用此方法从Resource目录加载资源 最后统一替换
	static public IEnumerator LoadFromResource(string assetPath, Callback<Object> callback = null){
		ResourceRequest request = Resources.LoadAsync(assetPath);
		yield return request;
		if(request.isDone){
			if(callback != null){
				callback(request.asset);
			}
		}
	}
}
