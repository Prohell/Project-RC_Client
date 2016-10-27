using UnityEngine;

public static class VectorExtension
{
    public static Vector2 xy(this Vector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }

    public static Vector2 abs(this Vector2 vec)
    {
        return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
    }

    public static Vector3 abs(this Vector3 vec)
    {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    // Only Calc the Rotation and Scale
    public static Matrix4x4 RS(this Matrix4x4 matr)
    {
        Matrix4x4 re = matr;
        re.m03 = 0;
        re.m13 = 0;
        re.m23 = 0;
        return re;
    }
}
