using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.RVO;
using Pathfinding;

/// <summary>
/// 个体行为控制器
/// by Zhengxuesong
/// 2016-11-11
/// </summary>
public class UnitController : BaseController
{


    //RVO controller 
    private RVOController mRVOController;
    private RichAI mRichAI;

    //the Unit Brain Thinking Time
    private float mThinkTime;
    private const float mThinkTimeSpace = 0.03f;
    // the Unit Date
    private UnitData mUnitData;
    // Enemy List
    private List<Transform> mEnemyTrans;

    private Transform mSearchAim;
    private Transform mAttackAim;

    //the Original transform of the RichAI 
    private Transform mOriginalAim;
    //Marching state;
    private bool mIsMarching;

    // the Attack Timer
    private float mAttackSpaceTimer;

    private Animation mUnitAnimation;

    //if the distance between A,B larger than mMinDistance they can search Each other 
    private float mMinDistance = 1f;

    //Arrow
    private Transform mArrow;
    private Arrow mArrowController;
    //the prefab of the Arrow
    private Object mArrowAsset;
    //Skill
    private Transform mSkill;
    private SkillEffect mSkillController;
    //the prefab of the Skill
    private Object mSkillAsset;

    //private float mAttackChaseTimer;
    //private float mAttackChaseTimeSpace = 2f;

    // if one Unit can sender a Arrow the Arrow start transformPos is the mSenderTrans Pos
    public Transform mSenderTrans;
    // if one was shoted by the Arrow the mReceverTrans Pos is the Arrow's end point
    public Transform mReceverTrans;
    //
    //public List<Transform> mForceAttackList;

    public bool MKillOrder { get; set; }
    public bool MWillDie { get; set; }

    public Vector3 RelativePosition { get; set; }
    public Transform SquadTransform { get; set; }
    // Use this for initialization
    void Awake()
    {
        mEnemyTrans = new List<Transform>();
        //mForceAttackList = new List<Transform>();
        mUnitData = new UnitData();

        mRVOController = transform.GetComponent<RVOController>();
        mRichAI = transform.GetComponent<RichAI>();

        mThinkTime = mThinkTimeSpace;

        mUnitAnimation = GetComponentInChildren<Animation>();

        InitAwake();

        mIdleState.mdGetInState += UnitIdleGetInFunc;
        mIdleState.mdGetOutState += UnitIdleGetOutFunc;
        mIdleState.mdExcuteState += UnitIdleExcuteFunc;

        mWalkState.mdGetInState += UnitWalkGetInFunc;
        mWalkState.mdExcuteState += UnitWalkExcuteFunc;
        mWalkState.mdGetOutState += UnitWalkGetOutFunc;

        mAttackState.mdGetInState += UnitAttackGetInFunc;
        mAttackState.mdExcuteState += UnitAttackExcuteFunc;
        mAttackState.mdGetOutState += UnitAttackGetOutFunc;

        mPrepareState.mdGetInState += UnitPrepareGetInFunc;
        mPrepareState.mdExcuteState += UnitPrepareExcuteFunc;
        mPrepareState.mdGetOutState += UnitPrepareGetOutFunc;

        mDieState.mdGetInState += UnitDieGetInFunc;
        mDieState.mdExcuteState += UnitDieExcuteFunc;
        mDieState.mdGetOutState += UnitDieGetOutFunc;

        mStateMachine.StartWorking();

        //the Event handlers
        SetUpEventHandlers();

    }
    void Start()
    {
    }
    void SetUpEventHandlers()
    {
        EventManager.GetInstance().AddEventListener(EventId.SomeOneDie, SomeOneDie);
    }
    public void SetmArrow(Transform tArrow)
    {
        mArrow = tArrow;
        mArrowController = mArrow.GetComponent<Arrow>();
    }
    public void SetmArrowAsset(Object tAsset)
    {
        mArrowAsset = tAsset;
    }
    public void SetmSkillAsset(Object tAsset)
    {
        mSkillAsset = tAsset;
    }

