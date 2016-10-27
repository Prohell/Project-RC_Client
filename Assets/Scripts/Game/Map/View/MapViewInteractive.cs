using UnityEngine;
using System;
using System.Collections;

public partial class MapView
{
    public Action<Coord> onClickHandler;
    public Action<Coord> onClickBattlingHandler;

    public Action onDragBeginHomecompass;
    public Action onDragUpdateHomecompass;
    public Action onDragEndUpdateHomecompass;

    InputWrapper _input;
    InputKit.DragGesture _dragGesture;
    InputKit.PinchGesture _pinchGesture;
    InputKit.ClickGesture _clickGesture;

    public static float CameraHeightMax = 1000f;
    public static float CameraHeightMin = 5f;
    public static float CameraHeightMinBounce = 7f;
    public static float CameraBounceTime = 0.2f;
    public static float CameraMapGapDelta = 500f;
    public static float CameraHeightMapGap = 23f;
    public static float MapPinchTimeGap = 0.5f;

    public static float CameraHeightLod0to1 = 28f;
    public static float CameraHeightLod1to0 = 600f;

    public static float CameraAngleMin = 45;
    public static float CameraAngleMax = 60;

    public static float CameraHeightAngleMin = 7;
    public static float CameraHeightAngleMax = 23;
    public static float CameraHeightInit = (CameraHeightAngleMin + CameraHeightAngleMax) / 4f;
    public static float DragDistanceFactor = 1.5f;

    static Vector2 highlightPos;
    public static Vector2 MapInfoHighlightPos
    {
        get
        {
            highlightPos = new Vector2(Screen.width / 2f, Screen.height / 2f);
            return highlightPos;
        }
    }

    public enum MapInteractiveState
    {
        Idle,
        FocusOnMarch,
    }
    MapInteractiveState _state = MapInteractiveState.Idle;

    bool _clickAnimationing = false;
    bool _gestureEnabled = true;
    public bool GestureEnabled
    {
        set
        {
            _gestureEnabled = value;
        }
    }

    // when localEulerAngles set to 88, it will read as 90. Unity Bug
    float _CameraPitch = 90f;
    public float CameraPitch
    {
        get
        {
            return _CameraPitch;
        }
        set
        {
            _CameraPitch = value;
            MapCamera.transform.localEulerAngles = new Vector3(_CameraPitch, 0, 0);
            OnCameraPitch(_CameraPitch);
        }
    }

    public float OutLod0 = 5;
    public float OutLod1 = 270;
    float OutRange
    {
        get
        {
            if (State == LodState.Lod0)
            {
                return OutLod0;
            }
            else
            {
                return OutLod1;
            }
        }
    }

    public void MoveCamera(Vector3 newPos)
    {
        Vector3 prePos = MapCamera.transform.localPosition;
        MapCamera.transform.localPosition = newPos;

        MapLayout.VisibleArea va = MapLayout.CalcRelativeVisibleArea(MapCamera);

        float xMin = -OutRange;
        float xMax = Layout.HexTileWidth * MapConst.MAP_WIDTH + OutRange;
        float zMin = -OutRange;
        float zMax = Layout.HexTileHeight * MapConst.MAP_HEIGHT + OutRange;

        if (newPos.z + va.near < zMin)
        {
            newPos.z = zMin - va.near;
        }

        if (newPos.z + va.far > zMax)
        {
            newPos.z = zMax - va.far;
        }

        if (newPos.x - va.farHalfWidth < xMin)
        {
            newPos.x = xMin + va.farHalfWidth;
        }

        if (newPos.x + va.farHalfWidth > xMax)
        {
            newPos.x = xMax - va.farHalfWidth;
        }

        MapCamera.transform.localPosition = newPos;
        OnCameraMove(newPos, prePos);
    }

    public void InitInteractive(Camera mapCamera, HexMapLayout layout)
    {

        CameraPitch = MapCamera.transform.localEulerAngles.x;

        _input = new InputWrapper();
        _dragGesture = new InputKit.DragGesture(_input);
        _dragGesture.DragHandler = OnDrag;
        _dragGesture.DragEndHandler = OnDragEnd;
        _dragGesture.DragBeginHandler = OnDragBegin;
        _dragGesture.TouchDownHandler = delegate ()
        {
            isInertia = false;
        };


        _pinchGesture = new InputKit.PinchGesture(_input);
        _pinchGesture.PinchHandler = OnPinch;
        _pinchGesture.PinchEndHandler = OnPinchEnd;
        _pinchGesture.PinchBeginHandler = OnPinchBegin;

        _clickGesture = new InputKit.ClickGesture(_input);
        _clickGesture.ClickHandler = OnClick;
    }

