using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapViewLod0 : MonoBehaviour
{
    public HEX hex = null;
    [HideInInspector]
    public Camera MapCamera;
    public HexMapLayout Layout;
    public HexSubMapView subMapViewTemplate;
    //	private Dictionary<string, MapTile> tileTmpDict = new Dictionary<string, MapTile>();

    private Dictionary<Coord, HexSubMapView> subMapViews = new Dictionary<Coord, HexSubMapView>();
    public Dictionary<Coord, HexSubMapView> CurSubMapViews
    {
        get
        {
            return subMapViews;
        }
    }

    public void OnMapDataUpdateEvent(Coord c)
    {
        HexSubMapView s = null;
        subMapViews.TryGetValue(c, out s);
        if (null != s)
        {
            s.DestroyTiles();
            s.InitTiles();
            s.SetPitch(MapView.Current.CameraPitch);
        }
    }

    public void CleanUp()
    {
        foreach (var kvp in subMapViews)
        {
            RetireSubMapView(kvp.Value);
        }
        subMapViews.Clear();
    }

    void OnDestroy()
    {
        //App.EventMgr.Remove(EventId.MapDataReceived, OnMapDataUpdateEvent);

        //		App.GameObjectPool.ClearPool(subMapViewTmp.gameObject.name);
        //		Dictionary<string, MapTile> _tileTmpDict = App.MapTileLoader.TileTmpDict;
        //		foreach (var kvp in _tileTmpDict){
        //			App.GameObjectPool.ClearPool(kvp.Value.gameObject.name);
        //		}
        //App.MapTileLoader.BeginClear ();
    }

    public IEnumerator Init()
    {
        yield return null;
    }

    HexSubMapView CreateSubMapView(int x, int y)
    {
#if UNITY_EDITOR
        if (MapEditorEntry.Instance != null) return CreateSubMapViewInEditor(x, y);
#endif
        HexSubMapView sub = null;
        var cachedGo = GameObjectPool.GetInstance().WithdrawGo(MapPrefabDef.MAP_SUB_VIEW);
        if (null == cachedGo)
        {
            sub = Instantiate(subMapViewTemplate);
            LogModule.WarningLog("HexSubMapView Allocated!");
        }
        else
        {
            sub = cachedGo.GetComponent<HexSubMapView>();
        }

        sub.name = string.Format("({0}, {1})", x, y);
        //sub.tileTmpDict = App.MapTileLoader.TileTmpDict;
        sub.hex = hex;
        sub.layout = Layout;
        sub.xIdx = x;
        sub.yIdx = y;
        sub.transform.parent = transform;
        return sub;
    }

    void RetireSubMapView(HexSubMapView sub)
    {
        sub.DestroyTiles();
        sub.DestroyBlocks();
        sub.ReplaceMesh();
        sub.yIdx = -1;
        sub.xIdx = -1;
        sub.inUse = false;
        sub.layout = null;
        sub.tileTmpDict = null;

        GameObjectPool.GetInstance().RecycleGo(MapPrefabDef.MAP_SUB_VIEW, sub.gameObject);
    }

    public void OnCameraMove(Vector3 curPos, Vector3 prePos)
    {
        UpdateVisibleBlocks();
        if (!submapRefreshing)
        {
			Game.StartCoroutine(RefreshSubmaps());
        }
    }

    public void UpdateVisibleBlocks()
    {
        List<Coord> visCoord = Layout.CalcVisibleSubMaps(MapCamera);
        GameFacade.GetProxy<MapProxy>().SetVisibleBlocks(visCoord);
    }

    public void RefreshTileMarches()
    {
        //foreach (var kvp in subMapViews){
        //	kvp.Value.RefreshTileMarchesAndCollect();
        //}
    }

    public void RefreshPVECooldown()
    {
        //foreach (var kvp in subMapViews)
        //{
        //	kvp.Value.RefreshPVECooldown();
        //}
    }

    public void OnCameraPitch(float eulurX)
    {
        SetPitch(eulurX);
    }

    bool isUseObjPool = true;
    bool submapRefreshing = false;
    IEnumerator RefreshSubmaps()
    {
        submapRefreshing = true;

        List<Coord> visCoord = Layout.CalcVisibleSubMaps(MapCamera);
        List<Coord> reuseSubmap = new List<Coord>();
        // TT version
        foreach (KeyValuePair<Coord, HexSubMapView> item in subMapViews)
        {
            if (visCoord.Contains(item.Key))
            {
                item.Value.inUse = true;
            }
            else
            {
                item.Value.inUse = false;
                if (isUseObjPool)
                {
                    RetireSubMapView(item.Value);
                }
                else
                {
                    Destroy(item.Value.gameObject);
                }
                reuseSubmap.Add(item.Key);
            }
        }
        foreach (Coord reuseCoord in reuseSubmap)
        {
            subMapViews.Remove(reuseCoord);
        }

		foreach (Coord coord in visCoord) {
			if (!subMapViews.ContainsKey (coord)) {
				HexSubMapView sub = CreateSubMapView (coord.x, coord.y);
				subMapViews.Add(coord, sub);
			}
		}

        foreach (HexSubMapView sub in subMapViews.Values) 
		{
            // cur coord
            //sub.xIdx
            //sub.yIdx
            sub.left = null;
            sub.prev = null;
            sub.diag = null;

            Coord cleft=new Coord(sub.xIdx-1,sub.yIdx);
			HexSubMapView bleft;
			if (subMapViews.TryGetValue(cleft, out bleft))
			{
                if(bleft.xIdx==sub.xIdx-1 && bleft.yIdx==sub.yIdx)
                {
                    sub.left = bleft;
                }
                else
                {
                    Debug.Log("Get left error!");
                }
            }

			Coord cprev=new Coord(sub.xIdx,sub.yIdx-1);
			HexSubMapView bprev;
			if (subMapViews.TryGetValue(cprev, out bprev))
			{
                if(bprev.xIdx==sub.xIdx && bprev.yIdx==sub.yIdx-1)
                {
                    sub.prev = bprev;
                }
                else
                {
                    Debug.Log("Get prev error!");
                }
			}

			Coord cdiag=new Coord(sub.xIdx-1,sub.yIdx-1);
			HexSubMapView bdiag;
			if (subMapViews.TryGetValue(cdiag, out bdiag))
			{
                if (bdiag.xIdx == sub.xIdx-1 && bdiag.yIdx == sub.yIdx - 1)
                {
                    sub.diag = bdiag;
                }
                else
                {
                    Debug.Log("Get diag error!");
                }
			}

            /*string s = "sub=(" + sub.xIdx.ToString() + ", " + sub.yIdx.ToString() + ") ";
            if (sub.left != null)
                s += "left=(" + sub.left.xIdx.ToString() + ", " + sub.left.yIdx.ToString() + ") ";
            if(sub.prev!=null)
                s += "prev=(" + sub.prev.xIdx.ToString() + ", " + sub.prev.yIdx.ToString() + ") ";
            if (sub.diag != null)
                s += "diag=(" + sub.diag.xIdx.ToString() + ", " + sub.diag.yIdx.ToString() + ") ";
            Debug.Log(s);*/
		}

        foreach (HexSubMapView sub in subMapViews.Values)
        {
            sub.InitPos();
            sub.InitBg();

            if (sub.left != null || sub.prev != null || sub.diag != null)
            {
                sub.mHexBg.isBlend = true;
            }
        }

        // sz test
        /*
        foreach (HexSubMapView sub in subMapViews.Values)
        {
            if (sub.xIdx == 0 && sub.yIdx == 0)
            {
                sub.mHexBg.spriteId.Clear();
                sub.mHexBg.spriteId.Add(1);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
            }
            else if (sub.xIdx == 1 && sub.yIdx == 0)
            {
                sub.mHexBg.spriteId.Clear();
                sub.mHexBg.spriteId.Add(6);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
            }
            else if (sub.xIdx == 0 && sub.yIdx == 1)
            {
                sub.mHexBg.spriteId.Clear();
                sub.mHexBg.spriteId.Add(6);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
            }
            else if (sub.xIdx == 1 && sub.yIdx == 1)
            {
                sub.mHexBg.spriteId.Clear();
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
                sub.mHexBg.spriteId.Add(0);
            }

            if (sub.mBlendBg)
            {
                if (sub.xIdx == 1 && sub.yIdx == 0)
                {
                    sub.mBlendBg.spriteId.Clear();
                    sub.mBlendBg.spriteId.Add(6);
                    sub.mBlendBg.spriteId.Add(1);
                    sub.mBlendBg.spriteId.Add(0);
                    sub.mBlendBg.spriteId.Add(0);
                }
                else if (sub.xIdx == 0 && sub.yIdx == 1)
                {
                    sub.mBlendBg.spriteId.Clear();
                    sub.mBlendBg.spriteId.Add(6);
                    sub.mBlendBg.spriteId.Add(0);
                    sub.mBlendBg.spriteId.Add(1);
                    sub.mBlendBg.spriteId.Add(0);
                }
                else if (sub.xIdx == 1 && sub.yIdx == 1)
                {
                    sub.mBlendBg.spriteId.Clear();
                    sub.mBlendBg.spriteId.Add(0);
                    sub.mBlendBg.spriteId.Add(6);
                    sub.mBlendBg.spriteId.Add(6);
                    sub.mBlendBg.spriteId.Add(1);
                }
            }
        }
        // sz test
         */

        foreach (HexSubMapView sub in subMapViews.Values)
        {
            sub.UpdateInterSprList();
        }

        foreach (HexSubMapView sub in subMapViews.Values)
        {
            /*string s = "sub=(" + sub.xIdx.ToString() + ", " + sub.yIdx.ToString() + ") ";
            if (sub.left != null)
                s += "left=(" + sub.left.xIdx.ToString() + ", " + sub.left.yIdx.ToString() + ") ";
            if (sub.prev != null)
                s += "prev=(" + sub.prev.xIdx.ToString() + ", " + sub.prev.yIdx.ToString() + ") ";
            if (sub.diag != null)
                s += "diag=(" + sub.diag.xIdx.ToString() + ", " + sub.diag.yIdx.ToString() + ") ";
            Debug.Log(s);*/

            /*string s="";
            if (sub.mBlendBg!=null)
                s += "blend=(" + sub.mBlendBg._x.ToString() + ", " + sub.yIdx.ToString() + ") ";
            if (sub.mBlendBg != null && sub.mBlendBg.left != null)
                s += "blend left=(" + sub.mBlendBg.left._x.ToString() + ", " + sub.mBlendBg.left._y.ToString() + ") ";
            if (sub.mBlendBg != null && sub.mBlendBg.prev != null)
                s += "blend prev=(" + sub.mBlendBg.prev._x.ToString() + ", " + sub.mBlendBg.prev._y.ToString() + ") ";
            if (sub.mBlendBg != null && sub.mBlendBg.diag != null)
                s += "blend diag=(" + sub.mBlendBg.diag._x.ToString() + ", " + sub.mBlendBg.diag._y.ToString() + ") ";
            Debug.Log(s);*/

            sub.mHexBg.UpdateBlock();
            sub.mHexWater.UpdateBlock();
            if(sub.mBlendBg!=null)
                sub.mBlendBg.UpdateBlock();
        }

        foreach (HexSubMapView sub in subMapViews.Values)
        {
            sub.inUse = true;
            sub.InitTiles();
            sub.InitBlocks();
            sub.SetPitch(MapView.Current.CameraPitch);
        }

        RefreshTileMarches();
        submapRefreshing = false;

        // test version
        //foreach (KeyValuePair<Coord, GameObject> item in subMaps)
        //{
        //    if (visCoord.Contains(item.Key))
        //    {
        //    }
        //    else
        //    {
        //        Destroy(item.Value);
        //        reuseSubmap.Add(item.Key);
        //    }
        //}
        //foreach (Coord reuseCoord in reuseSubmap)
        //{
        //    subMaps.Remove(reuseCoord);
        //}
        //foreach (Coord coord in visCoord)
        //{
        //    if (!subMaps.ContainsKey(coord))
        //    {
        //        subMaps.Add(coord, hex.AddBlock(coord.x, coord.y));
        //    }
        //}
        //submapRefreshing = false;
        yield break;
    }

    public void SetPitch(float angle)
    {
        foreach (var item in subMapViews)
        {
            item.Value.SetPitch(angle);
        }
    }

    void Start()
    {
        hex = GetComponent<HEX>();
    }

#if UNITY_EDITOR
    HexSubMapView CreateSubMapViewInEditor(int x, int y)
    {
        HexSubMapView sub = null;
        var cachedGo = GameObjectPool.GetInstance().WithdrawGo(MapPrefabDef.MAP_SUB_VIEW);
        if (null == cachedGo)
        {
            sub = Instantiate(subMapViewTemplate);
            LogModule.WarningLog("HexSubMapView Allocated!");
        }
        else
        {
            sub = cachedGo.GetComponent<HexSubMapView>();
        }

        sub.name = string.Format("({0}, {1})", x, y);
        //sub.tileTmpDict = App.MapTileLoader.TileTmpDict;
        sub.hex = hex;
        sub.layout = Layout;
        sub.xIdx = x;
        sub.yIdx = y;

        sub.transform.parent = transform;
        sub.InitPos();
        if (MapEditorEntry.Instance != null)
        {
            switch (MapEditorEntry.Instance.curOp)
            {
                case MapEditorEntry.EDIT_OP.EDIT_AREA:
                    sub.InitBg(false);
                    sub.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_AREA, MapEditorEntry.Instance.campColors);
                    break;

                case MapEditorEntry.EDIT_OP.EDIT_LV:
                    sub.InitBg(false);
                    sub.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_LV, MapEditorEntry.Instance.levelColors);
                    break;

                case MapEditorEntry.EDIT_OP.EDIT_TILETYPE:
                    sub.InitBg(false);
                    sub.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_TILETYPE, MapEditorEntry.Instance.typeColors);
                    break;

                default:
                    sub.InitBg(true);
                    sub.HideMarks();
                    sub.InitBlocks();
                    break;
            }
        }
        return sub;
    }

    /// <summary>
    /// 地图编辑器强制刷新
    /// </summary>
    /// <param name="tileCoord"></param>
    public void ForceRereshSubMapView(Coord tileCoord)
    {
        List<Coord> needRefresh = new List<Coord>();

        Coord submapCoord = new Coord();
        submapCoord.x = Mathf.FloorToInt(tileCoord.x / hex.xTile);
        submapCoord.y = Mathf.FloorToInt(tileCoord.y / hex.yTile);
        needRefresh.Add(submapCoord);

        int cx = tileCoord.x % hex.xTile;
        int cy = tileCoord.y % hex.yTile;

        if (cx == 0) needRefresh.Add(new Coord(submapCoord.x - 1, submapCoord.y));
        if (cy == 0) needRefresh.Add(new Coord(submapCoord.x - 1, submapCoord.y));
        if (cx == 0 && cy == 0) needRefresh.Add(new Coord(submapCoord.x - 1, submapCoord.y - 1));

        if (cx == hex.xTile - 1) needRefresh.Add(new Coord(submapCoord.x + 1, submapCoord.y));
        if (cy == hex.yTile) needRefresh.Add(new Coord(submapCoord.x - 1, submapCoord.y));
        if (cx == hex.xTile && cy == hex.yTile) needRefresh.Add(new Coord(submapCoord.x + 1, submapCoord.y + 1));

        foreach (Coord coord in needRefresh)
        {
            if (subMapViews.ContainsKey(coord))
            {
                HexSubMapView submap = subMapViews[coord];
                if (MapEditorEntry.Instance != null)
                {
                    submap.DestroyTiles();
                    submap.DestroyBlocks();
                    submap.ReplaceMesh();
                    switch (MapEditorEntry.Instance.curOp)
                    {
                        case MapEditorEntry.EDIT_OP.EDIT_AREA:
                            submap.InitBg(false);
                            submap.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_AREA, MapEditorEntry.Instance.campColors);
                            return;

                        case MapEditorEntry.EDIT_OP.EDIT_LV:
                            submap.InitBg(false);
                            submap.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_LV, MapEditorEntry.Instance.levelColors);
                            return;

                        case MapEditorEntry.EDIT_OP.EDIT_TILETYPE:
                            submap.InitBg(false);
                            submap.ShowMarks(MapEditorEntry.EDIT_OP.EDIT_TILETYPE, MapEditorEntry.Instance.typeColors);
                            break;

                        default:
                            submap.InitBg();
                            submap.InitBlocks();
                            submap.HideMarks();
                            break;
                    }
                }
                submap.SetPitch(MapView.Current.CameraPitch);
            }
        }
    }
#endif
}
