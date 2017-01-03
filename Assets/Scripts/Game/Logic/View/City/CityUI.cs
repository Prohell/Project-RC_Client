using UnityEngine;
using System.Collections;

public class CityUI : MonoBehaviour {
	public UIButton details;
	public UIButton levelUp;
	public UIButton training;
	public UIButton collect;

	private CityUIMediator mediator;
	void Awake(){
		Debug.Log ("CityUIAwake");
	}

	void Start () {
		mediator = GameFacade.GetMediator<CityUIMediator> ();
		mediator.selectType += BuildingSelected;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void BuildingSelected(int id){
		switch(id){
		case -1:
			details.gameObject.SetActive (false);
			levelUp.gameObject.SetActive (false);
			training.gameObject.SetActive (false);
			collect.gameObject.SetActive (false);
			break;
		case 1:
			details.gameObject.SetActive (true);
			training.gameObject.SetActive (false);
			collect.gameObject.SetActive (false);
			break;
		case 9:
			details.gameObject.SetActive (true);
			training.gameObject.SetActive (true);
			collect.gameObject.SetActive (false);
			break;
		default :
			details.gameObject.SetActive (true);
			levelUp.gameObject.SetActive (true);
			training.gameObject.SetActive (true);
			collect.gameObject.SetActive (true);
			break;
		}
		SortBtns ();
	}

	private float space = 120f;
	private void SortBtns(){
		int index = 0;
		if(details.gameObject.activeSelf){
			details.transform.localPosition = new Vector3 (index * space, details.transform.localPosition.y,0f);
			index++;
		}
		if(levelUp.gameObject.activeSelf){
			levelUp.transform.localPosition = new Vector3 (index * space, levelUp.transform.localPosition.y,0f);
			index++;
		}
		if(training.gameObject.activeSelf){
			training.transform.localPosition = new Vector3 (index * space, training.transform.localPosition.y,0f);
			index++;
		}
		if(collect.gameObject.activeSelf){
			collect.transform.localPosition = new Vector3 (index * space, collect.transform.localPosition.y,0f);
			index++;
		}

		float offset = space * (index - 1)* 0.5f;
		details.transform.localPosition = new Vector3 (details.transform.localPosition.x - offset, details.transform.localPosition.y,0f);
		levelUp.transform.localPosition = new Vector3 (levelUp.transform.localPosition.x - offset, levelUp.transform.localPosition.y,0f);
		training.transform.localPosition = new Vector3 (training.transform.localPosition.x - offset, training.transform.localPosition.y,0f);
		collect.transform.localPosition = new Vector3 (collect.transform.localPosition.x - offset, collect.transform.localPosition.y,0f);
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
