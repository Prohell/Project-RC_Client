using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class MySceneManager : MonoBehaviour, IInit, IReset
{
    private Dictionary<SceneId, Type> mSceneTypeDict = new Dictionary<SceneId, Type>();
    private SceneId mCurrentSceneId;
    public SceneId CurrentSceneId { get { return mCurrentSceneId; } }
    private SceneBase mCurScene = null;
    private SceneBase mNextScene = null;
    private object mParam = null;
    public delegate void SceneSwitchHandler(SceneId oriScene, SceneId curScene, object param);
    public event SceneSwitchHandler hdlSceneWillSwitch;

    void RegisterScene(SceneId sceneId, Type sceneType)
    {
        if(!mSceneTypeDict.ContainsKey(sceneId))
        {
            mSceneTypeDict.Add(sceneId, sceneType);
        }
    }

    public void OnInit()
    {
        mCurrentSceneId = Parse(SceneManager.GetActiveScene().name);
        // add new scene here.
        RegisterScene(SceneId.MapEditor, typeof(SceneMapEditor));
        RegisterScene(SceneId.Map, typeof(SceneMap));
        RegisterScene(SceneId.LuaTest, typeof(SceneLuaTest));
    }

    public void SwitchToScene(SceneId sceneId, object param = null)
    {
        if (sceneId == CurrentSceneId)
        {
            LogModule.WarningLog("switch to current scene: " + sceneId);
        }
        LogModule.DebugLog("SceneMgr  SwitchToScene  sceneId:" + sceneId.ToString() + "  " + System.DateTime.Now.ToString());
        mParam = param;

        if (mCurScene != null)
        {
            mCurScene.OnWillExit();
            //App.EventMgr.Post(EventId.SceneWillExit, mCurScene.Id);
        }

        if (mSceneTypeDict.ContainsKey(sceneId))
        {
            mNextScene = Activator.CreateInstance(mSceneTypeDict[sceneId]) as SceneBase;
            if (mNextScene != null)
            {
                mNextScene.OnWillEnter(param);
            }
        }

        if (hdlSceneWillSwitch != null)
        {
            hdlSceneWillSwitch.Invoke(mCurrentSceneId, sceneId, param);
        }

        SceneManager.LoadScene(sceneId.ToString());
    }

    // API function
    void OnLevelWasLoaded(int level)
    {
        mCurrentSceneId = Parse(SceneManager.GetActiveScene().name);

        if (mCurScene != null)
        {
            mCurScene.OnExited();
        }

        if (mNextScene != null)
        {
            mNextScene.OnEntered(mParam);
        }

        mCurScene = mNextScene;
        mNextScene = null;

        //GameFacade.SendEvent(EventId.SceneSwitched, new Tupple<SceneId, System.Object>(mCurrentSceneId, _param));
    }

    SceneId Parse(string levelName)
    {
        return (SceneId)Enum.Parse(typeof(SceneId), levelName);
    }

    public void OnReset()
    {
        if (mCurScene != null)
        {
            mCurScene.OnWillExit();
        }
    }
}
