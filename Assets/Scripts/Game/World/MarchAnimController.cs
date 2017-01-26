//----------------------------------------------
//	CreateTime  : 1/23/2017 10:38:40 AM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : MarchAnimController
//	ChangeLog   : None
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animation))]
public class MarchAnimController : MonoBehaviour, IInit, IDestroy
{
    public Animation CurrentAnimation;

    public enum ClipName
    {
        Stand,
        Run,
        Attack,
        Die,
    }

    public void Play(ClipName p_name, Action p_completeCallBack = null)
    {
        switch (p_name)
        {
            case ClipName.Stand:
            case ClipName.Run:
                {
                    if (!CurrentAnimation.IsPlaying(p_name.ToString()))
                    {
                        CurrentAnimation.Play(p_name.ToString(), PlayMode.StopAll);
                    }
                    break;
                }
            case ClipName.Attack:
                {
                    CurrentAnimation.Play(p_name.ToString(), PlayMode.StopAll);
                    if (p_completeCallBack != null)
                    {
                        StartCoroutine(AnimCompleteCallBack(CurrentAnimation.GetClip(p_name.ToString()).length,
                            p_completeCallBack));
                    }
                    CurrentAnimation.PlayQueued(ClipName.Stand.ToString(), QueueMode.CompleteOthers, PlayMode.StopAll);
                    break;
                }
            case ClipName.Die:
                {
                    CurrentAnimation.Play(p_name.ToString(), PlayMode.StopAll);
                    if (p_completeCallBack != null)
                    {
                        StartCoroutine(AnimCompleteCallBack(CurrentAnimation.GetClip(p_name.ToString()).length,
                            p_completeCallBack));
                    }
                    break;
                }
        }
    }

    IEnumerator AnimCompleteCallBack(float p_delay, Action p_completeCallBack)
    {
        yield return new WaitForSeconds(p_delay);

        p_completeCallBack();
    }

    void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        CurrentAnimation = GetComponent<Animation>();
        if (CurrentAnimation == null)
        {
            LogModule.ErrorLog("No animation in MarchAnimController");
            return;
        }
    }

    public void OnDestroy()
    {

    }
}
