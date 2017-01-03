using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LoadingController : MonoBehaviour, IInit
{
    public Slider progressSlider;
    public Text progressText;

    public const string initBaseText = "正在初始化游戏...";
    public const string updateAssetsText = "正在更新资源...";
    public const string loadAssetsText = "正在加载资源...";

    public void OnInit()
    {
        Game.StartCoroutine(UpdateProgress());
    }

    public void OnLoadingDone()
    {
        Destroy(gameObject);

        ProxyManager.GetInstance().Add(new LoginProxy());
        UIManager.GetInstance().OpenUI("Login", (view) =>
        {
            LuaHelper.CallFunctionWithSelf(view, "LoginView.Init");
        });
    }    

    public IEnumerator UpdateProgress()
    {
        while (true)
        {
            switch (Game.initState)
            {
                case GameInitState.Uninitialized:
                    {
                        progressSlider.value = 0.25f;
                        progressText.text = initBaseText;
                        break;
                    }
                case GameInitState.GameBaseInited:
                    {
                        progressSlider.value = 0.5f;
                        progressText.text = updateAssetsText;
                        break;
                    }
                case GameInitState.GameAssetsInited:
                    {
                        progressSlider.value = 0.75f;
                        progressText.text = loadAssetsText;
                        break;
                    }
                case GameInitState.Initialized:
                    {
                        OnLoadingDone();
                        yield break;
                    }
                default:
                    {
                        LogModule.ErrorLog("GameInitState: {0} not defined", Game.initState);
                        yield break;
                    }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void Awake()
    {
        OnInit();
    }
}
