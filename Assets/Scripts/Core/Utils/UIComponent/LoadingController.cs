using System;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class LoadingController : Singleton<LoadingController>, IReset, IDestroy
{
    public bool IsShowing;
    public int CurrentNum { get; private set; }
    public int TotalNum { get; private set; }

    private GameObject loadingPrefab;
    private LoadingView m_loadingView;
    private Action m_loadingDone;

    public const string loadingAssetsText = "正在更新资源...";
    public const string initText = "正在初始化游戏...";

    public void Show(int totalNum, Action loadingDone = null)
    {
        TotalNum = totalNum;
        IsShowing = true;
        m_loadingDone = loadingDone;

        if (loadingPrefab != null)
        {
            ShowInternal(loadingPrefab);
        }
        else
        {
            ShowInternal(Resources.Load<GameObject>("Build/Load_Preload/S_UI/UI_Loading"));
            //CM_Dispatcher.instance.StartCoroutine(
            //    GameAssets.LoadAssetAsync<GameObject>("load_preload$s_ui$ui_loading.assetbundle", "UI_Loading",
            //        ShowInternal));
        }
    }

    private void ShowInternal(GameObject prefab)
    {
        var loadingBar = Object.Instantiate(prefab);
        Utils.StandardizeObject(loadingBar);
        m_loadingView = loadingBar.GetComponent<LoadingView>();
    }

    public void UpdateProgress(int num = 1, string text = null)
    {
        if (m_loadingView != null)
        {
            CurrentNum += num;
            var progress = (float)CurrentNum / TotalNum;
            m_loadingView.Set(progress, text);

            if (progress >= 1 && m_loadingDone != null)
            {
                Hide();
                m_loadingDone();
            }
        }
    }

    public void Hide()
    {
        Object.Destroy(m_loadingView.gameObject);

        OnReset();
    }

    public void OnReset()
    {
        IsShowing = false;
        TotalNum = 0;
        loadingPrefab = null;
        m_loadingView = null;
    }

    public void OnDestroy()
    {
        OnReset();
    }
}
