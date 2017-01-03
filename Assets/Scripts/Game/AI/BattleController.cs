using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
/// <summary>
/// 战场信息配置类
/// by Zhengxuesong
/// 2016-11-30（暂时只是添加些测试的方法，以后会用来组织战场信息）
/// </summary>
public class BattleController : MonoBehaviour {

    //Test 
    Vector3 mTemRedBornPos;

    Vector3 mTemBlueBornPos;

    Quaternion mTemRedBornQu = Quaternion.LookRotation(Vector3.left, Vector3.up);

    Quaternion mTemBlueBornQu = Quaternion.LookRotation(Vector3.right, Vector3.up);


    //string mArcherPath = "RolelPrefab/Gongjianshou_01";
    string mArcherPath = "RolelPrefab/P_gongjianshou_d";
    string mUnitPath = "RolelPrefab/Kuangzhanshi_01";
    string mSquadPath = "SquadPrefab/Cylinder";
    string mUnitAimPath = "AimPrefab/AimObj";
    string mArrowPath = "MissilePrefab/jiantou_A01";
    //string mArrowPath = "MissilePrefab/ArrowPrefab";

    string mPosPath = "SquadPrefab/PositionCube";
    string mSkillPath = "MissilePrefab/Sphere";


    //string mtestPath = "SquadPrefab/Sphere";
    //Object mTestAsset;

    Object mArcherAsset;
    Object mUnitAsset;
    Object mSquadAsset;
    Object mUnitAimAsset;
    Object mArrowAsset;
    Object mSkillAsset;

    Object mPosAsset;

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
    List<SquadData> mRedSquadDataList;

    int mTemBlueUnitNumbers;
    int mTemBlueRowCount;
    int mTemBlueSquadNumbers;
    List<GameObject> mCampBlueSquadList;
    List<SquadData> mBlueSquadDataList;


    bool mBattleStart;
    float mStartTime=3f;

    public bool mIsChase;

    Tab_SceneClass mBattleSceneConfig;

    void Awake()
    {
        GameObject tbattleRoot = GameObjectCreater.CreateGo("BattleRoot");
        GameObjectPool.GetInstance().root = tbattleRoot;

        mCampRedSquadList = new List<GameObject>();
        mCampBlueSquadList = new List<GameObject>();

        mSquadSpace = 30;
        mUnitSpace = 4;

        mTemRedRowCount = 3;
        mTemRedSquadNumbers = 6;
        mTemBlueRowCount = 3;
        mTemBlueSquadNumbers = 6;

        mBattleSceneConfig = TableManager.GetSceneClassByID(100001)[0];

        mTemRedBornPos = SetPosition(mBattleSceneConfig.GetAttackPosXbyIndex(0),mBattleSceneConfig.GetAttackPosZbyIndex(0));
        mTemBlueBornPos = SetPosition(mBattleSceneConfig.GetDefencePosXbyIndex(0), mBattleSceneConfig.GetDefencePosZbyIndex(0));

        StartCoroutine(LoadUnitAssets());

        EventManager.GetInstance().AddEventListener(EventId.LoadSquad, LoadSquad);
        EventManager.GetInstance().AddEventListener(EventId.StartBattle, StartBattleTest);

        //Open Battle UI.
        MediatorManager.GetInstance().Add(new BattleUIController());
    }

    void OnDestroy()
    {
        //Close Battle UI.
        MediatorManager.GetInstance().Remove(typeof(BattleUIController));
    }