    public void ChangeMoving(bool RichAIEnable,bool RVOControllerEnable)
    {
        mRichAI.enabled = RichAIEnable;
        if (RVOControllerEnable == false)
            mRVOController.Move(Vector3.forward/10000);
    }
    // Change AI target
    public void ChangeTarget(Transform tTarget)
    {
        mIsMarching = false;
        mRichAI.target = tTarget;
        mRichAI.UpdatePath();
    }
    public void SetmOriginalAim (Transform tTarget)
    {

        mRichAI.target = tTarget;
        mOriginalAim = mRichAI.target;
        //mRichAI.UpdatePath();
    }

    //serach Attack Aim;
    public void SearchAim()
    {
        if(mSearchAim == null&& mAttackAim==null)
        {
            for (int i = 0; i < mEnemyTrans.Count; i++)
            {

               float temDistance = Vector3.Distance(transform.position, mEnemyTrans[i].position);
               if(temDistance<= mUnitData.GetUnitView())
                {
                    mSearchAim = mEnemyTrans[i];
                    ChangeTarget(mSearchAim);
                    break;
                }
            }
            //
        }
    }
    //search final Attack Aim
    public void SearchAttackAim()
    {
        if (mAttackAim == null && mSearchAim != null)
        {
            for (int i = 0; i < mEnemyTrans.Count; i++)
            {
                float temDistance = Vector3.Distance(transform.position, mEnemyTrans[i].position);
                if (temDistance <= mUnitData.GetUnitAttackRange())
                {
                    mAttackAim = mEnemyTrans[i];
                    TryOperState(StateEnum.Prepare, StateOper.Enter);
                    break;
                }
            }
            //float temDistance = Vector3.Distance(transform.position, mSearchAim.position);
            //if (temDistance <= mUnitData.GetUnitAttackRange())
            //{
            //    mAttackAim = mSearchAim;
            //    TryOperState(StateEnum.Prepare, StateOper.Enter);
            //}
        }
    }

    public void SetAttackAim(Transform tAttackAim)
    {
        mAttackAim = tAttackAim;
    }

    public StateMachine GetStateMachine()
    {
        return mStateMachine;
    }
    
    public UnitData GetUnitData()
    {
        return mUnitData;
    }
    public void SetUnitData(UnitData tUnitDate)
    {
        mUnitData = tUnitDate;
    }
    public Transform GetAttackAim()
    {
        return mAttackAim;
    }
    public Transform GetSearchAim()
    {
        return mSearchAim;
    }

    public void SetUnitHP(float tHP)
    {
        mUnitData.SetUnitHP(tHP);
        if (mStateMachine.GetCurrentState().StateType != StateEnum.Die&& MWillDie == true)
        {
            //if ((mUnitData.GetAttackType() & UnitAttackType.ShortAttack) == UnitAttackType.ShortAttack)
            {
                //avoid  endless loop death chain
                if (mAttackAim != null && mAttackAim.GetComponent<UnitController>().MWillDie == true)
                {
                    mAttackAim.GetComponent<UnitController>().TryOperState(StateEnum.Die, StateOper.Switch);
                    EventManager.GetInstance().SendEvent(EventId.SomeOneDie, mAttackAim);
                    //LogModule.DebugLog("avoid mAttackAim died");
                }
                //avoid  endless loop death chain
                if (mSearchAim != null && mSearchAim.GetComponent<UnitController>().MWillDie == true)
                {
                    mSearchAim.GetComponent<UnitController>().TryOperState(StateEnum.Die, StateOper.Switch);
                    EventManager.GetInstance().SendEvent(EventId.SomeOneDie, mSearchAim);
                    //LogModule.DebugLog("avoid mSearchAim died");
                }
            }
            MWillDie = false;
            TryOperState(StateEnum.Die, StateOper.Switch);
            EventManager.GetInstance().SendEvent(EventId.SomeOneDie, transform);
        }
    }

