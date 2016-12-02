using UnityEngine;
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
    private const float mThinkTimeSpace = 0.05f;
    // the Unit Date
    private UnitData mUnitDate;
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
    private float mMinDistance = 0.5f;
    // the SquadLeader of the Squad;
    public Transform mSquadLeader { get; set; }

    //Arrow
    private Transform mArrow;
    private Arrow mArrowController;
    //the prefab of the Arrow
    private Object mArrowAsset;

    // if one Unit can sender a Arrow the Arrow start transformPos is the mSenderTrans Pos
    public Transform mSenderTrans;
    // if one was shoted by the Arrow the mReceverTrans Pos is the Arrow's end point
    public Transform mReceverTrans;
    // Use this for initialization
	void Awake()
    {
        mEnemyTrans = new List<Transform>();
        mUnitDate = new UnitData();

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
    void SetUpEventHandlers()
    {
        EventManager.GetInstance().AddEventListener(EventId.EventConcentratedFire, EventConcentratedFire);

        EventManager.GetInstance().AddEventListener(EventId.AttackAnimationEnd + mUnitDate.GetID(), UnitAttackEnd);
        EventManager.GetInstance().AddEventListener(EventId.AttackAnimationEffect + mUnitDate.GetID(), UnitAttackEffect);
        EventManager.GetInstance().AddEventListener(EventId.DieAnimationEnd + mUnitDate.GetID(), UnitDieEnd);

        EventManager.GetInstance().AddEventListener(EventId.SomeOneDie, SomeOneDie);
        EventManager.GetInstance().AddEventListener(EventId.RefreshEnemyList + mUnitDate.GetID(), RefreshEnemyList);
        EventManager.GetInstance().AddEventListener(EventId.FindAndAttackEnemy + mUnitDate.GetID(), FindAndAttackEnemy);
        EventManager.GetInstance().AddEventListener(EventId.UnitMarching + mUnitDate.GetID(), UnitMarching);

        EventManager.GetInstance().AddEventListener(EventId.ArrowDamageEffect + mUnitDate.GetID(), ArrowDamageEffect);
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
        mRichAI.UpdatePath();
    }

    //serach Attack Aim;
    public void SearchAim()
    {
        if(mSearchAim == null&& mAttackAim==null)
        {
            for (int i = 0; i < mEnemyTrans.Count; i++)
            {
               float temDistance = Vector3.Distance(transform.position, mEnemyTrans[i].position);
               if(temDistance<= mUnitDate.GetUnitView())
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
                if (temDistance <= mUnitDate.GetUnitAttackRange())
                {
                    mAttackAim = mEnemyTrans[i];
                    TryOperState(StateEnum.Prepare, StateOper.Enter);
                    break;
                }
            }
        }
    }

    public StateMachine GetStateMachine()
    {
        return mStateMachine;
    }
    
    public UnitData GetUnitData()
    {
        return mUnitDate;
    }
    public void SetUnitData(UnitData tUnitDate)
    {
        mUnitDate = tUnitDate;
    }

    public void SetUnitHP(float tHP)
    {
        mUnitDate.SetUnitHP(tHP);
        if (tHP <= 0&&mStateMachine.GetCurrentState().StateType!=StateEnum.Die)
        {
            TryOperState(StateEnum.Die, StateOper.Switch);
            EventManager.GetInstance().SendEvent(EventId.SomeOneDie,transform);
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
    void TryOperState(StateEnum tAimStateEnum, StateOper tOper)
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
        transform.LookAt(new Vector3(transform.forward.x,0.0f,transform.forward.z), Vector3.up);

        if(mOriginalAim!=null)
            ChangeTarget(mOriginalAim);

        ChangeMoving(false, false);
        mUnitAnimation.Play(AnimationTags.mIdle);
    }
    void UnitIdleExcuteFunc()
    {
        if (mAttackSpaceTimer >= mUnitDate.GetAttackSpaceTime())
            mAttackSpaceTimer = mUnitDate.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime;


        if (mRVOController.enabled && mRVOController.velocity.sqrMagnitude>0.01f)
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
        if (mAttackSpaceTimer >= mUnitDate.GetAttackSpaceTime())
            mAttackSpaceTimer = mUnitDate.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime;;

        //if arrive the target Pos change the state
        if (mAttackAim == null&& mAttackAim == null)
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
    }
    void UnitWalkGetOutFunc()
    {
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

        if (mAttackAim != null && Vector3.Distance(transform.position, mAttackAim.position) > 1.1f * mUnitDate.GetUnitAttackRange())
        {
            mSearchAim = mAttackAim;
            mAttackAim = null;
            TryOperState(StateEnum.Prepare, StateOper.Exit);
        }

        if (mAttackSpaceTimer >= mUnitDate.GetAttackSpaceTime())
        {
            mAttackSpaceTimer = mUnitDate.GetAttackSpaceTime();
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

    void EventConcentratedFire(object param)
    {

    }
    void UnitAttackEnd(object param)
    {
        TryOperState(StateEnum.Attack, StateOper.Exit);
    }
    void UnitAttackEffect(object param)
    {
        if(mAttackAim != null)
        {
            if((mUnitDate.GetAttackType() & UnitAttackType.ShortAttack) == UnitAttackType.ShortAttack)
            {
                UnitController mAttackAimController = mAttackAim.GetComponent<UnitController>();
                mAttackAimController.SetUnitHP(mAttackAimController.GetUnitData().GetUnitHP() - mAttackAimController.GetUnitData().GetAttack());
            }

            if((mUnitDate.GetAttackType() & UnitAttackType.RemoteAttack) == UnitAttackType.RemoteAttack)
            {

                mArrow = GameObjectPool.GetInstance().SpawnGo((GameObject)mArrowAsset).transform;
                mArrow.parent = null;
                mArrow.gameObject.SetActive(true);
                UnitController mAttackAimController = mAttackAim.GetComponent<UnitController>();
                mArrow.position = transform.position;
                mArrowController = mArrow.GetComponent<Arrow>();
                SetmArrow(mArrow);
                mArrowController.MReceiver = mAttackAimController.mReceverTrans;
                mArrowController.MScender = mSenderTrans;
                mArrowController.SetArrowData();

            }
        }
    }

    void ArrowDamageEffect(object param)
    {
        SetUnitHP(mUnitDate.GetUnitHP() - (float)param);
    }

    void UnitDieEnd(object param)
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
                mAttackAim = null;
            if (mSearchAim == diedTrans)
                mSearchAim = null;
        }
    }
    // refresh EnemyList
    void RefreshEnemyList(object tEnemy)
    {
        Transform tAimEnemy = (Transform)tEnemy;
        mEnemyTrans.Clear();
        mEnemyTrans.AddRange(tAimEnemy.GetComponent<SquadController>().GetUnitControllerList());
    }

    void FindAndAttackEnemy(object tEnemy)
    {
        RefreshEnemyList(tEnemy);
        //Select the Enemy in the EnemyList;
        int tId = Random.Range(0, mEnemyTrans.Count);
        mSearchAim = mEnemyTrans[tId];
        ChangeTarget(mSearchAim);
        ChangeMoving(true, true);
    }
    void UnitMarching(object tPos)
    {
        Vector3 tAimPos = transform.position - mSquadLeader.position + (Vector3)tPos;
        mRichAI.target = mOriginalAim;
        mRichAI.target.position = tAimPos;
        mRichAI.UpdatePath();
        TryOperState(StateEnum.Walk, StateOper.Enter);
        mIsMarching = true;
    }
}
