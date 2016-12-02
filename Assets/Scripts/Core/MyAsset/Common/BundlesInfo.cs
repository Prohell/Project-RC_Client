using System.Collections.Generic;

[System.Serializable]
public class BundleInfo {
    public string name;
	public long size;
//	public string hash;
//  public string[] include;
//  public string[] dependency;
	public BundleInfo(){}
	public BundleInfo(string name) {
		this.name = name;
	}
}

public class BundlesInfo
{
	public List<BundleInfo> bundleList = new List<BundleInfo>();
}
