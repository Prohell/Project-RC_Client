using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 小队数据部分
/// by Zhengxuesong
/// 2016-11-11
/// </summary>
public class SquadData {
    //the View of the Unit
    private float mSquadView;
    //the Attack range of the Squad
    private float mSquadAttackRange;
    // the HP of the Squad
    private float mSquadHP;
  
    // the IdTimer of the Squad
    static int mIdTimer;
    //  the ID of the Squad
    private int mID;
    // the count of the Unit in Squad
    int mUnitCount = 15;
    // the Unit cout in one Row
    int mUnitCountOfRow= 5;
    //the number of the SquadPlace
    int mSquadPlaceNumber;
    private float mAttackSpaceTime;

    public SquadData()
    {
        mIdTimer++;
        mID = mIdTimer;
    }
    public int GetID()
    {
        return mID;
    }
    public int GetUnitCount()
    {
        return mUnitCount;
    }
    public void SetUnitCount(int tUnitCount)
    {
        mUnitCount = tUnitCount;
    }
    public int GetUnitCountOfRow()
    {
        return mUnitCountOfRow;
    }
    public void SetUnitCountOfRow(int tUnitCountOfRow)
    {
        mUnitCountOfRow = tUnitCountOfRow;
    }

    public float GetSquadHP()
    {
        return mSquadHP;
    }
    public float GetSquadView()
    {
        return mSquadView;
    }
    public float GetSquadAttackRange()
    {
        return mSquadAttackRange;
    }
    public float GetAttackSpaceTime()
    {
        return mAttackSpaceTime;
    }

}
