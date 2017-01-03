namespace InputKit
{

    public class BaseGesture
    {
        protected InputWrapper _input;

        public BaseGesture(InputWrapper input)
        {
            _input = input;
        }

        protected const float MinDragDis = 15;
        protected bool HasActiveTouch()
        {
            if (_input.GetTouchCount() > 0)
            {
				if (UICamera.isOverUI) {
					return false;
				}
				return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Update()
        {

        }

		public virtual void Reset(){
			
		}
    }
}