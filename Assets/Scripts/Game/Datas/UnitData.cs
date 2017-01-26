using GCGame.Table;
using UnityEngine;
/// <summary>
/// 个体数据部分
/// by Zhengxuesong
/// 2016-11-11
/// </summary>
public enum UnitAttackType
{
    ShortAttack = 1 << 0,
    RemoteAttack= 1 << 1,
    None=0
}
public class UnitData {

    //the View of the Unit
    private float mUnitView;
    //the Attack range of the Unit
    private float mUnitAttackRange ;
    // the MaxHP of the Unit
    private float mUnitMaxHp;
    // the HP of the Unit
    private float mUnitHP;
    // the Attack of the Unit
    private float mAttack;
    // the  space time of the Unit attack action
    private float mAttackSpaceTime;
    // the IdTimer of the Unit
    static int mIdTimer;
    //  the ID of the Unit
    private int mID;
    //  the Tpye of the Attack
    private UnitAttackType AttackType = UnitAttackType.RemoteAttack;
    //this attack range is used to adjust the position。and decide attack
    private float mAdjustAttackRange;

    public UnitAttackType GetAttackType()
    {
        return AttackType;
    }
    public void SetAttackType(UnitAttackType tAttackType)
    {
        AttackType = tAttackType;
    }

    private void MakeID()
    {
        mIdTimer++;
        mID = mIdTimer;
    }
    public UnitData()
    {
        MakeID();
    }
    public void SetInfor(int typID)
    {
        Tab_UnitTemplate tUnitTemplate = TableManager.GetUnitTemplateByID(typID)[0];
        mUnitView = tUnitTemplate.UnitView;
        mUnitAttackRange = tUnitTemplate.UnitAttackRange;
        mUnitMaxHp = tUnitTemplate.UnitMaxHP;
        mAttackSpaceTime = tUnitTemplate.AttackSpaceTime;
        AttackType = (UnitAttackType)tUnitTemplate.AttackType;
        mAttack = tUnitTemplate.Attack;
        mUnitHP = mUnitMaxHp;
    }
    public int GetID()
    {
        return mID;
    }
    
    public float GetAttack()
    {
        return mAttack;
    }
    public float GetUnitHP()
    {
        return mUnitHP;
    }
    public void SetUnitHP(float tHP)
    {
        mUnitHP = tHP;
    }
    public float GetUnitView()
    {
        return mUnitView;
    }
    public void SetUnitView(float tUnitView)
    {
        mUnitView = tUnitView;
    }

    public float GetUnitAttackRange()
    {
        return mUnitAttackRange+ mAdjustAttackRange;
    }
    public void SetUnitAttackRange(float tUnitAttackRange)
    {
        mUnitAttackRange = tUnitAttackRange;
    }

    public float GetAttackSpaceTime()
    {
        return mAttackSpaceTime+Random.Range(0,0.5f);
    }
    public void SetAdjustAttackRange(float tempAttackRange)
    {
        mAdjustAttackRange = tempAttackRange;
    }
    public float GetMaxHp()
    {
        return mUnitMaxHp;
    }
}
