using UnityEngine;
using GCGame.Table;
using System;
using System.Collections;

public class Game
{
	static private TableManager mTableManager;
	static public TableManager tableManager { get { return mTableManager; } }

	static private MySceneManager mSceneManager = null;
	static public MySceneManager SceneManager { get { return mSceneManager; } }

	static public LuaManager LuaManager { get; private set; }
	static public UIManager UIManager { get; set; }

	//游戏初始化状态
	static public GameInitState initState = GameInitState.Uninitialized;
    // 游戏初始化入口
	[RuntimeInitializeOnLoadMethod]
	static public void OnInit()
    {
		if(initState != GameInitState.Uninitialized){
			return;
		}
		InitGameBase ();
	    InitGameAssets();
    }
	//初始化游戏基础
	static public void InitGameBase(){
		//设置Log
		Application.logMessageReceived += OnLog;
		Application.logMessageReceivedThreaded += OnLog;
		//初始化游戏引擎的一些设置
		GameSettings.OnInit();
		// MVC
		GameFacade.GetInstance().OnInit();
		// 场景管理
		mSceneManager = MySceneManager.GetInstance();
		mSceneManager.OnInit();
		//添加全局FPS组件
		Main.Instance.AddMainComponent<ShowFPS>();
		//添加全局InGameLog组件
		Main.Instance.AddMainComponent<InGameLog>();
		// 对象池管理
		GameObject poolRoot = new GameObject("GameObjectPool");
		poolRoot.transform.SetParent (Main.Instance.transform);
		GameObjectPool.GetInstance().root = poolRoot;
		//Network
		NetManager.GetInstance().OnInit();
        //Add temp entrance.
	    Main.Instance.AddMainComponent<TempEntrance>();

        initState = GameInitState.GameBaseInited;
	}
		
	static public string ServerIp = "10.12.20.37";
	static public int ServerPort = 2231;
	//建立服务器连接

	static public void ConnectToServer(Action<bool,string> callback){
		NetManager.GetInstance().ConnectToServer(ServerIp, ServerPort, callback);
	}

	static public void InitGameAssets(Action callback = null){
		StartCoroutine (InitAssets(callback));
	}

	//异步初始化 和热更资源 缓存表格等
	static private IEnumerator InitAssets(Action callback = null)
	{
		yield return GameAssets.Init();
		initState = GameInitState.GameAssetsInited;
		yield return InitAfterAssets ();
		if(callback != null){
			callback ();
		}
	}

	//资源热更后初始化的游戏资源
	static private IEnumerator InitAfterAssets()
	{
        // 表格管理
        mTableManager = new TableManager();
		tableManager.InitTable();
		//Start lua VM.
		LuaManager = Main.Instance.AddMainComponent<LuaManager>();
		LuaManager.OnInit();
		//UI framework.
		UIManager = UIManager.GetInstance();
		yield return UIManager.InitUIManager();

		initState = GameInitState.Initialized;
	}

	static public void OnUpdate()
    {
        UpdateManager.GetInstance().OnUpdate();
        NetManager.GetInstance().OnUpdate();
    }

	static public void OnReset()
    {
        UpdateManager.GetInstance().OnReset();
        GameFacade.GetInstance().OnReset();
        mSceneManager.OnReset();
    }

	static public void OnLog(string message, string stackTrace, LogType type)
    {
        switch (type)
        {
            case LogType.Exception:
                {
                    string bugMsg = "{\n" +
                    "\"message\":" + "\"" + message.Replace("\n", "") + "\"" +
                        ",\n\"stacktrace\":" + "\"" + stackTrace.Replace("\n", "") + "\"" +
                        ",\n\"time\":" + "\"" + PlatformHelper.GetCurrentTime() + "\""
                        + "\n" + "\"" + "\n}";

                    //PlatformHelper.UploadBug(butMsg);
                    LogModule.ErrorLog(bugMsg);
                    break;
                }
            case LogType.Log:
                LogModule.DebugLog(message);
                break;
            case LogType.Warning:
                LogModule.WarningLog(message);
                break;
            case LogType.Error:
                LogModule.ErrorLog(message);
                break;
        }
    }


	static public GameObject gameObjectRoot{
		get{ 
			return Main.Instance.gameObject;
		}
	}

	static public void StartCoroutine(IEnumerator ie){
		Main.Instance.StartCoroutine (ie);
	}
}