//----------------------------------------------
//	CreateTime  : 12/28/2016 4:03:22 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : KeywordReplace
//	ChangeLog   : None
//----------------------------------------------

//Assets/Editor/KeywordReplace.cs
using UnityEngine;
using UnityEditor;
using System.Collections;

public class KeywordReplace : UnityEditor.AssetModificationProcessor
{

    public static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
        {
            return;
        }
        string file = path.Substring(index);
        if (file != ".cs" && file != ".js" && file != ".boo") return;
        index = Application.dataPath.LastIndexOf("Assets");
        if (index < 0)
        {
            return;
        }
        path = Application.dataPath.Substring(0, index) + path;
        file = System.IO.File.ReadAllText(path);

        file = file.Replace("#CREATIONDATE#", System.DateTime.Now + "");
        file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
        file = file.Replace("#SMARTDEVELOPERS#", PlayerSettings.companyName);

        System.IO.File.WriteAllText(path, file);
        AssetDatabase.Refresh();
    }
}