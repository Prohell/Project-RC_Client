﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 地图编辑器入口
/// </summary>
public class MapEditorEntry : MonoBehaviour
{
#if UNITY_EDITOR
    public static MapEditorEntry Instance = null;
    #region UI ref
    public Text coord;
    public Text curEdit;
    public GameObject texLayout;
    public GameObject heightLayout;
    public GameObject campLayout;
    public GameObject levelLayout;
    public GameObject tileTypeLayout;
    public GameObject blockTypeLayout;
    public Button saveBtn;

    public Color[] campColors;
    public Color[] levelColors;
    #endregion

    #region Edit params
    public enum EDIT_OP
    {
        EDIT_TEX = 0,
        EDIT_HEIGHT = 1,
        EDIT_AREA = 2,
        EDIT_LV = 3,
        EDIT_TILETYPE = 4,
        EDIT_BLOCKTYPE = 5
    }
    Coord mCurLeftClicked;
    Coord mDst;

    MapProxy mapProxy = null;
    bool mIsClickEditArea = false;
    public EDIT_OP curOp = EDIT_OP.EDIT_TEX;
    byte mTexIndex = 1;
    bool mHeightOp = true;// true:+1  false:-1
    byte mCampIndex = 0;
    byte mLvIndex = 0;
    #endregion

    IEnumerator Start()
    {
        Instance = this;
        yield return null;
        MapView.Current.EditMode = true;
        ProxyManager.GetInstance().Add(new MapProxy());
        Init();
    }

