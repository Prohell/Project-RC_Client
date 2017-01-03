using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using InputKit;

public class CameraMove : MonoBehaviour {
	private InputWrapper _input;
	private DragGesture _dragGesture;
	private PinchGesture _pinchGesture;
	private ClickGesture _clickGesture;

	public Camera cam;
	[Tooltip("摄像机投射平面海拔")]
	public float planeHigh = 0f;
	[Tooltip("摄像机移动的范围限制")]
	public Bounds limitBounds;
	[Tooltip("缩进角度")]
	public float scaleXAngle;
	[Tooltip("摄像机X轴角度的变化范围")]
	public Vector2 angleXRange;

	public Action<List<GameObject>> gameObjectClick;
	public Action dragBegin;
	public Action dragEnd;
	public Action pinchBegin;
	public Action pinchEnd;
	void Start()
	{
		InitInteractive ();
	}

	#if UNITY_EDITOR
	void Update(){
		if(Input.GetKey(KeyCode.UpArrow)){
			OnPinch (new Vector2(100f,0f),new Vector2(-1f,0f),new Vector2(200f,0f),new Vector2(1f,0f));
		}

		if(Input.GetKey(KeyCode.DownArrow)){
			OnPinch (new Vector2(100f,0f),new Vector2(1f,0f),new Vector2(200f,0f),new Vector2(-1f,0f));
		}

		if(Input.GetKey(KeyCode.PageDown)){
			OnPinch (new Vector2(100f,0f),new Vector2(1f,0f),new Vector2(200f,0f),new Vector2(-1f,0f));
		}

		if(Input.GetKey(KeyCode.PageUp)){
			OnPinch (new Vector2(100f,0f),new Vector2(-1f,0f),new Vector2(200f,0f),new Vector2(1f,0f));
		}
	}
	#endif

	void LateUpdate()
	{
		_input.Begin();
		_clickGesture.Update();
		_dragGesture.Update();
		_pinchGesture.Update();
		_input.End();
	}


	void InitInteractive()
	{
		_input = new InputWrapper();
		_dragGesture = new DragGesture(_input);
		_dragGesture.DragBeginHandler = OnDragBegin;
		_dragGesture.DragHandler = OnDrag;
		_dragGesture.DragEndHandler = OnDragEnd;


		_pinchGesture = new PinchGesture(_input);
		_pinchGesture.PinchHandler = OnPinch;
		_pinchGesture.PinchEndHandler = OnPinchEnd;
		_pinchGesture.PinchBeginHandler = OnPinchBegin;

		_clickGesture = new ClickGesture(_input);
		_clickGesture.ClickHandler = OnClick;
	}

	void OnDragBegin(Vector2 pos){
		isInertia = false;
		if(dragBegin != null){
			dragBegin ();
		}
	}

	void OnDrag(Vector2 curPos, Vector2 deltaPos)
	{
		Vector3 curCastPos = ScreenPos2WorldPos(cam, curPos);
		Vector3 oriCastPos = ScreenPos2WorldPos(cam, curPos - deltaPos);

		Vector3 localPos = transform.localPosition;
		localPos -= (curCastPos - oriCastPos);

		if(limitBounds.Contains(localPos)){
			transform.localPosition = localPos;
		} else {
			transform.localPosition = limitBounds.ClosestPoint(localPos);
		}
	}


	Vector3 ScreenPos2WorldPos(Camera camera, Vector2 screenPos)
	{
		Ray ray = camera.ScreenPointToRay(screenPos);
		Plane plane = new Plane(Vector3.up, new Vector3(0, planeHigh, 0));
		float dis = 0;
		if (plane.Raycast(ray, out dis)) {
			return ray.GetPoint(dis);
		} else {
			return Vector3.zero;
		}
	}

	void OnDragEnd(Vector2 curPos, Vector2 deltaPos)
	{
		isInertia = true;
		StartCoroutine(Inertia(curPos, deltaPos));
	}

	private bool isInertia = false;
	IEnumerator Inertia(Vector2 curPos, Vector2 deltaPos)
	{
		while (deltaPos.magnitude > 4)
		{
			if(!isInertia){
				yield break;
			}
			OnDrag(curPos, deltaPos);
			deltaPos *= (1.0f - (Time.deltaTime * 2));
			yield return null;
		}
		if(dragEnd != null){
			dragEnd ();
		}
		yield break;
	}


	void OnPinchBegin(Vector2 pos0, Vector2 pos1)
	{
		if(pinchBegin != null){
			pinchBegin ();
		}
	}

	void OnPinch(Vector2 pos0, Vector2 delta0, Vector2 pos1, Vector2 delta1) 
	{
		float curDis = (pos0 - pos1).magnitude;
		float preDis = (pos0 - delta0 - (pos1 - delta1)).magnitude;

		float py = transform.localPosition.y;

		float rate = (curDis - preDis) / preDis;

		float ny = py * (1f - rate);

		float angleX = (transform.localPosition.y - (limitBounds.center.y - limitBounds.extents.y)) / limitBounds.size.y * (angleXRange.y - angleXRange.x) + angleXRange.x;

		float pr = scaleXAngle +  ( angleXRange.y - angleX) *2f;
//		float pr = scaleXAngle +  (angleX) *4f;

		float oriZ = py / Mathf.Tan(pr * Mathf.Deg2Rad);
		float curZ = ny / Mathf.Tan(pr * Mathf.Deg2Rad);

		Vector3 localPos = transform.localPosition;

		localPos.y = ny;
		localPos.z -= (curZ - oriZ);

		float xx = localPos.x;
		float yy = localPos.y;
		float zz = localPos.z;

		if(yy < limitBounds.center.y + limitBounds.extents.y && yy > limitBounds.center.y - limitBounds.extents.y){
			xx = Mathf.Min (xx, limitBounds.center.x + limitBounds.extents.x);
			xx = Mathf.Max (xx, limitBounds.center.x - limitBounds.extents.x);
			zz = Mathf.Min (zz, limitBounds.center.z + limitBounds.extents.z);
			zz = Mathf.Max (zz, limitBounds.center.z - limitBounds.extents.z);
			transform.localPosition = new Vector3 (xx, yy, zz);
		}

		cam.transform.localEulerAngles = new Vector3 (angleX,cam.transform.localEulerAngles.y,cam.transform.localEulerAngles.z);
	}

	void OnPinchEnd(Vector2 pos0, Vector2 pos1)
	{
		if(pinchEnd != null){
			pinchEnd ();
		}
	}

	List<GameObject> list = new List<GameObject> ();
	void OnClick(Vector2 screenPos)
	{
		Ray ray = cam.ScreenPointToRay(screenPos);
		RaycastHit[] hits = Physics.RaycastAll(ray);

		list.Clear ();
		for(int i = 0;i < hits.Length;i++){
			list.Add (hits [i].collider.gameObject);
		}
		if(gameObjectClick != null){
			gameObjectClick (list);
		}
		_dragGesture.Reset ();
	}

	#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube (limitBounds.center,limitBounds.extents * 2f);
	}
	#endif
}
