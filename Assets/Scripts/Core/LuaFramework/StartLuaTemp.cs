using System;
using UnityEngine;
using System.Collections;

public class StartLuaTemp : MonoBehaviour
{
    public void StartLua()
    {
        Game.SceneManager.SwitchToScene(SceneId.LuaTest);
    }
}
