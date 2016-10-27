using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TransformExtension
{
    public static void SetLocalX(this Transform tr, float x)
    {
        Vector3 localPos = tr.localPosition;
        localPos.x = x;
        tr.localPosition = localPos;
    }

    public static void SetLocalY(this Transform tr, float y)
    {
        Vector3 localPos = tr.localPosition;
        localPos.y = y;
        tr.localPosition = localPos;
    }

    public static void SetLocalZ(this Transform tr, float z)
    {
        Vector3 localPos = tr.localPosition;
        localPos.z = z;
        tr.localPosition = localPos;
    }

    public static void Reset(this Transform tr)
    {
        tr.localPosition = Vector3.zero;
        tr.localRotation = Quaternion.identity;
        tr.localScale = Vector3.one;
    }

    public static string GetPath(this Transform tr)
    {
        List<string> paths = new List<string>();
        while (tr != null)
        {
            paths.Add(tr.name);
            tr = tr.parent;
        }
        paths.Reverse();
        return string.Join("/", paths.ToArray());
    }
}
