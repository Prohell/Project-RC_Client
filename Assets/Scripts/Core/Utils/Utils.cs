using System.IO;
using UnityEngine;
/// <summary>
/// 工具
/// by TT
/// 2016-07-04
/// </summary>
public static class Utils
{
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

    public static void Assert(bool cond, string err = "")
    {
        if (!cond)
        {
            throw new UnityException(err);
        }
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
}
