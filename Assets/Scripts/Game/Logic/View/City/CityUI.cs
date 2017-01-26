using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityUI : MonoBehaviour {

	public List<UIButton> buttons;

	private CityUIMediator mediator;
	void Awake(){
		EventManager.GetInstance().AddEventListener("Private_ShowBuildingButtons", ShowBtns);
		EventManager.GetInstance().AddEventListener("Private_HideBuildingButtons", HideBtns);
	}

	void Start () {
		mediator = GameFacade.GetMediator<CityUIMediator> ();
		mediator.selectType += ShowBtns;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void HideBtns(object obj){
		HideBtns ();
	}

	private void HideBtns(){
		for (int i = 0; i < buttons.Count; i++) {
			buttons [i].gameObject.SetActive (false);
		}
	}

	private void ShowBtns(object obj){
		ShowBtns (obj as string[]);
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
		EventManager.GetInstance().SendEvent("Private_CityUIBtnClick", "details");
	}

	public void LevelUpClick(){
		EventManager.GetInstance().SendEvent("Private_CityUIBtnClick", "levelUp");
	}

	public void TrainingClick(){
		EventManager.GetInstance().SendEvent("Private_CityUIBtnClick", "training");
	}

	public void CollectClick(){
		EventManager.GetInstance().SendEvent("Private_CityUIBtnClick", "collect");
	}

	public void ResearchClick(){
		EventManager.GetInstance().SendEvent("Private_CityUIBtnClick", "research");
	}

}
