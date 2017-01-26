using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using GCGame.Table;

public class CityInfoUIMediator : IUIMediator, IMediator {

	LuaTable IUIMediator.m_View { get; set; }

	string IUIMediator.m_UIName { get; set; }

	public int cityID;
	public string cityName;
	public int cityLevel;
	public int cityMaxLevel;

	public void OnInit()
	{
		
	}

	public void OnDestroy()
	{
	}

	public int GetNextLevel(){
		int nextLevel = 0;
		if (cityLevel == cityMaxLevel) {
			nextLevel = cityLevel;
		} else {
			nextLevel = cityLevel + 1;
		}
		return nextLevel;
	}

	public void RefreshData(){
		CityMediator cityMediator = GameFacade.GetMediator<CityMediator>();


		List<Tab_CityBuildingDefault> defaults = TableManager.GetCityBuildingDefaultByID(cityMediator.curSlotType);

		Tab_CityBuildingDefault building = defaults[0];

		PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();

		cityID = building.Id;
		cityName = building.Name;

		Dictionary<int,List<Tab_CityBuildingLevel>> dict = TableManager.GetCityBuildingLevel ();

		List<Tab_CityBuildingLevel> levels = TableManager.GetCityBuildingLevelByID (cityID);


		for(int i = 0;i < proxy.city.buildList.Count;i++){
			var data = proxy.city.buildList[i];
			if(data.type == cityID){
				cityLevel = data.level;
			}
		}

		EventManager.GetInstance ().SendEvent ("Private_RefreshCityInfoBase");
	}
}
