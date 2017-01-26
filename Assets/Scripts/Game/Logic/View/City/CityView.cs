using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LuaInterface;
using GCGame.Table;

public class CityView : MonoBehaviour {

	public GameObject slots;
	public GameObject camContainer;
	private CameraMove cameraMove;
	CityMediator mediator;
	void Awake(){
		mediator = new CityMediator();
		GameFacade.AddMediator<CityView> (mediator, gameObject);
		mediator.PlayerProxyUpdate (null);

		EventManager.GetInstance().AddEventListener("Private_RefreshBuildingLevel",BuildingLevelUp);
	}

	// Use this for initialization
	void Start () {
		cameraMove = camContainer.GetComponent<CameraMove> ();
		cameraMove.gameObjectClick += ClickBuilding;
		cameraMove.dragBegin += DragBeginHandle;
		cameraMove.pinchBegin += PinchBeginHandle;

		UIManager.GetInstance ().OpenUI ("CityUI", null, null, false);
	}

	private HighlighterController targetHighlight;

	private Slot curSlot;
	private void ClickBuilding(List<GameObject> list){
		if(UICamera.isOverUI){
			return;
		}

		for(int i = 0;i < list.Count;i++){
			if(list [i].tag == "Building"){
				GameObject obj = list [i];
				Slot slt = obj.transform.parent.GetComponent<Slot> ();

				if(curSlot != slt){
					if(curSlot != null){
						curSlot.HideBuildingName ();
						curSlot.HideBuildingLevel ();
					}

					HideBtns ();
					curSlot = slt;

					mediator.curSlot = curSlot.transform.GetSiblingIndex () + 1;
					mediator.curSlotType = curSlot.slotType;
					ShowCurSlotCenter ();
				}
				break;
			}
		}
	}

	public void ShowCurSlotCenter(){
		if (curSlot != null) {
			curSlot.ShowBuildingName ();

			BuildingData data = mediator.GetDataBySlot (mediator.curSlot);
			curSlot.level = data.level;

			curSlot.ShowBuildingLevel ();
			Vector3 offset = curSlot.cameraOffset;
			Vector3 pos = new Vector3 (curSlot.transform.position.x + offset.x, cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y + offset.y, curSlot.transform.position.z - Mathf.Tan (cameraMove.angleXRange.y) * (cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y) + offset.z);
			Vector3 angle = new Vector3 (cameraMove.angleXRange.y, 0f, 0f);


			cameraMove.transform.DOMove (pos, 0.5f).SetEase (Ease.OutCirc);
			cameraMove.cam.transform.DORotate (angle, 0.5f).SetEase (Ease.OutCirc);

			targetHighlight = curSlot.modelChild.GetComponent<HighlighterController> ();
			targetHighlight.Fire2 ();
			ShowBtns ();

		}
	}

	public void ShowBtns(){
		Game.StartCoroutine (ShowBtnsCoroutine());
	}

	IEnumerator ShowBtnsCoroutine(){
		yield return new WaitForSeconds (0.2f);
		if (curSlot != null) {
			EventManager.GetInstance().SendEvent(EventId.BuildingSelected, curSlot.transform.GetSiblingIndex() + 1);
		}
	}

	public void HideBtns(){
		if(curSlot != null){
			curSlot.HideBuildingName ();
			curSlot.HideBuildingLevel ();
		}
		curSlot = null;
		targetHighlight = null;
		EventManager.GetInstance().SendEvent(EventId.BuildingSelected, -1);
	}

	private void DragBeginHandle(){
		HideBtns ();
	}

	private void PinchBeginHandle(){
		HideBtns ();
	}

	public void UpdateAllData(List<BuildingData> datalist){
		for(int i = 0;i < datalist.Count;i++){
			UpdateData (datalist [i]);
		}
		targetHighlight = null;
	}


	private void UpdateData(BuildingData data){
		Transform trans = slots.transform.FindChild ("Slot" + data.slot);
		Slot slot = trans.GetComponent<Slot> ();
		if (slot.modelChild != null) {
			if(slot.modelChild.name != data.asset){
				//更新模型
				UpdateModel();

			}
		}
	}

	private void BuildingLevelUp(object obj){
		long guid = (long)obj;
		var data = mediator.GetDataByGuid (guid);

		Transform trans = slots.transform.FindChild ("Slot" + data.slot);
		Slot slot = trans.GetComponent<Slot> ();

		//更新等级
		slot.level = data.level;

		//更新模型
		if (slot.modelChild != null) {
			if(slot.modelChild.name != data.asset){
				UpdateModel();
			}
		}
	}

	private void UpdateModel(){

	}

	// Update is called once per frame
	void Update () {
		if (targetHighlight != null)
		{
			targetHighlight.MouseOver ();
		}
	}


}
