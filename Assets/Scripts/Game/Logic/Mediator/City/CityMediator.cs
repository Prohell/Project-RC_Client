using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

public class BuildingData
{
	public int id;
	public int slot;
	public int level;
	public int maxLevel;
	public bool isUpgrade;
	public string buildName;
	public string description;
	public string asset;
	public string bundle;
	public int prosperity;
	public List<int> needBuildingID;
	public int needTime;
	public int needFood;
	public int needStone;
	public int needIron;
	public int needGold;
}

public class CityMediator : Mediator<CityView>
{
	private int cityLevel;
	private long food;
	private long stone;
	private long iron;

	private List<BuildingData> buildings = new List<BuildingData>();

    override public void OnInit()
	{
		EventManager.GetInstance().AddEventListener(EventId.PlayerProxyUpdate,PlayerProxyUpdate);
    }

	override public void OnDestroy()
	{
	}

	// guid 建筑ID	type 建筑类型	slot 建筑位置	level 建筑等级
	private void PlayerProxyUpdate(object obj){
		PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();

		cityLevel = proxy.city.Level;
		food = proxy.city.Food;
		stone = proxy.city.Stone;
		iron = proxy.city.Iron;

		if(proxy.city.buildlistCount != 0){
			for(int i = 0;i < proxy.city.buildlistCount;i++){
				BuildingData data = new BuildingData ();

				data.id = proxy.city.buildlistList [i].Type;
				data.slot = proxy.city.buildlistList [i].Slot;
				data.level = proxy.city.buildlistList [i].Level;

				//建筑基本信息
				List<Tab_CityBuildingDefault> cityBuilding = TableManager.GetCityBuildingDefaultByID (data.id);
				data.buildName = cityBuilding[0].Name;
				data.isUpgrade = (cityBuilding[0].IsUpgrade == 0)?false:true;
				data.maxLevel = cityBuilding[0].MaxLevel;

				//建筑等级信息
				List<Tab_CityBuildingLevel> levelList = TableManager.GetCityBuildingLevelByID (data.id);

				for(int n = 0;n < levelList.Count;n++){
					if(levelList [n].Level == data.level){
						Tab_CityBuildingLevel cityBuildingLevel = levelList [n];
						data.description = cityBuildingLevel.Description;
						data.asset = cityBuildingLevel.Asset;
						data.bundle = cityBuildingLevel.Bundle;
						data.prosperity = cityBuildingLevel.Prosperity;
						for(int j = 0;j < cityBuildingLevel.getNeedBuildingCount();j++){
							data.needBuildingID.Add(cityBuildingLevel.GetNeedBuildingbyIndex (j));
						}
						data.needTime = cityBuildingLevel.NeedTime;
						data.needFood = cityBuildingLevel.NeedFood;
						data.needStone = cityBuildingLevel.NeedStone;
						data.needIron = cityBuildingLevel.NeedIron;
						data.needGold = cityBuildingLevel.NeedGold;
						break;
					}
				}

				buildings.Add (data);
			}
		}


//		view.UpdateAllData (buildings);
	}

	public BuildingData GetDataByNum(int num){
		for(int i = 0;i < buildings.Count;i++){
			if(buildings [i].slot == num){
				return buildings [i];
			}
		}
		return null;
	}




}
