using UnityEngine;
using UnityEngine.UI;

public class MapEditorBtn : MonoBehaviour
{
    public GameObject editorBtn;
    public InputField ip;
    public InputField port;

    void Start()
    {
#if !UNITY_EDITOR
        editorBtn.SetActive(false);
#endif
    }

    public void SwitchToEditScene()
    {
        Game.SceneManager.SwitchToScene(SceneId.MapEditor);
    }

    public void SwitchToMapScene()
    {
        Game.SceneManager.SwitchToScene(SceneId.Map);
    }

    public void ConnectToServer()
    {
        NetManager.GetInstance().ConnectToServer(ip.text, int.Parse(port.text), OnConnectResult);
    }

    void OnConnectResult(bool bSuccess, string result)
    {
        LogModule.DebugLog(result);
        if (bSuccess)
        {
            LogModule.DebugLog("connect success");
            SwitchToMapScene();
        }
        else
        {
            LogModule.WarningLog("connect fail");
        }
    }
}