using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
/// <summary>
/// 战场信息配置类
/// by Zhengxuesong
/// 2016-11-30（暂时只是添加些测试的方法，以后会用来组织战场信息）
/// </summary>
public class BattlleController : MonoBehaviour {

    //Test 
    Vector3 mTemRedBornPos;

    Vector3 mTemBlueBornPos;

    Quaternion mTemRedBornQu = Quaternion.LookRotation(Vector3.forward, Vector3.up);

    Quaternion mTemBlueBornQu = Quaternion.LookRotation(Vector3.back, Vector3.up);


    string mArcherPath = "RolelPrefab/Gongjianshou_01";
    string mUnitPath = "RolelPrefab/Kuangzhanshi_01";
    string mSquadPath = "SquadPrefab/Cylinder";
    string mUnitAimPath = "AimPrefab/AimObj";
    string mArrowPath = "MissilePrefab/ArrowPrefab";

    Object mArcherAsset;
    Object mUnitAsset;
    Object mSquadAsset;
    Object mUnitAimAsset;
    Object mArrowAsset;

    //Squad Space between each other
    float mSquadSpace;
    //Unit Space between each other
    float mUnitSpace;
    //Unit SquadPlace Count;
    int mSqaudPlaceCount;
    //Unit SquadPlace Row Count;
    int mSquadPlaceRowCount;


    int mTemSquadS = 2;
    int mTemRedUnitNumbers;
    int mTemRedRowCount;
    int mTemRedSquadNumbers;
    List<GameObject> mCampRedSquadList;

    int mTemBlueUnitNumbers;
    int mTemBlueRowCount;
    int mTemBlueSquadNumbers;
    List<GameObject> mCampBlueSquadList;


    bool mBattleStart;
    float mStartTime=3f;

    public bool mIsChase;


    void Awake()
    {
        GameObject tbattleRoot = GameObjectCreater.CreateGo("BattleRoot");
        GameObjectPool.GetInstance().root = tbattleRoot;

        mCampRedSquadList = new List<GameObject>();
        mCampBlueSquadList = new List<GameObject>();

        Tab_TeamConfig tTeamConfig = TableManager.GetTeamConfigByID(100001)[0];
        mSquadSpace = tTeamConfig.SquadSpace;
        mUnitSpace = tTeamConfig.UnitSpace;
        //mTemRedUnitNumbers = 1;
        //mTemRedRowCount = 1;
        //mTemRedSquadNumbers =2;
        //mTemBlueUnitNumbers = 1;
        //mTemBlueRowCount = 1;
        //mTemBlueSquadNumbers = 2;

        mTemRedUnitNumbers = tTeamConfig.UnitNumbers;
        mTemRedRowCount = tTeamConfig.SquadRowCount;
        mTemRedSquadNumbers = tTeamConfig.SquadNumbers;
        mTemBlueUnitNumbers = tTeamConfig.UnitNumbers;
        mTemBlueRowCount = tTeamConfig.SquadRowCount;
        mTemBlueSquadNumbers = tTeamConfig.SquadNumbers;

        Tab_BattleSceneConfig tBattleSceneConfig = TableManager.GetBattleSceneConfigByID(100001)[0];

        mTemRedBornPos = new Vector3(tBattleSceneConfig.ABornPosX, tBattleSceneConfig.ABornPosY, tBattleSceneConfig.ABornPosZ);
        mTemBlueBornPos = new Vector3(tBattleSceneConfig.BBornPosX, tBattleSceneConfig.BBornPosY, tBattleSceneConfig.BBornPosZ);
        Vector3 tTestHit = SetPosition(tBattleSceneConfig.ABornPosX, tBattleSceneConfig.ABornPosZ);

        StartCoroutine(AssetLoadManager.LoadFromResource(mSquadPath, GenerateSquad));
    }
    void Start()
    {
        SetSquadEnemy();
        RefreshEnemyList();
        StartCoroutine(BattleStart());
    }