    // Update is called once per frame
    void Update () {
        /******************
        Unit StateMachine Running
        *******************/
        if (mThinkTime < float.Epsilon)
        {
            mStateMachine.StateWork();
            mThinkTime = mThinkTimeSpace;
            SearchAim();
            SearchAttackAim();
        }
        else
        {
            mThinkTime -= Time.deltaTime;
        }
    }
    /******************
    Squad order Function 
    *******************/
    public void TryOperState(StateEnum tAimStateEnum, StateOper tOper)
    {
        if (mStateMachine.GetCurrentState().StateType == StateEnum.Die)
        {
            return;
        }
        mStateMachine.OperState(tAimStateEnum, tOper);
    }
    /******************
    Idle State Function 
    *******************/
    void UnitIdleGetInFunc()
    {
        if(mOriginalAim!=null)
            ChangeTarget(mOriginalAim);

        ChangeMoving(false, false);
        mUnitAnimation.Play(AnimationTags.mIdle);
    }
    void UnitIdleExcuteFunc()
    {
        if (mAttackSpaceTimer >= mUnitData.GetAttackSpaceTime())
            mAttackSpaceTimer = mUnitData.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime;


        if (mRichAI.enabled == true && mRVOController.enabled && mRVOController.velocity.sqrMagnitude>0.01f)
        {
            TryOperState(StateEnum.Walk, StateOper.Enter);
        }
    }
    void UnitIdleGetOutFunc()
    {
    }
    /******************
    Walk State Function 
    *******************/
    void UnitWalkGetInFunc()
    {
        ChangeMoving(true, true);
        mUnitAnimation.Play(AnimationTags.mRun);
    }

    void UnitWalkExcuteFunc()
    {
        //calculate  Attack time space
        if (mAttackSpaceTimer >= mUnitData.GetAttackSpaceTime())
            mAttackSpaceTimer = mUnitData.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime;

        //if arrive the target Pos change the state
        if (mAttackAim == null&& mSearchAim == null)
        {
            //if arrive the target Pos change the state
            if (Vector3.Distance(transform.position, mRichAI.target.transform.position) < mMinDistance)
            {
                TryOperState(StateEnum.Walk, StateOper.Exit);
                return;
            }
        }
        //if there is No Enemy change to Idle
        if(mEnemyTrans.Count == 0&&mIsMarching ==false)
        {
            TryOperState(StateEnum.Walk, StateOper.Exit);
            return;
        }

        //if (mSearchAim != null && mSearchAim.GetComponent<UnitController>().mStateMachine.GetCurrentState().StateType == StateEnum.Walk)
        //{
        //    if (mAttackChaseTimer >= mAttackChaseTimeSpace)
        //    {
        //        mAttackChaseTimer = 0f;
        //        ReFindEnemy();
        //    }
        //    else
        //    {
        //        mAttackChaseTimer += Time.deltaTime;
        //    }
        //}
    }
    void UnitWalkGetOutFunc()
    {
        //mAttackChaseTimer = 0f;
        mSearchAim = null;
        ChangeMoving(false, false);
    }
    /******************
    Atack State Function 
    *******************/
    void UnitAttackGetInFunc()
    {
        transform.LookAt(mAttackAim, Vector3.up);
        mAttackSpaceTimer = 0.0f;

        mUnitAnimation.Play(AnimationTags.mAttack);
    }
    void UnitAttackExcuteFunc()
    {
        if (mAttackAim == null)
            TryOperState(StateEnum.Attack, StateOper.Exit);
    }
    void UnitAttackGetOutFunc()
    {

    }
    /******************
    Prepare State Function 
    *******************/
    void UnitPrepareGetInFunc()
    {
        transform.LookAt(mAttackAim, Vector3.up);
        mUnitAnimation.Play(AnimationTags.mAttackStand);
    }
    void UnitPrepareExcuteFunc()
    {
        if (mAttackAim == null)
            TryOperState(StateEnum.Prepare, StateOper.Exit);

        if (mAttackAim != null && Vector3.Distance(transform.position, mAttackAim.position) > 1.1f * mUnitData.GetUnitAttackRange())
        {
            mSearchAim = mAttackAim;
            mAttackAim = null;
            TryOperState(StateEnum.Prepare, StateOper.Exit);
            //LogModule.DebugLog("HIt and RUN");
        }

        if (mAttackSpaceTimer >= mUnitData.GetAttackSpaceTime())
        {
            mAttackSpaceTimer = mUnitData.GetAttackSpaceTime();
            TryOperState(StateEnum.Attack, StateOper.Enter);
        }
        mAttackSpaceTimer += Time.deltaTime;
    }
    void UnitPrepareGetOutFunc()
    {
    }

