using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using Random = System.Random;

public class LuaTestProxy : IProxy
{
    public DelegateUtil.VoidDelegate OnUpdateData;

    private Random random = new Random();

    public List<int> BagItemDataList = new List<int>();

    public void StartUpdateData()
    {
		Game.StartCoroutine(UpdateData());
    }

    private IEnumerator UpdateData()
    {
        yield return new WaitForSeconds(3);

        BagItemDataList.Clear();
        for (int i = 0; i < 12; i++)
        {
            BagItemDataList.Add(random.Next(100));
        }

        EventManager.GetInstance().SendEvent(EventId.LuaTestUpdate, null);

		Game.StartCoroutine(UpdateData());
    }

    public void OnDestroy()
    {

    }

    public void OnInit()
    {
        StartUpdateData();
    }
}
