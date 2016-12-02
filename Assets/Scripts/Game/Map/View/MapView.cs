using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class MapView : MonoBehaviour
{
    public Camera MapCamera;
    public GameObject CameraMask;

    public HexMapLayout Layout;
    public Action<Coord> hdlCameraMoved;

    [NonSerialized]
    public bool EditMode = false;

    public enum LodState
    {
        Lod0,
        Lod0to1,
        Lod1,
        Lod1to0,
        // the two below, are used when running on device.
        GoingToLod0,
        GoingToLod1,
    }
    bool inited = false;
    public LodState State = LodState.Lod0;
    public MapViewLod0 Lod0;
    public MapViewLod1 Lod1;

    private GameObject highlight = null;

    private Coord _at = new Coord(-1, -1);
    public Coord At
    {
        get
        {
            return _at;
        }
    }

    private static MapView _current = null;
    public static MapView Current
    {
        get
        {
            return _current;
        }
    }

    void Start()
    {

    }

    void OnDestroy()
    {
        _current = null;
    }

    void Awake()
    {
        _current = this;
    }

    public IEnumerator Init()
    {
        inited = false;

        Layout = new HexMapLayout(1f);

        // highlight
        //if (null == App.preloader){
        //	GoLoader highlightLoader = new GoLoader(MapPrefabDef.MAP_HIGHLIGHT);
        //	yield return highlightLoader.Load();
        //	highlight = Instantiate (highlightLoader.go);
        //}
        //else{
        //	highlight = Instantiate (App.preloader.highlightPrefab);
        //}
        //highlight.transform.parent = gameObject.transform;
        //highlight.SetActive (false);

        Lod0.Layout = Layout;
        Lod0.MapCamera = MapCamera;
        yield return Lod0.Init();

        Lod1.Layout = Layout;
        yield return Lod1.Init();
        Lod1.gameObject.SetActive(false);

        MapCamera.transform.SetLocalY(CameraHeightInit);
        CameraPitch = CameraHeight2Angle(CameraHeightInit);

        MoveTo(GameFacade.GetProxy<CityProxy>().myCityCoord);

        //App.ProxyMgr.MapProxy.GetBlockDataFromServer();
        //App.ProxyMgr.MarchProxy.CreateMarchlines();

        InitInteractive(MapCamera, Layout);
        inited = true;

        //App.EventMgr.Post(EventId.SceneSwitchDone, SceneSwitcher.SceneSwitcherType.GameLoading);
        yield break;
    }

    public void CleanUp()
    {
        Lod0.CleanUp();
        //ClearMarches();
    }

    public void RefreshView()
    {
        if (State == LodState.Lod0)
        {
            Lod0.UpdateVisibleBlocks();
            //App.ProxyMgr.MapProxy.ForceRefreshVisibleBlocks();
        }
        else
        {
            // Do nothing
        }
    }

    void OnCameraMove(Vector3 curPos, Vector3 prePos)
    {
        Coord newCoord = Layout.ScreenPos2Coord(MapCamera, MapInfoHighlightPos);
        if (_at != newCoord)
        {
            _at = newCoord;
            if (hdlCameraMoved != null)
            {
                hdlCameraMoved.Invoke(_at);
            }
        }

#if UNITY_EDITOR
        if (State == LodState.Lod0)
        {
            if (curPos.y > CameraHeightLod0to1)
            {
                SwithToLod1();
            }
            else
            {
                Lod0.OnCameraMove(curPos, prePos);
            }
        }
        else if (State == LodState.Lod1 && curPos.y < CameraHeightLod1to0)
        {
            SwithToLod0();
        }
#else
		if (State == LodState.Lod0)
		{
			Lod0.OnCameraMove(curPos, prePos);
		} 
		else if (State == LodState.GoingToLod1)
		{
			SwithToLod1();
		}
		else if (State == LodState.GoingToLod0)
		{
			SwithToLod0();
		}
#endif

        if (onDragUpdateHomecompass != null)
        {
            onDragUpdateHomecompass();
        }
    }

    public void SwithToLod1()
    {
        StartCoroutine(Lod0to1());
    }

    void SwithToLod0()
    {
        Vector3 pos = MapCamera.transform.localPosition;
        pos.y = MapView.CameraHeightAngleMax;
        StartCoroutine(Lod1to0(pos));
    }

    public void SwithToLod0ByCoord(Coord c, Action successCallBack = null)
    {
        Vector3 prePos = MapCamera.transform.localPosition;
        Vector3 lookPos = Layout.HexCenter(c);
        lookPos.y = MapView.CameraHeightInit;
        lookPos.z -= 8f;
        Game.TaskManager.Exec(Lod1to0(lookPos, 30, successCallBack));

    }

    void OnCameraPitch(float eulurX)
    {
        if (State == LodState.Lod0)
        {
            Lod0.OnCameraPitch(eulurX);
        }
    }

    public void MoveTo(Coord c)
    {
        Vector3 prePos = MapCamera.transform.localPosition;
        Vector3 lookPos = prePos + (Layout.HexCenter(c) - Layout.ScreenPos2WorldPos(MapCamera, MapInfoHighlightPos));

        MoveCamera(lookPos);
        //App.ProxyMgr.MapProxy.GetBlockDataFromServer();
    }

    public void OnMapDataUpdateEvent(Coord c)
    {
        if (State == LodState.Lod0)
        {
            Lod0.OnMapDataUpdateEvent(c);
        }
    }

    Vector3 _highlightedPos;
    public void ShowHighLight(Vector3 pos)
    {
        return;
        _highlightedPos = pos;
        highlight.transform.position = pos;
        highlight.gameObject.SetActive(true);
    }

    public void HideHighlight(Coord c)
    {
        return;
        Vector3 pos = Layout.HexCenter(c);
        if (pos.NearlyEquals(_highlightedPos))
            highlight.gameObject.SetActive(false);
    }

    public void RefreshTileMarches()
    {
        if (State == LodState.Lod0)
        {
            Lod0.RefreshTileMarches();
        }
    }

    public void RefreshPVECooldown()
    {
        if (State == LodState.Lod0)
        {
            Lod0.RefreshPVECooldown();
        }
    }

    #region marchline
    /*
	Dictionary<string, MarchLine> _marchLines = new Dictionary<string, MarchLine>();
	
	public Dictionary<string, MarchLine> MarchLines{
		get{
			return _marchLines;
		}
	}

	public void CreateMarch(MarchVO march){
		string marchId = march.MarchInfo.id;
		List<Coord> path;
		Color c = Color.red;
		if (march.MarchInfo.marchStatus == proto_structs.MarchStatus.MARCH_STATUS_MARCHING){
			if (march.MarchInfo != null && march.MarchInfo.playerInfo != null && march.MarchInfo.playerInfo.rpPlayerInfo.playerId == App.ProxyMgr.PlayerProxy.player.id){
				c = new Color(218f/255, 68f/255, 0);
			}
			else{
				c = new Color(208f/255, 116f/255, 0);
			}
		}
		else if (march.MarchInfo.marchStatus == proto_structs.MarchStatus.MARCH_STATUS_RETREAT){
			c = new Color(142f/255, 168f/255, 0);
		}
		path = march.Paths;
		if (_marchLines.ContainsKey(marchId)){
			Debug.LogWarning("MarchId Exited!!");
			return;
		}
		if (path == null)
			return;
		Vector3[] corners = getCorners(path);
		if (corners == null)
			return;
		MarchLine ins = null;
		switch(march.GetMarchLineModel())
		{
		case MarchVO.eMarchLineModel.eMLM_BattleLose:
			ins = Instantiate(battleLoseMarchTmp) as MarchLine;
			break;
		case MarchVO.eMarchLineModel.eMLM_BattleWin:
			ins = Instantiate(battleWinMarchTmp) as MarchLine;
			break;
		case MarchVO.eMarchLineModel.eMLM_ResourceEmpty:
			ins = Instantiate(resourceEmptyMarchTmp) as MarchLine;
			break;
		case MarchVO.eMarchLineModel.eMLM_ResourceFull:
			ins = Instantiate(resourceFullMarchTmp) as MarchLine;
			break;
		default:
			break;
		}
		Utils.AddChildAndReset(Lod0.gameObject, ins.gameObject);
		_marchLines.Add(marchId, ins);
		string myPlayerId = App.ProxyMgr.PlayerProxy.player.id;
		
		float marchLineDispLen = 4f;
		if (march.MarchInfo.playerInfo != null && march.MarchInfo.playerInfo.rpPlayerInfo.playerId == myPlayerId ||
		    march.MarchInfo.pvpTargetInfo != null && march.MarchInfo.pvpTargetInfo.rpPlayerInfo.playerId == myPlayerId){
			marchLineDispLen = -1f;
		}
		
		ins.InitMarchLine(march, corners, marchLineDispLen);
		ins.SetColor(c);
		ins.setRelation(TempRelationFunc.GetRelation(march.MarchInfo.playerInfo.rpPlayerInfo.playerId, march.MarchInfo.playerInfo.rpPlayerInfo.allianceId, proto_structs.CampType.CAMP_TYPE_E));
		ins.UpdateMarchState(march.MarchInfo.jobParams.endAt, (uint)march.MarchInfo.jobParams.duration);
		ins.Arriving += (mId) => {
			if (_marchLines.ContainsKey(mId)){
				_marchLines.Remove(mId);
			}
		};
	}

	Vector3[] getCorners(List<Coord> path)
	{
		Vector3[] corners = new Vector3[path.Count];
		for (int i=0; i<path.Count; ++i){
			corners[i] = Layout.HexCenter(path[i]);
		}
		return corners;
	}

	public void UpdateMarchState(string marchId, ulong endTime, ulong duration){
		if (_marchLines.ContainsKey(marchId)){
			_marchLines[marchId].UpdateMarchState(endTime, duration);
		}
	}

	public void OnCalcNewPath(MarchVO march)
	{
		if (!_marchLines.ContainsKey (march.MarchInfo.id))
			return;
		MarchLine _marchLine = _marchLines[march.MarchInfo.id];
		Vector3[] corners = getCorners (march.Paths);
		_marchLine.SetMarchCorners (corners);
	}

	public void FinishMarch(string marchId){
		if (_marchLines.ContainsKey(marchId)){
			_marchLines[marchId].Arrive();
		}
	}
	
	public void ClearMarches(){
		 _marchLines.Clear();
	}
    */
    #endregion

    #region relocate effect
    /*
public IEnumerator RelocateEffectCor(Coord c){
    App.GUIMgr.HoldMouseEnabled();

    if (State == LodState.Lod1){
        yield return Lod1to0(Vector2.zero);
    }

    MoveTo(c);

    GoLoader loader = new GoLoader("Ef_MapTileCity");
    yield return loader.Load();
    var effectGo = GameObject.Instantiate<GameObject>(loader.go);

    Coord submapCoord;
    submapCoord.x = Mathf.FloorToInt( c.x / SubMapView.xTileCnt );
    submapCoord.y = Mathf.FloorToInt( c.y / SubMapView.yTileCnt );
    SubMapView sbv = Lod0.CurSubMapViews[submapCoord];

    GameObject origCity = sbv.DetachSingleTile(c);
    Utils.AssertNotNull(origCity);

    sbv.AttachRelocateEffect(c, effectGo);
    GameObject subroot = effectGo.FindChild("root");

    subroot.transform.localEulerAngles = new Vector3(CameraPitch, 0, 0);
    Utils.AddChildAndReset(subroot, origCity, false);

    Animator anim = subroot.GetComponent<Animator>();
    anim.Play("Ef_MapTileCity");

    while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f){
        yield return null;
    }
    App.SoundMgr.Play ("se_other_relocatecity");
    yield return new WaitForSeconds(1f);
    GameObject.DestroyImmediate(effectGo);
    sbv.RecreateSingleTile(c);
    sbv.SetPitch(MapView.Current.CameraPitch);

    App.GUIMgr.ReleaseMouseEnabled();
    yield break;
}
*/
    #endregion
}