using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IAssetAsyncLoader {

	IEnumerator Init();
	IEnumerator LoadAssetBundle (string assetBundleName, Action<AssetBundle> callback = null);
	IEnumerator LoadAssetAsync<T> (string assetBundleName, string assetName, Action<T> callback)where T : UnityEngine.Object;

	void GetBundleNeedLoads (string bundleName, List<string> needLoad);
	long GetBundleSize (string assetBundleName);

	void AddBundleReference (string bundleName);
	void SubBundleReference (string bundleName);
	void Unload(string bundleName, bool b);
	void UnloadAll(bool b);
}
