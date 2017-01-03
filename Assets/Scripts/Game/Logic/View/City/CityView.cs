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

	void Awake(){
		GameFacade.AddMediator<CityView> (new CityMediator(), gameObject);
	}

	// Use this for initialization
	void Start () {
		cameraMove = camContainer.GetComponent<CameraMove> ();
		cameraMove.gameObjectClick += ClickBuilding;
		cameraMove.dragBegin += DragBeginHandle;
		cameraMove.pinchBegin += PinchBeginHandle;
	}

	private HighlighterController targetHighlight;

	private Slot curSlot;
	private void ClickBuilding(List<GameObject> list){
		if(UICamera.isOverUI){
			return;
		}

		for(int i = 0;i < list.Count;i++){
			if(list [i].tag == "Building"){
				if(curSlot != null){
					curSlot.HideBuildingName ();
				}

				GameObject obj = list [i];
				curSlot = obj.transform.parent.GetComponent<Slot> ();

				ShowCurSlotCenter ();

				ShowBtns ();
				break;
			}
		}
	}

	public void ShowCurSlotCenter(){
		if (curSlot != null) {
			curSlot.ShowBuildingName ();
			Vector3 offset = curSlot.cameraOffset;
			Vector3 pos = new Vector3 (curSlot.transform.position.x + offset.x, cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y + offset.y, curSlot.transform.position.z - Mathf.Tan (cameraMove.angleXRange.y) * (cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y) + offset.z);
			Vector3 angle = new Vector3 (cameraMove.angleXRange.y, 0f, 0f);

			cameraMove.transform.DOMove (pos, 0.5f).SetEase (Ease.OutCirc);
			cameraMove.cam.transform.DORotate (angle, 0.5f).SetEase (Ease.OutCirc);

			targetHighlight = curSlot.modelChild.GetComponent<HighlighterController> ();
			targetHighlight.Fire2 ();
		}
	}

	public void ShowCurSlotDetail(){
		if (curSlot != null) {
			curSlot.ShowBuildingName ();
			Vector3 offset = curSlot.cameraOffset;
			Vector3 pos = new Vector3 (curSlot.transform.position.x + offset.x + 50f, cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y + offset.y, curSlot.transform.position.z - Mathf.Tan (cameraMove.angleXRange.y) * (cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y) + offset.z);
			Vector3 angle = new Vector3 (cameraMove.angleXRange.y, 0f, 0f);

			cameraMove.transform.DOMove (pos, 0.5f).SetEase(Ease.OutCirc);
			cameraMove.cam.transform.DORotate (angle, 0.5f).SetEase(Ease.OutCirc);

			targetHighlight = curSlot.modelChild.transform.GetComponent<HighlighterController>();
			targetHighlight.Fire2 ();
		}
	}

	public void ShowBtns(){
		if (curSlot != null) {
			//选择建筑物事件
			if (UIManager.GetInstance ().GetItem ("CityUI") != null && UIManager.GetInstance ().GetItem ("CityUI").IsShowing) {
				UIManager.GetInstance ().CloseUI ("CityUI", false);
			}
			UIManager.GetInstance ().OpenUI ("CityUI", null, CityUIRefresh);
		}
	}

	public void HideBtns(){
		if(curSlot != null){
			curSlot.HideBuildingName ();
		}
		curSlot = null;
		targetHighlight = null;
		if(UIManager.GetInstance ().GetItem("CityUI") != null && UIManager.GetInstance ().GetItem("CityUI").IsShowing){
			UIManager.GetInstance ().CloseUI ("CityUI", false);
		}
	}

	private void CityUIRefresh(LuaTable table){
		EventManager.GetInstance().SendEvent(EventId.BuildingSelected, curSlot.transform.GetSiblingIndex() + 1);
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


	public void UpdateData(BuildingData data){
		Transform trans = slots.transform.FindChild ("Slot" + data.slot);
		if (trans.GetComponent<Slot> ().modelChild != null) {
			if(trans.GetComponent<Slot> ().modelChild.name != data.asset){
				//更新模型
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
