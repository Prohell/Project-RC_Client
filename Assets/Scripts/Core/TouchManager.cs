using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class TouchManager : MonoBehaviour
{
    void OnEnable ()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
    }

    void OnDisable ()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
    }

    void OnDestroy()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
    }

    private void On_TouchStart(Gesture gesture)
    {
        LogModule.DebugLog("Touch in " + gesture.position);
    }
}
