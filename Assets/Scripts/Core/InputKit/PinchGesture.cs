using UnityEngine;

namespace InputKit
{
    public class PinchGesture : BaseGesture
    {
        public PinchGesture(InputWrapper input) : base(input) { }

        public delegate void OnPinchBegin(Vector2 pos0, Vector2 pos1);
        public delegate void OnPinch(Vector2 pos0, Vector2 delta0, Vector2 pos1, Vector2 delta1);
        public delegate void OnPinchEnd(Vector2 pos0, Vector2 pos1);

        public OnPinchBegin PinchBeginHandler;
        public OnPinch PinchHandler;
        public OnPinchEnd PinchEndHandler;

        enum State
        {
            kIdle,
            kPinching,
        }

        State _state = State.kIdle;

        void Update_kIdle()
        {
            if (!HasActiveTouch())
                return;

            if (_input.GetTouchCount() == 2)
            {
                TouchPhase ph0 = _input.GetTouchPhase(0);
                TouchPhase ph1 = _input.GetTouchPhase(1);
                if (ph0 == TouchPhase.Began && (ph1 == TouchPhase.Began || ph1 == TouchPhase.Moved || ph1 == TouchPhase.Stationary) ||
                    ph1 == TouchPhase.Began && (ph0 == TouchPhase.Began || ph0 == TouchPhase.Moved || ph0 == TouchPhase.Stationary))
                {
                    _state = State.kPinching;
                    if (PinchBeginHandler != null)
                    {
                        PinchBeginHandler.Invoke(_input.GetTouchPosition(0), _input.GetTouchPosition(1));
                    }
                }
            }
        }

        void Update_kPinching()
        {
            if (_input.GetTouchCount() != 2 || !HasActiveTouch())
            {
                _state = State.kIdle;
                if (PinchEndHandler != null)
                {
                    PinchEndHandler.Invoke(_input.GetTouchPosition(0), _input.GetTouchPosition(1));
                }
            }
            else
            {
                TouchPhase ph0 = _input.GetTouchPhase(0);
                TouchPhase ph1 = _input.GetTouchPhase(1);
                if (ph0 == TouchPhase.Ended ||
                    ph0 == TouchPhase.Canceled ||
                    ph1 == TouchPhase.Ended ||
                    ph1 == TouchPhase.Canceled)
                {

                    _state = State.kIdle;
                    if (PinchEndHandler != null)
                    {
                        PinchEndHandler.Invoke(_input.GetTouchPosition(0), _input.GetTouchPosition(1));
                    }
                }
                else if (ph0 == TouchPhase.Moved || ph1 == TouchPhase.Moved)
                {
                    PinchHandler.Invoke(_input.GetTouchPosition(0), _input.GetTouchDeltaPosition(0),
                                        _input.GetTouchPosition(1), _input.GetTouchDeltaPosition(1));
                }
            }
        }

        public override void Update()
        {
            switch (_state)
            {
                case State.kIdle:
                    Update_kIdle();
                    break;
                case State.kPinching:
                    Update_kPinching();
                    break;
            }
        }
    }
}