    void LateUpdate()
    {
        if (!inited)
            return;
        if (GameSettings.SceneGestureEnabled && _gestureEnabled)
        {
            _input.Begin();
            _clickGesture.Update();
            _dragGesture.Update();
            _pinchGesture.Update();
            _input.End();
        }

        switch (_state)
        {
            case MapInteractiveState.Idle:
                break;

            case MapInteractiveState.FocusOnMarch:
                //UpdateFocusMarch();
                break;
        }
    }

    public void DoClickOnTile(Coord c)
    {
        MapTileVO mt = GameFacade.GetProxy<MapProxy>().GetTile(c);
        _clickAnimationing = true;
        Vector3 prePos = MapCamera.transform.localPosition;
        Vector3 lookPos = prePos + (Layout.HexCenter(c) - Layout.ScreenPos2WorldPos(MapCamera, MapInfoHighlightPos));
        GoTweenConfig config = new GoTweenConfig();
        FloatCallbackTweenProterty ftpy = new FloatCallbackTweenProterty(0, 1, delegate (float obj)
        {
            MoveCamera(Vector3.Lerp(prePos, lookPos, obj));
        });

        config.addTweenProperty(ftpy);
        config.onCompleteHandler = delegate (AbstractGoTween go)
        {
            _clickAnimationing = false;
        };
        ShowHighLight(Layout.HexCenter(c));
        Go.to(MapCamera.transform, 0.5f, config);
        if (onClickHandler != null)
            onClickHandler(c);
    }

    void OnClickLod0(Vector2 screenPos)
    {
        Ray ray = MapCamera.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        // map space
        Coord c = Layout.ScreenPos2Coord(MapCamera, screenPos);
        DoClickOnTile(c);
    }

    public void GoTo(Coord c)
    {
        if (_clickAnimationing)
        {
            return;
        }
        _clickAnimationing = true;
        Vector3 prePos = MapCamera.transform.localPosition;
        Vector3 lookPos = prePos + (Layout.HexCenter(c) - Layout.ScreenPos2WorldPos(MapCamera, MapInfoHighlightPos));


        GoTweenConfig config = new GoTweenConfig();
        FloatCallbackTweenProterty ftpy = new FloatCallbackTweenProterty(0, 1, delegate (float obj)
        {
            MoveCamera(Vector3.Lerp(prePos, lookPos, obj));
        });
        config.addTweenProperty(ftpy);
        config.onCompleteHandler = delegate (AbstractGoTween go)
        {
            _clickAnimationing = false;
            //App.ProxyMgr.MapProxy.GetBlockDataFromServer();
        };
        Go.to(MapCamera.transform, 1f, config);
    }

    public void TweenCameraHeightTo(float newHeight, float duration = 0.2f)
    {
        float yOffset = newHeight - MapCamera.transform.localPosition.y;
        float zOffset = -yOffset / Mathf.Tan(MapCamera.transform.localEulerAngles.x * Mathf.Deg2Rad);
        Vector3 newPos = MapCamera.transform.localPosition + new Vector3(0, yOffset, zOffset);
        TweenCameraTo(newPos, duration);
    }

    public void TweenCameraTo(Vector3 newpos, float duration = 0.2f)
    {
        Vector3 prePos = MapCamera.transform.localPosition;
        GoTweenConfig config = new GoTweenConfig();
        FloatCallbackTweenProterty ftpy = new FloatCallbackTweenProterty(0, 1, delegate (float obj)
        {
            MoveCamera(Vector3.Lerp(prePos, newpos, obj));
        });
        config.addTweenProperty(ftpy);
        Go.to(MapCamera.transform, duration, config);
    }

