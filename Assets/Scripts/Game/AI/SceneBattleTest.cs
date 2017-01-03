using UnityEngine;
using System.Collections;

public class SceneBattleTest : SceneBase
{
    public override SceneId Id { get { return SceneId.BattleTest; } }

    public override void OnEntered(object param)
    {
        UIManager.GetInstance().OpenUI("MainUI", view =>
        {
            LuaHelper.CallFunctionWithSelf(view, "MainUIView.Init");
        }, view =>
        {
            LuaHelper.CallFunctionWithSelf(view, "MainUIView.ShowCastleButton");
        }, false);
        UIManager.GetInstance().OpenUI("MainResource", null, null, false);
    }

    public override void OnWillExit()
    {

    }
}
