using System;
using UnityEngine;
using System.Collections;
using LuaInterface;

public class LuaManager : Mono_Singleton<LuaManager>, IInit, IReset, IUpdate, IDestroy
{
    private string LuaFileFolder = "";

    public LuaState LuaState { get; private set; }

    void OpenLibs()
    {
        LuaState.OpenLibs(LuaDLL.luaopen_pb);
        LuaState.OpenLibs(LuaDLL.luaopen_lpeg);
        LuaState.OpenLibs(LuaDLL.luaopen_bit);
        LuaState.OpenLibs(LuaDLL.luaopen_socket_core);
    }

    public void OnInit()
    {
        LuaFileFolder = Application.dataPath + "/Lua/Scripts";

        LuaState = new LuaState();
        this.OpenLibs();
        LuaState.LuaSetTop(0);

        LuaBinder.Bind(LuaState);
        LuaCoroutine.Register(LuaState, this);

#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(LuaFileFolder))
        {
            LuaState.AddSearchPath(LuaFileFolder);
        }
#endif
        LuaState.Start();
    }

    public void OnReset()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnDestroy()
    {

    }
}
