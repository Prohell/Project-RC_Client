using UnityEngine;

public interface IInputDevice
{
    int GetTouchCount();
    Vector2 GetTouchPosition(int idx);
    Vector2 GetTouchDeltaPosition(int idx);
    TouchPhase GetTouchPhase(int idx);
}

public class InputWrapper
{
    IInputDevice _device;
    MouseWrapper _mouse;
    TouchWrapper _touch;

    public InputWrapper()
    {
#if UNITY_EDITOR
        _mouse = new MouseWrapper();
        _device = _mouse;
#else
		_touch = new TouchWrapper();
		_device = _touch;
#endif
    }
    public int GetTouchCount()
    {
        return _device.GetTouchCount();
    }

    public Vector2 GetTouchPosition(int idx)
    {
        return _device.GetTouchPosition(idx);
    }

    public Vector2 GetTouchDeltaPosition(int idx)
    {
        return _device.GetTouchDeltaPosition(idx);
    }

    public TouchPhase GetTouchPhase(int idx)
    {
        return _device.GetTouchPhase(idx);
    }

    public void Begin()
    {
#if UNITY_EDITOR
        _mouse.Update();
#else
		_touch.Begin();
#endif
    }

    public void End()
    {
#if UNITY_EDITOR
#else
		_touch.End();
#endif
    }
}