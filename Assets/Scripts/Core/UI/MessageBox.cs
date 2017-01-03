using System;
using UnityEngine;

public partial class UIManager
{
    public void ShowMessageBox(string msgText, EventDelegate.Callback okCallBack = null, EventDelegate.Callback cancelCallBack = null)
    {
        var config = m_uiItemConfigMap["MessageBox"].Clone() as UIItemConfig;
        config.Name += "_" + Time.realtimeSinceStartup;

        OpenUIInternal(config,
            (view) =>
            {
                view["InfoStr"] = msgText;
                view["OKCallBack"] = okCallBack;
                view["CancelCallBack"] = cancelCallBack;
                LuaHelper.CallFunctionWithSelf(view, "MessageBoxView:Init");
            });
    }
}
