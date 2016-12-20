using UnityEngine;

namespace InputKit
{
	public class DragGesture : BaseGesture
    {
        public DragGesture(InputWrapper input) : base(input) { }

        public delegate void OnTouchDown();
        public delegate void OnDragBegin(Vector2 oriPos);
        public delegate void OnDrag(Vector2 curPos, Vector2 deltaPos);
        public delegate void OnDragEnd(Vector2 curPos, Vector2 deltaPos);

        public OnTouchDown TouchDownHandler;
        public OnDragBegin DragBeginHandler;
        public OnDrag DragHandler;
        public OnDragEnd DragEndHandler;

        private Vector2 _curPos = Vector2.zero;
        private Vector2 _prePos = Vector2.zero;
        private Vector2 _momentum = Vector2.zero;

        const float MomentumAmount = 50;

        enum State
        {
            kIdle,
            kTouchDown,
            kDraging,
        }

        State _state = State.kIdle;

        void Update_Idle()
        {
            if (!HasActiveTouch())
                return;

            if (_input.GetTouchCount() == 1 && _input.GetTouchPhase(0) == TouchPhase.Began)
            {
                _state = State.kTouchDown;
                _curPos = _input.GetTouchPosition(0);
                _momentum = Vector2.zero;

                if (TouchDownHandler != null)
                {
                    TouchDownHandler.Invoke();
                }
            }
        }

        void Update_TouchDown()
        {
            if (!HasActiveTouch() || _input.GetTouchCount() > 1)
            {
                _state = State.kIdle;
                return;
            }

            // Touch Cnt must be 1
            if (_input.GetTouchPhase(0) == TouchPhase.Moved)
            {
                Vector2 deltaMove = _input.GetTouchPosition(0) - _curPos;
                float deltaMoveDis = deltaMove.magnitude;
                if (deltaMoveDis > MinDragDis)
                {
                    _state = State.kDraging;

                    if (deltaMoveDis > MinDragDis * 3)
                    {
                        if (DragHandler != null)
                        {
                            DragHandler.Invoke(_input.GetTouchPosition(0), deltaMove);
                        }
                    }
                    _curPos = _input.GetTouchPosition(0);

                    if (DragBeginHandler != null)
                    {
                        DragBeginHandler.Invoke(_curPos);
                    }
                }
            }
        }

        void Update_Draging()
        {
            if (!HasActiveTouch() || _input.GetTouchCount() > 1)
            {
                if (DragEndHandler != null)
                {
                    DragEndHandler.Invoke(_prePos, Vector2.zero);
                }
                _state = State.kIdle;
                return;
            }

            // Touch Cnt must be 1
            Vector2 detaPos = _input.GetTouchDeltaPosition(0);

            if (_input.GetTouchPhase(0) == TouchPhase.Ended)
            {
                if (DragHandler != null)
                {
                    DragHandler.Invoke(_input.GetTouchPosition(0), detaPos);
                }

                if (DragEndHandler != null)
                {
                    DragEndHandler.Invoke(_input.GetTouchPosition(0), _momentum);
                }
                _state = State.kIdle;
                return;
            }

            if (_input.GetTouchPhase(0) == TouchPhase.Moved)
            {
                if (DragHandler != null)
                {
                    DragHandler.Invoke(_input.GetTouchPosition(0), detaPos);
                }
            }
            _momentum = Vector2.Lerp(_momentum, _momentum + detaPos * (0.01f * MomentumAmount), 0.67f);
            _momentum *= 0.5f;
        }

        public override void Update()
        {
            switch (_state)
            {
                case State.kIdle:
                    Update_Idle();
                    break;
                case State.kTouchDown:
                    Update_TouchDown();
                    break;
                case State.kDraging:
                    Update_Draging();
                    break;
            }

            if (_input.GetTouchCount() == 1)
            {
                _prePos = _input.GetTouchPosition(0);
            }
        }

		override public void Reset(){
			_state = State.kIdle;
		}

    }
}