using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 游戏设置
/// by TT
/// 2016-07-04
/// </summary>
public struct GameSettings
{
    /// <summary>
    /// 是否是单机模式
    /// </summary>
    public readonly static bool IsStandRun = false;
    //asset 文件夹地址
    private static string mDataPath = "";
    // 持久数据路径 Application.persistentDataPath 有读写权限
    private static string mPersistentDataPath = "";
    // streaming  
    private static string mStreamingAssetsPath = "";
    //临时文件夹地址
    private static string mTempPath = "";

    private static bool mIsInit = false;

    public static bool SceneGestureEnabled = true;

    public static void OnInit()
    {
        if (mIsInit)
        {
            return;
        }
        mIsInit = true;
        //设置游戏帧频
        Application.targetFrameRate = 30;
        //垂直同步
        QualitySettings.vSyncCount = 0;
        //设置使用的质量
        QualitySettings.SetQualityLevel(4);
        //是否在后台运行
        Application.runInBackground = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.AutoRotation;
        //asset 文件夹地址
        mDataPath = Application.dataPath;
        // 持久数据路径 
        mPersistentDataPath = Application.persistentDataPath;
        //streaming  
        mStreamingAssetsPath = Application.streamingAssetsPath;
        //临时文件夹地址
        mTempPath = Application.temporaryCachePath;
    }

    #region Paths
    public static string TempPath
    {
        get
        {
            return mTempPath;
        }
    }
    public static string StreamingAssetsPath
    {
        get
        {
            return mStreamingAssetsPath;
        }
    }
    public static string DataPath
    {
        get
        {
            return mDataPath;
        }
    }
    public static string PersistentDataPath
    {
        get
        {
            return mPersistentDataPath;
        }
    }
    #endregion
}
