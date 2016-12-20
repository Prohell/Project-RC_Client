using UnityEngine;
using System.Collections;
/// Buff数据
/// by Zhengxuesong
/// 2016-12-7
public enum BuffType
{
    Hurt = 1 << 0,
    SlowDown = 1 << 1,
    Cure = 1<<2
}

public class BuffData {
    // the IdTimer of the Unit
    static int mIdTimer;
    //  the ID of the Unit
    private int mID;
    private void MakeID()
    {
        mIdTimer++;
        mID = mIdTimer;
    }
    public int GetID()
    {
        return mID;
    }
}
