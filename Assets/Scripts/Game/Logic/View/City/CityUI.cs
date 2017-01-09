using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityUI : MonoBehaviour {

	public List<UIButton> buttons;

	private CityUIMediator mediator;
	void Awake(){
		Debug.Log ("CityUIAwake");
	}

	void Start () {
		mediator = GameFacade.GetMediator<CityUIMediator> ();
		mediator.selectType += ShowBtns;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HideBtns(){
		for (int i = 0; i < buttons.Count; i++) {
			buttons [i].gameObject.SetActive (false);
		}
	}

	private void ShowBtns(string[] names){
		if(names == null){
			HideBtns ();
			return;
		}

		for(int i = 0;i < buttons.Count;i++){
			buttons [i].gameObject.SetActive (false);
			for(int j = 0;j < names.Length;j++){
				if(names[j] == buttons[i].gameObject.name){
					buttons [i].gameObject.SetActive (true);
				}
			}
		}
	}

	public void DetailsClick(){
		EventManager.GetInstance().SendEvent(EventId.BuildingBtnClick, "details");
	}

	public void LevelUpClick(){
		EventManager.GetInstance().SendEvent(EventId.BuildingBtnClick, "levelUp");
	}

	public void TrainingClick(){
		EventManager.GetInstance().SendEvent(EventId.BuildingBtnClick, "training");
	}

	public void CollectClick(){
		EventManager.GetInstance().SendEvent(EventId.BuildingBtnClick, "collect");
	}

}
