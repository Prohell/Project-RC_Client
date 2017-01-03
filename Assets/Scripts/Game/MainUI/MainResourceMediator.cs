using UnityEngine;
using System.Collections;
using LuaInterface;

public class MainResourceMediator : IUIMediator, IMediator
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
