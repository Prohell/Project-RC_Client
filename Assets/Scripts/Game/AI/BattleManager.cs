
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
public enum BattleState
{
    prepare =1  <<0,
    Start   =1  <<1,
    Stop    =1  <<2,
    End     =1  <<3,

}
public class BattleManager : MonoBehaviour
{
    Quaternion mBornQu = Quaternion.LookRotation(Vector3.forward, Vector3.up);

    string mPosPath = "SquadPrefab/PositionCube";


    Object mSquadAsset;
    Object mSkillAsset;
    Object mPosAsset;
    Object mAimAsset;

    List<SquadData> mSquadDataList;
    Dictionary<int, GameObject> mSquadGameObjDict;

    private int mSceneID=1;
    float mStartTime = 30f;

    Tab_SceneClass mBattleSceneConfig;

    //the Squad Brain Thinking Time
    private float mSynTime;
    private const float mSynTimeTimeSpace = 1f;

    Embattling mEmb;
    BattleState mBattleState;
    void Awake()
    {
        GameObject tbattleRoot = GameObjectCreater.CreateGo("BattleRoot");
        mEmb = transform.GetComponent<Embattling>();
        GameObjectPool.GetInstance().root = tbattleRoot;

        mSquadDataList = new List<SquadData>();
        mSquadGameObjDict = new Dictionary<int, GameObject>();

        //Tab_TeamConfig tTeamConfig = TableManager.GetTeamConfigByID(100001)[0];

        mBattleSceneConfig = TableManager.GetSceneClassByID(100001)[0];
        StartCoroutine(AssetLoadManager.LoadFromResource(mPosPath, GeneratePos));

        EventManager.GetInstance().AddEventListener(EventId.LoadSquad, LoadSquad);
        EventManager.GetInstance().AddEventListener(EventId.StartBattle, StartBattle);
        EventManager.GetInstance().AddEventListener(EventId.StartFight, StartFight);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveFight, ReceiveFight);


