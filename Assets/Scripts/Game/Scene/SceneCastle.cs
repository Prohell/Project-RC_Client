using UnityEngine;
using System.Collections;

public class SceneCastle : SceneBase
{
    public override SceneId Id { get { return SceneId.Castle; } }

    public override void OnEntered(object param)
    {
        UIManager.GetInstance().OpenUI("MainUI", view =>
        {
            LuaHelper.CallFunctionWithSelf(view, "MainUIView.Init");
        }, view =>
        {
            LuaHelper.CallFunctionWithSelf(view, "MainUIView.ShowWorldButton");
        }, false);
        UIManager.GetInstance().OpenUI("MainResource", null, null, false);
    }

    public override void OnWillExit()
    {
        UIManager.GetInstance().CloseUI("CityUI");
    }
}
