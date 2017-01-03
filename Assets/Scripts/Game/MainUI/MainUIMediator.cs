using UnityEngine;
using System.Collections;
using LuaInterface;

public class MainUIMediator : IUIMediator, IMediator
{
    public LuaTable m_View { get; set; }
    public string m_UIName { get; set; }

    public void OnInit()
    {
        
    }

    public void OnDestroy()
    {
        
    }
}
