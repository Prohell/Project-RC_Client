using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class ReleaseLoader :  IAssetAsyncLoader {
	private AssetBundleManifest	streamManifest;
	private AssetBundleManifest remoteManifest;


	private Dictionary<string, AssetBundle> bundleDic = new Dictionary<string, AssetBundle> ();
	private bool hasInit = false;
	public IEnumerator Init(){
		yield return ClientCheck ();
		yield return LoadManifest ();
		hasInit = true;
	}

	IEnumerator ClientCheck(){
		string persistentCVFile = GameUtility.PersistentHotfixPath + "/ClientVersion.txt";
		string streamingCVFile = GameUtility.StreamingHotfixPathFull + "/ClientVersion.txt";

		yield return GameLoader.LoadText(streamingCVFile,(str) => {
			ClientVersion streamCC = JsonUtility.FromJson<ClientVersion> (str);
			ClientVersion persistentCC = LoadPersistentCC (persistentCVFile);
			if (persistentCC != null) {
				if (streamCC.md5 != persistentCC.md5) {
					Caching.CleanCache ();
					File.WriteAllText (persistentCVFile, str);
					Debug.Log ("CleanCache And WriteClient");
				}
			} else {
				if(!Directory.Exists(GameUtility.PersistentHotfixPath)){
					Directory.CreateDirectory (GameUtility.PersistentHotfixPath);
				}
				File.WriteAllText (persistentCVFile, str);
				Debug.Log ("WriteClient");
			}
		});
	}

	ClientVersion LoadPersistentCC(string fullName){
		if(File.Exists(fullName)){
			string txt = File.ReadAllText (fullName);
			ClientVersion cc = JsonUtility.FromJson<ClientVersion>(txt);
			return cc;
		}
		return null;
	}


	IEnumerator LoadManifest(){
		string file = GameUtility.StreamingHotfixPathApp + "/" + Configs.clientConfig.hotfixFile;

		AssetBundle curBundle = null;
		yield return GameLoader.LoadFromFileAsync (file,(bundle)=>{
			curBundle = bundle;
		});
		AssetBundleRequest requst = curBundle.LoadAssetAsync<AssetBundleManifest> ("AssetBundleManifest");
		yield return requst;
		if(requst.asset != null){
			streamManifest = (AssetBundleManifest)requst.asset;
		}
		curBundle.Unload (false);
		curBundle = null;

		yield return GameLoader.LoadAssetBundle (GameUtility.resURL + Configs.clientConfig.hotfixFile,(bundle)=>{
			curBundle = bundle;
		});
		requst = curBundle.LoadAssetAsync<AssetBundleManifest> ("AssetBundleManifest");
		yield return requst;
		if(requst.asset != null){
			remoteManifest = (AssetBundleManifest)requst.asset;
		}
		curBundle.Unload (false);
		curBundle = null;
	}


	public IEnumerator LoadAssetAsync<T>(string assetBundleName, string assetName, Callback<T> callback) where T:Object{
		//未初始化先初始化
		if(!hasInit){
			yield return Init ();
		}
		//加载相对应的Bundle
		yield return LoadAssetBundle (assetBundleName);

		AssetBundle bundle = bundleDic [assetBundleName];

		AssetBundleRequest request = bundle.LoadAssetAsync<T> (assetName);
		yield return request;
		if(request.isDone){
			if (request.asset != null) {
				Object obj = request.asset;
				callback (obj as T);
			} else {
				#if UNITY_EDITOR
				Debug.Log("request.asset is Null");
				#endif
				Messenger.Broadcast<string, string> (LoadEvent.AssetLoadFailed, assetBundleName, assetName);
			}
		}
	}

	public IEnumerator LoadAssetBundle(string assetBundleName, Callback<AssetBundle> callback = null){
		if(bundleDic.ContainsKey(assetBundleName)){
			yield break;
		}

		Hash128 remoteHash = remoteManifest.GetAssetBundleHash (assetBundleName);
		Hash128 streamHash = streamManifest.GetAssetBundleHash (assetBundleName);
		AssetBundle curBundle = null;
		//不存在就是增量 存在了但值不同就是更新
		if (!streamHash.isValid || remoteHash != streamHash) {
			yield return GameLoader.LoadFromCacheOrDownload (GameUtility.resURL + assetBundleName,1,0,(bundle) =>{
				curBundle = bundle;
			});
		} else {
			string file = GameUtility.StreamingHotfixPathApp + "/" + assetBundleName;
			yield return GameLoader.LoadFromFileAsync (file, (bundle)=>{
				curBundle = bundle;
			});
		}

		if (curBundle != null) {
			string[] arr = remoteManifest.GetAllDependencies (curBundle.name);
			if (arr != null && arr.Length > 0) {
				for (int i = 0; i < arr.Length; i++) {
					yield return LoadAssetBundle (arr [i]);
				}
			}
			bundleDic.Add (curBundle.name, curBundle);
			if(callback != null){
				callback (curBundle);
			}
		}
	}


	public void AddBundleReference(string bundleName){
		AddReferens (bundleName);
		string[] dependencies = remoteManifest.GetAllDependencies (bundleName);
		if(dependencies != null && dependencies.Length > 0){
			for(int i = 0;i < dependencies.Length;i++){
				AddBundleReference (dependencies[i]);
			}
		}
	}

	public void SubBundleReference(string bundleName){
		SubReferens (bundleName);
		string[] dependencies = remoteManifest.GetAllDependencies (bundleName);
		if(dependencies != null && dependencies.Length > 0){
			for(int i = 0;i < dependencies.Length;i++){
				SubBundleReference (dependencies[i]);
			}
		}
	}
		
	public void Unload(string bundleName,bool unloadAllLoadedObject = true){
		if (bundleDic.ContainsKey (bundleName)) {
			bundleDic [bundleName].Unload (unloadAllLoadedObject);
			if(unloadAllLoadedObject){
				references.Remove (bundleName);
				bundleDic.Remove (bundleName);
			}
		} else {
			#if UNITY_EDITOR
				Debug.LogError(bundleName + "not find." );
			#endif
		}
	}

	public void UnloadAll(bool unloadAllLoadedObject = true){
		foreach (KeyValuePair<string, AssetBundle> kv in bundleDic)
		{
			kv.Value.Unload (unloadAllLoadedObject);
		}
		if(unloadAllLoadedObject){
			references.Clear ();
			bundleDic.Clear ();
		}
	}


	public long GetBundleSize(string assetBundleName){
		long size = 0;
		for(int j = 0;j < GameUtility.bundlesInfo.bundleList.Count;j++){
			if (assetBundleName == GameUtility.bundlesInfo.bundleList [j].name) {
				size = GameUtility.bundlesInfo.bundleList [j].size;
			}
		}
		return size;
	}

	public void GetBundleNeedLoads(string bundleName, List<string> needLoad){
		if(bundleDic.ContainsKey(bundleName)){
			return;
		}

		Hash128 remoteHash = remoteManifest.GetAssetBundleHash (bundleName);
		Hash128 streamHash = streamManifest.GetAssetBundleHash (bundleName);
		//不存在就是增量 存在了但值不同就是更新
		if (!streamHash.isValid || remoteHash != streamHash) {
			if (!Caching.IsVersionCached (GameUtility.resURL + bundleName, 1)) {
				if(!needLoad.Contains(bundleName)){
					needLoad.Add (bundleName);
				}
			}
		}

		string[] arr = remoteManifest.GetAllDependencies (bundleName);
		if(arr != null && arr.Length > 0){
			for(int i = 0;i < arr.Length;i++){
				GetBundleNeedLoads (arr[i],needLoad);
			}
		} 
	}
		
	private Dictionary<string,int> references = new Dictionary<string, int> (); 
	private void AddReferens(string bundleName){
		if (!references.ContainsKey (bundleName)) {
			references.Add (bundleName, 1);
		} else {
			references[bundleName]++;
		}
	}

	private void SubReferens(string bundleName){
		if(references.ContainsKey(bundleName)){
			references[bundleName]--;
		}
		if(references[bundleName] == 0){
			Unload (bundleName, true);
		}
	}
}
