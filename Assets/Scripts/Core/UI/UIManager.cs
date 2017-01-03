using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LuaInterface;
using Object = UnityEngine.Object;

public partial class UIManager : Singleton<UIManager>, IInit, IDestroy, IReset
{
    private GameObject UI_Root;

    public void MoveUIItemDepth(string p_uiItem, int p_targetDepth)
    {
        var rootPanel = m_UIItemMap[p_uiItem].RootObject.GetComponent<UIPanel>();
        int originalDepth = rootPanel.depth;
        rootPanel.depth = p_targetDepth;
        m_UIItemMap[p_uiItem].PanelDepth = p_targetDepth;

        Utils.SetUILayer(m_UIItemMap[p_uiItem].UIObject, p_targetDepth - originalDepth);
    }

    public void DestroyCache(string p_uiItem)
    {
        DestroyUI(p_uiItem);
    }

    private Dictionary<string, UICategory> m_UICategoryMap = new Dictionary<string, UICategory>();
    private Dictionary<string, UIItem> m_UIItemMap = new Dictionary<string, UIItem>();

    public UICategory GetCategory(string key)
    {
        UICategory value;
        m_UICategoryMap.TryGetValue(key, out value);

        return value;
    }

    public UIItem GetItem(string key)
    {
        UIItem value;
        m_UIItemMap.TryGetValue(key, out value);

        return value;
    }

    #region Open/Close

    /// <summary>
    /// Get config and Open UI.
    /// </summary>
    /// <param name="p_name">name you defined</param>
    /// <param name="p_viewInitCallBack"></param>
    /// <param name="p_viewRefreshCallBack"></param>
    /// <param name="isShowAnimation"></param>
    public void OpenUI(string p_name, DelegateUtil.TableDelegate p_viewInitCallBack = null, DelegateUtil.TableDelegate p_viewRefreshCallBack = null, bool isShowAnimation = true)
    {
        try
        {
            if (!m_uiItemConfigMap.ContainsKey(p_name))
            {
                throw new KeyNotFoundException(string.Format("UIItem key: {0} not found in config", p_name));
            }

            var config = m_uiItemConfigMap[p_name];

            OpenUIInternal(config, p_viewInitCallBack, p_viewRefreshCallBack, isShowAnimation);
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in open UI, {0}\n{1}", e.Message, e.StackTrace);
            return;
        }
    }

    /// <summary>
    /// Open a UI, will create one if no cache exist
    /// </summary>
    /// <param name="p_type">UI category: fullscreen, window, popup, etc</param>
    /// <param name="p_name">name you defined</param>
    /// <param name="p_mediatorType"></param>
    /// <param name="p_bundlePath">prefab to instantiate</param>
    /// <param name="p_viewInitCallBack"></param>
    /// <param name="p_viewRefreshCallBack"></param>
    /// <param name="p_allocateDepth">allocate several panel depth to ui</param>
    /// <param name="p_viewName"></param>
    /// <param name="isShowAnimation"></param>
    public bool OpenUIInternal(UIItemConfig p_config, DelegateUtil.TableDelegate p_viewInitCallBack = null, DelegateUtil.TableDelegate p_viewRefreshCallBack = null, bool isShowAnimation = true)
    {
        try
        {
            if (m_UIItemMap.ContainsKey(p_config.Name) && m_UIItemMap[p_config.Name].IsShowing)
            {
                LogModule.WarningLog("Cancel open {0} UI cause already in showing.", p_config.Name);
                //Execute refresh without animation.
                ExeAfterOpenUI(p_config, p_viewRefreshCallBack, false);

                return false;
            }

            if (!m_UICategoryMap[p_config.Type].CanOpenUI)
            {
                m_UICategoryMap[p_config.Type].AddToToShowList(new UICategory.ToShowConfig()
                {
                    ItemConfig = p_config,
                    ViewDelegate = p_viewInitCallBack,
                    RefreshDelegate = p_viewRefreshCallBack,
                    IsShowAnim = isShowAnimation
                });
                return false;
            }

            if (!m_UIItemMap.ContainsKey(p_config.Name))
            {
                CreateUI(p_config, p_viewInitCallBack, p_viewRefreshCallBack, isShowAnimation);
            }
            else
            {
                m_UICategoryMap[p_config.Type].CheckOpenDepth(p_config.Name);

                ExeAfterOpenUI(p_config, p_viewRefreshCallBack, isShowAnimation);
            }

            return true;
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in open UI internal, {0}\n{1}", e.Message, e.StackTrace);
            return false;
        }
    }

