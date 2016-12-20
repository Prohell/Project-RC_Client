using UnityEngine;
using System.Collections;

public class TempEntrance : Mono_Singleton<TempEntrance>
{
    public void SwitchToEditScene()
    {
        Game.SceneManager.SwitchToScene(SceneId.MapEditor);
    }

    public void SwitchToMapScene()
    {
        Game.SceneManager.SwitchToScene(SceneId.Map);
    }

    public void SwitchToCastle()
    {
        Game.SceneManager.SwitchToScene(SceneId.Loading, SceneId.Castle);
    }

    public void StartLua()
    {
        Game.SceneManager.SwitchToScene(SceneId.LuaTest);
    }

    public void StartBattle()
    {
        Game.SceneManager.SwitchToScene(SceneId.BattleTest);
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
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
