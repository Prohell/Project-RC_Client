using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class SceneLuaTest : SceneBase
{
    public override SceneId Id { get { return SceneId.LuaTest; } }

    public override void OnWillEnter(object param)
    {

    }

    public override void OnEntered(object param)
    {
        //Create proxy
        ProxyManager.GetInstance().Add(new LuaTestProxy());
    }

    public override void OnWillExit()
    {
        UIManager.GetInstance().OnReset();
    }

    public override void OnExited()
    {

    }
}
