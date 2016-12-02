using UnityEngine;
using System.Collections;
using LuaInterface;

public class BagWindow
{
    public GameObject m_BagWindowObject;

    public BagModel m_BagModel;
    public LuaTable m_BagController;
    public LuaTable m_BagView;

    public void Init()
    {
        if (m_BagWindowObject == null)
        {
            var prefab = LuaHelper.LoadGB("LuaTest/BagWindow");
            m_BagWindowObject = Object.Instantiate(prefab);
        }

        m_BagModel = new BagModel();
        m_BagView = LuaHelper.GetComponent(m_BagWindowObject, "BagView");
        m_BagController = (LuaTable)LuaHelper.CallStaticFunction("BagController.New")[0];

        m_BagModel.Init(m_BagController);
        LuaHelper.CallFunction(m_BagView, "BagView:Init", m_BagController);
        LuaHelper.CallFunction(m_BagController, "BagController:Init", m_BagModel, m_BagView);

        m_BagModel.StartUpdateData();
    }
}
