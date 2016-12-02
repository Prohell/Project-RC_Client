using System;

/// <summary>
/// 网络管理器
/// by TT
/// 2016/11/28
/// </summary>
public class NetManager : Singleton<NetManager>, IInit, IUpdate, IReset
{
    private string m_connectIP;
    private int m_connectPort;
    private NetWorkLogic.ConnectDelegate m_delConnect;

    public void OnInit()
    {
        NetWorkLogic.SetConnectLostDelegate(ConnectLost);
    }

    public void OnReset()
    {
        throw new NotImplementedException();
    }

    public void OnUpdate()
    {
        NetWorkLogic.GetMe().Update();
    }

    /// <summary>
    /// 连接丢失处理
    /// </summary>
    public void ConnectLost()
    {
        LogModule.DebugLog("ConnectLost");
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="_ip"></param>
    /// <param name="_port"></param>
    /// <param name="delConnect"></param>
    public void ConnectToServer(string _ip, int _port, NetWorkLogic.ConnectDelegate delConnect)
    {
        m_connectIP = _ip;
        m_connectPort = _port;
        m_delConnect = delConnect;
        LogModule.DebugLog("Connect to Server... ip:" + m_connectIP + " port :" + m_connectPort.ToString());
        NetWorkLogic.SetConcnectDelegate(m_delConnect);
        NetWorkLogic.GetMe().ConnectToServer(m_connectIP, m_connectPort, 500);
    }
}