using UnityEngine;

/// <summary>
/// 资源管理器
/// 封装Resources、Asset两种资源使用方式
/// by TT
/// 2016-07-14
/// </summary>
public class AssetManager : Singleton<AssetManager>
{
    public delegate void LoadSuccessHandler<T>(T obj) where T : Object;
    ILoader mLoader = new ResourceLoader();

    public void LoadAsset<T>(string path, LoadSuccessHandler<T> callback) where T : Object
    {
        Object obj = mLoader.Load(path);
        if (obj != null)
        {
            callback.Invoke(obj as T);
        }
        else
        {
            callback(null);
            LogModule.ErrorLog(path + "can not be load.");
        }
    }

    public Object LoadAsset(string path)
    {
        return mLoader.Load(path);
    }

    public void UnloadAsset(Object assetToUnload)
    {
        mLoader.UnloadAsset(assetToUnload);
    }
}