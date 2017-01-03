using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class LuaTestMediator4 : IUIMediator, IMediator
{
    public void UpdateItems(object paras)
    {
        LuaHelper.CallFunctionWithSelf(((IUIMediator)this).m_View, "LuaTestView:UpdateItems");
    }

    public void OnInit()
    {
        EventManager.GetInstance().AddEventListener(EventId.LuaTestUpdate, UpdateItems);
    }

    public LuaTable GetBagItemData()
    {
        return LuaHelper.ConvertToTable(ProxyManager.GetInstance().Get<LuaTestProxy>().BagItemDataList);
    }

    public void UseItem(int num)
    {
        ProxyManager.GetInstance().Get<LuaTestProxy>().BagItemDataList.Remove(num);
        LogModule.DebugLog(num + " removed.");
    }

    public void UseAllItems()
    {
        ProxyManager.GetInstance().Get<LuaTestProxy>().BagItemDataList.Clear();
        LogModule.DebugLog("All removed.");
    }


    public void OnDestroy()
    {
        EventManager.GetInstance().RemoveEventListener(EventId.LuaTestUpdate, UpdateItems);
    }

    LuaTable IUIMediator.m_View { get; set; }

    string IUIMediator.m_UIName { get; set; }
}