    /******************
    Die State Function 
    *******************/
    void UnitDieGetInFunc()
    {
        mUnitAnimation.Play(AnimationTags.mDie);
        mOriginalAim.gameObject.SetActive(false);
        ChangeMoving(false, false);
    }
    void UnitDieExcuteFunc()
    {
    }
    void UnitDieGetOutFunc()
    {
    }

    /******************
    Event From the AnimationController
    *******************/
    public void UnitAttackEnd()
    {
        TryOperState(StateEnum.Attack, StateOper.Exit);
    }
    public void UnitAttackEffect()
    {
        if(mAttackAim != null)
        {
            if((mUnitData.GetAttackType() & UnitAttackType.ShortAttack) == UnitAttackType.ShortAttack)
            {
                UnitController mAttackAimController = mAttackAim.GetComponent<UnitController>();
                mAttackAimController.SetUnitHP(mAttackAimController.GetUnitData().GetUnitHP() - mAttackAimController.GetUnitData().GetAttack());
                return;
            }

            if((mUnitData.GetAttackType() & UnitAttackType.RemoteAttack) == UnitAttackType.RemoteAttack)
            {
                //Transform tFinalTrans;
                //if (mForceAttackList.Count != 0)
                //{
                //    tFinalTrans = mForceAttackList[0];
                //}else
                //{
                //    tFinalTrans = mAttackAim;
                //}
                if (Vector3.Distance(transform.position, mAttackAim.position) < 10f)
                {
                    UnitController tAttackAimController = mAttackAim.GetComponent<UnitController>();
                    tAttackAimController.SetUnitHP(tAttackAimController.GetUnitData().GetUnitHP() - tAttackAimController.GetUnitData().GetAttack());
                    return;
                }

                mArrow = GameObjectPool.GetInstance().SpawnGo((GameObject)mArrowAsset).transform;
                mArrow.parent = null;
                mArrow.gameObject.SetActive(true);
                UnitController mAttackAimController = mAttackAim.GetComponent<UnitController>();
                mArrow.position = transform.position;
                SetmArrow(mArrow);
                mArrowController.MReceiver = mAttackAimController.mReceverTrans;
                mArrowController.MScender = mSenderTrans;
                mArrowController.SetArrowData();

            }
        }
    }

    public void ArrowDamageEffect(float param)
    {
        SetUnitHP(mUnitData.GetUnitHP() - param);
    }

    public void UnitDieEnd()
    {
        transform.gameObject.SetActive(false);
    }
    void SomeOneDie(object param)
    {
        Transform diedTrans = (Transform)param;
                        
        if (diedTrans != transform&&mEnemyTrans.Contains(diedTrans))
        {
            mEnemyTrans.Remove(diedTrans);
            if (mAttackAim == diedTrans)
            {
                mAttackAim = null;
                MKillOrder = false;
            }
            if (mSearchAim == diedTrans)
            {
                mSearchAim = null;
                MKillOrder = false;
            }

            if (mEnemyTrans.Count == 0)
            {
                UnitMarching(SquadTransform.position);
            }               
        }
        //if(mForceAttackList.Contains(diedTrans))
        //{
        //    mForceAttackList.Remove(diedTrans);
        //}
    }
    //public void SetDiedState()
    //{
    //    MWillDie = true;
    //    if (mStateMachine.GetCurrentState().StateType != StateEnum.Die)
    //    {
    //        TryOperState(StateEnum.Die, StateOper.Switch);
    //        EventManager.GetInstance().SendEvent(EventId.SomeOneDie, transform);
    //    }

    //    StartCoroutine(DieLag(0.3f));

