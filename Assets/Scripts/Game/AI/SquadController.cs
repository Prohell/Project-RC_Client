using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.RVO;
using Pathfinding;

/// <summary>
/// 小队行为控制器
/// by Zhengxuesong
/// 2016-11-11
/// </summary>

public class SquadController : BaseController
{
    private RichAI mRichAI;

    // States of the Team 
    private StateMachine mSquadStateMachine;
    // the Squad Date
    private SquadData mSquadData;
    //the Unit Date List
    private List<Transform> mUnitTransformList;
    // EnemySquad List
    private Dictionary<int, SquadController> mEnemySquadDic;

    private Transform mSearchAim;
    private Transform mAttackAim;
    private Transform mOriginalAim;
    //Marching state;
    private bool mIsMarching;

    // the Attack Timer
    private float mAttackSpaceTimer;

    //private float mLastTime;
    private float mMinDistance = 0.5f;

    //the Squad Brain Thinking Time
    private float mThinkTime;
    private const float mThinkTimeSpace = 0.05f;

    void Awake()
    {
        mRichAI = GetComponent<RichAI>();
        mSquadData = new SquadData();
        mUnitTransformList = new List<Transform>();
        mEnemySquadDic = new Dictionary<int, SquadController>();

        InitAwake();

        mIdleState.mdGetInState += SquadIdleGetInFunc;
        mIdleState.mdGetOutState += SquadIdleGetOutFunc;
        mIdleState.mdExcuteState += SquadIdleExcuteFunc;

        mWalkState.mdGetInState += SquadWalkGetInFunc;
        mWalkState.mdExcuteState += SquadWalkExcuteFunc;
        mWalkState.mdGetOutState += SquadWalkGetOutFunc;

        mAttackState.mdGetInState += SquadAttackGetInFunc;
        mAttackState.mdExcuteState += SquadAttackExcuteFunc;
        mAttackState.mdGetOutState += SquadAttackGetOutFunc;

        mPrepareState.mdGetInState += SquadPrepareGetInFunc;
        mPrepareState.mdExcuteState += SquadPrepareExcuteFunc;
        mPrepareState.mdGetOutState += SquadPrepareGetOutFunc;

        mDieState.mdGetInState += SquadDieGetInFunc;
        mDieState.mdExcuteState += SquadDieExcuteFunc;
        mDieState.mdGetOutState += SquadDieGetOutFunc;

        mStateMachine.StartWorking();

    }

    public List<Transform> GetUnitControllerList()
    {
        return mUnitTransformList;
    }

    public void AddEnemy(int id, SquadController tSquad)
    {
        mEnemySquadDic.Add(id,tSquad);
    }
    public SquadData GetSquadData()
    {
        return mSquadData;
    }

    // Update is called once per frame
    void Update()
    {
        /******************
        Squad StateMachine Running
        *******************/
        if (mThinkTime < float.Epsilon)
        {
            mStateMachine.StateWork();
            mThinkTime = mThinkTimeSpace;
        }
        else
        {
            mThinkTime -= Time.deltaTime;
        }
    }
    public void ChangeTarget(Transform tTarget)
    {
        /******************
        the follow script is test the RichAI move
        *******************/
        mIsMarching = false;
        mRichAI.target = tTarget;
        mRichAI.UpdatePath();
    }
    public void SetmOriginalAim(Transform tTarget)
    {

        mRichAI.target = tTarget;
        mOriginalAim = mRichAI.target;
        mRichAI.UpdatePath();
    }

