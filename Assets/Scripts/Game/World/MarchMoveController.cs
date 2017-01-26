//----------------------------------------------
//	CreateTime  : 1/20/2017 5:26:12 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : MarchMoveController
//	ChangeLog   : None
//----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(MarchAnimController))]
public class MarchMoveController : MonoBehaviour
{
    public const float MoveSpeed = 1f;
    private float Duration = 1f;

    public MarchAnimController m_MarchAnimController;

    public Action SingleMoveEndAction { get; private set; }
    public Action TotalMoveEndAction { get; private set; }

    public List<Vector3> MoveDestPath { get; private set; }
    private int CurrentMoveIndex = 0;

    private void StartMove(Vector3 p_dest, Action p_singleEndAction)
    {
        if (IsMoving)
        {
            return;
        }

        MoveStartPos = transform.position;
        MoveEndPos = p_dest;
        var lookAt = MoveEndPos - MoveStartPos;
        lookAt = new Vector3(lookAt.x, 0, lookAt.z);
        transform.forward = lookAt;
        SingleMoveEndAction = p_singleEndAction;
        MoveStartTime = Time.realtimeSinceStartup;
        Duration = Vector3.Distance(MoveStartPos, MoveEndPos) / MoveSpeed;

        IsMoving = true;
    }

    public void StartMove(List<Vector3> p_destList, Action p_totalEndAction)
    {
        MoveDestPath = p_destList;
        TotalMoveEndAction = p_totalEndAction;
        if (MoveDestPath.Any())
        {
            CurrentMoveIndex = 0;
            StartMove(MoveDestPath[CurrentMoveIndex], NextMove);
            m_MarchAnimController.Play(MarchAnimController.ClipName.Run);
        }
    }

    private void NextMove()
    {
        CurrentMoveIndex++;
        if (CurrentMoveIndex < MoveDestPath.Count)
        {
            StartMove(MoveDestPath[CurrentMoveIndex], NextMove);
        }
        else
        {
            m_MarchAnimController.Play(MarchAnimController.ClipName.Stand);

            if (TotalMoveEndAction != null)
            {
                var tempAction = TotalMoveEndAction;
                TotalMoveEndAction = null;
                tempAction();
            }
        }
    }

    public void EndMove()
    {
        IsMoving = false;
    }

    public bool IsMoving { get; private set; }

    public Vector3 MoveStartPos { get; private set; }
    public Vector3 MoveEndPos { get; private set; }

    private float MoveStartTime = -1f;

    void LateUpdate()
    {
        if (IsMoving)
        {
            var ratio = (Time.realtimeSinceStartup - MoveStartTime) / Duration;
            if (ratio > 1)
            {
                IsMoving = false;

                if (SingleMoveEndAction != null)
                {
                    var tempAction = SingleMoveEndAction;
                    SingleMoveEndAction = null;
                    tempAction();
                }
            }
            else if (ratio < 0)
            {
                LogModule.ErrorLog("March Move Error, ratio:{0}", ratio);
                IsMoving = false;
            }
            else
            {
                transform.position = Vector3.Lerp(MoveStartPos, MoveEndPos, (Time.realtimeSinceStartup - MoveStartTime) / Duration);
            }
        }
    }

    void Awake()
    {
        m_MarchAnimController = GetComponent<MarchAnimController>();
        if (m_MarchAnimController == null)
        {
            LogModule.ErrorLog("No MarchAnimController exist.");
            return;
        }
    }
}
