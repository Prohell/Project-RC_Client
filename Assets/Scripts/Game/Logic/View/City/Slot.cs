using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {
	public int slotType;
	public string slotName;
	public GameObject modelChild;

	public Vector3 modelOffset = Vector3.zero;
	public Vector3 modleEuler = Vector3.zero;
	public Vector3 cameraOffset = Vector3.zero;

	private TextMesh nameTxt;
	private TextMesh levelTxt;

	public void ShowBuildingName(){
		if(nameTxt == null){
			nameTxt = transform.FindChild ("Name").GetComponent<TextMesh>();
		}
		nameTxt.text = slotName;
		nameTxt.gameObject.SetActive (true);
	}

	public void HideBuildingName(){
		if(nameTxt == null){
			nameTxt = transform.FindChild ("Name").GetComponent<TextMesh>();
		}
		nameTxt.gameObject.SetActive (false);
	}


	public int level = 0;
	public void ShowBuildingLevel(){
		if(levelTxt == null){
			levelTxt = transform.FindChild ("Level").GetComponent<TextMesh>();
		}
		levelTxt.text = "等级 " + level.ToString();
		levelTxt.gameObject.SetActive (true);
	}

	public void HideBuildingLevel(){
		if(levelTxt == null){
			levelTxt = transform.FindChild ("Level").GetComponent<TextMesh>();
		}
		levelTxt.gameObject.SetActive (false);
	}
}
