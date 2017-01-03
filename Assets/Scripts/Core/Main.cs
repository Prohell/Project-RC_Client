using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : Mono_Singleton<Main>
{
	public T AddMainComponent<T>()where T : Component{
		return gameObject.AddComponent<T> ();
	}

	public T GetMainComponent<T>(){
		return gameObject.GetComponent<T> ();
	}

	public void RmvMainComponent<T>()where T : Component{
		 T t = gameObject.GetComponent<T> ();
		Component.DestroyImmediate (t);
	}

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InvokeRepeating("HeartbeatInvoker", 0, 1);
    }

    void HeartbeatInvoker()
    {
        GameFacade.SendEvent(EventId.HeartBeat);
    }

    void Update()
    {
		Game.OnUpdate();
    }


}