    void PrepareForBattle(int tSquadNumbers,int tSquadRow,int tRowCount, int tUnitNumbers, Vector3 tBornPos, Quaternion mtBornQu, List<GameObject> tSquadList,bool tReverse)
    {
        for (int j = 0; j < tSquadNumbers; j++)
        {
            int rowRoot = j * tSquadRow / tSquadNumbers;
            int colRoot = j % ((tSquadNumbers + 1) / tSquadRow);

            if (tReverse)
                rowRoot = -rowRoot;

            GameObject tSquad =Instantiate(mSquadAsset, tBornPos + new Vector3(rowRoot * mSquadSpace, 0f, colRoot * mSquadSpace), mtBornQu)as GameObject;
            tSquadList.Add(tSquad);

            GameObject tSquadAim = Instantiate(mUnitAimAsset, tBornPos + new Vector3(rowRoot * mSquadSpace, 0f, colRoot * mSquadSpace), mtBornQu) as GameObject;
            tSquadList[j].GetComponent<SquadController>().SetmOriginalAim(tSquadAim.transform);
            tSquadList[j].GetComponent<SquadController>().GetSquadData().SetUnitCount(tUnitNumbers);
            tSquadList[j].GetComponent<SquadController>().GetSquadData().SetUnitCountOfRow(tUnitNumbers/ tRowCount);

            for (int i = 0; i < tUnitNumbers; i++)
            {
                int row = i * tRowCount / tUnitNumbers;
                int col = i % (tUnitNumbers / tRowCount);

                col = col - tUnitNumbers / (2 * tRowCount);
                row = row - tRowCount / 2;

                if (tReverse)
                    row = -row;

                GameObject tUnit;
                if (rowRoot == 1)
                {
                    tUnit = Instantiate(mUnitAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;
                }
                else
                {
                    tUnit = Instantiate(mArcherAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;
                }                   

                GameObject tUnitAim = Instantiate(mUnitAimAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;

                UnitController tUnitController = tUnit.GetComponent<UnitController>();
                if (rowRoot == 1)
                    tUnitController.GetUnitData().SetInfor(100001);
                else
                    tUnitController.GetUnitData().SetInfor(100002);

                tUnitController.SetmOriginalAim(tUnitAim.transform);
                tUnitController.SetmArrowAsset(mArrowAsset);
              
                tSquadList[j].GetComponent<SquadController>().GetUnitControllerList().Add(tUnit.transform);
            }
            tSquadList[j].GetComponent<SquadController>().SetLeader();
        }
    }
    void GenerateArrow(Object tObject)
    {
        mArrowAsset = tObject;
        PrepareForBattle(mTemRedSquadNumbers, mTemSquadS, mTemRedRowCount, mTemRedUnitNumbers, mTemRedBornPos, mTemRedBornQu, mCampRedSquadList,false);
        PrepareForBattle(mTemBlueSquadNumbers, mTemSquadS, mTemBlueRowCount, mTemBlueUnitNumbers, mTemBlueBornPos, mTemBlueBornQu, mCampBlueSquadList,true);
    }
    void GenerateUnitAim(Object tObject)
    {
        mUnitAimAsset = tObject;
        StartCoroutine(AssetLoadManager.LoadFromResource(mArrowPath, GenerateArrow));
    }

    void GenerateArchers(Object tObject)
    {
        mArcherAsset = tObject;
        StartCoroutine(AssetLoadManager.LoadFromResource(mUnitAimPath, GenerateUnitAim));
    }
    void GenerateUnits(Object tObject)
    {
        mUnitAsset = tObject;
        StartCoroutine(AssetLoadManager.LoadFromResource(mArcherPath, GenerateArchers));
    }
    void GenerateSquad(Object tObject)
    {
        mSquadAsset = tObject;
        StartCoroutine(AssetLoadManager.LoadFromResource(mUnitPath, GenerateUnits));
    }
    void SetSquadEnemy()
    {
        for (int i = 0; i < mCampRedSquadList.Count; i++)
        {
            for (int j = 0; j < mCampBlueSquadList.Count; j++)
            {
                mCampRedSquadList[i].GetComponent<SquadController>().AddEnemy(mCampBlueSquadList[j].GetComponent<SquadController>().GetSquadData().GetID(), mCampBlueSquadList[j].GetComponent<SquadController>());
                mCampBlueSquadList[j].GetComponent<SquadController>().AddEnemy(mCampRedSquadList[i].GetComponent<SquadController>().GetSquadData().GetID(), mCampRedSquadList[i].GetComponent<SquadController>());
            }
        }
    }
    public void refreshPath()
    {

    }

    public void SetDestination()
    {
        Vector3 tAimPos = new Vector3(60f, 0f, -25f);
        Vector3 tAimPos2 = new Vector3(60f, 0f, 15f);

        //mCampRedSquadList[0].GetComponent<SquadController>().UnitMarching(tAimPos);
        //mCampBlueSquadList[0].GetComponent<SquadController>().UnitMarching(tAimPos);
        //mCampRedSquadList[1].GetComponent<SquadController>().UnitMarching(tAimPos2);
        //mCampBlueSquadList[1].GetComponent<SquadController>().UnitMarching(tAimPos2);

        mCampRedSquadList[0].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos);
        mCampBlueSquadList[0].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos);
        mCampRedSquadList[1].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos2);
        mCampBlueSquadList[1].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos2);
    }
    public void RefreshEnemyList()
    {
        //mCampRedSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());
        //mCampBlueSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());

        //mCampRedSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());
        //mCampBlueSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[1].GetComponent<SquadController>().GetSquadData().GetID());

        mCampRedSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID(),mUnitSpace);
        mCampBlueSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[0].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);

        mCampRedSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
        mCampBlueSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[1].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
    }
    public void FindAttackEnemyTest()
    {
        mCampRedSquadList[0].GetComponent<SquadController>().UnitAttackEnemy(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());
        mCampBlueSquadList[0].GetComponent<SquadController>().UnitAttackEnemy(mCampRedSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());

//      mCampRedSquadList[1].GetComponent<SquadController>().UnitAttackEnemy(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());

    }
    IEnumerator BattleStart()
    {
        yield return new WaitForSeconds(mStartTime);
        if (mIsChase)
        {
            FindAttackEnemyTest();
        }else
        {
            SetDestination();
        }    
    }

    Vector3 SetPosition(float tPosx,float tPosz)
    {
        Ray tRay = new Ray(new Vector3(tPosx,100f,tPosz),Vector3.down);
        RaycastHit tHit;
        bool judeg = Physics.Raycast(tRay, out tHit);
        if (judeg)
        {
            return tHit.point;
        }
        else
        {
            return new Vector3(tPosx, 0.1f, tPosz);
        }
    }
}
