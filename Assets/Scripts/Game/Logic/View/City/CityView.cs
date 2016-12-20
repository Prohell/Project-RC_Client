using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CityView : MonoBehaviour {

	public GameObject slots;
	public GameObject camContainer;
	private CameraMove cameraMove;
	// Use this for initialization
	void Start () {

		Dictionary<int,List<object>> cityBuildingDefault = new Dictionary<int, List<object>> ();

		GCGame.Table.Tab_CityBuildingDefault.LoadTable(cityBuildingDefault);



		cameraMove = camContainer.GetComponent<CameraMove> ();
		cameraMove.gameObjectClick += ClickBuilding;
	}
	private HighlighterController targetHighlight;
	private void ClickBuilding(GameObject obj){
		if(obj.tag == "Building"){
			Vector3 pos = new Vector3 (obj.transform.position.x, cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y, obj.transform.position.z - Mathf.Tan (cameraMove.angleXRange.y) * (cameraMove.limitBounds.center.y + cameraMove.limitBounds.extents.y));
			Vector3 angle = new Vector3 (cameraMove.angleXRange.y, 0f, 0f);

			cameraMove.transform.DOMove (pos, 0.5f).SetEase(Ease.OutCirc);
			cameraMove.cam.transform.DORotate (angle, 0.5f).SetEase(Ease.OutCirc);

			targetHighlight = obj.GetComponent<HighlighterController>();
			targetHighlight.Fire2 ();
		}
	}

	// Update is called once per frame
	void Update () {
		if (targetHighlight != null)
		{
			targetHighlight.MouseOver ();
		}
	}
}
