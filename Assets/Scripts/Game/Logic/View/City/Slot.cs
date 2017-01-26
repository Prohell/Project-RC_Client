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

	void Awake(){
		if(levelTxt == null){
			levelTxt = transform.FindChild ("Level").GetComponent<TextMesh>();
		}
		if(nameTxt == null){
			nameTxt = transform.FindChild ("Name").GetComponent<TextMesh>();
		}
	}

	public void ShowBuildingName(){
		nameTxt.text = slotName;
		nameTxt.gameObject.SetActive (true);
	}

	public void HideBuildingName(){
		nameTxt.gameObject.SetActive (false);
	}

	private int _level = 0;
	public int level{
		get{ 
			return _level;
		}
		set{ 
			_level = value;
			levelTxt.text = "等级 " + _level.ToString();
		}
	}


	public void ShowBuildingLevel(){
		levelTxt.text = "等级 " + level.ToString();
		levelTxt.gameObject.SetActive (true);
	}

	public void HideBuildingLevel(){
		levelTxt.gameObject.SetActive (false);
	}
}