    void Init()
    {
        foreach (Button btn in texLayout.GetComponentsInChildren<Button>())
        {
            byte index = byte.Parse(btn.name.Substring(1, 1));
            btn.onClick.AddListener(() =>
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_TEX);
                blockTypeLayout.transform.parent.gameObject.SetActive(false);
                mTexIndex = (byte)(index - 1);
            });
        }

        foreach (Button btn in heightLayout.GetComponentsInChildren<Button>())
        {
            byte index = byte.Parse(btn.name.Substring(1, 1));
            btn.onClick.AddListener(() =>
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_HEIGHT);
                blockTypeLayout.transform.parent.gameObject.SetActive(false);
                mHeightOp = index == 2 ? true : false;
                curEdit.text = mHeightOp ? "Height+1 " : "Height-1";
            });
        }

        foreach (Button btn in campLayout.GetComponentsInChildren<Button>())
        {
            byte index = byte.Parse(btn.name.Substring(1, 1));
            btn.onClick.AddListener(()=> 
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_AREA);
                blockTypeLayout.transform.parent.gameObject.SetActive(false);
                mCampIndex = index;
            });
        }

        foreach (Button btn in levelLayout.GetComponentsInChildren<Button>())
        {
            byte index = byte.Parse(btn.name.Substring(1, 1));
            btn.onClick.AddListener(() =>
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_LV);
                blockTypeLayout.transform.parent.gameObject.SetActive(false);
                mLvIndex = index;
            });
        }

        foreach (Button btn in tileTypeLayout.GetComponentsInChildren<Button>())
        {
            byte index = byte.Parse(btn.name.Substring(1, 1));
            btn.onClick.AddListener(() =>
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_TILETYPE);
                blockTypeLayout.transform.parent.gameObject.SetActive(index == 2);
            });
        }

        foreach (Button btn in blockTypeLayout.GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(() =>
            {
                mIsClickEditArea = true;
                ChangeCurOp(EDIT_OP.EDIT_BLOCKTYPE);
                blockTypeLayout.transform.parent.gameObject.SetActive(false);
            });
        }
        saveBtn.onClick.AddListener(() => 
        {
            mIsClickEditArea = true;
            GameFacade.GetProxy<MapProxy>().Export();
        });
    }

    void ChangeCurOp(EDIT_OP goingOp)
    {
        if (curOp != goingOp)
        {
            curOp = goingOp;
            MapView.Current.Lod0.ForceRereshSubMapView(mCurLeftClicked);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(mIsClickEditArea)
            {
                mIsClickEditArea = false;
            }
            else
            {
                mCurLeftClicked = MapView.Current.Layout.ScreenPos2Coord(MapView.Current.MapCamera, Input.mousePosition.xy());
                coord.text = mCurLeftClicked.ToString();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            Coord newdst = MapView.Current.Layout.ScreenPos2Coord(MapView.Current.MapCamera, Input.mousePosition.xy());
            mDst = newdst;
            OnMRBAction_Common(mDst);
        }
        else if (Input.GetMouseButton(1))
        {
            Coord newdst = MapView.Current.Layout.ScreenPos2Coord(MapView.Current.MapCamera, Input.mousePosition.xy());
            if (newdst != mDst)
            {
                mDst = newdst;
                OnMRBAction_Common(mDst);
            }
        }
    }

    //Seed All
    void OnMRBAction_Common(Coord c)
    {
        if(mapProxy == null)
        {
            mapProxy = GameFacade.GetProxy<MapProxy>();
        }
        MapTileVO tile = mapProxy.GetTile(mDst);
        switch(curOp)
        {
            case EDIT_OP.EDIT_TEX:
                if (tile.mat == mTexIndex) return;
                tile.mat = mTexIndex;
                break;
            case EDIT_OP.EDIT_HEIGHT:
                byte newHeight = 0;
                if (mHeightOp && tile.height > 0)
                {
                    newHeight = (byte)(tile.height - 1);
                }
                else if (!mHeightOp && tile.height < 15)
                {
                    newHeight = (byte)(tile.height + 1);
                }
                else
                {
                    return;
                }
                if (!TestNewHeight(newHeight)) return;
                newHeight = (byte)Mathf.Clamp(newHeight, 0, 15);
                if (newHeight == tile.height) return;
                tile.height = newHeight;
                break;

            case EDIT_OP.EDIT_AREA:
                if (tile.camp == mCampIndex) return;
                tile.camp = mCampIndex;
                break;

            case EDIT_OP.EDIT_LV:
                if (tile.level == mLvIndex) return;
                tile.level = mLvIndex;
                break;
        }
        GameFacade.GetProxy<MapProxy>().SeedTile(tile);
    }

    /// <summary>
    /// 测试被编辑高度是否合法
    /// </summary>
    /// <param name="newHeight"></param>
    /// <returns></returns>
    bool TestNewHeight(byte newHeight)
    {
        // get the bounds
        int maxHeight = -1;
        int minHeight = -1;
        maxHeight = minHeight = mapProxy.GetTile(mDst.x + 1, mDst.y).height;
        byte h = mapProxy.GetTile(mDst.x, mDst.y + 1).height;
        maxHeight = Mathf.Max(h, maxHeight);
        minHeight = Mathf.Min(h, minHeight);
        // 偶数行
        if ((mDst.y & 0x1) == 0)
        {
            if (mDst.x > 0)
            {
                h = mapProxy.GetTile(mDst.x - 1, mDst.y + 1).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
                h = mapProxy.GetTile(mDst.x - 1, mDst.y).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
                if (mDst.y > 0)
                {
                    h = mapProxy.GetTile(mDst.x - 1, mDst.y - 1).height;
                    maxHeight = Mathf.Max(h, maxHeight);
                    minHeight = Mathf.Min(h, minHeight);
                }
            }
            if (mDst.y > 0)
            {
                h = mapProxy.GetTile(mDst.x, mDst.y - 1).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
            }
        }
        else
        {
            // 奇数行
            h = mapProxy.GetTile(mDst.x + 1, mDst.y + 1).height;
            maxHeight = Mathf.Max(h, maxHeight);
            minHeight = Mathf.Min(h, minHeight);
            if (mDst.x > 0)
            {
                h = mapProxy.GetTile(mDst.x - 1, mDst.y).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
            }
            if (mDst.y > 0)
            {
                h = mapProxy.GetTile(mDst.x, mDst.y - 1).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
                h = mapProxy.GetTile(mDst.x + 1, mDst.y - 1).height;
                maxHeight = Mathf.Max(h, maxHeight);
                minHeight = Mathf.Min(h, minHeight);
            }
        }
        if (minHeight == maxHeight)
        {
            if (Mathf.Abs(minHeight - newHeight) > 1) return false;
        }
        else if (maxHeight - minHeight > 1)
        {
            return false;
        }
        else
        {
            if (newHeight != minHeight && newHeight != maxHeight) return false;
        }
        return true;
    }

    //void WayPointEditor()
    //   {
    //	MapTileVO[] waypoints = GameFacade.GetProxy<MapProxy>().GetWayPointsByCamp();
    //	for (int i=0; i<4; ++i){
    //		GUILayout.BeginHorizontal();
    //		GUILayout.Label(MapTileVO.CampName[i]);
    //		GUILayout.Label(" X:");
    //		string xStr = GUILayout.TextField(waypoints[i].coord.x + "");
    //		GUILayout.Label("Y:");
    //		string yStr = GUILayout.TextField(waypoints[i].coord.y + "");
    //		GUILayout.EndHorizontal();
    //		int x = 0;
    //		if (int.TryParse(xStr, out x)){
    //			waypoints[i].coord.x = x;
    //		}
    //		int y = 0;
    //		if (int.TryParse(yStr, out y)){
    //			waypoints[i].coord.y = y;
    //		}
    //	}
    //	App.ProxyMgr.MapProxy.SeedWayPoints(waypoints);
    //}

    //void GeneratePVE(){
    //	MapTileVO[,] tiles = App.ProxyMgr.MapProxy.EditorTiles;
    //	App.ProxyMgr.MapProxy.EditorTiles = MapTileGeneratorRun.GeneratePVETiles(tiles);
    //}

    //	void Expand(List<Coord> sum, Coord coord, System.Predicate<Coord> condition){
    //		if (!sum.Contains(coord) && condition.Invoke(coord)){
    //			sum.Add(coord);
    //			var nearList = MapLayout.Nearby(coord);
    //			foreach (Coord n in nearList){
    //				Expand(sum, n, condition);
    //			}
    //		}
    //	}
#endif
}