using UnityEngine;
using GCGame.Table;
using System;
/// <summary>
/// Game
/// by TT
/// 2016-07-08
/// </summary>
public class Game : Singleton<Game>, IInit, IUpdate, IReset, IDestroy
{
    private TableManager mTableManager;
    public TableManager tableManager { get { return mTableManager; } }
    private static TaskManager mTaskManager = null;
    public static TaskManager TaskManager { get { return mTaskManager; } }
    private static MySceneManager mSceneManager = null;
    public static MySceneManager SceneManager { get { return mSceneManager; } }

    public static LuaManager LuaManager { get; private set; }

    public static UIManager UIManager { get; set; }

    /// <summary>
    /// 游戏初始化完成
    /// </summary>
    public bool IsInit = false;

    /// <summary>
    /// 游戏初始化
    /// </summary>
	public void OnInit()
    {
        GameSettings.InitGame();

        Application.logMessageReceived += OnLog;
        Application.logMessageReceivedThreaded += OnLog;

        //Start lua VM.
        LuaManager = GameObjectCreater.CreateComponent<LuaManager>("LuaManager", Main.DontDestroyRoot);
        LuaManager.OnInit();
        LoadingController.GetInstance().UpdateProgress(1,LoadingController.initText);

        //UI framework.
        UIManager = UIManager.GetInstance();
        UIManager.OnInit();
        LoadingController.GetInstance().UpdateProgress();

        // Task系统
        mTaskManager = GameObjectCreater.CreateComponent<TaskManager>("TaskManager", Main.DontDestroyRoot);
        LoadingController.GetInstance().UpdateProgress();

        // 表格管理
        mTableManager = new TableManager();
        tableManager.InitTable();
        LoadingController.GetInstance().UpdateProgress();

        // MVC
        GameFacade.GetInstance().OnInit();
        LoadingController.GetInstance().UpdateProgress();

        // 场景管理
        mSceneManager = GameObjectCreater.CreateComponent<MySceneManager>("MySceneManager", Main.DontDestroyRoot);
        mSceneManager.OnInit();
        LoadingController.GetInstance().UpdateProgress();

        // 对象池管理
        GameObject invisibleGoPoolRoot = GameObjectCreater.CreateGo("GoPoolRoot", Main.DontDestroyRoot);
        GameObjectPool.GetInstance().root = invisibleGoPoolRoot;
        LoadingController.GetInstance().UpdateProgress();

        //FPS
        GameObjectCreater.CreateComponent<ShowFPS>("ShowFPS", Main.DontDestroyRoot);

        //Network
        NetManager.GetInstance().OnInit();
        LoadingController.GetInstance().UpdateProgress();

        // 游戏初始化完成
        IsInit = true;

        //show test entrance.
        var temp = TempEntrance.Instance;
    }

    public void OnUpdate()
    {
        UpdateManager.GetInstance().OnUpdate();
        NetManager.GetInstance().OnUpdate();
    }

    public void OnReset()
    {
        UpdateManager.GetInstance().OnReset();
        mTaskManager.OnReset();
        GameFacade.GetInstance().OnReset();
        mSceneManager.OnReset();
    }

    public void OnDestroy()
    {
        OnReset();
    }

    public void OnLog(string message, string stackTrace, LogType type)
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
}