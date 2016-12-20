using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class BundleBuildSetting : ScriptableObject {
	public List<bool> pathSelectList = new List<bool>();
	public List<string> pathList = new List<string>();

	public string pathName;

	public string eventPrefix;
	public string groupPrefix;
	public string singlePrefix;
	public string seqSign;

	public BuildAssetBundleOptions buildAssetBundleOptions;
	public BuildOptions buildOptions;

}
