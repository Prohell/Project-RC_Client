using UnityEngine;
using System.Collections;
/// <summary>
/// 弓箭箭矢逻辑
/// by Zhengxuesong
/// 2016-11-22
/// </summary>
public class Arrow : MonoBehaviour {

    private DamageData mMovingDamageObj;
    public Transform mAimtrans;

    public DamageData GetMovingDamageObj()
    {
        return mMovingDamageObj;
    }
    public Transform MScender { get; set; }
    public Transform MReceiver { get; set; }

    //private Vector3 mLastScenderPos;
    //private Vector3 mLastReceiverPos;
    public float MDamage { get; set; }
    public float MGravity { get; set; }

    private float mSpeed2=2500f;
    private float mSpeedBase =90000f;

    private float mLifeTimeMax = 2f;
    private float mLifeTime ;

    private Vector3 mVeclocity;
    //private Vector3 mLastVeclocity;

    private Transform mEffectPoint;

    private UnitController mReceriveController;

    private float mEffectDistance = 2f;

    private float mReceiveHeight;

    private string mPrefabName;

    public Vector3 GetVeclocity()
    {
        return mVeclocity;
    }
    void Awake()
    {
        int tmpIndx = gameObject.name.IndexOf("(");
        mPrefabName = gameObject.name.Substring(0,tmpIndx);
    }
    public void SetArrowData()
    {
        mSpeed2 = Random.Range(0.6f, 1f) * mSpeedBase;
        MGravity = 40f;
       
        transform.position = MScender.position;

        //if (Vector3.Distance(mLastScenderPos, transform.position) >0.1f|| Vector3.Distance(mLastReceiverPos, MReceiver.position)>0.1f)
        //    mVeclocity = CalculateSpeedReal();
        //else
        //    mVeclocity = mLastVeclocity;

        //mLastScenderPos = transform.position;
        //mLastReceiverPos = MReceiver.position;
        //mLastVeclocity = mVeclocity;
        mVeclocity = CalculateSpeedReal();

        mReceriveController = MReceiver.GetComponentInParent<UnitController>();
        MDamage = mReceriveController.GetUnitData().GetAttack();
        mLifeTime = mLifeTimeMax;

    }
    public Vector3 CalculateSpeedReal()
    {
        Vector3 tStartVeclocity = Vector3.zero;
        float tDistanceXZ = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(MReceiver.position.x, MReceiver.position.z));
        float tXRatio = Mathf.Abs(MReceiver.position.x - transform.position.x) / tDistanceXZ;
        float tZRatio = Mathf.Abs(MReceiver.position.z - transform.position.z) / tDistanceXZ;

        float tDistanceY = MReceiver.position.y - transform.position.y;

        float a = MGravity / 4;
        float b = tDistanceY * MGravity - mSpeed2;
        float c = tDistanceXZ * tDistanceXZ + tDistanceY * tDistanceY;

        float tm = Mathf.Sqrt(b * b - 4 * a * c);

        float t2 = (-b - tm) /2*a;
        float t = Mathf.Sqrt(t2);

        float tXZSpeed = tDistanceXZ / t;
        float tYSpeed = (tDistanceY + MGravity * t2 * 0.5f) / t;


        if (MReceiver.position.x - transform.position.x > 0)
            tStartVeclocity.x = tXZSpeed * tXRatio;
        else
            tStartVeclocity.x = -tXZSpeed * tXRatio;

        if (MReceiver.position.z - transform.position.z > 0)
            tStartVeclocity.z = tXZSpeed * tZRatio;
        else
            tStartVeclocity.z = -tXZSpeed * tZRatio;
        tStartVeclocity.y = tYSpeed;

        return tStartVeclocity;
    }
    //Vector3 CalculateSpeed()
    //{
    //    Vector3 tStartVeclocity = Vector3.zero;
    //    float tDistanceXZ = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(MReceiver.position.x, MReceiver.position.z));
    //    float tXRatio = Mathf.Abs(MReceiver.position.x - transform.position.x) / tDistanceXZ;
    //    float tZRatio = Mathf.Abs(MReceiver.position.z - transform.position.z) / tDistanceXZ;

    //    if (mSpeed2 - MGravity * tDistanceXZ < 0)
    //    {
    //        LogModule.DebugLog("Out of range!:Error");
    //        //Add the Speed
    //        mSpeed2 = MGravity * tDistanceXZ + 300;
    //    }
    //    //      else
    //    {
    //        float tSecond = Mathf.Sqrt(mSpeed2 - MGravity * tDistanceXZ);
    //        float tFirst = Mathf.Sqrt(mSpeed2 + MGravity * tDistanceXZ);
    //        float tXZSpeed = (tFirst + tSecond) / 2;
    //        float tYSpeed = (tFirst - tSecond) / 2;

    //        //
    //        if (MReceiver.position.x - transform.position.x > 0)
    //            tStartVeclocity.x = tXZSpeed * tXRatio;
    //        else
    //            tStartVeclocity.x = -tXZSpeed * tXRatio;

    //        if (MReceiver.position.z - transform.position.z > 0)
    //            tStartVeclocity.z = tXZSpeed * tZRatio;
    //        else
    //            tStartVeclocity.z = -tXZSpeed * tZRatio;
    //        tStartVeclocity.y = tYSpeed;
    //    }
    //    return tStartVeclocity;
    //}
    void Start() {
    }

    // Update is called once per frame
	void FixedUpdate () {

        if (mVeclocity != Vector3.zero)
        {
            mVeclocity.y -= MGravity * Time.deltaTime;
            transform.position += mVeclocity * Time.deltaTime;
        }
        Quaternion quation = Quaternion.LookRotation(mVeclocity);
        transform.rotation = quation;
        //transform.LookAt(mAimtrans);
        //face the camera
        //transform.rotation = Camera.main.transform.rotation;
        //transform.Rotate(-90f, 0f, 0f);

        //Cause Damage
        if (MReceiver != null)
        {
            if (Vector3.Distance(transform.position, MReceiver.position) < mEffectDistance && gameObject.activeSelf == true)
            {
                mReceriveController.ArrowDamageEffect(MDamage);
                gameObject.transform.position = MScender.position;
                mLifeTime = mLifeTimeMax;
                GameObjectPool.GetInstance().RecycleGo(mPrefabName, gameObject);
                gameObject.SetActive(false);
            }
        }  
        if(mLifeTime<float.Epsilon)
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
    public void DamageCalculations()
    {

    }
    public void  DetectorCollision()
    {

    }
    public void NotifyDamage()
    {

    }
}
