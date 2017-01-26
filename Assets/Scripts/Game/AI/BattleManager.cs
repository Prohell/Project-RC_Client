
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
public enum ServerBattleState
{
    STATUS_OPENED = 0,
    STATUS_READY = 1, // 交战双方进入,
    STATUS_LINE, // 布阵
    STATUS_SELECTTARGET,//选择目标
    STATUS_MARCH, // 行军
    STATUS_COMBAT, // 战斗
    STATUS_SETTLEMENT,    // 结束
    STATUS_CLOSED,
    STATUS_OVER,
}
public class BattleFunction
{
    public readonly static int mDenominator = 100;
    public static Vector3 SetPosition(float tPosx, float tPosz)
    {
        tPosx = tPosx / mDenominator;
        tPosz = tPosz / mDenominator;
        Ray tRay = new Ray(new Vector3(tPosx, 100f, tPosz), Vector3.down);
        RaycastHit tHit;
        bool judge = Physics.Raycast(tRay, out tHit);
        if (judge)
            return tHit.point;
        else
            return new Vector3(tPosx, 0.1f, tPosz);
    }
}
public class BattleManager : MonoBehaviour
{
    string mPosPath = "SquadPrefab/PositionCube";


    Object mSquadAsset;
    Object mSkillAsset;
    Object mPosAsset;
    Object mAimAsset;

    List<SquadData> mSquadDataList;
    Dictionary<int, GameObject> mSquadGameObjDict;

    private int mSceneID =-1;
    private SquadCamp mCurrentCamp;
    float mStartTime = 30f;

    Tab_SceneClass mBattleSceneConfig;

    //the Squad Brain Thinking Time
    private float mSynTime;
    private const float mSynTimeTimeSpace = 1f;

    Embattling mEmb;
    BattleState mBattleState;

