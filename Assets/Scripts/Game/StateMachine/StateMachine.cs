using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 状态机
/// by Zhengxuesong
/// 2016-11-11
/// </summary>
public enum StateOper{
    Enter=0,
    Switch,
    Exit
}
public enum StateEnum {
    Idle=0,
    Walk,
    Prepare,
    Attack,
    Die
}
public delegate void GetInState();
public delegate void ExcuteState();
public delegate void GetOutState();
public delegate bool OperState(StateOper tOper);

public class StateMachine {
    List<BaseState> mStateList;
    Dictionary<StateEnum, BaseState>mAllStateDic;
    public bool IsWorking { get; set; }

    public StateMachine(BaseState tAimState, StateEnum tunitStateEnum)
    {
        mStateList = new List<BaseState>();
        mStateList.Add(tAimState);

        mAllStateDic = new Dictionary<StateEnum, BaseState>();
        mAllStateDic[tunitStateEnum] = tAimState;
    }

    public  void StartWorking()
    {
        mStateList[0].mdGetInState();
    }
    public void InSertState(BaseState tAimState)
    {
        mStateList.Insert(0, tAimState);
    }
    public void RegisterState(BaseState tAimState, StateEnum tunitStateEnum)
    {
        mAllStateDic[tunitStateEnum] = tAimState;
    }
    public void StateWork()
    {
        if (IsWorking)
        {
            mStateList[0].mdExcuteState();
        }   
    }
    public BaseState GetCurrentState()
    {
        return mStateList[0];
    }
    /******************
    this Function aims to operThe State
    *******************/
    public void OperState(StateEnum tAimStateEnum, StateOper tOper)
    {
        if (!IsWorking)
        {
            return;
        }

        switch (tOper)
        {
            case StateOper.Enter:
                if (mStateList.Count > 0)
                    mStateList[0].mdGetOutState();
                mStateList.Insert(0, mAllStateDic[tAimStateEnum]);
                mStateList[0].mdGetInState();
                break;

            case StateOper.Switch:
                mStateList[0].mdGetOutState();
                mStateList[0] = mAllStateDic[tAimStateEnum];
                mStateList[0].mdGetInState();
                break;

            case StateOper.Exit:
                mStateList[0].mdGetOutState();
                mStateList.RemoveAt(0);
                if (mStateList.Count > 0)
                    mStateList[0].mdGetInState();             
                break;
        }
        if (mStateList.Count == 0)
        {
            Debug.LogError("state machine is empty");
        }
    }
};
public class BaseState {

    public StateEnum StateType { get; set; }
    public GetInState mdGetInState;
    public ExcuteState mdExcuteState;
    public ExcuteState mdGetOutState;
};



