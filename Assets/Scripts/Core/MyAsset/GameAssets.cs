using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class GameAssets
{

    static public IEnumerator Init()
    {
        //初始化 资源加载器
        yield return AssetLoadManager.Init();
        //加载资源信息文件
        yield return GameUtility.LoadAssetsInfo();
        //加载Bundle信息文件
        if (Configs.clientConfig.isReleaseBundle)
        {
            yield return GameUtility.LoadBundlesInfo();
        }
        //加载所有配置文件
        yield return Configs.LoadAllConfigs();
        //加载 热更策略文件
        yield return LoadAllPolicys();


        //加载 所有后续需要处理的资源
        yield return DownloadAllAssets();
    }

    static private IEnumerator LoadAllPolicys()
    {
        yield return AssetLoadManager.LoadAssetAsync<LoadPolicys>("load_first$loadfirst.assetbundle", "LoadPolicys", (asset) =>
        {
            Configs.loadPolicys = asset;
        });

        SubBundleRef("load_first$loadfirst.assetbundle");
    }

    static public IEnumerator DownloadAllAssets()
    {
        //下载 所有预加载资源
        yield return DownloadBundlesByPolicy("load_preload");
        //加载 所有表文件并缓存
        yield return LoadAndSaveTables();
        //
        yield return LoadAndSaveLuaScripts();
    }


    //加载 并转存所有表格
    static private IEnumerator LoadAndSaveTables()
    {
        LoadPolicy policy = GetPolicy("load_table");
        if (policy.list != null && policy.list.Count > 0)
        {
            string filePath = Application.persistentDataPath + "/Tables";
            if (!System.IO.Directory.Exists(filePath))
            {
                System.IO.Directory.CreateDirectory(filePath);
            }
            //正式版 和Develop模式表的分别处理
            if (Configs.clientConfig.isReleaseBundle)
            {
                for (int i = 0; i < policy.list.Count; i++)
                {
                    //表是没有依赖的 所以只取第一个
                    yield return AssetLoadManager.LoadAssetBundle(policy.list[i], (bundle) =>
                    {
                        TextAsset[] assets = bundle.LoadAllAssets<TextAsset>();
                        string fileName = assets[0].name + ".txt";
                        File.WriteAllText(filePath + "/" + fileName, assets[0].text);
                    });
                }
                yield return null;
            }
            else
            {
                List<AssetInfo> list = new List<AssetInfo>();
                for (int i = 0; i < policy.list.Count; i++)
                {
                    GetAllAssetByBundleName(policy.list[i], list);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    TextAsset ta = Resources.Load<TextAsset>(list[i].path);
                    string fileName = ta.name + ".txt";
                    File.WriteAllText(filePath + "/" + fileName, ta.text);
                }
                yield return null;
            }
        }
    }

    //
    static private IEnumerator LoadAndSaveLuaScripts()
    {
        string filePath = string.Format("{0}/{1}/", Application.persistentDataPath, "Lua");
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        string bundleName = "load_preload$g_lua$lua.assetbundle";
        //正式版 和Develop的分别处理
        if (Configs.clientConfig.isReleaseBundle)
        {
            yield return AssetLoadManager.LoadAssetBundle(bundleName, (bundle) =>
            {
                TextAsset[] textAssets = bundle.LoadAllAssets<TextAsset>();
                for (int i = 0; i < textAssets.Length; i++)
                {
                    string fileName = textAssets[i].name + ".txt";
                    File.WriteAllText(filePath + "/" + fileName, textAssets[i].text);
                }
            });
            yield return null;
        }
        else
        {
            List<AssetInfo> list = new List<AssetInfo>();
            GetAllAssetByBundleName(bundleName, list);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    TextAsset ta = Resources.Load<TextAsset>(list[i].path);
                    string fileName = ta.name + ".txt";
                    File.WriteAllText(filePath + "/" + fileName, ta.text);
                }
            }
            yield return null;
        }
    }

    static public void GetAllAssetByBundleName(string str, List<AssetInfo> list)
    {
        if (GameUtility.assetsInfo != null)
        {
            for (int i = 0; i < GameUtility.assetsInfo.assetList.Count; i++)
            {
                if (GameUtility.assetsInfo.assetList[i].bundleName == str)
                {
                    list.Add(GameUtility.assetsInfo.assetList[i]);
                }
            }
        }
    }

    //获取指定名称的 加载策略文件
    static private LoadPolicy GetPolicy(string str)
    {
        LoadPolicy ip = null;
        for (int i = 0; i < Configs.loadPolicys.policys.Count; i++)
        {
            if (Configs.loadPolicys.policys[i].policyName == str)
            {
                ip = Configs.loadPolicys.policys[i];
                break;
            }
        }
        return ip;
    }

    //下载指定 “加载策略文件” 中的所有bundle
    static public IEnumerator DownloadBundlesByPolicy(string str)
    {
        LoadPolicy policy = GetPolicy(str);
        if (policy != null)
        {
            yield return DownloadBundlesByPolicy(policy);
        }
        yield break;
    }

    //下载指定 “加载策略文件” 中的所有bundle
    static public IEnumerator DownloadBundlesByPolicy(LoadPolicy policy)
    {
        if (policy.list != null && policy.list.Count > 0)
        {
            List<string> needLoads = new List<string>();
            for (int i = 0; i < policy.list.Count; i++)
            {
                AssetLoadManager.GetBundleNeedLoads(policy.list[i], needLoads);
            }

            for (int i = 0; i < needLoads.Count; i++)
            {
                yield return AssetLoadManager.LoadAssetBundle(needLoads[i], (bundle) =>
                {
                    AssetLoadManager.Unload(bundle.name, true);
                });
            }
        }
    }

    //下载并缓存指定 “加载策略文件” 中的所有bundle
    static public IEnumerator LoadBundlesByPolicy(string str)
    {
        LoadPolicy policy = GetPolicy(str);
        if (policy.list != null && policy.list.Count > 0)
        {
            for (int i = 0; i < policy.list.Count; i++)
            {
                yield return AssetLoadManager.LoadAssetBundle(policy.list[i]);
            }
        }
    }


    //卸载指定 “加载策略文件” 中的所有bundle
    static public void UnloadBundlesByPolicy(string str, bool unloadAllLoadedObject)
    {
        LoadPolicy policy = GetPolicy(str);
        if (policy.list != null && policy.list.Count > 0)
        {
            for (int i = 0; i < policy.list.Count; i++)
            {
                AssetLoadManager.Unload(policy.list[i], unloadAllLoadedObject);
            }
        }
    }


    //获取指定加载策略文件中所有需要从远端下载更新的文件大小
    static public long GetNeedDownByPolicy(string str)
    {
        long totalSize = 0;
        LoadPolicy policy = GetPolicy(str);
        List<string> bundleNames = new List<string>();
        for (int i = 0; i < policy.list.Count; i++)
        {
            AssetLoadManager.GetBundleNeedLoads(policy.list[i], bundleNames);
        }
        if (bundleNames.Count > 0)
        {
            for (int i = 0; i < bundleNames.Count; i++)
            {
                totalSize += AssetLoadManager.GetBundleSize(bundleNames[i]);
            }
        }

        return totalSize;
    }

    static public IEnumerator LoadAssetBundle(string assetBundleName, Action<AssetBundle> callback = null)
    {
        yield return AssetLoadManager.LoadAssetBundle(assetBundleName, callback);
    }
    static public IEnumerator LoadAssetAsync<T>(string assetBundleName, string assetName, Action<T> callback) where T : UnityEngine.Object
    {
        yield return AssetLoadManager.LoadAssetAsync<T>(assetBundleName, assetName, callback);
    }
    static public void AddBundleRef(string bundleName)
    {
        AssetLoadManager.AddBundleRef(bundleName);
    }
    static public void SubBundleRef(string bundleName)
    {
        AssetLoadManager.SubBundleRef(bundleName);
    }
    static public void UnloadAll(bool unloadAllLoadedObject = true)
    {
        AssetLoadManager.UnloadAll(unloadAllLoadedObject);
    }
    static public void Unload(string bundleName, bool unloadAllLoadedObject = true)
    {
        AssetLoadManager.Unload(bundleName, unloadAllLoadedObject);
    }

    //反向查找资源并缓存加载
    static public IEnumerator LoadAssetAsyncByFileName<T>(string fileName, Action<T> callback) where T : UnityEngine.Object
    {
        List<AssetInfo> infos = new List<AssetInfo>();

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < GameUtility.assetsInfo.assetList.Count; i++)
        {
            strBuilder.Remove(0, strBuilder.Length);
            strBuilder.Append(GameUtility.assetsInfo.assetList[i].name);
            strBuilder.Append(".");
            strBuilder.Append(GameUtility.assetsInfo.assetList[i].suffix);
            if (strBuilder.ToString() == fileName)
            {
                //冗余资源的反向查找未支持,找到一个Bundle就直接Break
                infos.Add(GameUtility.assetsInfo.assetList[i]);
                fileName = GameUtility.assetsInfo.assetList[i].name;
                if (!GameUtility.assetsInfo.assetList[i].multiple)
                {
                    break;
                }
            }
        }

        if (infos.Count == 0)
        {
#if UNITY_EDITOR
            Debug.LogError("Not find file from AssetsInfo ：" + fileName);
#endif
        }
        else if (infos.Count == 1)
        {
            yield return AssetLoadManager.LoadAssetAsync<T>(infos[0].bundleName, fileName, callback);
        }
        else if (infos.Count > 1)
        {
#if UNITY_EDITOR
            Debug.LogError("冗余资源的反向查找未支持!");
#endif
        }
        yield break;
    }
}
