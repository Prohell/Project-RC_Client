using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
/// <summary>
/// 小队数据部分
/// by Zhengxuesong
/// 2016-11-11
/// </summary>
public enum SquadCamp {
    CampRed =1,
    CampBlue =2
}
public enum SquadState {
    AI_IDLE = 0,//待机
    AI_COMBAT,//战斗
    AI_MARCH,//行军
    AI_TRACE,//追击
}


public class SquadData {
    //the View of the Unit
    private float mSquadView = 15;
    //the Attack range of the Squad
    private float mSquadAttackRange= 5;
    // the HP of the Squad
    private float mHP =18;
    // the HP of the Squad
    private float mHPMax = 18;
    // the Attack of the Squad
    private float mSquadAttack =3;
    // the Defence of the Squad
    private float mSquadDefence = 1;
    // the Sp of the Squad 
    private float mSp = 1;
    // the level of the Squad 
    private float mlevel = 1;
    // the IdTimer of the Squad
    static int mIdTimer;
    //  the ID of the Squad
    private int mID;
    // the UnitCounts
    private int mUnitCount;
    // the Unit cout in one Row
    private int mUnitCountOfRow;
    //the Squad Template ID
    private int mSquadTemplateID =1;
    // the Unit Template ID
    private int mUnitTemplateID= 100001;
    // the teamformation ID
    private int mTeamFormationID = 100001;
    // the bornPosionIndex 
    private int mBornPosionIndex;
    // the bornPosition
    private float mBornPosionX;
    private float mBornPosionZ;

    private float mAttackSpaceTime;

    private SquadCamp mSquadCamp;

    public IList<int> mSkillIDList;


    public int SkillTemplateID { get; set; }
    //the prefab of the Skill
    public Object SkillAsset { get; set; }
    public Object UnitAsset { get; set; }
    public Object UnitArrowAsset { get; set; }
    public Object UnitSkillAsset { get; set; }
    // the count of the Unit in Squad
    public int GetUnitCount()
    {
        return mUnitCount;
    }
    public SquadData()
    {
        if (BattleMode.Client)
        {
            mIdTimer++;
            mID = mIdTimer;
            mAttackSpaceTime = 2f;
        }else
        {
            mAttackSpaceTime = 0f;
        }
    }
    public SquadCamp GetSquadCamp()
    {
        return mSquadCamp;
    }
    public void SetSquadInfor(CObjInfor tObjInfor)
    {
        mID = tObjInfor.id;
        mSquadCamp = (SquadCamp)tObjInfor.camp;
        mUnitCount = tObjInfor.unitcount;
        mHP = tObjInfor.hp;
        mHPMax = tObjInfor.maxhp;
        mSquadAttack = tObjInfor.attack;
        mSquadDefence = tObjInfor.defence;
        mSp = tObjInfor.sp;
        mlevel = tObjInfor.level;
        mBornPosionX = tObjInfor.posx;
        mBornPosionZ = tObjInfor.posz;
        mSquadTemplateID = tObjInfor.unitDataId;
        mBornPosionIndex = tObjInfor.arrangeindex;
        Tab_RoleBaseAttr tRoleBaseAttr = TableManager.GetRoleBaseAttrByID(tObjInfor.unitDataId)[0];
        mUnitTemplateID = tRoleBaseAttr.UnitDataID;
        mSkillIDList = tObjInfor.skilldataid;
        Tab_SkillEx tSkillEx = TableManager.GetSkillExByID(mSkillIDList[0])[0];
        SkillTemplateID = tSkillEx.SkillDataID;
    }
    public int GetBornPosionIndex()
    {
        return mBornPosionIndex;
    }

    public float GetBornPosionX()
    {
        return mBornPosionX;
    }
    public float GetBornPosionZ()
    {
        return mBornPosionZ;
    }
    public int GetID()
    {
        return mID;
    }
    public void SetID(int tID)
    {
        mID = tID;
    }
    public int GetUnitTemplateID()
    {
        return mUnitTemplateID;
    }
    public int GetTeamFormationID()
    {
        return mTeamFormationID;
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
        return mHP;
    }

    public void  SetSquadHP(float tSquadHP)
    {
        mHP = tSquadHP;
    }

    public float GetAttack()
    {
        return mSquadAttack;
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
