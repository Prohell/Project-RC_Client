using System;

public interface ILoader
{
    UnityEngine.Object Load(string path);
    UnityEngine.Object Load(string path, Type type);
    UnityEngine.AsyncOperation LoadAsync(string path);
    UnityEngine.AsyncOperation LoadAsync(string path, Type type);
    void UnloadAsset(UnityEngine.Object assetToUnload);
    void UnloadUnusedAssets();
}