    //    AttemptSuicide();
    //}
    //void AttemptSuicide()
    //{
    //    MWillDie = true;
    //    int count = Random.Range(0, mEnemyTrans.Count);
    //    mEnemyTrans[count].GetComponent<UnitController>().DoForceAttack(transform);
    //}
    //public void DoForceAttack(Transform param)
    //{
    //    mForceAttackList.Add(param);
    //    mAttackSpaceTimer = mUnitData.GetAttackSpaceTime();
    //}
    public void SetAttackSpaceTimer(float tSpaceTimer)
    {
        mAttackSpaceTimer = tSpaceTimer;
    }
    IEnumerator DieLag(float time)
    {
        yield return new WaitForSeconds(time);
        if(mStateMachine.GetCurrentState().StateType != StateEnum.Die)
        {
            TryOperState(StateEnum.Die, StateOper.Switch);
            EventManager.GetInstance().SendEvent(EventId.SomeOneDie, transform);
        }
    }

    // refresh EnemyList
    public void RefreshEnemyList(Transform tAimEnemy)
    {
        mEnemyTrans.Clear();
        mEnemyTrans.AddRange(tAimEnemy.GetComponent<SquadController>().GetUnitControllerList());
    }
    public void FindAndAttackEnemy(Transform tAimEnemy)
    {
        RefreshEnemyList(tAimEnemy);
        //Select the Enemy in the EnemyList;
        int tId = Random.Range(0, mEnemyTrans.Count);
        mAttackAim = null;
        mSearchAim = mEnemyTrans[tId];
        ChangeTarget(mSearchAim);;
        ChangeMoving(true, true);
    }
    public void UnitMarching(Vector3 tPos,float tAngle=0f)
    {
        //Vector3 tAimPos = transform.position - mSquadLeader.position + (Vector3)tPos;
        if (mStateMachine.GetCurrentState().StateType == StateEnum.Idle)
            TryOperState(StateEnum.Walk, StateOper.Enter);
        else
            return;

        Vector3 tAimPos = RelativePosition + tPos;
        mRichAI.target = mOriginalAim;
        mRichAI.target.position = tAimPos;

        mRichAI.target.RotateAround(tPos, Vector3.up, tAngle);
        mRichAI.UpdatePath(); 
        mIsMarching = true;
    }
    public void EmbattingSetPos(Vector3 tPos)
    {
        Vector3 tAimPos = RelativePosition + tPos;
        transform.position = tAimPos;
        mRichAI.target.position = tAimPos;
    }
    public void MoveForce(Vector3 tPos)
    {
        mRichAI.target = mOriginalAim;
        mRichAI.target.position = tPos;
        if (mStateMachine.GetCurrentState().StateType == StateEnum.Idle)
            TryOperState(StateEnum.Walk, StateOper.Enter);
        mIsMarching = true;
    }
    public StateEnum GetUnitState()
    {
        return mStateMachine.GetCurrentState().StateType;
    }
    // Asynchronous back
    public IEnumerator CheckState()
    {
        bool judge=true;
        while (judge)
        {
            yield return new WaitForSeconds(0.5f);
            judge = mStateMachine.GetCurrentState().StateType != StateEnum.Idle;
        }
        //LogModule.DebugLog("Unit Done");
    }

    //Re find Enemy
    void ReFindEnemy()
    {
        //float tDistanceFinal = 100f;
        //Transform tTransfrom = mEnemyTrans[0];
        //for (int i = 0; i < mEnemyTrans.Count; i++)
        //{
        //    if (mEnemyTrans[i].GetComponent<UnitController>().mStateMachine.GetCurrentState().StateType == StateEnum.Walk)
        //    {
        //        continue;
        //    }
        //    float tDistance = Vector3.Distance(transform.position, mEnemyTrans[i].position);
        //    if (tDistance <= mUnitData.GetUnitAttackRange())
        //    {
        //        mAttackAim = tTransfrom;
        //        TryOperState(StateEnum.Prepare, StateOper.Enter);
        //        break;
        //    }
        //    else
        //    {
        //        if (tDistanceFinal > tDistance)
        //        {
        //            tDistanceFinal = tDistance;
        //            tTransfrom = mEnemyTrans[i];
        //        }
        //    }
        //}
        //if (mAttackAim == null)
        //{
        //    mSearchAim = tTransfrom;
        //    ChangeTarget(mSearchAim);
        //}
    }
}
