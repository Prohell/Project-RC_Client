using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏初始前加载的第一个配置文件
/// </summary>
public class CustomConfig : ScriptableObject {
	[SerializeField]
	public List<string> _keys;
	private List<string> keys{
		get{ 
			if(_keys == null){
				_keys = new List<string> ();
			}
			return _keys;
		}
	}

	[SerializeField]
	public List<string> _values;
	private List<string> values{
		get{ 
			if(_values == null){
				_values = new List<string> ();
			}
			return _values;
		}
	}
		
	public int Count{
		get{
			return keys.Count;
		}
	}

	public void Add(string key,string value){
		keys.Add (key);
		values.Add (value);
	}

	public void Remove(string key){
		if(keys.Contains(key)){
			int index = keys.IndexOf (key);
			keys.RemoveAt (index);
			values.RemoveAt (index);
		}
	}

	public void Remove(int index){
		keys.RemoveAt (index);
		values.RemoveAt (index);
	}

	public List<string> GetKeys(){
		return keys;
	}

	public List<string> GetValues(){
		return values;
	}

	public string GetKey(int index){
		return values[index];
	}

	public string GetValue(int index){
		return keys[index];
	}

	public int GetKeyIndex(string key){
		return keys.IndexOf (key);
	}

	public int GetValueIndex(string value){
		return values.IndexOf (value);
	}

	public string GetValueByKey(string key){
		return GetValue(GetKeyIndex (key));
	}

	public string GetKeyByValue(string value){
		return GetValue(GetValueIndex (value));
	}

	public CustomConfig(){

	}
}
