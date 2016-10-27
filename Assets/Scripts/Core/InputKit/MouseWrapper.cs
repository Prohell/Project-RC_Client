using UnityEngine;
using System.Collections;

public class MouseWrapper : IInputDevice {
	
	int _touchCount = 0;
	Vector2[] _preTouchPosition = new Vector2[2];
	Vector2[] _curTouchPosition = new Vector2[2];
	TouchPhase[] _touchPhase = new TouchPhase[2];
	
	public int GetTouchCount()
	{
		return _touchCount;
	}
	
	public Vector2 GetTouchPosition(int idx)
	{
		return _curTouchPosition[idx];
	}
	
	public Vector2 GetTouchDeltaPosition(int idx)
	{
		return _curTouchPosition[idx] - _preTouchPosition[idx];
	}
	
	public TouchPhase GetTouchPhase(int idx)
	{
		return _touchPhase[idx];
	}
	
	enum PinchSimState{
		kNone,
		
		kBegin,
		kMove,
		kEnd,
	}
	
	PinchSimState _pichSimState = PinchSimState.kNone;
	float _axis = 0f;
	
	float oriDis = 100;
	float gap = 10;
	
	public void Update()
	{
		if (_pichSimState == PinchSimState.kNone){
			if (Input.GetMouseButton(0)){
				_touchCount = 1;
				if (Input.GetMouseButtonDown(0)){
					_touchPhase[0] = TouchPhase.Began;
					_curTouchPosition[0] = Input.mousePosition.xy();
					_preTouchPosition[0] = _curTouchPosition[0];
				}
				else{
					_preTouchPosition[0] = _curTouchPosition[0];
					_curTouchPosition[0] = Input.mousePosition.xy();
					if (_curTouchPosition[0] != _preTouchPosition[0]){
						_touchPhase[0] = TouchPhase.Moved;
					}
					else{
						_touchPhase[0] = TouchPhase.Stationary;
					}
				}
			}
			else if (Input.GetMouseButtonUp(0)){
				_touchCount = 1;
				_touchPhase[0] = TouchPhase.Ended;
				_preTouchPosition[0] = _curTouchPosition[0];
				//_curTouchPosition[0] = Input.mousePosition.xy();
			}
			
			else{
				_axis = Input.GetAxis("Mouse ScrollWheel");
				if (_axis != 0){
					_pichSimState = PinchSimState.kBegin;
					_touchCount = 2;
					Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);
					_curTouchPosition[0] = screenCenter - new Vector2(oriDis, 0);
					_curTouchPosition[1] = screenCenter + new Vector2(oriDis, 0);
					_preTouchPosition[0] = _curTouchPosition[0];
					_preTouchPosition[1] = _curTouchPosition[1];
					_touchPhase[0] = TouchPhase.Began;
					_touchPhase[1] = TouchPhase.Began;
				}
			}
		}
		else{
			if (_pichSimState == PinchSimState.kBegin){
				_pichSimState = PinchSimState.kMove;
				
				_touchPhase[0] = TouchPhase.Moved;
				_touchPhase[1] = TouchPhase.Moved;
				
				
				
				Vector2 screenCenter = new Vector2(Screen.width/2f, Screen.height/2f);
				if (_axis < 0){
					_curTouchPosition[0] = screenCenter - new Vector2(oriDis - gap, 0);
					_curTouchPosition[1] = screenCenter + new Vector2(oriDis - gap, 0);	
				}
				else if (_axis > 0){
					_curTouchPosition[0] = screenCenter - new Vector2(oriDis + gap, 0);
					_curTouchPosition[1] = screenCenter + new Vector2(oriDis + gap, 0);
				}
			}
			else if (_pichSimState == PinchSimState.kMove){
				_pichSimState = PinchSimState.kEnd;
				_touchPhase[0] = TouchPhase.Ended;
				_touchPhase[1] = TouchPhase.Ended;
				_preTouchPosition[0] = _curTouchPosition[0];
				_preTouchPosition[1] = _curTouchPosition[1];
			}
			else if (_pichSimState == PinchSimState.kEnd){
				_pichSimState = PinchSimState.kNone;
				_touchCount = 0;
			}
		}
	}
}
