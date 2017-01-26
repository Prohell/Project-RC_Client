//----------------------------------------------
//	CreateTime  : 1/24/2017 5:55:37 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : WorldUnitTest
//	ChangeLog   : None
//----------------------------------------------

using UnityEngine;

public class WorldUnitTest : Mono_Singleton<WorldUnitTest>
{
    void OnGUI()
    {
        if (GUILayout.Button("ReqAllMarch"))
        {
            GameFacade.GetProxy<WorldProxy>().SendQuestAllMarch();
        }
    }
}
