using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

public class BuildingData
{
	public long guid;
	public int type;
	public int slot;
	public int level;
	public int maxLevel;
	public bool isUpgrade;
	public string buildName;
	public string description;
	public string asset;
	public string bundle;
	public int prosperity;
	public List<int> needBuildingID = new List<int>();
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

	public int curSlot;
	public int curSlotType;

    override public void OnInit()
	{
		EventManager.GetInstance().AddEventListener(EventId.PlayerProxyUpdate,PlayerProxyUpdate);
		EventManager.GetInstance().AddEventListener(EventId.BuildingLevelUp,BuildingLevelUp);
    }

	override public void OnDestroy()
	{
	}

	// guid 建筑ID	type 建筑类型	slot 建筑位置	level 建筑等级
	public void PlayerProxyUpdate(object obj){
		PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();

		cityLevel = proxy.city.level;
		food = proxy.city.food;
		stone = proxy.city.stone;
		iron = proxy.city.iron;

		if(proxy.city.buildList.Count != 0){
			for(int i = 0;i < proxy.city.buildList.Count;i++){
				BuildingData data = new BuildingData ();
				data.guid = proxy.city.buildList [i].guid;
				data.type = proxy.city.buildList [i].type;
				data.slot = proxy.city.buildList [i].slot;
				data.level = proxy.city.buildList [i].level;

				//建筑基本信息
				List<Tab_CityBuildingDefault> cityBuilding = TableManager.GetCityBuildingDefaultByID (data.type);
				data.buildName = cityBuilding[0].Name;
				data.isUpgrade = (cityBuilding[0].IsUpgrade == 0)?false:true;
				data.maxLevel = cityBuilding[0].MaxLevel;

				//建筑等级信息
				List<Tab_CityBuildingLevel> levelList = TableManager.GetCityBuildingLevelByID (data.type);

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

	public BuildingData GetDataBySlot(int s){
		for(int i = 0;i < buildings.Count;i++){
			if(buildings [i].slot == s){
				return buildings [i];
			}
		}
		return null;
	}

	public BuildingData GetDataByGuid(long guid){
		for(int i = 0;i < buildings.Count;i++){
			if(buildings [i].guid == guid){
				return buildings [i];
			}
		}
		return null;
	}


	private void BuildingLevelUp(object id){
		long guid = (long)id;

		PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();
		for(int i = 0;i < proxy.city.buildList.Count;i++){
			BuildingVo buildingData = proxy.city.buildList [i];
			if(buildingData.guid == guid){
				for(int j = 0;j < buildings.Count;j++){
					if(buildings [j].guid == guid){
						buildings [j].level = buildingData.level;

						EventManager.GetInstance ().SendEvent ("Private_RefreshBuildingLevel", guid);
						break;
					}
				}
				break;
			}
		}
	}
}
