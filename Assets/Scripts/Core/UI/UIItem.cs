using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class UIItem
{
    public string Name;
    public string CategoryName;
    public string BundlePath;
    public int AllocateDepth;
    public GameObject RootObject;
    public GameObject UIObject;
    public UIViewBase ViewBase;
    public string ViewName;
    public LuaTable View;
    public object Mediator;
    public int PanelDepth;

    public bool IsShowing
    {
        get { return isShowing; }
        set
        {
            if (isShowing != value)
            {
                isShowing = value;
                OnShowingChange(isShowing);
            }
        }
    }
    private bool isShowing;

    public Dictionary<string, float> UIStateMap
    {
        get { return uiStateMap; }
        set { uiStateMap = value; }
    }

    private Dictionary<string, float> uiStateMap = new Dictionary<string, float>();

    private void OnShowingChange(bool isShowing)
    {
        if (!isShowing)
        {
            UIManager.GetInstance().GetCategory(CategoryName).OnUIClose(this);
        }
        else
        {
            UIManager.GetInstance().GetCategory(CategoryName).OnUIOpen(this);
        }
    }
}
