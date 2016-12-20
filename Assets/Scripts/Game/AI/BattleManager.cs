
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

public class BattleManager : MonoBehaviour
{

    Vector3 mTemRedBornPos;

    Vector3 mTemBlueBornPos;

    Quaternion mTemRedBornQu = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    Quaternion mTemBlueBornQu = Quaternion.LookRotation(Vector3.back, Vector3.up);


    string mArcherPath = "RolelPrefab/Gongjianshou_01";
    string mUnitPath = "RolelPrefab/Kuangzhanshi_01";
    string mSquadPath = "SquadPrefab/Cylinder";
    string mUnitAimPath = "AimPrefab/AimObj";
    string mArrowPath = "MissilePrefab/ArrowPrefab";
    string mPosPath = "SquadPrefab/PositionCube";
    string mSkillPath = "MissilePrefab/Sphere";

    Object mArcherAsset;
    Object mUnitAsset;
    Object mSquadAsset;
    Object mUnitAimAsset;
    Object mArrowAsset;
    Object mSkillAsset;

    Object mPosAsset;

    Object mAimAsset;

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
    float mStartTime = 3f;

    public bool mIsChase;

    Tab_SceneClass mBattleSceneConfig;

    void Awake()
    {
        GameObject tbattleRoot = GameObjectCreater.CreateGo("BattleRoot");
        GameObjectPool.GetInstance().root = tbattleRoot;

        mCampRedSquadList = new List<GameObject>();
        mCampBlueSquadList = new List<GameObject>();

        Tab_TeamConfig tTeamConfig = TableManager.GetTeamConfigByID(100001)[0];
        //mSquadSpace = tTeamConfig.SquadSpace;
        //mSquadSpace = 30;
        //mUnitSpace = tTeamConfig.UnitSpace;
        //mTemRedUnitNumbers = 1;
        //mTemRedRowCount = 1;
        //mTemRedSquadNumbers =2;
        //mTemBlueUnitNumbers = 1;
        //mTemBlueRowCount = 1;
        //mTemBlueSquadNumbers = 2;

        //mTemRedUnitNumbers = tTeamConfig.UnitNumbers;
        //mTemRedRowCount = tTeamConfig.SquadRowCount;
        //mTemRedSquadNumbers = tTeamConfig.SquadNumbers;
        //mTemBlueUnitNumbers = tTeamConfig.UnitNumbers;
        //mTemBlueRowCount = tTeamConfig.SquadRowCount;
        //mTemBlueSquadNumbers = tTeamConfig.SquadNumbers;

        mBattleSceneConfig = TableManager.GetSceneClassByID(100001)[0];

        mTemRedBornPos = SetPosition(mBattleSceneConfig.GetAttackPosXbyIndex(0), mBattleSceneConfig.GetAttackPosZbyIndex(0));
        mTemBlueBornPos = SetPosition(mBattleSceneConfig.GetDefencePosXbyIndex(0), mBattleSceneConfig.GetDefencePosZbyIndex(0));

        StartCoroutine(AssetLoadManager.LoadFromResource(mPosPath, GeneratePos));

        EventManager.GetInstance().AddEventListener(EventId.LoadSquad, LoadSquad);
        EventManager.GetInstance().AddEventListener(EventId.StartBattle, StartBattleTest);
        EventManager.GetInstance().AddEventListener(EventId.GetBattleInfor, GetBattleInfor);
    }
    void Start()
    {

    }
    void PrepareForBattle(Vector3 tBornPos, Quaternion mtBornQu, List<GameObject> tSquadList, List<SquadData> tSquadDataList)
    {
        for (int j = 0; j < tSquadDataList.Count; j++)
        {
            GameObject tSquad = Instantiate(mSquadAsset) as GameObject;
            tSquad.transform.position = SetPosition(tSquadDataList[j].GetBornPosionX(), tSquadDataList[j].GetBornPosionZ());
            tSquad.transform.rotation = mtBornQu;

            GameObject tSquadAim = Instantiate(mAimAsset) as GameObject;
            tSquadAim.transform.position = tSquad.transform.position;

            SquadController tSquadController = tSquad.GetComponent<SquadController>();
            tSquadController.SetmOriginalAim(tSquadAim.transform);
            for (int i = 0; i < tSquadDataList[j].GetUnitCount(); i++)
            {
                GameObject tUnit = Instantiate(tSquadDataList[j].UnitAsset) as GameObject;
                tUnit.transform.rotation = mtBornQu;

                UnitController tUnitController = tUnit.GetComponent<UnitController>();

                GameObject tUnitAim = Instantiate(mAimAsset) as GameObject;
                tUnitController.SetmOriginalAim(tUnitAim.transform);
                tUnitController.SetmArrowAsset(tSquadDataList[j].UnitArrowAsset);
                tUnitController.SquadTransform = tSquad.transform;

                tSquadController.GetUnitControllerList().Add(tUnit.transform);
                
            }

            tSquadController.GetSquadData().SkillAsset = mSkillAsset;
            tSquadList.Add(tSquad);

            tSquadController.FormationStrategy();

            for (int i = 0; i < tSquadDataList[j].GetUnitCount(); i++)
            {
                tSquadController.GetUnitControllerList()[i].GetComponent<UnitController>().EmbattingSetPos(tSquad.transform.position);
            }
        }
    }
    void LoadAllResource()
    {

    }

