using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceLoader : IAssetAsyncLoader{
	
	public IEnumerator Init(){
		yield break;
	}

	public IEnumerator LoadAssetBundle (string assetBundleName, Callback<AssetBundle> callback = null){
		yield break;
	}

	public IEnumerator LoadAssetAsync<T>(string assetBundleName, string assetName, Callback<T> callback)where T:Object{
		string assetPath = null;
		AssetsInfo assetsInfo = GameUtility.assetsInfo;
		if(assetsInfo.assetList != null && assetsInfo.assetList.Count > 0){
			for(int i = 0;i < assetsInfo.assetList.Count;i++){
				if(assetsInfo.assetList[i].bundleName == assetBundleName){
					if(assetsInfo.assetList[i].name == assetName){
						assetPath = assetsInfo.assetList [i].path;
						break;
					}	
				}
			}
		}
		ResourceRequest request = Resources.LoadAsync(assetPath,typeof(T));
		yield return request;
		if(request.isDone){
			if(request.asset == null){
				Debug.LogError ("request.asset is Null : " + assetPath);
			}
			callback(request.asset as T);
		}
	}


	public long GetBundleSize (string assetBundleName){
		return 0;
	}
	public void GetBundleNeedLoads (string bundleName, List<string> needLoad){}


	public void AddBundleReference (string bundleName){}
	public void SubBundleReference (string bundleName){}
	public void Unload(string bundleName,bool b){}
	public void UnloadAll(bool b){}
}
