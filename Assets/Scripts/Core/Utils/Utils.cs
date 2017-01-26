using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
/// <summary>
/// 工具
/// by TT
/// 2016-07-04
/// </summary>
public static class Utils
{
    #region Transform

    public static bool NearlyEquals(this Vector2 vec1, Vector2 vec2)
    {
        Vector2 disVec = vec1 - vec2;
        return (disVec.magnitude < 0.001f);
    }

    public static bool NearlyEquals(this Vector3 vec1, Vector3 vec2)
    {
        Vector3 disVec = vec1 - vec2;
        return (disVec.magnitude < 0.001f);
    }

    public static GameObject AddChild(Transform p_parent, string p_childName)
    {
        var ins = new GameObject(p_childName);
        ins.transform.parent = p_parent;
        StandardizeObject(ins);

        return ins;
    }

    public static void StandardizeObject(GameObject go, Transform parent)
    {
        go.transform.parent = parent;

        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        if (parent != null)
        {
            go.layer = parent.gameObject.layer;
        }
    }

    public static void StandardizeObject(GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        var parent = go.transform.parent;
        if (parent != null)
        {
            go.layer = parent.gameObject.layer;
        }
    }

    #endregion

    public static void Assert(bool cond, string err = "")
    {
        if (!cond)
        {
            throw new UnityException(err);
        }
    }

    public static void Swap<T>(T T1, T T2)
    {
        T temp = T1;
        T1 = T2;
        T2 = temp;
    }

    public static void Swap<T>(List<T> list, int index1, int index2)
    {
        T temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    public static void Swap<T>(ref T T1, ref T T2)
    {
        T temp = T1;
        T1 = T2;
        T2 = temp;
    }

    /// <summary>
    /// Copy all (sub)directories and files RECURSIVEly.
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="destPath"></param>
    public static void CopyAll(string sourcePath, string destPath)
    {
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));
        }

        foreach (string newPath in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
        }
    }

    public static void AddChildAndReset(GameObject parent, GameObject child)
    {
        child.transform.parent = parent.transform;
        child.transform.Reset();
        child.SetLayerRec(parent.layer);
    }

    public static int SetUILayer(GameObject parent, int panelOffset)
    {
        var panels = parent.GetComponentsInChildren<UIPanel>(true);

        if (panels.Any())
        {
            foreach (var panel in panels)
            {
                panel.depth += panelOffset;
            }

            return panels.Max(item => item.depth);
        }
        else
        {
            return -1;
        }
    }
}
