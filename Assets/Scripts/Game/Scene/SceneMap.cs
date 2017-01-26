/// <summary>
/// 大地图场景
/// by TT
/// 2016-11-1
/// </summary>
using UnityEngine;
public class SceneMap : SceneBase
{
    public override SceneId Id { get { return SceneId.Map; } }

    public override void OnEntered(object param)
    {
        UIManager.GetInstance().CloseUI("MainUI");
        Main.Instance.RmvMainComponent<TempEntrance>();

        ProxyManager.GetInstance().Add(new MapProxy());
        GameFacade.AddMediator(new MapMediator(), MapView.Current.gameObject);
        Game.StartCoroutine(MapView.Current.Init());

        ProxyManager.GetInstance().Add(new WorldProxy());

        var worldView = UnityEngine.Object.FindObjectOfType<WorldView>();
        worldView.OnInit();

        UIManager.GetInstance().OpenUI("World", (view) =>
            {
                WorldController.GetInstance().m_WorldUIView = view;
                LuaHelper.CallFunctionWithSelf(view, "WorldView.Init");

                WorldController.GetInstance().m_WorldModel = ProxyManager.GetInstance().Get<WorldProxy>();
                WorldController.GetInstance().m_MapModel = ProxyManager.GetInstance().Get<MapProxy>();
                WorldController.GetInstance().m_WorldView = worldView;
                WorldController.GetInstance().OnInit();

                ProxyManager.GetInstance().Get<WorldProxy>().InitMarchData();
                //ProxyManager.GetInstance().Get<WorldProxy>().InitCityData();
            },
            (view =>
            {
                WorldController.GetInstance().RefreshUI();
            }));
    }

    public override void OnWillExit()
    {
        MapView.Current.CleanUp();
        GameFacade.RemoveMediator(typeof(MapMediator));

        UIManager.GetInstance().CloseUI("World");
    }
}