    /// <summary>
    /// Open a UI, will create one if no cache exist
    /// </summary>
    /// <param name="p_type">UI category: fullscreen, window, popup, etc</param>
    /// <param name="p_name">name you defined</param>
    /// <param name="p_viewRefreshCallBack"></param>
    /// <param name="isShowAnimation"></param>
    private void ExeAfterOpenUI(UIItemConfig p_config, DelegateUtil.TableDelegate p_viewRefreshCallBack = null, bool isShowAnimation = true)
    {
        try
        {
            switch (m_uiCategoryConfigMap[p_config.Type].MultiPolicy)
            {
                case "Single":
                    {
                        foreach (var item in m_UICategoryMap[p_config.Type].UIItemList.Where(item => item.Name != p_config.Name))
                        {
                            CloseUI(item.Name);
                        }
                        break;
                    }
                case "Overlay":
                    {
                        break;
                    }
                default:
                    {
                        throw new Exception(string.Format("MultiPolicy: {0} not defined.", m_uiCategoryConfigMap[p_config.Type].MultiPolicy));
                    }
            }

            Utils.StandardizeObject(m_UIItemMap[p_config.Name].RootObject);
            m_UIItemMap[p_config.Name].RootObject.SetActive(true);
            m_UIItemMap[p_config.Name].IsShowing = true;

            if (m_UIItemMap[p_config.Name].View != null && p_viewRefreshCallBack != null)
            {
                p_viewRefreshCallBack(m_UIItemMap[p_config.Name].View);
            }

            if (isShowAnimation)
            {
                m_UIItemMap[p_config.Name].ViewBase.OnOpen();
            }
            //Refresh
            else
            {
                if (p_viewRefreshCallBack != null && m_UIItemMap[p_config.Name].View != null)
                {
                    m_UIItemMap[p_config.Name].ViewBase.OnOpenUIComplete += () =>
                    {
                        p_viewRefreshCallBack(m_UIItemMap[p_config.Name].View);
                    };
                }
            }
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in exe after open UI, {0}\n{1}", e.Message, e.StackTrace);
            return;
        }
    }