    void Start()
    {
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
                if (rowRoot != 1)
                {
                    tUnit = Instantiate(mArcherAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;
                }
                else
                {
                    tUnit = Instantiate(mUnitAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;
                }                   

                GameObject tUnitAim = Instantiate(mUnitAimAsset, tBornPos + new Vector3(row * mUnitSpace + rowRoot * mSquadSpace, 0f, col * mUnitSpace + colRoot * mSquadSpace), mtBornQu) as GameObject;

                UnitController tUnitController = tUnit.GetComponent<UnitController>();
                if (rowRoot != 1)
                    tUnitController.GetUnitData().SetInfor(100003);
                    //tUnitController.GetUnitData().SetInfor(100002);
                else
                    tUnitController.GetUnitData().SetInfor(100001);

                tUnitController.SetmOriginalAim(tUnitAim.transform);
                tUnitController.SetmArrowAsset(mArrowAsset);
                tUnitController.SquadTransform = tSquadList[j].transform;

                tSquadList[j].GetComponent<SquadController>().GetUnitControllerList().Add(tUnit.transform);
            }
            tSquadList[j].GetComponent<SquadController>().FormationStrategy();
            tSquadList[j].GetComponent<SquadController>().GetSquadData().SkillAsset = mSkillAsset ;
        }
    }
    IEnumerator LoadUnitAssets()
    {
        yield return AssetLoadManager.LoadFromResource<Object>(mSquadPath, (Object tObject) => {
            mSquadAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mUnitPath, (Object tObject) => {
            mUnitAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mArcherPath, (Object tObject) => {
            mArcherAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mUnitAimPath, (Object tObject) => {
            mUnitAimAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mArrowPath, (Object tObject) => {
            mArrowAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mSkillPath, (Object tObject) => {
            mSkillAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>(mPosPath, (Object tObject) =>
        {
            mPosAsset = tObject;
        });
        //yield return AssetLoadManager.LoadFromResource<Object>(mtestPath, (Object tObject) =>
        //{
        //    mTestAsset = tObject;
        //});
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
        Vector3 tAimPos = new Vector3(30f, 0f, -55f);
        Vector3 tAimPos2 = new Vector3(30f, 0f, -25f);

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

        mCampRedSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[1].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
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
        StartBattle(null);
    }
    public void StartBattle(object parm)
    {
        if (mIsChase)
        {
            FindAttackEnemyTest();
        }
        else
        {
            SetDestination();
        }
    }
    public void LoadSquad(object parm)
    {
        string Text = (string)parm;
        int tNumbers;
        if (!Text.Equals(""))
        {
            tNumbers = System.Int32.Parse(Text);           
        }else
        {
            tNumbers = 18;
        }
        PrepareForBattle(mTemRedSquadNumbers, mTemSquadS, mTemRedRowCount, tNumbers, mTemRedBornPos, mTemRedBornQu, mCampRedSquadList, false);
        PrepareForBattle(mTemBlueSquadNumbers, mTemSquadS, mTemBlueRowCount, tNumbers, mTemBlueBornPos, mTemBlueBornQu, mCampBlueSquadList, true);

        SetSquadEnemy();
        RefreshEnemyList();
        AddEmbateBornList();

        //StartCoroutine(BattleStart());
    }
    public void StartBattleTest(object parm)
    {
        StartCoroutine(BattleStart());
    }

    public void AddEmbateBornList()
    {
        Embattling tEmb = transform.GetComponent<Embattling>();
        tEmb.MPosCube = mPosAsset;
        for (int i = 0; i < mBattleSceneConfig.getAttackPosXCount(); i++)
        {
            BornPosInfor tBorPos = new BornPosInfor();
            tBorPos.mBornPos = SetPosition(mBattleSceneConfig.GetAttackPosXbyIndex(i), mBattleSceneConfig.GetAttackPosZbyIndex(i));
            if (i < mCampRedSquadList.Count)
            {
                tBorPos.mSquadID = mCampRedSquadList[i].GetComponent<SquadController>().GetSquadData().GetID();
            }
            GameObject tCube = Instantiate(mPosAsset, tBorPos.mBornPos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
            tBorPos.mPosInstance = tCube.transform;
            tEmb.mBornInforList.Add(tBorPos);
        }

        //for (int i = 0; i < mBattleSceneConfig.getDefencePosXCount(); i++)
        //{
        //    tBorPos.mBornPos = SetPosition(mBattleSceneConfig.GetDefencePosXbyIndex(i), mBattleSceneConfig.GetDefencePosZbyIndex(i));
        //    if (i < mCampBlueSquadList.Count)
        //    {
        //        tBorPos.mSquadID = mCampBlueSquadList[i].GetComponent<SquadController>().GetSquadData().GetID();
        //    }
        //    tEmb.AddBornList(tBorPos);
        //}
    }

    Vector3 SetPosition(float tPosx,float tPosz)
    {
        Ray tRay = new Ray(new Vector3(tPosx,100f,tPosz),Vector3.down);
        RaycastHit tHit;
        bool judge = Physics.Raycast(tRay, out tHit);
        if (judge)
            return tHit.point;
        else
            return new Vector3(tPosx, 0.1f, tPosz);
    }
}
public class BattleMode
{
    public const bool Client = true;
}