    void OnClickLod1(Vector2 screenPos)
    {
        Coord coord = Layout.ScreenPos2Coord(MapCamera, screenPos);

        Ray ray = MapCamera.ScreenPointToRay(screenPos);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        // MyCityFlag
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == Lod1.MyCityFlag)
            {
                coord = GameFacade.GetProxy<CityProxy>().myCityCoord;
                break;
            }
        }

        coord.x = Mathf.Clamp(coord.x, 0, MapConst.MAP_WIDTH);
        coord.y = Mathf.Clamp(coord.y, 0, MapConst.MAP_HEIGHT);
        Vector3 wp = GetCameraPosWhenLookAt(coord, MapView.CameraHeightInit);

        StartCoroutine(Lod1to0(wp));
    }

    void OnClick(Vector2 screenPos)
    {
        if (EditMode) return;

        if (_clickAnimationing) return;

        if (State == LodState.Lod0)
        {
            OnClickLod0(screenPos);
        }
        else if (State == LodState.Lod1)
        {
            OnClickLod1(screenPos);
        }
    }

    void OnDrag(Vector2 curPos, Vector2 deltaPos)
    {
        Vector3 curCastPos = Layout.ScreenPos2WorldPos(MapCamera, curPos);
        Vector3 oriCastPos = Layout.ScreenPos2WorldPos(MapCamera, curPos - deltaPos);

        if (_clickAnimationing)
        {
            Go.killAllTweensWithTarget(MapCamera.transform);
            _clickAnimationing = false;
        }
        else
            MoveCamera(MapCamera.transform.localPosition - (curCastPos - oriCastPos) * DragDistanceFactor);
    }

    void OnDragEnd(Vector2 curPos, Vector2 deltaPos)
    {
        if (onDragEndUpdateHomecompass != null)
        {
            onDragEndUpdateHomecompass();
        }
        StartCoroutine(Inertia(curPos, deltaPos));
    }

    void OnDragBegin(Vector2 oriPos)
    {
        if (onDragBeginHomecompass != null)
        {
            onDragBeginHomecompass();
        }
    }

    bool isInertia = false;
    IEnumerator Inertia(Vector2 curPos, Vector2 deltaPos)
    {
        isInertia = true;
        while (deltaPos.magnitude > 4 && isInertia)
        {
            OnDrag(curPos, deltaPos);
            curPos += deltaPos;
            deltaPos *= (1.0f - (Time.deltaTime * 2));
            yield return null;
        }

        //App.ProxyMgr.MapProxy.GetBlockDataFromServer();
        isInertia = false;
        yield break;
    }

    float scaleSpeed = 5f;
    IEnumerator Lod0to1()
    {
        CameraMask.SetActive(false);
        Lod0.gameObject.SetActive(false);
        Lod0.CleanUp();

        Lod1.gameObject.SetActive(true);
        Lod1.SyncMyCityFlagPos();

        State = LodState.Lod0to1;
        Vector3 local = MapCamera.transform.localPosition;

        while (local.y < CameraHeightLod1to0)
        {
            local.y *= (1.0f + scaleSpeed * Time.deltaTime);
            MoveCamera(local);
            yield return null;
        }
        State = LodState.Lod1;
        yield break;
    }

    IEnumerator Lod1to0(Vector3 worldPos, int duration = 10, Action successCallBack = null)
    {
        State = LodState.Lod1to0;
        Vector3 oriLocal = MapCamera.transform.localPosition;
        float oriPitch = MapCamera.transform.localEulerAngles.x;
        float dstPitch = CameraHeight2Angle(worldPos.y);
        for (int i = 0; i < duration; ++i)
        {
            Vector3 local = Vector3.Lerp(oriLocal, worldPos, (i + 1) / (float)duration);
            float pitch = Mathf.Lerp(oriPitch, dstPitch, (i + 1) / (float)duration);
            CameraPitch = pitch;
            MoveCamera(local);
            yield return null;
        }

        State = LodState.Lod0;

        CameraMask.SetActive(true);
        Lod0.gameObject.SetActive(true);
        //		Lod0.OnCameraMove(Vector3.zero, Vector3.zero);
        Lod1.gameObject.SetActive(false);
        MoveCamera(worldPos);

        //App.ProxyMgr.MapProxy.GetBlockDataFromServer();
        if (onDragUpdateHomecompass != null)
        {
            onDragUpdateHomecompass();
        }
        if (successCallBack != null)
        {
            successCallBack();
        }
        yield break;
    }

    void OnPinch(Vector2 pos0, Vector2 delta0, Vector2 pos1, Vector2 delta1)
    {
        if (mStopPinch) return;
        if (!(State == LodState.Lod0 || State == LodState.Lod1)) return;

        OnDrag((pos0 + pos1) / 2f, (delta0 + delta1) / 2f);

        float curDis = (pos0 - pos1).magnitude;
        float preDis = (pos0 - delta0 - (pos1 - delta1)).magnitude;
        float py = MapCamera.transform.localPosition.y;

        float rate = (curDis - preDis) / preDis;

        float ny = py * (1f - rate);
        // = py * (curDis / preDis)
#if UNITY_EDITOR
        ny = Mathf.Clamp(ny, CameraHeightMin, CameraHeightMax);
#else
		if(State == LodState.Lod0)
		{
			ny = Mathf.Clamp(ny, CameraHeightMin, CameraHeightLod0to1);
		}
		else
		{
			ny = Mathf.Clamp(ny, CameraHeightLod1to0, CameraHeightMax);
		}
#endif
        float nr = CameraHeight2Angle(ny);
        float pr = CameraPitch;

        float oriZ = py / Mathf.Tan(pr * Mathf.Deg2Rad);
        float curZ = ny / Mathf.Tan(nr * Mathf.Deg2Rad);

        Vector3 localPos = MapCamera.transform.localPosition;

        localPos.y = ny;
        localPos.z -= (curZ - oriZ);

        CameraPitch = nr;

#if !UNITY_EDITOR
		float delta = mPinchDis - curDis;
		if(Mathf.Abs(delta) >= CameraMapGapDelta)
		{
			if (State == LodState.Lod0 && delta > 0f)
			{
				State = LodState.GoingToLod1;
				mStopPinch = true;
			}
			else if(State == LodState.Lod1 && delta < 0f)
			{
				State = LodState.GoingToLod0;
				mStopPinch = true;
			}
		}
#endif

        MoveCamera(localPos);

        if (EditMode) return;
    }

    float mPinchDis = 0f;
    float mPinchTime = 0f;
    bool mStopPinch = false;
    void OnPinchBegin(Vector2 pos0, Vector2 pos1)
    {
        mStopPinch = false;
        if (MapCamera.transform.localPosition.y >= CameraHeightMapGap)
        {
            mPinchDis = (pos1 - pos0).magnitude;
            mPinchTime = RealTime.time;
        }
        else
        {
            mPinchDis = 0f;
            mPinchTime = 0f;
        }
    }

    void OnPinchEnd(Vector2 pos0, Vector2 pos1)
    {
        if (mStopPinch) return;
        if (!(State == LodState.Lod0 || State == LodState.Lod1)) return;

        float py = MapCamera.transform.localPosition.y;
        float ny = 0;
        if (py < CameraHeightMinBounce)
        {
            ny = CameraHeightMinBounce;
        }
#if !UNITY_EDITOR
		if(State == LodState.Lod0 && py >= CameraHeightMapGap)
		{
			ny = CameraHeightMapGap;
		}
#endif
        if (ny == 0) return;

        float nr = CameraHeight2Angle(ny);
        float pr = CameraPitch;

        float oriZ = py / Mathf.Tan(pr * Mathf.Deg2Rad);
        float curZ = ny / Mathf.Tan(nr * Mathf.Deg2Rad);

        Vector3 localPos = MapCamera.transform.localPosition;

        localPos.y = ny;
        localPos.z -= (curZ - oriZ);

        CameraPitch = nr;

        TweenCameraTo(localPos, CameraBounceTime);

    }

    float CameraHeight2Angle(float height)
    {
        if (height <= CameraHeightAngleMin)
        {
            return CameraAngleMin;
        }
        else if (height >= CameraHeightAngleMax)
        {
            return CameraAngleMax;
        }
        else
        {
            return (height - CameraHeightAngleMin) / (CameraHeightAngleMax - CameraHeightAngleMin) * (CameraAngleMax - CameraAngleMin) + CameraAngleMin;
        }
    }

    Vector3 GetCameraPosWhenLookAt(Coord coord, float height)
    {
        Vector3 pos = Layout.HexCenter(coord);
        pos.y = height;
        float pitch = CameraHeight2Angle(height);
        float zOffset = height / Mathf.Tan(Mathf.Deg2Rad * pitch);
        pos.z -= zOffset;
        return pos;
    }
}