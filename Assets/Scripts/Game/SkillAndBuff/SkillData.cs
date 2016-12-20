using UnityEngine;
using System.Collections;
/// 技能数据
/// by Zhengxuesong
/// 2016-12-7
public enum SkillType
{
    FixedPoint = 1 << 0,
    Move = 1 << 1,
}
public class SkillData {

    // the IdTimer of the Unit
    static int mIdTimer;
    //  the ID of the Unit
    private int mID;
    private float mDamage;
    private SkillType mSkillType;


    private void MakeID()
    {
        mIdTimer++;
        mID = mIdTimer;
    }
    public int GetID()
    {
        return mID;
    }
    public SkillType GetSkillType()
    {
        return mSkillType;
    }
    public void SetSkillType(SkillType tSkillType)
    {
        mSkillType = tSkillType;
    }
}
