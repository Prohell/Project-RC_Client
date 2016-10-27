using UnityEngine;
using System;

public class ResourceLoader : ILoader
{
    public UnityEngine.Object Load(string path)
    {
        return Resources.Load(path);
    }

    public UnityEngine.Object Load(string path, Type type)
    {
        return Resources.Load(path, type);
    }

    public AsyncOperation LoadAsync(string path)
    {
        return Resources.LoadAsync(path);
    }

    public AsyncOperation LoadAsync(string path, Type type)
    {
        return Resources.LoadAsync(path, type);
    }

    public void UnloadAsset(UnityEngine.Object assetToUnload)
    {
        Resources.UnloadAsset(assetToUnload);
    }

    public void UnloadUnusedAssets()
    {
        Resources.UnloadUnusedAssets();
    }
}
