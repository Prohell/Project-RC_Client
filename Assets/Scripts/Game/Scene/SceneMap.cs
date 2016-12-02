/// <summary>
/// 大地图场景
/// by TT
/// 2016-11-1
/// </summary>
public class SceneMap : SceneBase
{
    public override SceneId Id { get { return SceneId.Map; } }

    public override void OnEntered(object param)
    {
        ProxyManager.GetInstance().Add(new MapProxy());
        GameFacade.AddMediator(new MapMediator(), MapView.Current.gameObject);
        Game.TaskManager.Exec(MapView.Current.Init());
        UpdateManager.GetInstance().StartHeartBeat();
    }

    public override void OnWillExit()
    {
        MapView.Current.CleanUp();
        GameFacade.RemoveMediator(typeof(MapMediator));
    }
}