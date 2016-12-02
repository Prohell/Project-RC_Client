using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IAssetAsyncLoader {

	IEnumerator Init();
	IEnumerator LoadAssetBundle (string assetBundleName, Callback<AssetBundle> callback = null);
	IEnumerator LoadAssetAsync<T> (string assetBundleName, string assetName, Callback<T> callback)where T:Object;

	void GetBundleNeedLoads (string bundleName, List<string> needLoad);
	long GetBundleSize (string assetBundleName);

	void AddBundleReference (string bundleName);
	void SubBundleReference (string bundleName);
	void Unload(string bundleName, bool b);
	void UnloadAll(bool b);
}
