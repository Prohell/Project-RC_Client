using UnityEngine;

public class GameObjectCreater
{
	public static GameObject CreateGo(string name, Transform parent = null)
    {
		GameObject go = new GameObject();
		go.name = name;
		go.transform.parent = parent;
		return go;
	}

	public static T CreateComponent<T>(string name, Transform parent = null) where T : Component
    {
		GameObject go = CreateGo(name, parent);
		T comp = go.AddComponent<T>();
		return comp;
	}
	
	public static T FindOrCreate<T>() where T : Component
    {
		T re = Object.FindObjectOfType<T>();
		if (re == null)
        {
			re = CreateComponent<T>("Unnamed");
		}
		return re;
	}
}
