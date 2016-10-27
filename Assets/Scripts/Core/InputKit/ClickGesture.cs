using UnityEngine;
using System.Collections;

namespace InputKit{
	public class ClickGesture : BaseGesture {
		public ClickGesture(InputWrapper input):base(input){}
	
		public delegate void OnClick(Vector2 screenPos);
		public OnClick ClickHandler;
		enum State{
			kIdle,
			KTouchDown,
		}
		State _state = State.kIdle;
		Vector2 _touchDownPos = Vector2.zero;
		
		void Update_Idle(){
			if (!HasActiveTouch())
				return;
				
			if (_input.GetTouchCount() == 1 && _input.GetTouchPhase(0) == TouchPhase.Began){
				_state = State.KTouchDown;
				_touchDownPos = _input.GetTouchPosition(0);
			}
		}
		
		void Update_TouchDown(){
			if (!HasActiveTouch() || 
			    _input.GetTouchCount() != 1 ||
			    (_input.GetTouchPosition(0) - _touchDownPos).magnitude > MinDragDis){
			    
				_state = State.kIdle;
				return;
				
			}
			
			if (_input.GetTouchPhase(0) == TouchPhase.Ended){
				if (ClickHandler != null){
					ClickHandler.Invoke(_touchDownPos);
				}
				_state = State.kIdle;
			}
		}
		
		public override void Update(){
			switch(_state){
			case State.kIdle:
				Update_Idle();
				break;
			case State.KTouchDown:
				Update_TouchDown();
				break;
			}
		}
	}
}