    int mAutoFight = 1;//the value 1 is AutoFight
    void Awake()
    {
        GameObject tbattleRoot = GameObjectCreater.CreateGo("BattleRoot");
        mEmb = transform.GetComponent<Embattling>();
        GameObjectPool.GetInstance().root = tbattleRoot;

        mSquadDataList = new List<SquadData>();
        mSquadGameObjDict = new Dictionary<int, GameObject>();

        mBattleSceneConfig = TableManager.GetSceneClassByID(1)[0];
        StartCoroutine(AssetLoadManager.LoadFromResource(mPosPath, GeneratePos));

        //EventManager.GetInstance().AddEventListener(EventId.LoadSquad, LoadSquad);
        EventManager.GetInstance().AddEventListener(EventId.StartBattle, StartBattle);
        //EventManager.GetInstance().AddEventListener(EventId.StartFight, StartFight);
        //EventManager.GetInstance().AddEventListener(EventId.ReceiveFight, ReceiveFight);
        //EventManager.GetInstance().AddEventListener(EventId.SendMarch, SendMerch);
        EventManager.GetInstance().AddEventListener(EventId.UseSkill, UseSkill);
        EventManager.GetInstance().AddEventListener(EventId.AutoFight, AutoFight);


        EventManager.GetInstance().AddEventListener(EventId.ReceiveBattleInfor, ReceiveBattleInfor);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveSquadPosInfor, ReceivePosList);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveCommandPursue, ReceiveCommandPursue);
        EventManager.GetInstance().AddEventListener(EventId.ReceivePrepareForAttack, ReceivePrepareForAttack);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveObjGetHurt, ReceiveObjGetHurt);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveSkill, ReceiveSkill);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveBattleEnd, ReceiveBattleEnd);
        //EventManager.GetInstance().AddEventListener(EventId.ReceiveUpdateMarchMsg, ReceiveSendMarch);

        LoadSquad(null);
        //GetBattlerInfor();

    }
    void Start()
    {
        //mSceneID = GameFacade.GetProxy<PlayerProxy>().marchList[0].sceneId;
    }
    void PrepareForBattle( Dictionary<int,GameObject> tSquadGameObjDic, List<SquadData> tSquadDataList)
    {
        Quaternion mBornQuRed = Quaternion.LookRotation(Vector3.left, Vector3.up);
        Quaternion mBornQuBlue = Quaternion.LookRotation(Vector3.right, Vector3.up);
        for (int j = 0; j < tSquadDataList.Count; j++)
        {
            GameObject tSquad = Instantiate(mSquadAsset) as GameObject;
            tSquad.transform.position = BattleFunction.SetPosition(tSquadDataList[j].GetBornPosionX(), tSquadDataList[j].GetBornPosionZ());
            if(tSquadDataList[j].GetSquadCamp()==SquadCamp.CampRed)
                tSquad.transform.rotation = mBornQuRed;
            else
                tSquad.transform.rotation = mBornQuBlue;

            GameObject tSquadAim = Instantiate(mAimAsset) as GameObject;
            tSquadAim.transform.position = tSquad.transform.position;

            SquadController tSquadController = tSquad.GetComponent<SquadController>();
            tSquadController.SetmOriginalAim(tSquadAim.transform);
            tSquadController.SetSquadData(tSquadDataList[j]);
            for (int i = 0; i < tSquadDataList[j].GetUnitCount(); i++)
            {
                GameObject tUnit = Instantiate(tSquadDataList[j].UnitAsset) as GameObject;
                tUnit.transform.rotation = tSquad.transform.rotation;

                UnitController tUnitController = tUnit.GetComponent<UnitController>();

                GameObject tUnitAim = Instantiate(mAimAsset) as GameObject;

                tUnitController.GetUnitData().SetInfor(tSquadDataList[j].GetUnitTemplateID());
                tUnitController.SetmOriginalAim(tUnitAim.transform);
                tUnitController.SetmArrowAsset(tSquadDataList[j].UnitArrowAsset);
                tUnitController.SquadTransform = tSquad.transform;

                tSquadController.GetUnitControllerList().Add(tUnit.transform);

            }

            //tSquadController.GetSquadData().SkillAsset = mSkillAsset;
            tSquadGameObjDic.Add(tSquadDataList[j].GetID(), tSquad);

            tSquadController.FormationStrategy();

            for (int i = 0; i < tSquadDataList[j].GetUnitCount(); i++)
            {
                tSquadController.GetUnitControllerList()[i].GetComponent<UnitController>().EmbattingSetPos(tSquad.transform.position);
            }
        }
    }
    IEnumerator LoadUnitAssets(CBattleInfor tBattleInfor)
    {
        yield return AssetLoadManager.LoadFromResource<Object>("SquadPrefab/Cylinder", (Object tObject)=>
        {
            mSquadAsset = tObject;
        });
        yield return AssetLoadManager.LoadFromResource<Object>("AimPrefab/AimObj", (Object tObject) =>
        {
            mAimAsset = tObject;
        });        
        for (int i = 0; i < tBattleInfor.objList.Count; i++)
        {
            SquadData tSquadData = new SquadData();
            tSquadData.SetSquadInfor(tBattleInfor.objList[i]);
            Tab_UnitTemplate tUnitTpData = TableManager.GetUnitTemplateByID(tSquadData.GetUnitTemplateID())[0];
            Tab_SkillTemplate tSkillTemplate = TableManager.GetSkillTemplateByID(tSquadData.SkillTemplateID)[0];
            yield return AssetLoadManager.LoadFromResource<Object>(tUnitTpData.UnitPath, (Object tObject) => {
                tSquadData.UnitAsset = tObject;
            });
            yield return AssetLoadManager.LoadFromResource<Object>(tSkillTemplate.UnitPath, (Object tObjcet) =>
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

        PrepareForBattle(mSquadGameObjDict, mSquadDataList);
        switch (mBattleState)
        {
            case BattleState.prepare:
                AddEmbateBornList();
                StartCoroutine(BattleStart());
                break;
            case BattleState.Start:

                break;
        }
        SetSquadEnemy();
    }

    void GeneratePos(Object tObject)
    {
        mPosAsset = tObject;
    }
    public void SetSquadEnemy()
    {
        foreach (var item in mSquadGameObjDict)
        {
            SquadController itemController = item.Value.GetComponent<SquadController>();
            SquadData itemData = item.Value.GetComponent<SquadController>().GetSquadData();
            foreach (var item2 in mSquadGameObjDict)
            {
                SquadController item2Controller = item2.Value.GetComponent<SquadController>();
                SquadData item2Data = item2.Value.GetComponent<SquadController>().GetSquadData();
                if (itemData.GetSquadCamp() != item2Data.GetSquadCamp())
                {
                    itemController.AddEnemy(item2Data.GetID(), item2Controller);
                }
            }
        }
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
        if (parm != null)
        {
            GC_BattleStart tBattleInfor = (GC_BattleStart)parm;
            if (mSceneID != tBattleInfor.SceneId)
                return;
        }
          
        if (mEmb != null)
        {
            mEmb.PreparePosition(mSceneID,mCurrentCamp);
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
        if (mSceneID == -1)
        {
            mSceneID = GameFacade.GetProxy<WorldProxy>().FightInfor.sceneId;
        }
        GetBattlerInfor();
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
        if(mCurrentCamp == SquadCamp.CampRed)
        {
            for (int i = 0; i < mBattleSceneConfig.getAttackPosXCount(); i++)
            {
                BornPosInfor tBorPos = new BornPosInfor();
                tBorPos.mBornPos = BattleFunction.SetPosition(mBattleSceneConfig.GetAttackPosXbyIndex(i) * BattleFunction.mDenominator, mBattleSceneConfig.GetAttackPosZbyIndex(i) * BattleFunction.mDenominator);
                foreach (var item in mSquadDataList)
                {
                    if (item.GetSquadCamp() == SquadCamp.CampRed)
                    {
                        if (item.GetBornPosionIndex() == i)
                        {
                            tBorPos.mSquadID = item.GetID();
                            tBorPos.mPosCamp = SquadCamp.CampRed;
//                          tBorPos.mArrangeindex = item.GetBornPosionIndex();
                        }
                    }
                }
                GameObject tCube = Instantiate(mPosAsset, tBorPos.mBornPos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                tBorPos.mPosInstance = tCube.transform;
                mEmb.mBornInforList.Add(tBorPos);
            }
        }
        if (mCurrentCamp == SquadCamp.CampBlue)
        {
            for (int i = 0; i < mBattleSceneConfig.getDefencePosXCount(); i++)
            {
                BornPosInfor tBorPos = new BornPosInfor();
                tBorPos.mBornPos = BattleFunction.SetPosition(mBattleSceneConfig.GetDefencePosXbyIndex(i) * BattleFunction.mDenominator, mBattleSceneConfig.GetDefencePosZbyIndex(i) * BattleFunction.mDenominator);
                foreach (var item in mSquadDataList)
                {
                    if (item.GetSquadCamp() == SquadCamp.CampBlue)
                    {
                        if (item.GetBornPosionIndex() == i)
                        {
                            tBorPos.mSquadID = item.GetID();
                            tBorPos.mPosCamp = SquadCamp.CampBlue;
//                          tBorPos.mArrangeindex = item.GetBornPosionIndex();
                        }
                    }
                }
                GameObject tCube = Instantiate(mPosAsset, tBorPos.mBornPos, Quaternion.LookRotation(Vector3.forward, Vector3.up)) as GameObject;
                tBorPos.mPosInstance = tCube.transform;
                mEmb.mBornInforList.Add(tBorPos);
            }
        }
    }
    public void GetFightInfor(int tAimMarchID)
    {
        LogModule.DebugLog("Send Get Fight Infor!");
        CG_FIGHT fight = (CG_FIGHT)PacketDistributed.CreatePacket(MessageID.PACKET_CG_FIGHT);
        fight.SetType(1);
		fight.SetAttackId(GameFacade.GetProxy<PlayerProxy>().marchList[0].marchId);
        fight.SetDefenceId(tAimMarchID);
        fight.SetSceneId(mSceneID);
        fight.SendPacket();
    }
    public void SendMerch(object parm)
    {
		long tMarrchID = GameFacade.GetProxy<PlayerProxy>().marchList[0].marchId;
        CG_SEND_MARCH objListPacket = (CG_SEND_MARCH)PacketDistributed.CreatePacket(MessageID.PACKET_CG_SEND_MARCH);
        objListPacket.SetMarchid(tMarrchID);
        objListPacket.SendPacket();
    }
    public void UseSkill(object parm)
    {
        int tindex = (int)parm;
        CG_SKILL_USE useSkillPacket = (CG_SKILL_USE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_SKILL_USE);
        useSkillPacket.SetSenderId(mSquadDataList[0].GetID());
        int tSkillid =mSquadGameObjDict[tindex].GetComponent<SquadController>().GetSquadData().mSkillIDList[0];
        useSkillPacket.SetSkillId(tSkillid);

        SquadData tSquadData= mSquadDataList[mSquadDataList.Count -1];
        for (int i = 1; i < mSquadDataList.Count; i++)
        {
            if(mSquadDataList[i].GetSquadCamp()!= mSquadDataList[0].GetSquadCamp())
            {
                tSquadData = mSquadDataList[i];
                break;
            }
        }
        useSkillPacket.SetTargetId(tSquadData.GetID());
        useSkillPacket.SetSceneId(mSceneID);
        useSkillPacket.SendPacket();
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
        mSceneID = GameFacade.GetProxy<BattleProxy>().FightInfor.sceneId;
        LogModule.DebugLog("ReceiveFight");
    }

    public void ReceivePosList(object parm)
    {
        //LogModule.DebugLog("ReceivePosList");
        if (mBattleState != BattleState.prepare && mBattleState != BattleState.Start)
        {
            //LogModule.DebugLog("AssetNotOk");
            return;
        }
        GC_OBJPOSLIST tObjPosList = (GC_OBJPOSLIST)parm;
        if (mSceneID != tObjPosList.SceneId)
            return;
        foreach (var item in tObjPosList.objPosListList)
        {
            SquadController tSquadController = mSquadGameObjDict[item.ObjId].GetComponent<SquadController>();

            GameObject targetSquadController=null;
            mSquadGameObjDict.TryGetValue(item.TargetId, out targetSquadController);
            SquadController tAimController=null;
            if (targetSquadController!=null)         
                tAimController = mSquadGameObjDict[item.TargetId].GetComponent<SquadController>();
            tSquadController.CorretSquadInfor(item, tAimController);
            //tSquadController.UnitMarching(SetPosition(item.PosX,item.PosZ));
        }
    }
    public void ReceiveBattleInfor(object parm)
    {
        LogModule.DebugLog("ReceiveBattlerInfor");
        CBattleInfor tBattleInfor = GameFacade.GetProxy<BattleProxy>().BattleInfor;
        mCurrentCamp = (SquadCamp)tBattleInfor.camp;
        ServerBattleState tServerBattleState = (ServerBattleState)tBattleInfor.currentState;
        if (tServerBattleState == ServerBattleState.STATUS_SELECTTARGET
            || tServerBattleState == ServerBattleState.STATUS_MARCH
            || tServerBattleState == ServerBattleState.STATUS_COMBAT)
        {
            mBattleState = BattleState.Start;
        }else
        {
            mBattleState = BattleState.prepare;
        }
        if (mSceneID != tBattleInfor.sceneId)
            return;
        StartCoroutine(LoadUnitAssets(tBattleInfor));

        //Open Battle UI.
        MediatorManager.GetInstance().Add(new BattleUIController());
    }
    public void ReceiveCommandPursue(object parm)
    {
        LogModule.DebugLog("ReceiveCommandPursue");
        if (mBattleState != BattleState.prepare && mBattleState != BattleState.Start)
        {
            LogModule.DebugLog("AssetNotOk");
            return;
        }
        CPursueInfor tPursueInfor = GameFacade.GetProxy<BattleProxy>().PursueInfor;
        if (mSceneID != tPursueInfor.sceneId)
            return;
        Vector3 tAimPos = mSquadGameObjDict[tPursueInfor.aimObjId].transform.position;
        SquadController tAimController = mSquadGameObjDict[tPursueInfor.aimObjId].transform.GetComponent<SquadController>();
        //mSquadGameObjDict[tObjCommandPursue.ObjId].GetComponent<SquadController>().SquadAttackEnemy(tAimController);
        //mSquadGameObjDict[tObjCommandPursue.ObjId].GetComponent<SquadController>().UnitMarching(tAimPos);
    }
    public void ReceivePrepareForAttack(object parm)
    {
        LogModule.DebugLog("ReceivePrepareForAttack");
        if (mBattleState != BattleState.prepare && mBattleState != BattleState.Start)
        {
            LogModule.DebugLog("AssetNotOk");
            return;
        }
        CPrepareForAttackInfor tObjCommandPrepare = GameFacade.GetProxy<BattleProxy>().PrepareForAttackInfor;
        if (mSceneID != tObjCommandPrepare.sceneId)
            return;
        Vector3 tAimPos = mSquadGameObjDict[tObjCommandPrepare.aimObjId].transform.position;
        SquadController tAimController = mSquadGameObjDict[tObjCommandPrepare.aimObjId].transform.GetComponent<SquadController>();   
        SquadController tObjController = mSquadGameObjDict[tObjCommandPrepare.objId].transform.GetComponent<SquadController>();
        tObjController.SquadAttackPrepare(tAimController);
    }
    public void ReceiveSkill(object parm)
    {
        LogModule.DebugLog("ReceiveSkill");
        if (mBattleState != BattleState.prepare && mBattleState != BattleState.Start)
        {
            LogModule.DebugLog("AssetNotOk");
            return;
        }
        CUseSkill tSkill = GameFacade.GetProxy<BattleProxy>().UseSkill;
        if (mSceneID != tSkill.sceneId)
            return;
        SquadController sender = mSquadGameObjDict[tSkill.senderId].transform.GetComponent<SquadController>();
        Transform receiver = mSquadGameObjDict[tSkill.targetId].transform;
        sender.CastSkill(receiver);
    }
    public void ReceiveObjGetHurt(object parm)
    {
        LogModule.DebugLog("ReceiveObjGetHurt");
        if (mBattleState != BattleState.prepare && mBattleState != BattleState.Start)
        {
            LogModule.DebugLog("AssetNotOk");
            return;
        }
        CObjectGetHurt tObjHurt = GameFacade.GetProxy<BattleProxy>().ObjectGetHurt;
        if (mSceneID != tObjHurt.sceneId)
            return;        
        mSquadGameObjDict[tObjHurt.objId].GetComponent<SquadController>().SetSquadHP(tObjHurt.damage, mSquadGameObjDict[tObjHurt.attackObjId].transform);
        if (tObjHurt.deathNumber != 0)
            mSquadGameObjDict[tObjHurt.objId].GetComponent<SquadController>().UnitDie(tObjHurt.deathNumber, mSquadGameObjDict[tObjHurt.attackObjId].transform);
        if (tObjHurt.objDead == 1)
            mSquadGameObjDict[tObjHurt.objId].GetComponent<SquadController>().SquadDeath();
    }
    public void ReceiveBattleEnd(object parm)
    { 
        LogModule.DebugLog("BattleEnd");
    }
    public void ReceiveSendMarch(object parm)
    {
        LogModule.DebugLog("ReceiveSendMarch");
        CSendMarch tSendMarch = GameFacade.GetProxy<BattleProxy>().SendMarch;
        mSceneID = tSendMarch.sceneId;
    }
    public void AutoFight(object parm)
    {
        CG_ROBOT_OPEN robotOpen = (CG_ROBOT_OPEN)PacketDistributed.CreatePacket(MessageID.PACKET_CG_ROBOT_OPEN);
        robotOpen.SetSceneId(mSceneID);
        robotOpen.SetOpen(mAutoFight);
        robotOpen.SendPacket();
        mAutoFight = 1 - mAutoFight;
        //LogModule.DebugLog("Get Pos!");
    }
}
