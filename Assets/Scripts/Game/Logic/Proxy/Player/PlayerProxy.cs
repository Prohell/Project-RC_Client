using UnityEngine;
using System;
using System.Collections;

public class PlayerProxy : IProxy {
	public long userid;
	public string oid;
	public string accesstoken;
	public string playername;
	public int  level;
	public GC_CityData city;
	public GC_HeroList heroList;
	public GC_MarchList marchlist;

	public void OnDestroy()
	{
	}

	public void OnInit()
	{

	}
}
