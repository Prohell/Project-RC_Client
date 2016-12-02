using UnityEngine;

public class SimpleDragCamera : MonoBehaviour
{
    InputWrapper _input;
    InputKit.DragGesture _dragGesture;
    InputKit.PinchGesture _pinchGesture;

    void Start()
    {
        _input = new InputWrapper();
        _dragGesture = new InputKit.DragGesture(_input);
        _dragGesture.DragHandler = OnDrag;
        _pinchGesture = new InputKit.PinchGesture(_input);
        _pinchGesture.PinchHandler = OnPinch;
    }

    void LateUpdate()
    {
        _input.Begin();
        _dragGesture.Update();
        _pinchGesture.Update();
        _input.End();
    }

    void OnDrag(Vector2 curPos, Vector2 deltaPos)
    {
        transform.position += new Vector3(deltaPos.x, 0, deltaPos.y) / 10f;
    }

    void OnPinch(Vector2 pos0, Vector2 delta0, Vector2 pos1, Vector2 delta1)
    {
        float curDis = (pos0 - pos1).magnitude;
        float preDis = (pos0 - delta0 - (pos1 - delta1)).magnitude;
        transform.position += transform.forward * (preDis - curDis) / 20f;
    }
}