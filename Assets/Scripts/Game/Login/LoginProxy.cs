using UnityEngine;
using System.Collections;

public class LoginProxy : IProxy
{
    private string m_account;
    private string m_password;
    private int m_port;

    public void ConnectToServer(string account, string password, int port = -1)
    {
        m_account = account;
        m_password = password;
        m_port = port;
        Game.ConnectToServer(ConnectHandle, m_port);
    }

    void ConnectHandle(bool success, string result)
    {
        if (success)
        {
            SendCGLogin(m_account, m_password);
        }
        else
        {
            LogModule.ErrorLog(result);
        }
    }

    //发送登录请求
    public void SendCGLogin(string account, string password)
    {
        CG_LOGIN packet = (CG_LOGIN)PacketDistributed.CreatePacket(MessageID.PACKET_CG_LOGIN);
        packet.SetAccount(account);
        packet.SetMaxpacketid((int)MessageID.PACKET_SIZE);
        packet.SendPacket();
    }

    //发送创建角色请求
    public void SendCGCreateRole(string account, string password)
    {
        CG_CREATEROLE packet = (CG_CREATEROLE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_CREATEROLE);
        packet.SetName(account);
        packet.SetGender(1);
        packet.SendPacket();
    }

    public void EnterGame(object para)
    {
        UIManager.GetInstance().CloseUI("Login");
        MySceneManager.GetInstance().SwitchToScene(SceneId.Castle);
    }

    private void AddListener()
    {
        EventManager.GetInstance().AddEventListener(EventId.PlayerProxyUpdate, EnterGame);
    }

    public void OnDestroy()
    {

    }

    public void OnInit()
    {
        AddListener();
    }
}
