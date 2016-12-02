using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class LoadPolicy{
	public string policyName;
	public List<string> list = new List<string>();
}

[System.Serializable]
public class LoadPolicyJson {
	public List<LoadPolicy> policys = new List<LoadPolicy>();

	private LoadPolicy AddPolicy(string str){
		LoadPolicy policy = new LoadPolicy ();
		policy.policyName = str;
		policys.Add (policy);
		return policy;
	}

	private LoadPolicy GetPolicy(string name){
		LoadPolicy p = null;
		bool b = false;
		for(int i = 0;i < policys.Count;i++){
			if (policys [i].policyName == name) {
				b = true;
				p = policys [i];
				break;
			}
		}
		if(!b){
			p = AddPolicy (name);
		}
		return p;
	}

	public void AddBundleName(string pName, string bName){
		LoadPolicy policy = GetPolicy (pName);
		if(!policy.list.Contains(bName)){
			policy.list.Add (bName);
		}
	}
}
