using UnityEngine;
using System.Collections;
/// <summary>
/// 基础行为控制器
/// by Zhengxuesong
/// 2016-11-11
/// </summary>

public class BaseController : MonoBehaviour {

    // States of the Unit 
    protected BaseState mIdleState;
    protected BaseState mWalkState;
    protected BaseState mAttackState;
    protected BaseState mPrepareState;
    protected BaseState mDieState;
    protected StateMachine mStateMachine;

    protected void InitAwake()
    {
        mIdleState = new BaseState();
        mIdleState.StateType = StateEnum.Idle;

        mWalkState = new BaseState();
        mWalkState.StateType = StateEnum.Walk;
  

        mAttackState = new BaseState();
        mAttackState.StateType = StateEnum.Attack;

        mPrepareState = new BaseState();
        mPrepareState.StateType = StateEnum.Prepare;

        mDieState = new BaseState();
        mDieState.StateType = StateEnum.Die;



        mIdleState.mdGetInState += IdleGetInFunc;
        mIdleState.mdGetOutState += IdleGetOutFunc;
        mIdleState.mdExcuteState += IdleExcuteFunc;

        mWalkState.mdGetInState += WalkGetInFunc;
        mWalkState.mdExcuteState += WalkExcuteFunc;
        mWalkState.mdGetOutState += WalkGetOutFunc;

        mAttackState.mdGetInState += AttackGetInFunc;
        mAttackState.mdExcuteState += AttackExcuteFunc;
        mAttackState.mdGetOutState += AttackGetOutFunc;

        mPrepareState.mdGetInState += PrepareGetInFunc;
        mPrepareState.mdExcuteState += PrepareExcuteFunc;
        mPrepareState.mdGetOutState += PrepareGetOutFunc;

        mDieState.mdGetInState += DieGetInFunc;
        mDieState.mdExcuteState += DieExcuteFunc;
        mDieState.mdGetOutState += DieGetOutFunc;


        mStateMachine = new StateMachine(mIdleState, StateEnum.Idle);
        mStateMachine.IsWorking = true;
        mStateMachine.RegisterState(mWalkState, StateEnum.Walk);
        mStateMachine.RegisterState(mAttackState, StateEnum.Attack);
        mStateMachine.RegisterState(mPrepareState, StateEnum.Prepare);
        mStateMachine.RegisterState(mDieState, StateEnum.Die);
    }
    void IdleGetInFunc()
    {

    }
    void IdleGetOutFunc()
    {

    }
    void IdleExcuteFunc()
    {

    }

    void WalkGetInFunc()
    {

    }
    void WalkExcuteFunc()
    {

    }
    void WalkGetOutFunc()
    {

    }

    void AttackGetInFunc()
    {

    }
    void AttackExcuteFunc()
    {

    }
    void AttackGetOutFunc()
    {

    }

    void PrepareGetInFunc()
    {

    }
    void PrepareExcuteFunc()
    {

    }
    void PrepareGetOutFunc()
    {

    }
    void DieGetInFunc()
    {

    }
    void DieExcuteFunc()
    {

    }
    void DieGetOutFunc()
    {

    }
}
