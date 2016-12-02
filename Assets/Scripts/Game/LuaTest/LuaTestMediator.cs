using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaTestMediator : IMediator
{
    public LuaOutlet m_LuaTestView;

    public void UpdateItems()
    {


        LuaHelper.CallFunction(m_LuaTestView.m_LuaTable, "LuaTestView:UpdateItems");
    }

    public void OnInit()
    {
        var prefab = LuaHelper.LoadGB("LuaTest/LuaTestWindow");
        var luaTestWindow = Object.Instantiate(prefab);
        m_LuaTestView = LuaHelper.GetOutletComponent(luaTestWindow, "LuaTestView");

        ProxyManager.GetInstance().Get<LuaTestProxy>().OnUpdateData += UpdateItems;
    }

    public void OnDestroy()
    {

    }
}