        EventManager.GetInstance().AddEventListener(EventId.ReceiveBattleInfor, ReceiveBattleInfor);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveSquadPosInfor, ReceivePosList);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveCommandPursue, ReceiveCommandPursue);
        EventManager.GetInstance().AddEventListener(EventId.ReceivePrepareForAttack, ReceivePrepareForAttack);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveObjGetHurt, ReceiveObjGetHurt);

        //GetBattlerInfor();
    }
    void Start()
    {
        SENDMARCH(GameFacade.GetProxy<PlayerProxy>().marchlist.marchlistList[0].Marchid);
    }
    void PrepareForBattle( Quaternion mtBornQu, Dictionary<int,GameObject> tSquadGameObjDic, List<SquadData> tSquadDataList)
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
            tSquadGameObjDic.Add(tSquadDataList[j].GetID(), tSquad);

            tSquadController.FormationStrategy();

            for (int i = 0; i < tSquadDataList[j].GetUnitCount(); i++)
            {
                tSquadController.GetUnitControllerList()[i].GetComponent<UnitController>().EmbattingSetPos(tSquad.transform.position);
            }
        }
    }
    IEnumerator LoadUnitAssets(GC_BATTLEINFOR tBattleInfor)
    {
        yield return AssetLoadManager.LoadFromResource<Object>("SquadPrefab/Cylinder", (Object tObject)=>
        {
            mSquadAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>("MissilePrefab/Sphere", (Object tObject) =>
        {
            mAimAsset = tObject;
        });        
        for (int i = 0; i < tBattleInfor.objListList.Count; i++)
        {
            SquadData tSquadData = new SquadData();
            tSquadData.SetSquadInfor(tBattleInfor.objListList[i]);
            Tab_UnitTemplate tUnitTpData = TableManager.GetUnitTemplateByID(tSquadData.GetUnitTemplateID())[0];
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
            mSquadDataList.Add(tSquadData);
        }

        PrepareForBattle(mBornQu, mSquadGameObjDict, mSquadDataList);
        AddEmbateBornList();
        mBattleState = BattleState.prepare;
    }

    void GeneratePos(Object tObject)
    {
        mPosAsset = tObject;
    }
    void SetSquadEnemy()
    {

    }
    public void RefreshEnemyList()
    {

    }
    IEnumerator BattleStart()
    {
        yield return new WaitForSeconds(mStartTime);
        if (mEmb.enabled==true&& mBattleState ==BattleState.prepare)
        {
            foreach (var item in mEmb.mBornInforList)
            {
                item.mPosInstance.gameObject.SetActive(false);
            }
            mEmb.enabled = false;
            mBattleState = BattleState.Start;
        }
    }
    public void StartBattle(object parm)
    {
        if (mEmb != null)
        {
            foreach (var item in mEmb.mBornInforList)
            {
                item.mPosInstance.gameObject.SetActive(false);
            }
            mEmb.enabled = false;
            mBattleState = BattleState.Start;
        }
    }
    public void LoadSquad(object parm)
    {
        GetBattlerInfor();
        //GameFacade.GetProxy<BattleProxy>().GetBattlerInfor(1);
        //PrepareForBattle(mBornQu, mSquadGameObjDict, mSquadDataList);

        //SetSquadEnemy();
        //RefreshEnemyList();
        //AddEmbateBornList();
    }
    void Update()
    {
        if(mBattleState == BattleState.Start)
        {
            if (mSynTime < float.Epsilon)
            {
                mSynTime = mSynTimeTimeSpace;
                GetPosList();
            }
            else
            {
                mSynTime -= Time.deltaTime;
            }
        }
    }
    public void StartFight(object parm)
    {
        string Text = (string)parm;
        int tNumbers;
        if (!Text.Equals(""))
        {
            tNumbers = System.Int32.Parse(Text);
            GetFightInfor(tNumbers);
        }       
    }
    public void AddEmbateBornList()
    {
        mEmb.MPosCube = mPosAsset;
        foreach (var item in mSquadDataList)
        {
            if (item.GetSquadCamp()==SquadCamp.CampRed)
            {
                for (int i = 0; i < mBattleSceneConfig.getAttackPosXCount(); i++)
                {
                    BornPosInfor tBorPos = new BornPosInfor();
                    tBorPos.mBornPos = SetPosition(mBattleSceneConfig.GetAttackPosXbyIndex(i), mBattleSceneConfig.GetAttackPosZbyIndex(i));

                    if(item.GetBornPosionIndex() ==i)
                    {
                        tBorPos.mSquadID = item.GetID();
                    }

                    GameObject tCube = Instantiate(mPosAsset, tBorPos.mBornPos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                    tBorPos.mPosInstance = tCube.transform;
                    mEmb.mBornInforList.Add(tBorPos);
                }

            }
            else if(item.GetSquadCamp() == SquadCamp.CampBlue)
            {

                for (int i = 0; i < mBattleSceneConfig.getDefencePosXCount(); i++)
                {
                    BornPosInfor tBorPos = new BornPosInfor();
                    tBorPos.mBornPos = SetPosition(mBattleSceneConfig.GetDefencePosXbyIndex(i), mBattleSceneConfig.GetDefencePosZbyIndex(i));

                    if (item.GetBornPosionIndex() == i)
                    {
                        tBorPos.mSquadID = item.GetID();
                    }
                    GameObject tCube = Instantiate(mPosAsset, tBorPos.mBornPos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                    tBorPos.mPosInstance = tCube.transform;
                    mEmb.mBornInforList.Add(tBorPos);
                }
            }
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

    public void GetFightInfor(int tAimMarchID)
    {
        LogModule.DebugLog("Send Get Fight Infor!");
        CG_FIGHT fight = (CG_FIGHT)PacketDistributed.CreatePacket(MessageID.PACKET_CG_FIGHT);
        fight.SetType(1);
        fight.SetAttackId(GameFacade.GetProxy<PlayerProxy>().marchlist.marchlistList[0].Marchid);
        fight.SetDefenceId(tAimMarchID);
        fight.SetSceneId(mSceneID);
        fight.SendPacket();
    }
    public void SENDMARCH(long marchID)
    {
        CG_SEND_MARCH objListPacket = (CG_SEND_MARCH)PacketDistributed.CreatePacket(MessageID.PACKET_CG_SEND_MARCH);
        objListPacket.SetMarchid(marchID);
        objListPacket.SendPacket();
    }

    public void GetPosList()
    {
        CG_OBJPOSLIST objListPacket = (CG_OBJPOSLIST)PacketDistributed.CreatePacket(MessageID.PACKET_CG_OBJPOSLIST);
        objListPacket.SetSceneId(mSceneID);
        objListPacket.SendPacket();
        //LogModule.DebugLog("Get Pos!");
    }
    public void GetBattlerInfor()
    {
        LogModule.DebugLog("GetBattlerInfor");
        CG_BATTLEINFOR battlerInforPacket = (CG_BATTLEINFOR)PacketDistributed.CreatePacket(MessageID.PACKET_CG_BATTLEINFOR);
        battlerInforPacket.SetSceneId(mSceneID);
        battlerInforPacket.SendPacket();
    }
    public void ReceiveFight(object parm)
    {
        mSceneID = GameFacade.GetProxy<BattleProxy>().FightInfor.SceneId;
        LogModule.DebugLog("ReceiveFight");
    }

    public void ReceivePosList(object parm)
    {
        LogModule.DebugLog("ReceivePosList");
        GC_OBJPOSLIST tObjPosList = (GC_OBJPOSLIST)parm;
        if (mSceneID != tObjPosList.SceneId)
            return;
        foreach (var item in tObjPosList.objPosListList)
        {
            SquadController tSquadController = mSquadGameObjDict[item.ObjId].GetComponent<SquadController>();
            tSquadController.UnitMarching(SetPosition(item.PosX,item.PosZ));
        }
    }
    public void ReceiveBattleInfor(object parm)
    {
        LogModule.DebugLog("ReceiveBattlerInfor");
        GC_BATTLEINFOR tBattleInfor = GameFacade.GetProxy<BattleProxy>().BattleInfor;
        if (mSceneID != tBattleInfor.SceneId)
            return;
        StartCoroutine(LoadUnitAssets(tBattleInfor));
    }
    public void ReceiveCommandPursue(object parm)
    {
        LogModule.DebugLog("ReceiveCommandPursue");
        GC_OBJCOMMANDPURSUE tObjCommandPursue = GameFacade.GetProxy<BattleProxy>().PursueInfor;
        if (mSceneID != tObjCommandPursue.SceneId)
            return;
        Vector3 tAimPos = mSquadGameObjDict[tObjCommandPursue.AimObjId].transform.position;
        mSquadGameObjDict[tObjCommandPursue.ObjId].GetComponent<SquadController>().UnitMarching(tAimPos);
    }
    public void ReceivePrepareForAttack(object parm)
    {
        LogModule.DebugLog("ReceivePrepareForAttack");
        GC_OBJPREPAREFORATTACK tObjCommandPursue = GameFacade.GetProxy<BattleProxy>().PrepareForAttackInfor;
        if (mSceneID != tObjCommandPursue.SceneId)
            return;
        Vector3 tAimPos = mSquadGameObjDict[tObjCommandPursue.AimObjId].transform.position;
        mSquadGameObjDict[tObjCommandPursue.ObjId].GetComponent<SquadController>().UnitMarching(tAimPos);
    }
    public void ReceiveObjGetHurt(object parm)
    {
        LogModule.DebugLog("ReceiveObjGetHurt");
        GC_OBJGETHURT tObjHurt = GameFacade.GetProxy<BattleProxy>().ObjectGetHurt;
        if (mSceneID != tObjHurt.SceneId)
            return;        
        mSquadGameObjDict[tObjHurt.ObjId].GetComponent<SquadController>().SetSquadHP(tObjHurt.Damage, mSquadGameObjDict[tObjHurt.AttackObjId].transform);
        if (tObjHurt.DeathNumber != 0)
            mSquadGameObjDict[tObjHurt.ObjId].GetComponent<SquadController>().UnitDie(tObjHurt.DeathNumber, mSquadGameObjDict[tObjHurt.AttackObjId].transform);
        if (tObjHurt.ObjDead == 1)
            mSquadGameObjDict[tObjHurt.ObjId].GetComponent<SquadController>().SquadDeath();
    }
}
