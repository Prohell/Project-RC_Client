using UnityEngine;

/// <summary>
/// 游戏入口
/// by TT
/// 2016-07-04
/// </summary>
public class Main : MonoBehaviour
{
    public static Transform DontDestroyRoot;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyRoot = transform;

        // Game Start
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

    //void OnGUI()
    //{
    //    if (GUILayout.Button("Map Editor"))
    //    {
    //        Game.SceneManager.SwitchToScene(SceneId.MapEditor);
    //    }
    //}
}