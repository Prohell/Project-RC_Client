using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public class CityUIMediator : IUIMediator, IMediator{

	private Dictionary<int,string[]> typeDic = new Dictionary<int, string[]>();

	public void OnInit()
	{
		EventManager.GetInstance ().AddEventListener (EventId.BuildingSelected, BuildingSelected);
		EventManager.GetInstance().AddEventListener("Private_CityUIBtnClick", BuildingBtnClick);
		EventManager.GetInstance().AddEventListener(EventId.BuildingLevelUp,BuildingLevelUp);



		typeDic.Add (0,new string[3]{"Btn_Info","Btn_LevelUp","Btn_Train"});
		typeDic.Add (1,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (2,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (3,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (4,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (5,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (6,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (7,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (8,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (9,new string[3]{"Btn_Info","Btn_LevelUp","Btn_Train"});
		typeDic.Add (10,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (11,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (12,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (13,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (14,new string[3]{"Btn_Info","Btn_LevelUp","Btn_Research"});
		typeDic.Add (15,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (16,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (17,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (18,new string[2]{"Btn_Info","Btn_LevelUp"});
		typeDic.Add (19,new string[2]{"Btn_Info","Btn_LevelUp"});
	}

	public void OnDestroy()
	{
	}

	private int curSlot;
	private void BuildingSelected(object obj){
		int num = (int)obj;
		curSlot = num;
		if(num == -1){
			if (selectType != null) {
				selectType (null);
			}
			return;
		}

		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		BuildingData data = cityMediator.GetDataBySlot (curSlot);
		if (data != null) {
			if (selectType != null) {
				selectType (typeDic[data.type]);
			}
		} else {
			#if UNITY_EDITOR
			Game.StartCoroutine (AAA());
			#endif
		}
	}

	private void BuildingBtnClick(object name){
		string n = name.ToString ();

		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		switch(n){
		case "details":
			EventManager.GetInstance ().SendEvent ("Private_HideBuildingButtons");
			UIManager.GetInstance ().OpenUI ("CityInfoUI");
			break;
		case "levelUp":
			int s = cityMediator.curSlot;
			SendBuildingLevelUp(cityMediator.GetDataBySlot (s).guid);
			break;
		case "training":
			UIManager.GetInstance ().OpenUI ("TroopTrainUI");
			break;
		case "collect":
			
			break;
		case "research":
			UIManager.GetInstance ().OpenUI ("ResearchUI");
			break;
		}
	}

	private bool levelUpReceived = true;
	private void SendBuildingLevelUp(long buildingID){
		if(levelUpReceived){
			levelUpReceived = false;
			CG_Building_LevelUp packet = (CG_Building_LevelUp)PacketDistributed.CreatePacket(MessageID.PACKET_CG_BUILDING_LEVELUP);
			packet.SetBuildingID (buildingID);
			packet.SendPacket();
		}

	}
	private void BuildingLevelUp(object id){
		levelUpReceived = true;
	}


	private IEnumerator AAA(){
		yield return new WaitForSeconds (0.1f);
		if(selectType != null){
			selectType (typeDic[0]);
		}
	}

	public Action<string[]> selectType;


	LuaTable IUIMediator.m_View { get; set; }

	string IUIMediator.m_UIName { get; set; }
}
