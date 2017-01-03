/// <summary>
/// 地图编辑器
/// by TT
/// 2016-07-11
/// </summary>
public class SceneMapEditor : SceneBase
{
    public override SceneId Id { get { return SceneId.MapEditor; } }

    public override void OnEntered(object param)
    {
        GameFacade.AddMediator(new MapMediator(), MapView.Current.gameObject);
		Game.StartCoroutine (MapView.Current.Init());

        UIManager.GetInstance().OnReset();
    }

    public override void OnWillExit()
    {
        GameFacade.RemoveMediator(typeof(MapMediator));
	}
}