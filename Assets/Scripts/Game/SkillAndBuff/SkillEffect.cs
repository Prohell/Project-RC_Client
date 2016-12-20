using UnityEngine;
using System.Collections;
/// 技能效果逻辑
/// by Zhengxuesong
/// 2016-12-7
public class SkillEffect : MonoBehaviour {

    private SkillData mSkillData;
    private float mLifeTimeMax = 5f;
    private float mLifeTime;
    private float mRadius;

    private Vector3 mVeclocity;

    private string mPrefabName;


    public float MSpeed { get; set; }
    public Transform MScender { get; set; }
    public Transform MReceiver { get; set; }

    void Awake () {
        int tmpIndx = gameObject.name.IndexOf("(");
        mPrefabName = gameObject.name.Substring(0, tmpIndx);

        mSkillData = new SkillData();
        mSkillData.SetSkillType(SkillType.Move);
        MSpeed = 50f;
        mLifeTime = mLifeTimeMax;
    }

	// Update is called once per frame
	void Update () {
        if ((mSkillData.GetSkillType() & SkillType.Move) == SkillType.Move)
        {
            Moving();
        }
    }

    public void CalculateEffect()
    {
        //Add Buffs
    }
    public void Moving()
    {
        if (mVeclocity != Vector3.zero)
        {
            transform.position += mVeclocity * Time.deltaTime;
        }

        if (mLifeTime < float.Epsilon)
        {
            mLifeTime = mLifeTimeMax;
            GameObjectPool.GetInstance().RecycleGo(mPrefabName, gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            mLifeTime -= Time.deltaTime;
        }
    }
    public void SetSkillEffectData()
    {
        if((mSkillData.GetSkillType()&SkillType.Move)== SkillType.Move)
        {
            mVeclocity = (MReceiver.position - MScender.position).normalized* MSpeed;
        }
    }
    public SkillData GetSkillData()
    {
        return mSkillData;
    }
}
