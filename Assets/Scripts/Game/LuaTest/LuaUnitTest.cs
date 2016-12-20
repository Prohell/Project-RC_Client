using UnityEngine;
using System.Collections;

public class LuaUnitTest : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Show pop 1"))
        {
            UIManager.GetInstance().OpenUI("LuaTest1", (table) => { LuaHelper.CallFunction(table, "LuaTestView:Init"); },
                (table) => { LuaHelper.CallFunction(table, "LuaTestView:Refresh"); });
        }

        if (GUILayout.Button("Show pop 2"))
        {
            UIManager.GetInstance().OpenUI("LuaTest2", (table) => { LuaHelper.CallFunction(table, "LuaTestView:Init"); },
                (table) => { LuaHelper.CallFunction(table, "LuaTestView:Refresh"); });
        }

        if (GUILayout.Button("Show pop 3"))
        {
            UIManager.GetInstance().OpenUI("LuaTest3", (table) => { LuaHelper.CallFunction(table, "LuaTestView:Init"); },
                (table) => { LuaHelper.CallFunction(table, "LuaTestView:Refresh"); });
        }

        if (GUILayout.Button("Show pop 4"))
        {
            UIManager.GetInstance().OpenUI("LuaTest4", (table) => { LuaHelper.CallFunction(table, "LuaTestView:Init"); },
                (table) => { LuaHelper.CallFunction(table, "LuaTestView:Refresh"); });
        }

        if (GUILayout.Button("Show message"))
        {
            UIManager.GetInstance().ShowMessageBox("This is a message: " + Time.realtimeSinceStartup, () => { LogModule.DebugLog("Who clicked OK?"); },
                () => { LogModule.DebugLog("I clicked cancel."); });
        }
    }

    void Start()
    {
        Destroy(TempEntrance.Instance);
    }
}