    /// <summary>
    /// Create a UI
    /// </summary>
    /// <param name="p_type">UI category: fullscreen, window, popup, etc</param>
    /// <param name="p_name">name you defined</param>
    /// <param name="p_mediatorType"></param>
    /// <param name="p_bundlePath">prefab to instantiate</param>
    /// <param name="p_viewInitCallBack"></param>
    /// <param name="p_viewRefreshCallBack"></param>
    /// <param name="p_allocateDepth">allocate several panel depth to ui</param>
    /// <param name="p_viewName"></param>
    /// <param name="isShowAnimation"></param>
    private void CreateUI(UIItemConfig p_config, DelegateUtil.TableDelegate p_viewInitCallBack, DelegateUtil.TableDelegate p_viewRefreshCallBack = null, bool isShowAnimation = true)
    {
        try
        {
            if (!m_UICategoryMap.ContainsKey(p_config.Type))
            {
                throw new KeyNotFoundException(string.Format("Key: {0} not found in UI Category.", p_config.Type));
            }

            if (p_config.BundleName == null)
            {
                throw new Exception("Cannot create UI cause no res provided.");
            }

            Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(p_config.BundleName, p_config.AssetName,
                (prefab) =>
                {
                    CreateUIInternal(p_config, prefab, p_viewInitCallBack,
                        p_viewRefreshCallBack, isShowAnimation);
                }));
            GameAssets.AddBundleRef(p_config.BundleName);
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in create UI, {0}\n{1}", e.Message, e.StackTrace);
            return;
        }
    }

    /// <summary>
    /// Create a UI
    /// </summary>
    /// <param name="p_type">UI category: fullscreen, window, popup, etc</param>
    /// <param name="p_name">name you defined</param>
    /// <param name="p_mediatorType"></param>
    /// <param name="p_prefab">prefab to instantiate</param>
    /// <param name="p_viewInitCallBack"></param>
    /// <param name="p_viewRefreshCallBack"></param>
    /// <param name="p_allocateDepth">allocate several panel depth to ui</param>
    /// <param name="p_viewName"></param>
    /// <param name="isShowAnimation"></param>
    private void CreateUIInternal(UIItemConfig p_config, GameObject p_prefab, DelegateUtil.TableDelegate p_viewInitCallBack, DelegateUtil.TableDelegate p_viewRefreshCallBack = null, bool isShowAnimation = true)
    {
        try
        {
            if (!m_UICategoryMap.ContainsKey(p_config.Type))
            {
                throw new KeyNotFoundException(string.Format("Key: {0} not found in UI Category.", p_config.Type));
            }

            if (p_prefab == null)
            {
                throw new Exception("Cannot create UI cause no res provided.");
            }

            if (m_UICategoryMap[p_config.Type].RemainingDepth <= 0)
            {
                LogModule.WarningLog("Cannot create UI: {0} cause remaining depth of {1} is {2}", p_config.Name, p_config.Type, m_UICategoryMap[p_config.Type].RemainingDepth);
                return;
            }

            //Create UI
            var root = Utils.AddChild(m_UICategoryMap[p_config.Type].RootObject.transform, p_config.Name);
            UIPanel panel = root.AddComponent<UIPanel>();

            var ui = NGUITools.AddChild(root, p_prefab);
            var viewBase = ui.AddComponent<UIViewBase>();
            viewBase.Name = p_config.Name;

            //Initialize UI
            LuaTable view = null;
            object mediator = null;

            if (!string.IsNullOrEmpty(p_config.ViewName))
            {
                view = LuaHelper.GetOutletComponent(ui, p_config.ViewName).m_LuaTable;
                view["UIName"] = p_config.Name;
            }

            if (!string.IsNullOrEmpty(p_config.MediatorType))
            {
                mediator = Activator.CreateInstance(Type.GetType(p_config.MediatorType));
                MediatorManager.GetInstance().Add((IMediator)mediator);
                ((IUIMediator)mediator).m_UIName = p_config.Name;
            }

            if (!string.IsNullOrEmpty(p_config.ViewName) && !string.IsNullOrEmpty(p_config.MediatorType))
            {
                view["Mediator"] = mediator;
                ((IUIMediator)mediator).m_View = view;
            }

            m_UIItemMap.Add(p_config.Name, new UIItem()
            {
                CategoryName = p_config.Type,
                Name = p_config.Name,
                PanelDepth = panel.sortingOrder,
                RootObject = root,
                ViewBase = viewBase,
                ViewName = p_config.ViewName,
                View = view,
                Mediator = mediator,
                UIObject = ui,
                BundlePath = p_config.BundleName
            });

            //Get depth and set.
            int minDepth = m_UICategoryMap[p_config.Type].GetNewItemDepth();
            panel.depth = minDepth;
            int maxDepth = Utils.SetUILayer(ui, minDepth);
            if (maxDepth > 0)
            {
                m_UICategoryMap[p_config.Type].AddItem(m_UIItemMap[p_config.Name], maxDepth - minDepth + 1);
            }
            else
            {
                m_UICategoryMap[p_config.Type].AddItem(m_UIItemMap[p_config.Name], 1);
            }

            //Add delegate
            if (isShowAnimation)
            {
                if (p_viewInitCallBack != null && m_UIItemMap[p_config.Name].View != null)
                {
                    Action removeInitAction = null;
                    Action initAction = () =>
                    {
                        p_viewInitCallBack(m_UIItemMap[p_config.Name].View);
                        removeInitAction();
                    };

                    removeInitAction = () =>
                    {
                        m_UIItemMap[p_config.Name].ViewBase.OnOpenUIComplete -= initAction;
                    };

                    m_UIItemMap[p_config.Name].ViewBase.OnOpenUIComplete += initAction;
                }

                if (p_viewRefreshCallBack != null && m_UIItemMap[p_config.Name].View != null)
                {
                    m_UIItemMap[p_config.Name].ViewBase.OnOpenUIComplete += () =>
                    {
                        p_viewRefreshCallBack(m_UIItemMap[p_config.Name].View);
                    };
                }

                m_UIItemMap[p_config.Name].ViewBase.OnCloseUIComplete += () =>
                {
                    CloseUIInternal(p_config.Name);
                };
            }
            //Init
            else
            {
                if (p_viewInitCallBack != null && m_UIItemMap[p_config.Name].View != null)
                {
                    p_viewInitCallBack(m_UIItemMap[p_config.Name].View);
                }
            }

            ExeAfterOpenUI(p_config, p_viewRefreshCallBack, isShowAnimation);

            return;
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in create UI intrenal, {0}\n{1}", e.Message, e.StackTrace);
            return;
        }
    }

    public bool CloseUI(string p_name)
    {
        return CloseUI(p_name, true);
    }

    /// <summary>
    /// </summary>
    /// <param name="p_name"></param>
    /// <param name="isShowAnimation"></param>
    /// <returns></returns>
    public bool CloseUI(string p_name, bool isShowAnimation)
    {
        try
        {
            if (!m_UIItemMap.ContainsKey(p_name))
            {
                throw new KeyNotFoundException(string.Format("Cannot close UI: {0} cause key not found in UIItemMap",
                    p_name));
            }

            if (m_UIItemMap.ContainsKey(p_name) && !m_UIItemMap[p_name].IsShowing)
            {
                LogModule.WarningLog("Cancel close {0} UI cause already closed.", p_name);
                return false;
            }

            if (isShowAnimation)
            {
                m_UIItemMap[p_name].ViewBase.OnClose();
            }
            else
            {
                CloseUIInternal(p_name);
            }

            return true;
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in close UI, {0}\n{1}", e.Message, e.StackTrace);
            return false;
        }
    }

    private void CloseUIInternal(string p_name)
    {
        m_UIItemMap[p_name].RootObject.SetActive(false);
        m_UIItemMap[p_name].IsShowing = false;
    }

    public bool DestroyUI(string p_name)
    {
        return DestroyUI(p_name, true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p_name"></param>
    /// <returns></returns>
    private bool DestroyUI(string p_name, bool isShowAnimation)
    {
        try
        {
            if (!m_UIItemMap.ContainsKey(p_name))
            {
                throw new KeyNotFoundException(string.Format("Cannot destroy UI: {0} cause key not found in UIItemMap",
                    p_name));
            }

            if (isShowAnimation)
            {
                m_UIItemMap[p_name].ViewBase.OnCloseUIComplete += () =>
                {
                    DestroyUIInternal(p_name);
                };
                if (!CloseUI(p_name, true))
                {
                    DestroyUIInternal(p_name);
                }
            }
            else
            {
                return DestroyUIInternal(p_name);
            }

            return true;
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in destroy UI, {0}\n{1}", e.Message, e.StackTrace);
            return false;
        }
    }

    private bool DestroyUIInternal(string p_name)
    {
        try
        {
            if (!m_UIItemMap.ContainsKey(p_name))
            {
                throw new KeyNotFoundException(string.Format("Cannot destroy UI: {0} cause key not found in UIItemMap",
                    p_name));
            }

            var item = m_UIItemMap[p_name];

            Object.Destroy(item.RootObject);
            if (item.Mediator != null)
            {
                MediatorManager.GetInstance().Remove(item.Mediator.GetType());
            }

            GameAssets.SubBundleRef(m_UIItemMap[p_name].BundlePath);

            m_UICategoryMap[item.CategoryName].RemoveItem(item);
            m_UIItemMap.Remove(p_name);

            return true;
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in destroy UI, {0}\n{1}", e.Message, e.StackTrace);
            return false;
        }
    }

    #endregion

    #region ViewBase

    public void ReportState(string p_name, string p_funcName)
    {
        try
        {
            switch (p_funcName)
            {
                case "OnEnable":
                    {
                        //OpenUI(p_name, null, false);

                        break;
                    }
                case "OnDisable":
                    {
                        //CloseUI(p_name, false);

                        break;
                    }
                case "Destroy":
                    {
                        //DestroyUI(p_name);

                        break;
                    }
            }
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in report UI state, {0}\n{1}", e.Message, e.StackTrace);
            throw;
        }
    }

    #endregion

    #region Config

    private Dictionary<string, UICategoryConfig> m_uiCategoryConfigMap = new Dictionary<string, UICategoryConfig>();
    private Dictionary<string, UIItemConfig> m_uiItemConfigMap = new Dictionary<string, UIItemConfig>();

    private bool LoadConfig()
    {

        try
        {
            if (m_uiCategoryConfigMap == null || !m_uiCategoryConfigMap.Any())
            {
                m_uiCategoryConfigMap = UICategoryConfig.LoadConfig();
            }

            if (m_uiItemConfigMap == null || !m_uiItemConfigMap.Any())
            {
                m_uiItemConfigMap = UIItemConfig.LoadConfig();
            }
        }
        catch (Exception e)
        {
            LogModule.ErrorLog("Exception in load UICategoryConfig, {0}\n{1}", e.Message, e.StackTrace);
            return false;
        }
        return true;
    }

    #endregion

    private IEnumerator InitUIFramework()
    {
        yield return GameAssets.LoadAssetAsync<GameObject>("load_preload$s_ui$ui_root.assetbundle", "UI_Root", InitUIFrameworkInternal);
    }

    private void InitUIFrameworkInternal(GameObject prefab)
    {
        UI_Root = Object.Instantiate(prefab);

        foreach (var pair in m_uiCategoryConfigMap)
        {
            var ins = Utils.AddChild(UI_Root.transform, pair.Key);
            var category = new UICategory()
            {
                Name = pair.Key,
                RootObject = ins,
                m_Config = pair.Value
            };
            category.Init();

            m_UICategoryMap.Add(pair.Key, category);
        }

        Object.DontDestroyOnLoad(UI_Root);
    }

    public float WidthInCoor { get; private set; }
    public float HeightInCoor { get; private set; }

    private const float FixedWidth = 2048;
    private const float FixedHeight = 1536;

    private void AutoResize()
    {
        if (WidthInCoor > 0 && HeightInCoor > 0)
        {
            return;
        }

        bool isResetWidth = Screen.width / FixedWidth > Screen.height / FixedHeight;
        float ratio = Math.Min(Screen.width / FixedWidth, Screen.height / FixedHeight);

        if (isResetWidth)
        {
            HeightInCoor = FixedHeight;
            WidthInCoor = Screen.width / ratio;
        }
        else
        {
            HeightInCoor = Screen.height / ratio;
            WidthInCoor = FixedWidth;
        }
    }

    public void OnInit()
    {
        Game.StartCoroutine(InitUIManager());
    }

    public IEnumerator InitUIManager()
    {
        if (LoadConfig())
        {
            yield return InitUIFramework();
        }

        AutoResize();
    }

    public void OnDestroy()
    {
        m_UICategoryMap.Clear();
        m_UIItemMap.Clear();

        Object.Destroy(UI_Root);

        mInstance = null;
    }

    public void OnReset()
    {
        m_UICategoryMap.Clear();

        foreach (var pair in m_UIItemMap)
        {
            Object.Destroy(pair.Value.RootObject);
        }
        m_UIItemMap.Clear();
    }
}
