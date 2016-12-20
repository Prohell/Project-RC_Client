using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏入口
/// by TT
/// 2016-07-04
/// </summary>
public class Main : MonoBehaviour
{
    public static Transform DontDestroyRoot;

    /// <summary>
    /// Set to first to show full log.
    /// </summary>
    public static InGameLog InGameLogManager { get; private set; }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyRoot = transform;

        //Show loading
        LoadingController.GetInstance().Show(9, Login.GetInstance().ConnectToServer);

        //InGame log.
        InGameLogManager = GameObjectCreater.CreateComponent<InGameLog>("InGameLog", Main.DontDestroyRoot);

        StartCoroutine(InitGameAssets());
    }

    //异步初始化 和热更资源 缓存表格等
    private IEnumerator InitGameAssets()
    {
        yield return GameAssets.Init();
        LoadingController.GetInstance().UpdateProgress(1, LoadingController.initText);

        // Game init
        Game.GetInstance().OnInit();
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
        Game.GetInstance().OnUpdate();
    }
}