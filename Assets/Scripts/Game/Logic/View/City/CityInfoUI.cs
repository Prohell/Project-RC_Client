using UnityEngine;
using System.Collections;

public class CityInfoUI : MonoBehaviour {
	public UILabel nameLabel;
	public UILabel levelLabel;
	public UILabel nextLevelLabel;
	public UILabel descriptionLabel;

	public CityInfoUIMediator mediator;
	// Use this for initialization
	void Start () {
		mediator = GameFacade.GetMediator<CityInfoUIMediator> ();

		EventManager.GetInstance ().AddEventListener ("Private_RefreshCityInfoBase",RefreshBase);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void RefreshBase(object obj = null){
		nameLabel.text = mediator.cityName;
		levelLabel.text = mediator.cityLevel.ToString();
		nextLevelLabel.text = mediator.GetNextLevel().ToString();
	}

	public void CloseClick(){
		UIManager.GetInstance ().CloseUI ("CityInfoUI", false);
		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		cityMediator.view.ShowCurSlotCenter ();
		cityMediator.view.ShowBtns ();
	}
}
