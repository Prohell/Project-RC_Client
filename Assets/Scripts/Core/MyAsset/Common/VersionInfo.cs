using UnityEngine;
using System.Collections.Generic;

public class VersionInfo :ScriptableObject
{
	public List<BundleInfo> bundles;
	public VersionInfo(){
		bundles = new List<BundleInfo> ();
	}
}