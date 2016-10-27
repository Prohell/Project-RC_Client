using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapViewLod0 : MonoBehaviour
{
    public HEX hex = null;
    [HideInInspector]
    public Camera MapCamera;
    public HexMapLayout Layout;
    private HexSubMapView subMapViewTemplate = null;
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
        // 加载地块
        //yield return App.MapTileLoader.BeginLoadCo ();
        yield break;
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
            if (null == subMapViewTemplate)
            {
                AssetManager.GetInstance().LoadAsset(MapPrefabDef.MAP_SUB_VIEW,
                 (GameObject go) =>
                 {
                     subMapViewTemplate = go.GetComponent<HexSubMapView>();
                 });
            }
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
        sub.InitBg();
        sub.InitWater();
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
            this.RecCoroutine(RefreshSubmaps());
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

                        default:
                            submap.InitBg();
                            submap.InitWater();
                            submap.InitBlocks();
                            submap.HideMarks();
                            break;
                    }
                }
                submap.SetPitch(MapView.Current.CameraPitch);
            }
        }
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
                if(isUseObjPool)
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
        foreach (Coord coord in visCoord)
        {
            if (!subMapViews.ContainsKey(coord))
            {
                HexSubMapView sub = CreateSubMapView(coord.x, coord.y);
                sub.inUse = true;
                sub.InitTiles();
                sub.InitBlocks();
                sub.SetPitch(MapView.Current.CameraPitch);
                subMapViews.Add(coord, sub);
            }
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
            if (null == subMapViewTemplate)
            {
                AssetManager.GetInstance().LoadAsset(MapPrefabDef.MAP_SUB_VIEW,
                 (GameObject go) =>
                 {
                     subMapViewTemplate = go.GetComponent<HexSubMapView>();
                 });
            }
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

                default:
                    sub.InitBg(true);
                    sub.InitWater();
                    sub.HideMarks();
                    sub.InitBlocks();
                    break;
            }
        }
        return sub;
    }
#endif
}
