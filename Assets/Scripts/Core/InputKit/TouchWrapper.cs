using UnityEngine;
using System.Collections;

public class TouchWrapper : IInputDevice
{
	public int GetTouchCount()
	{
		return Input.touchCount;
	}
	
	public Vector2 GetTouchPosition(int idx)
	{
		return Input.GetTouch(idx).position;
	}
	
	public Vector2 GetTouchDeltaPosition(int idx)
	{
		return GetTouchPosition(idx) - _preTouchPosition[idx];
	}
	
	public TouchPhase GetTouchPhase(int idx)
	{
		return Input.GetTouch(idx).phase;
	}
	
	private Vector2[] _preTouchPosition = new Vector2[2];
	public void Begin()
	{
		for (int i=0; i<Mathf.Min(GetTouchCount(), 2); i++)
		{
			if (GetTouchPhase(i) != TouchPhase.Moved)
			{
				_preTouchPosition[i] = GetTouchPosition(i);
			}
		}
	}
	
	public void End()
	{
		for (int i=0; i<Mathf.Min(GetTouchCount(), 2); i++)
		{
			_preTouchPosition[i] = GetTouchPosition(i);
		}
	}
}
