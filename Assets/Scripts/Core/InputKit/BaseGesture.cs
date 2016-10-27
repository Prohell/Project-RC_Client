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

                if (UICamera.lastHit.collider != null)
                {
                    return false;
                }
                else
                {
                    //bool uiHold = App.GUIMgr != null && !App.GUIMgr.MouseEnabled;
                    //if (uiHold){
                    //	return false;
                    //}
                    //else{
                    return true;
                    //}
                }
            }
            else
            {
                return false;
            }
        }

        public virtual void Update()
        {

        }
    }
}