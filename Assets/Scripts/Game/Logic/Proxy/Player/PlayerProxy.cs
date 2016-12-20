using UnityEngine;
using System.Collections;

public class PlayerProxy : IProxy {
	public long userid;
	public string oid;
	public string accesstoken;
	public GC_CityData city;
	public string playername;
	public int  level;
	public GC_HeroList heroList;
	public GC_MarchList marchlist;

	public void OnDestroy()
	{
	}

	public void OnInit()
	{

	}
}
