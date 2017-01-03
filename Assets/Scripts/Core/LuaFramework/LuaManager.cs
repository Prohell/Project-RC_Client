using System;
using UnityEngine;
using System.Collections;
using System.IO;
using LuaInterface;

public class LuaManager : Mono_Singleton<LuaManager>, IInit, IReset, IUpdate, IDestroy
{
    public LuaState LuaState { get; private set; }

    private LuaLooper loop = null;
    public LuaLooper GetLooper()
    {
        return loop;
    }

    void OpenLibs()
    {
        LuaState.OpenLibs(LuaDLL.luaopen_pb);
        LuaState.OpenLibs(LuaDLL.luaopen_struct);
        LuaState.OpenLibs(LuaDLL.luaopen_lpeg);

#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        LuaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

        if (LuaConst.openLuaSocket)
        {
            OpenLuaSocket();
        }

        if (LuaConst.openZbsDebugger)
        {
            OpenZbsDebugger();
        }
    }

    protected void OpenLuaSocket()
    {
        LuaConst.openLuaSocket = true;

        LuaState.BeginPreLoad();
        LuaState.RegFunction("socket.core", LuaOpen_Socket_Core);
        LuaState.RegFunction("mime.core", LuaOpen_Mime_Core);
        LuaState.EndPreLoad();
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {
        return LuaDLL.luaopen_socket_core(L);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }

    public void OpenZbsDebugger(string ip = "localhost")
    {
        if (!Directory.Exists(LuaConst.zbsDir))
        {
            Debugger.LogWarning("ZeroBraneStudio not install or LuaConst.zbsDir not right");
            return;
        }

        if (!LuaConst.openLuaSocket)
        {
            OpenLuaSocket();
        }

        //if (!string.IsNullOrEmpty(LuaConst.zbsDir))
        //{
        //    LuaState.AddSearchPath(LuaConst.zbsDir);
        //}

        //Cancel start debug at start of app.
        //LuaState.LuaDoString(string.Format("DebugServerIp = '{0}'", ip));
    }

    //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
    protected void OpenCJson()
    {
        LuaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
        LuaState.OpenLibs(LuaDLL.luaopen_cjson);
        LuaState.LuaSetField(-2, "cjson");

        LuaState.OpenLibs(LuaDLL.luaopen_cjson_safe);
        LuaState.LuaSetField(-2, "cjson.safe");
    }

    protected void StartLooper()
    {
        loop = gameObject.AddComponent<LuaLooper>();
        loop.luaState = LuaState;
    }

    public void OnInit()
    {
        LuaState = new LuaState();
        this.OpenLibs();
        LuaState.LuaSetTop(0);

        LuaBinder.Bind(LuaState);
        LuaCoroutine.Register(LuaState, this);

#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(LuaConst.luaDir))
        {
            foreach (var subDir in Directory.GetDirectories(LuaConst.luaDir, "*", SearchOption.AllDirectories))
            {
                LuaState.AddSearchPath(subDir);
            }
        }
#endif
        LuaState.Start();

        LuaHelper.CallFunction("Main.Init");
    }

    public void OnReset()
    {

    }

    public void OnUpdate()
    {

    }

    public new void OnDestroy()
    {
        base.OnDestroy();
    }
}
