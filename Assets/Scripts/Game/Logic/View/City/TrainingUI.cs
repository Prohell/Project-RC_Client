using UnityEngine;
using System.Collections;

public class TrainingUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CloseClick(){
		UIManager.GetInstance ().CloseUI ("TrainingUI", false);
		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		cityMediator.view.ShowCurSlotCenter ();
		cityMediator.view.ShowBtns ();
	}
}