    IEnumerator LoadUnitAssets(GC_BATTLEINFOR tBattleInfor)
    {
        yield return AssetLoadManager.LoadFromResource<Object>("SquadPrefab/Cylinder", (Object tObject)=>
        {
            mSquadAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>("AimPrefab/AimObj", (Object tObject) =>
        {
            mAimAsset = tObject;
        });        
        for (int i = 0; i < tBattleInfor.objListList.Count; i++)
        {
            SquadData tSquadData = new SquadData();
            tSquadData.SetSquadInfor(tBattleInfor.objListList[i]);
            Tab_UnitTemplate tUnitTpData = TableManager.GetUnitTemplateByID(tBattleInfor.objListList[i].UnitDataId)[0];
            yield return AssetLoadManager.LoadFromResource<Object>(tUnitTpData.UnitPath, (Object tObject) => {
                tSquadData.UnitAsset = tObject;
            });
            yield return AssetLoadManager.LoadFromResource<Object>("SkillPath", (Object tObjcet) =>
             {
                 tSquadData.SkillAsset = tObjcet;
             });
            yield return AssetLoadManager.LoadFromResource<Object>(tUnitTpData.UnitSkillPath, (Object tObjcet) =>
            {
                tSquadData.UnitSkillAsset = tObjcet;
            });
            yield return AssetLoadManager.LoadFromResource<Object>(tUnitTpData.UnitArrowPath, (Object tObjcet) =>
            {
                tSquadData.UnitArrowAsset = tObjcet;
            });
            switch (tSquadData.GetSquadCamp())
            {
                case SquadCamp.CampRed:
                    mRedSquadDataList.Add(tSquadData);
                    break;
                case SquadCamp.CampBlue:
                    mBlueSquadDataList.Add(tSquadData);
                    break;
            }
        }
    }

    void GeneratePos(Object tObject)
    {
        mPosAsset = tObject;
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


        mCampRedSquadList[0].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos);
        mCampBlueSquadList[0].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos);
        mCampRedSquadList[1].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos2);
        mCampBlueSquadList[1].GetComponent<SquadController>().UnitMarchAttacking(mUnitSpace, tAimPos2);
    }
    public void RefreshEnemyList()
    {

        mCampRedSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
        mCampBlueSquadList[0].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[0].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);

        mCampRedSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampBlueSquadList[1].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
        mCampBlueSquadList[1].GetComponent<SquadController>().RefreshUnitEnemyList(mCampRedSquadList[1].GetComponent<SquadController>().GetSquadData().GetID(), mUnitSpace);
    }
    public void FindAttackEnemyTest()
    {
        mCampRedSquadList[0].GetComponent<SquadController>().UnitAttackEnemy(mCampBlueSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());
        mCampBlueSquadList[0].GetComponent<SquadController>().UnitAttackEnemy(mCampRedSquadList[0].GetComponent<SquadController>().GetSquadData().GetID());
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
        }
        else
        {
            tNumbers = 18;
        }
        PrepareForBattle(mTemRedBornPos, mTemRedBornQu, mCampRedSquadList, mRedSquadDataList);
        PrepareForBattle(mTemBlueBornPos, mTemBlueBornQu, mCampBlueSquadList, mBlueSquadDataList);

        SetSquadEnemy();
        RefreshEnemyList();
        AddEmbateBornList();
    }
    public void StartBattleTest(object parm)
    {
        StartCoroutine(BattleStart());
    }
    public void GetBattleInfor(object parm)
    {
        GC_BATTLEINFOR tBattleInfor = (GC_BATTLEINFOR)parm;
        StartCoroutine(LoadUnitAssets(tBattleInfor));
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
            tEmb.AddBornList(tBorPos);
        }
    }

    Vector3 SetPosition(float tPosx, float tPosz)
    {
        Ray tRay = new Ray(new Vector3(tPosx, 100f, tPosz), Vector3.down);
        RaycastHit tHit;
        bool judge = Physics.Raycast(tRay, out tHit);
        if (judge)
            return tHit.point;
        else
            return new Vector3(tPosx, 0.1f, tPosz);
    }

}
