using System;
using UnityEngine;
using System.Collections;

public class SceneLuaTest : SceneBase
{
    public override SceneId Id { get { return SceneId.LuaTest; } }

    public override void OnEntered(object param)
    {
        ProxyManager.GetInstance().Add(new LuaTestProxy());
        MediatorManager.GetInstance().Add(new LuaTestMediator());
    }

    public override void OnWillExit()
    {
        
    }
}
