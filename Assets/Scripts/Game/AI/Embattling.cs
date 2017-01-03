using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BornPosInfor {
    public Vector3 mBornPos;
    public int mSquadID = -1;
    public Transform mPosInstance;
}

public class Embattling : MonoBehaviour {

    private Vector3 mTargetPos;

    public List<BornPosInfor> mBornInforList;
    private float mHalfEdgeLength=15;

    private int mCurrentSelectIndx =-1;
    private int mCurrentSelectSquadID = -1;

    public Object MPosCube { get; set; }

    // Use this for initialization
    void Awake () {
        mBornInforList = new List<BornPosInfor>();
    }
	
	// Update is called once per frame
	void Update () {
        if (mBornInforList.Count <= 0)
            return;
#if UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount > 0)
        {
            Vector3 mScreenPos = Input.GetTouch(0).position;
            Ray mRay = Camera.main.ScreenPointToRay(mScreenPos);
            RaycastHit mHit;
            if (Physics.Raycast(mRay, out mHit))
            {
                if (mHit.collider.gameObject.tag == "Terrain")
                {
                    mTargetPos = mHit.point;
                    PickUpSquad(mTargetPos);
                }
            }
        }

#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mScreenPos = Input.mousePosition;
            Ray mRay = Camera.main.ScreenPointToRay(mScreenPos);
            RaycastHit mHit;
            if (Physics.Raycast(mRay, out mHit))
            {
                if (mHit.collider.gameObject.tag == "Terrain")
                {
                    mTargetPos = mHit.point;
                    //LogModule.DebugLog("GetMouseButtonDown");
                    PickUpSquad(mTargetPos);
                }
            }
        }
#endif
    }

    //test if the aimPositon is in the mBornPosList
    public int JudegeInSide(Vector3 tBornPos)
    {
        for (int i = 0; i < mBornInforList.Count; i++)
        {
            if ((tBornPos.x < mBornInforList[i].mBornPos.x + mHalfEdgeLength)
            && (tBornPos.x > mBornInforList[i].mBornPos.x - mHalfEdgeLength)
            && (tBornPos.z < mBornInforList[i].mBornPos.z + mHalfEdgeLength)
            && (tBornPos.z > mBornInforList[i].mBornPos.z - mHalfEdgeLength)){
                return i;
            }
        }
        return -1;
    }

    public void InitBornInfor(params int[] tSquadID)
    {
        for (int i = 0; i < tSquadID.Length; i++)
        {
            mBornInforList[i].mSquadID = tSquadID[i];
        }
    }

    public void PickUpSquad(Vector3 tBornPos)
    {
        int tBornInforIndex = JudegeInSide(tBornPos);
        //LogModule.DebugLog("tBornInforIndex:"+ tBornInforIndex);
        if (mCurrentSelectIndx == tBornInforIndex)
            return;
        int tBornInforSquadID = -1;
        if (tBornInforIndex != -1)
        {
            tBornInforSquadID = mBornInforList[tBornInforIndex].mSquadID;
            //swap the Squad;
            if (mCurrentSelectIndx != -1&& mCurrentSelectSquadID!=-1)
            {
                mBornInforList[tBornInforIndex].mSquadID = mBornInforList[mCurrentSelectIndx].mSquadID;
                
                //LogModule.DebugLog("Aim tBornInforIndex:" + tBornInforIndex + "the ID: " + mBornInforList[tBornInforIndex].mSquadID);
                mBornInforList[mCurrentSelectIndx].mSquadID = tBornInforSquadID;

                EventManager.GetInstance().SendEvent(EventId.ReSetPosition, mBornInforList[mCurrentSelectIndx]);
                EventManager.GetInstance().SendEvent(EventId.ReSetPosition, mBornInforList[tBornInforIndex]);

                //LogModule.DebugLog("Current mCurrentSelectIndx:" + mCurrentSelectIndx + "the ID:" + mBornInforList[mCurrentSelectIndx].mSquadID);
                // Give up mCurrentSelectIndx Squad
                mCurrentSelectIndx = -1;
                mCurrentSelectSquadID = -1;

            }
            //PickUp Squad 
            else
            {
                mCurrentSelectIndx = tBornInforIndex;
                mCurrentSelectSquadID = mBornInforList[mCurrentSelectIndx].mSquadID;
                //LogModule.DebugLog("Light up  mCurrentSelectIndx Squad:" + mCurrentSelectIndx + "the ID:" + mBornInforList[mCurrentSelectIndx].mSquadID);
                // Light up  mCurrentSelectIndx Squad
            }
        }
    }
    public void AddBornList(BornPosInfor tAimPosInfor)
    {
        mBornInforList.Add(tAimPosInfor);
    }
}
