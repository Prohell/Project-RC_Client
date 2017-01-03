using UnityEngine;
using System.Collections;
using LuaInterface;

public class LoginMediator : IUIMediator, IMediator
{
    public void ConnectToServer(string account, string password)
    {
        ProxyManager.GetInstance().Get<LoginProxy>().ConnectToServer(account, password);
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
