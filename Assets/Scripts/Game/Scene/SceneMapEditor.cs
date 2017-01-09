/// <summary>
/// 地图编辑器
/// by TT
/// 2016-07-11
/// </summary>
using UnityEngine;
public class SceneMapEditor : SceneBase
{
    public override SceneId Id { get { return SceneId.MapEditor; } }

    public override void OnEntered(object param)
    {
        ProxyManager.GetInstance().Add(new MapProxy());
        GameFacade.AddMediator(new MapMediator(), MapView.Current.gameObject);
		Game.StartCoroutine (MapView.Current.Init());

        UIManager.GetInstance().OnReset();
        Object.Destroy(Main.Instance.GetComponent<TempEntrance>());      
        Main.Instance.RmvMainComponent<TempEntrance>();
    }

    public override void OnWillExit()
    {
        GameFacade.RemoveMediator(typeof(MapMediator));
	}
}