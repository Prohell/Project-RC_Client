using UnityEngine;
using System.Collections;
using LuaInterface;

public class LoginMediator : IUIMediator, IMediator
{
    public void ConnectToServer(string account, string password, string port)
    {
        int serverPort = string.IsNullOrEmpty(port) ? -1 : int.Parse(port);
        ProxyManager.GetInstance().Get<LoginProxy>().ConnectToServer(account, password, serverPort);
    }

    public void EnterGame()
    {
        ProxyManager.GetInstance().Get<LoginProxy>().EnterGame(null);
    }

    public LuaTable m_View { get; set; }
    public string m_UIName { get; set; }
    public void OnInit()
    {

    }

    public void OnDestroy()
    {

    }
}
