using UnityEngine;
using System.Collections;

public class TroopTrainUI : MonoBehaviour {

	public UIScrollView characterScroll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ScrollNext(){
		Debug.Log(characterScroll.currentMomentum);
	}

	public void ScrollPrev(){
		
	}


	public void ClickCharacter(){
		Debug.Log ("ClickCharacter");
	}

	public void CloseClick(){
		UIManager.GetInstance ().CloseUI ("TroopTrainUI", false);
		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		cityMediator.view.ShowCurSlotCenter ();
		cityMediator.view.ShowBtns ();
	}
}
