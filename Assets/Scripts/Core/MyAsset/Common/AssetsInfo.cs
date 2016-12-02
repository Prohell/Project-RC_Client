using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AssetInfo {
	public string name;
	public string suffix;
	public string path;
	public string bundleName;
	public long size;
	public bool multiple;
}


public class AssetsInfo
{
	public List<AssetInfo> assetList = new List<AssetInfo>();
}
