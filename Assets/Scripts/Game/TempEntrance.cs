using UnityEngine;
using System.Collections;

public class TempEntrance : MonoBehaviour
{
    public void SwitchToEditScene()
    {
        MySceneManager.GetInstance().SwitchToScene(SceneId.MapEditor);
    }

    public void SwitchToMapScene()
    {
        MySceneManager.GetInstance().SwitchToScene(SceneId.Map);
    }

    public void SwitchToCastle()
    {
        MySceneManager.GetInstance().SwitchToScene(SceneId.Loading, SceneId.Castle);
    }

    public void StartLua()
    {
        MySceneManager.GetInstance().SwitchToScene(SceneId.LuaTest);
    }

    public void StartBattle()
    {
        MySceneManager.GetInstance().SwitchToScene(SceneId.BattleTest);
    }

    public void ShowMainUI()
    {
        UIManager.GetInstance().OpenUI("MainUI", view =>
        {
            LuaHelper.CallFunctionWithSelf(view, "MainUIView.Init");
        }, null, false);
        UIManager.GetInstance().OpenUI("MainResource", null, null, false);
    }

    void OnGUI()
    {
        if (GUILayout.Button("MapEditor"))
        {
            SwitchToEditScene();
        }

        if (GUILayout.Button("StartLua"))
        {
            StartLua();
        }

        if (GUILayout.Button("StartBattle"))
        {
            StartBattle();
        }

        if (GUILayout.Button("SwitchToCastle"))
        {
            SwitchToCastle();
        }

        if (GUILayout.Button("ShowMainUI"))
        {
            ShowMainUI();
        }

        if (GUILayout.Button("StartLuaDebugger"))
        {
            LuaHelper.CallFunction("LuaScriptHelper.StartDebugger");
        }
    }
}
