using UnityEngine;
using System.Collections;
using LuaInterface;

public class DetailsUIMediator : IUIMediator, IMediator {

	LuaTable IUIMediator.m_View { get; set; }

	string IUIMediator.m_UIName { get; set; }

	public void OnInit()
	{
	}

	public void OnDestroy()
	{
	}
}
