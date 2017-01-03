using UnityEngine;
using System;
using System.Collections;
using LuaInterface;

public class CityUIMediator : IUIMediator, IMediator{

	public void OnInit()
	{
		EventManager.GetInstance ().AddEventListener (EventId.BuildingSelected, BuildingSelected);
		EventManager.GetInstance().AddEventListener(EventId.BuildingBtnClick, BuildingBtnClick);
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
				selectType (-1);
			}
			return;
		}

		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		BuildingData data = cityMediator.GetDataByNum (num);
		if (data != null) {
			if (selectType != null) {
				selectType (data.id);
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
			if (UIManager.GetInstance ().GetItem ("CityUI") != null && UIManager.GetInstance ().GetItem ("CityUI").IsShowing) {
				UIManager.GetInstance ().CloseUI ("CityUI", false);
			}

			UIManager.GetInstance ().OpenUI ("DetailsUI");

			CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
			cityMediator.view.ShowCurSlotDetail ();

			break;
		case "levelUp":
			break;
		case "training":

			UIManager.GetInstance ().OpenUI ("TrainingUI");
			break;
		case "collect":
			break;
		}
	}

	private IEnumerator AAA(){
		yield return new WaitForSeconds (0.1f);
		if(selectType != null){
			selectType (100);
		}
	}

	public Action<int> selectType;


	LuaTable IUIMediator.m_View { get; set; }

	string IUIMediator.m_UIName { get; set; }
}
