using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using Random = System.Random;

public class BagModel
{
    public LuaTable m_BagController;

    public DelegateUtil.VoidDelegate OnUpdateData;

    private Random random = new Random();

    public string TestString = "FFF";

    public void Init(LuaTable window)
    {
        m_BagController = window;
    }

    public void StartUpdateData()
    {
        CM_Job.Make(UpdateData()).Start();
    }

    private IEnumerator UpdateData()
    {
        yield return new WaitForSeconds(3);

        var newList = new List<int>();

        for (int i = 0; i < 12; i++)
        {
            newList.Add(random.Next(100));
        }

        //m_BagController["BagItemDataList"] = newList;
        m_BagController["BagLabelText"] = newList[0];

        LuaHelper.CallFunction(m_BagController, "BagController:UpdateView");
        //OnUpdateData();

        CM_Job.Make(UpdateData()).Start();
    }
}
