//----------------------------------------------
//	CreateTime  : 1/19/2017 3:32:07 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : WorldController
//	ChangeLog   : None
//----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using LuaInterface;
using UnityEngine;

public class WorldController : Singleton<WorldController>, IInit, IDestroy
{
    public MapProxy m_MapModel;
    public WorldProxy m_WorldModel;
    public LuaTable m_WorldUIView;
    public WorldView m_WorldView;
    public MapAStarCompact m_MapAStarCompact;

    #region FromUI

    public void MarchListClick(int index)
    {
        SelectMyMarch(index);
    }

    public void MoveClick()
    {
        m_WorldModel.SendQuestAllMarch();

        if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyMarch && m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.Block)
        {
            DoMove(m_WorldModel.m_SelectedOtherItem.SelectedBlock.coord, false, null);
        }
    }

    public void DoMove(Coord p_c, bool p_isGoToNext, Action EndAction, bool IsInstant = false)
    {
        if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyMarch && m_WorldModel.m_SelectedOtherItem.m_SelectedState != WorldProxy.SelectedOtherItem.SelectedState.Null)
        {
            if (m_WorldModel.m_SelectedMyItem.SelectedMyMarch.IsMoving)
            {
                return;
            }

            List<Coord> coordPath = new List<Coord>();
            if (IsInstant)
            {
                coordPath = new List<Coord>() { m_WorldModel.m_SelectedMyItem.SelectedMyMarch.Position, p_c, p_c };
            }
            else
            {
                coordPath = m_MapAStarCompact.CalcPath(m_WorldModel.m_SelectedMyItem.SelectedMyMarch.Position, p_c);
            }

            if (coordPath == null || coordPath.Count <= 1)
            {
                if (EndAction != null)
                {
                    var tempAction = EndAction;
                    EndAction = null;
                    tempAction();
                }
                return;
            }
            if (p_isGoToNext)
            {
                coordPath.RemoveAt(coordPath.Count - 1);
            }
            if (coordPath == null || coordPath.Count <= 1)
            {
                if (EndAction != null)
                {
                    var tempAction = EndAction;
                    EndAction = null;
                    tempAction();
                }
                return;
            }

            //Notify server move.
            m_WorldModel.SendMarchMoveMsg(m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchData.marchId, coordPath.Last());

            var vecPath = MapHelper.ConvertCoordPathToVec(coordPath);
            vecPath.RemoveAt(0);

            m_WorldModel.m_SelectedMyItem.SelectedMyMarch.IsMoving = true;
            m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchMoveController.StartMove(vecPath, () =>
            {
                m_WorldModel.m_SelectedMyItem.SelectedMyMarch.IsMoving = false;
                m_WorldModel.m_SelectedMyItem.SelectedMyMarch.Position = coordPath.Last();
                if (EndAction != null)
                {
                    var tempAction = EndAction;
                    EndAction = null;
                    tempAction();
                }
            });
        }
    }

    public void AttackClick()
    {
        if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyMarch && m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.OtherMarch)
        {
            DoMove(m_WorldModel.m_SelectedOtherItem.SelectOtherMarch.Position, true, () =>
              {
                  var lookAt = m_WorldModel.m_SelectedOtherItem.SelectOtherMarch.MarchMoveController.transform.position - m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchMoveController.transform.position;
                  lookAt = new Vector3(lookAt.x, 0, lookAt.z);
                  m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchMoveController.transform.forward = lookAt;

                  m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchAnimController.Play(MarchAnimController.ClipName.Attack, () =>
                  {
                      //MySceneManager.GetInstance().SwitchToScene(SceneId.BattleTest);
                      m_WorldModel.SendFightMsg();
                  });
              });
        }
    }

    public void GoOutClick()
    {
        var toSend = m_WorldModel.MyMarchList.Where(item => item.IsOut == false).ToList();

        if (toSend.Any())
        {
            m_WorldModel.SendSendMarchMsg(toSend.First());
        }
        else
        {
            LogModule.WarningLog("No remaining march to send.");
        }
    }

    #endregion

    #region ToUI

    public void SetMarchUI()
    {
        RefreshUI();

        for (int i = 0; i < m_WorldModel.MyOutMarchList.Count; i++)
        {
            if (i < m_WorldModel.MyOutMarchList.Count)
            {
                LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.SetMarchObject", i, m_WorldModel.MyOutMarchList[i].State,
                    m_WorldModel.MyOutMarchList[i].Time);
            }
        }
    }

    /// <summary>
    /// Will not change data stored in view frame.
    /// </summary>
    public void RefreshUI()
    {
        for (int i = 0; i < WorldProxy.MarchListNum; i++)
        {
            if (i < m_WorldModel.MyOutMarchList.Count)
            {
                LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowMarchObject", i);
            }
            else
            {
                LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.HideMarchObject", i);
            }
        }

        LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.HideAllButtons");
        switch (m_WorldModel.m_SelectedMyItem.m_SelectedState)
        {
            case WorldProxy.SelectedMyItem.SelectedState.MyMarch:
                {
                    switch (m_WorldModel.m_SelectedOtherItem.m_SelectedState)
                    {
                        case WorldProxy.SelectedOtherItem.SelectedState.Block:
                            {
                                LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowMoveButton");
                                break;
                            }
                        case WorldProxy.SelectedOtherItem.SelectedState.OtherMarch:
                            {
                                LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowAttackButton");
                                break;
                            }
                    }

                    break;
                }
            case WorldProxy.SelectedMyItem.SelectedState.MyCity:
                {
                    LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowGoOutButton");
                    break;
                }
        }

        if (m_WorldModel.m_SelectedOtherItem.m_SelectedState != WorldProxy.SelectedOtherItem.SelectedState.Null)
        {
            LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowInfoButton");
            LuaHelper.CallFunctionWithSelf(m_WorldUIView, "WorldView.ShowFocusButton");
        }
    }

    #endregion

    public void MapBlockClick(object screenPos)
    {
        Coord c = MapView.Current.Layout.ScreenPos2Coord(MapView.Current.MapCamera, (Vector2)screenPos);
        var marchs = m_WorldModel.MyOutMarchList.Where(item => item.Position == c);
        if (marchs.Any())
        {
            SelectMyMarch(m_WorldModel.MyOutMarchList.IndexOf(marchs.First()));
        }
        else if (m_WorldModel.MyCity != null && m_WorldModel.MyCity.Position == c)
        {
            SelectMyCity();
        }
        else
        {
            SelectItem(c);
        }
    }

    private void SelectMyMarch(int index)
    {
        LogModule.DebugLog("SelectMarch:{0}", index);

        if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyMarch)
        {
            m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchController.UnSelect();
        }
        else if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyCity)
        {
            m_WorldModel.m_SelectedMyItem.SelectedMyCity.CityController.UnSelect();
        }

        m_WorldModel.SelectMarch(index);
        m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchController.Select();
        MapHelper.MoveMapCamera(m_WorldModel.m_SelectedMyItem.SelectedMyMarch.Position);
        RefreshUI();
    }

    private void SelectMyCity()
    {
        if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyMarch)
        {
            m_WorldModel.m_SelectedMyItem.SelectedMyMarch.MarchController.UnSelect();
        }
        else if (m_WorldModel.m_SelectedMyItem.m_SelectedState == WorldProxy.SelectedMyItem.SelectedState.MyCity)
        {
            m_WorldModel.m_SelectedMyItem.SelectedMyCity.CityController.UnSelect();
        }

        m_WorldModel.m_SelectedMyItem.SelectedMyCity = m_WorldModel.MyCity;
        m_WorldModel.m_SelectedMyItem.SelectedMyCity.CityController.Select();
        RefreshUI();
    }

    private void SelectOtherMarch(WorldProxy.MarchInfo march)
    {
        if (m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.OtherMarch)
        {
            m_WorldModel.m_SelectedOtherItem.SelectOtherMarch.MarchController.UnSelect();
        }
        else if (m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.Block)
        {
            m_WorldView.UnSelectTagObject();
        }

        m_WorldModel.m_SelectedOtherItem.SelectOtherMarch = march;
        m_WorldModel.m_SelectedOtherItem.SelectOtherMarch.MarchController.Select();
        LogModule.DebugLog("SelectMarch:{0}", march);
    }

    private void SelectBlock(Coord c)
    {
        if (m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.OtherMarch)
        {
            m_WorldModel.m_SelectedOtherItem.SelectOtherMarch.MarchController.UnSelect();
        }
        else if (m_WorldModel.m_SelectedOtherItem.m_SelectedState == WorldProxy.SelectedOtherItem.SelectedState.Block)
        {
            m_WorldView.UnSelectTagObject();
        }

        m_WorldModel.m_SelectedOtherItem.SelectedBlock = m_MapModel.GetTile(c);
        m_WorldView.SelectTagObject(MapHelper.CoordToVector3(c));
        LogModule.DebugLog("SelectBlock:{0}", c);
    }

    public void SelectItem(Coord c)
    {
        if (m_WorldModel.MyCity != null && m_WorldModel.MyCity.Position == c)
        {
            SelectMyCity();
        }
        else
        {
            var marchs = m_WorldModel.MyOutMarchList.Where(item => item.Position == c);
            if (marchs.Any())
            {
                SelectMyMarch(m_WorldModel.MyOutMarchList.IndexOf(marchs.First()));
            }
            else
            {
                marchs = m_WorldModel.OtherMarchList.Where(item => item.Position == c);
                if (marchs.Any())
                {
                    SelectOtherMarch(marchs.First());
                }
                else
                {
                    SelectBlock(c);
                }
            }
        }

        RefreshUI();
    }

    public void SetMarch(object para)
    {
        SetMarchUI();
        m_WorldView.SetMarchView();

        foreach (WorldProxy.MarchInfo item in m_WorldModel.MyOutMarchList)
        {
            if (item.MarchMoveController == null)
            {
                item.MarchMoveController = item.MarchObject.GetComponent<MarchMoveController>();
            }
            if (item.MarchController == null)
            {
                item.MarchController = item.MarchObject.GetComponent<MarchController>();
            }
            if (item.MarchAnimController == null)
            {
                item.MarchAnimController = item.MarchObject.GetComponent<MarchAnimController>();
            }
        }
        foreach (WorldProxy.MarchInfo item in m_WorldModel.OtherMarchList)
        {
            if (item.MarchMoveController == null)
            {
                item.MarchMoveController = item.MarchObject.GetComponent<MarchMoveController>();
            }
            if (item.MarchController == null)
            {
                item.MarchController = item.MarchObject.GetComponent<MarchController>();
            }
            if (item.MarchAnimController == null)
            {
                item.MarchAnimController = item.MarchObject.GetComponent<MarchAnimController>();
            }
        }
    }

    public void SetCity(object para)
    {
        m_WorldView.SetCityView();

        m_WorldModel.MyCity.CityController = m_WorldModel.MyCity.CityObject.GetComponent<CityController>();
        //Set other city.
        for (int i = 0; i < m_WorldModel.OtherCityList.Count; i++)
        {
            m_WorldModel.OtherCityList[i].CityController = m_WorldModel.OtherCityList[i].CityObject.GetComponent<CityController>();
        }
    }

    public void OnInit()
    {
        m_MapAStarCompact = new MapAStarCompact();

        EventManager.GetInstance().AddEventListener(EventId.MapBlockSelected, MapBlockClick);

        EventManager.GetInstance().AddEventListener(EventId.WorldMarchDataUpdate, SetMarch);
        EventManager.GetInstance().AddEventListener(EventId.WorldCityDataUpdate, SetCity);
    }

    public void OnDestroy()
    {

    }
}
