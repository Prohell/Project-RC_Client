using UnityEngine;
using GCGame.Table;
/// <summary>
/// 动画控制器
/// by Zhengxuesong
/// 2016-11-14
/// </summary>
public class AnimationController :MonoBehaviour {
    private Animation mAnimation;

    private UnitController mUnitController;

    void Awake()
    {
    }
    void SetAnimationClipCallBack(string tClipName,string tCallBackName,float tEventTimes=99.9f,int tCallBackPara=0)
    {

        AnimationClip tAnimationClip = mAnimation.GetClip(tClipName);
        for (int i = 0; i < tAnimationClip.events.Length; i++)
        {
            if (tAnimationClip.events[i].functionName == tCallBackName)
            {
                return;
            }
        }
        AnimationEvent tEvt = new AnimationEvent();
        tEvt.intParameter = tCallBackPara;
        if (tEventTimes == 99.9f)
            tEvt.time = tAnimationClip.length - 0.05f;
        else
            tEvt.time = tEventTimes;
        tEvt.functionName = tCallBackName;
        tAnimationClip.AddEvent(tEvt);
    }
    public Animation GetAnimation()
    {
        return mAnimation;
    }

    void Start()
    {
        mAnimation = GetComponent<Animation>();
        mUnitController = transform.parent.GetComponent<UnitController>();

        foreach (var item in TableManager.GetAnimationEvent())
        {
            if (item.Value[0].PrefabName.Equals(transform.name))
            {
                SetAnimationClipCallBack(item.Value[0].AnimationClipName, item.Value[0].FunctionName, item.Value[0].Time);
            }
        }
    }
    public void AttackEnd()
    {
        mUnitController.UnitAttackEnd();
    }
    public void DieEnd()
    {
        mUnitController.UnitDieEnd();
    }
    public void AttackEffect()
    {
        mUnitController.UnitAttackEffect();
    }
}

public class AnimationTags {
    public const string mRun = "Run";
    public const string mIdle = "Stand";
    public const string mDie = "Die";
    public const string mAttack = "Attack2B_01";
    public const string mAttackStand = "Attack_Stand";
}