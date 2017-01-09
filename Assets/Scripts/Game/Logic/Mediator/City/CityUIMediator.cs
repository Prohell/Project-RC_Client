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
		EventManager.GetInstance().AddEventListener(EventId.BuildingBtnClick, BuildingBtnClick);


		typeDic.Add (1,new string[3]{"Btn_Info","Btn_LevelUp","Btn_Train"});
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
		BuildingData data = cityMediator.GetDataByNum (num);
		if (data != null) {
			if (selectType != null) {
				selectType (typeDic[data.id]);
			}
		} else {
			#if UNITY_EDITOR
			Game.StartCoroutine (AAA());
			#endif
		}
	}

	private void BuildingBtnClick(object name){
		string n = name.ToString ();
		switch(n){
		case "details":
			BuildingSelected (-1);

			UIManager.GetInstance ().OpenUI ("DetailsUI");

			CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
			cityMediator.view.ShowCurSlotDetail ();

			break;
		case "levelUp":
			break;
		case "training":
			BuildingSelected (-1);

			UIManager.GetInstance ().OpenUI ("TroopTrainUI");
			break;
		case "collect":
			break;
		}
	}

	private IEnumerator AAA(){
		yield return new WaitForSeconds (0.1f);
		if(selectType != null){
			selectType (typeDic[1]);
		}
	}

	public Action<string[]> selectType;


	LuaTable IUIMediator.m_View { get; set; }

	string IUIMediator.m_UIName { get; set; }
}
