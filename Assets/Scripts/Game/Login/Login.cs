using System;
using UnityEngine;
using System.Collections;

public class Login : Singleton<Login>
{
    public string ServerIp = "10.12.20.37";
    public int ServerPort = 2231;

    public void ConnectToServer()
    {
        //NetManager.GetInstance().ConnectToServer(ServerIp, ServerPort, OnConnectResult);
        OnConnectResult(true, "");
    }

    void OnConnectResult(bool bSuccess, string result)
    {
        LogModule.DebugLog(result);
        if (bSuccess)
        {
            LogModule.DebugLog("connect success");
			TempEntrance.Instance.SwitchToCastle();
        }
        else
        {
            LogModule.WarningLog("connect fail");
        }
    }
}