    //serach Attack Aim;
    public void SearchAim()
    {
        if (mSearchAim == null && mAttackAim == null)
        {
            for (int i = 0; i < mEnemySquadDic.Count; i++)
            {
                float temDistance = Vector3.Distance(transform.position, mEnemySquadDic[i].transform.position);
                if (temDistance <= mSquadData.GetSquadView())
                {
                    mSearchAim = mEnemySquadDic[i].transform;
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
            //search Attack Aim
            for (int i = 0; i < mEnemySquadDic.Count; i++)
            {
                float temDistance = Vector3.Distance(transform.position, mEnemySquadDic[i].transform.position);
                if (temDistance <= mEnemySquadDic[i].GetSquadData().GetSquadAttackRange())
                {
                    mAttackAim = mEnemySquadDic[i].transform;
                    TryOperState(StateEnum.Prepare, StateOper.Enter);
                    break;
                }
            }
        }
    }
    /******************
    Idle State Function 
    *******************/
    void SquadIdleGetInFunc()
    {
        transform.LookAt(new Vector3(transform.forward.x, 0.0f, transform.forward.z), Vector3.up);

        if (mOriginalAim != null)
            ChangeTarget(mOriginalAim);

        ChangeMoving(false);
    }
    void SquadIdleGetOutFunc()
    {

    }
    void SquadIdleExcuteFunc()
    {
        if (mAttackSpaceTimer >= mSquadData.GetAttackSpaceTime())
            mAttackSpaceTimer = mSquadData.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime;


        if (mRichAI.enabled && mRichAI.Velocity.sqrMagnitude > 0.01f)
        {
            TryOperState(StateEnum.Walk, StateOper.Enter);
        }
    }
    /******************
    Walk State Function 
    *******************/
    void SquadWalkGetInFunc()
    {
        ChangeMoving(true);
    }
    void SquadWalkGetOutFunc()
    {
        mSearchAim = null;
        ChangeMoving(false);
    }
    void SquadWalkExcuteFunc()
    {
        //calculate  Attack time space
        if (mAttackSpaceTimer >= mSquadData.GetAttackSpaceTime())
            mAttackSpaceTimer = mSquadData.GetAttackSpaceTime();
        else
            mAttackSpaceTimer += Time.deltaTime; ;

        //if arrive the target Pos change the state
        if (mAttackAim == null && mAttackAim == null)
        {
            //if arrive the target Pos change the state
            if (Vector3.Distance(transform.position, mRichAI.target.transform.position) < mMinDistance)
            {
                TryOperState(StateEnum.Walk, StateOper.Exit);
                return;
            }
        }
        //if there is No Enemy change to Idle
        if (mEnemySquadDic.Count == 0 && mIsMarching == false)
        {
            TryOperState(StateEnum.Walk, StateOper.Exit);
            return;
        }
    }
    /******************
    Atack State Function 
    *******************/
    void SquadAttackGetInFunc()
    {
        transform.LookAt(mAttackAim, Vector3.up);
        mAttackSpaceTimer = 0.0f;
    }
    void SquadAttackExcuteFunc()
    {
        if (mAttackAim == null)
            TryOperState(StateEnum.Attack, StateOper.Exit);
    }
    void SquadAttackGetOutFunc()
    {

    }
    /******************
    Prepare State Function 
    *******************/
    void SquadPrepareGetInFunc()
    {
        transform.LookAt(mAttackAim, Vector3.up);
    }
    void SquadPrepareExcuteFunc()
    {
        if (mAttackAim == null)
            TryOperState(StateEnum.Prepare, StateOper.Exit);

        if (mAttackSpaceTimer >= mSquadData.GetAttackSpaceTime())
        {
            mAttackSpaceTimer = mSquadData.GetAttackSpaceTime();
            TryOperState(StateEnum.Attack, StateOper.Enter);
        }
        mAttackSpaceTimer += Time.deltaTime;
    }
    void SquadPrepareGetOutFunc()
    {

    }

    /******************
    Die State Function 
    *******************/
    void SquadDieGetInFunc()
    {
        ChangeMoving(false);
    }
    void SquadDieExcuteFunc()
    {

    }
    void SquadDieGetOutFunc()
    {

    }

    void TryOperState(StateEnum tAimStateEnum, StateOper tOper)
    {
        if (mStateMachine.GetCurrentState().StateType == StateEnum.Die)
        {
            return;
        }
        mStateMachine.OperState(tAimStateEnum, tOper);
    }

    void ResetPosition(Vector3 tAimPostion)
    {
        transform.position = tAimPostion;
    }
    //
    public void ChangeMoving(bool RichAIEnable)
    {
        mRichAI.enabled = RichAIEnable;
    }
    // chose the UnitController has lowest HP 
    void UnitDie()
    {
        if (mUnitTransformList.Count == 0)
            return;
        UnitController tAimUnitController = mUnitTransformList[0].GetComponent<UnitController>();
        for (int i = 1; i < mUnitTransformList.Count; i++)
        {
            if (mUnitTransformList[i].GetComponent<UnitController>().GetUnitData().GetUnitHP() < tAimUnitController.GetUnitData().GetUnitHP())
                tAimUnitController = mUnitTransformList[i].GetComponent<UnitController>();
        }
        EventManager.GetInstance().SendEvent(EventId.SomeOneDie, tAimUnitController.transform);
    }
    // Send command to all the Unit of this Squad let them to select a Enemy in the EnemyList to Attack 
    public void UnitAttackEnemy(int tEnemyId)
    {
        List<Transform> tEnemyList = mEnemySquadDic[tEnemyId].GetUnitControllerList();
        for (int i = 0; i < mUnitTransformList.Count; i++)
        {
            EventManager.GetInstance().SendEvent(EventId.FindAndAttackEnemy + mUnitTransformList[i].GetComponent<UnitController>().GetUnitData().GetID(), mEnemySquadDic[tEnemyId].transform);            
        }
    }
    // Send command to all the Unit of this Squad let them to refresh EnemyList
    public void RefreshUnitEnemyList(int tEnemyId)
    {
        List<Transform> tEnemyList = mEnemySquadDic[tEnemyId].GetUnitControllerList();
        for (int i = 0; i < mUnitTransformList.Count; i++)
        {
            EventManager.GetInstance().SendEvent(EventId.RefreshEnemyList + mUnitTransformList[i].GetComponent<UnitController>().GetUnitData().GetID(), mEnemySquadDic[tEnemyId].transform);
        }
    }

    // Send command to all the Unit of this Squad let them to ConcentratedFire
    void GiveOConcentratedFirerders()
    {
        EventManager.GetInstance().SendEvent(EventId.EventConcentratedFire, null);
    }
    // Send command to all the Unit of this Squad let the to Move to the AimPos, Keeping the relative position
    public void UnitMarching(Vector3 tAimPos)
    {
        for (int i = 0; i < mUnitTransformList.Count; i++)
        {
            EventManager.GetInstance().SendEvent(EventId.UnitMarching+ mUnitTransformList[i].GetComponent<UnitController>().GetUnitData().GetID(), tAimPos);
        }

        mRichAI.target = mOriginalAim;
        mRichAI.target.position = tAimPos;
        mRichAI.UpdatePath();
        TryOperState(StateEnum.Walk, StateOper.Enter);
        mIsMarching = true;
    }
    //Set the Leader of All the Unit
    public void SetLeader()
    {
        for (int i = 0; i < mUnitTransformList.Count; i++)
        {
            mUnitTransformList[i].GetComponent<UnitController>().mSquadLeader = mUnitTransformList[mUnitTransformList.Count/2];
        }
    }
    public void UnitMarchAttacking(float space, Vector3 tAimPos)
    {
        TakeCountOthers(space);
        UnitMarching(tAimPos);
    }
    // Take cout of other Unit in the Squad and adjust the Attack distance
    public void TakeCountOthers(float space)
    {
        for (int i = 0; i < mUnitTransformList.Count; i++)
        {
            UnitData tUnitData = mUnitTransformList[i].GetComponent<UnitController>().GetUnitData();
            space = space * Random.Range(0.5f, 1f);
            switch (i / mSquadData.GetUnitCountOfRow())
            {
                case 0:
                    tUnitData.SetAdjustAttackRange( - 2 * space);
                    break;
                case 1:
                    tUnitData.SetAdjustAttackRange( - space);
                    break;
                case 2:
                    tUnitData.SetAdjustAttackRange(0);
                    break;
            }
        }
    }
    public void RefreshUnitEnemyList(int tEnemyId,float space )
    {
        TakeCountOthers(space);
        RefreshUnitEnemyList(tEnemyId);
    }

}
