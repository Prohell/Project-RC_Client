using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NetworkTest : MonoBehaviour {

	public Dropdown dropdown;

	public Text IP;
	public Text Port;
	public Text Connect;

	public GameObject desObj;
	public GameObject valuesObj;

	public List<Text> descriptions = new List<Text>();
	public List<InputField> values = new List<InputField>();

	void Awake(){
		for(int i = 0;i < desObj.transform.childCount;i++){
			Transform trans = desObj.transform.FindChild ("Param" + (i + 1));
			descriptions.Add (trans.GetComponent<Text>());
		}

		for(int i = 0;i < valuesObj.transform.childCount;i++){
			Transform trans = valuesObj.transform.FindChild ("Value" + (i + 1));
			values.Add (trans.GetComponent<InputField>());
		}

		EventManager.GetInstance ().AddEventListener (EventId.CreateRole, CreateRole);

	}

	private void CreateRole(object obj){
		SendCGLogin ();
	}

	public void ConnectToServer(){
		if(Connect.text == "Connect") {
			NetManager.GetInstance().ConnectToServer(IP.text, int.Parse(Port.text), ConnectHandle);
		}else if(Connect.text == "Diconnect") {
			NetManager.GetInstance().DisconnectServer();
			Connect.text = "Connect";
		}
	}

	void ConnectHandle(bool success,string result){
		if (success) {
			Debug.Log ("Connect Success");
			Connect.text = "Diconnect";
		} else {
			Debug.LogWarning ("Connect Failed");
		}
	}

	private void InitParams(){
		for (int i = 0; i < descriptions.Count; i++) {
			descriptions [i].text = "Param" + (i + 1);
		}

		for (int i = 0; i < values.Count; i++) {
			values [i].text = "";
		}
	}

	public void ShowParams(int index){
		for (int i = index; i < descriptions.Count; i++) {
			descriptions [i].gameObject.SetActive (true);
			values [i].gameObject.SetActive (true);
		}
	}

	public void HideParams(int index){
		for (int i = index; i < descriptions.Count; i++) {
			descriptions [i].gameObject.SetActive (false);
			values [i].gameObject.SetActive (false);
		}
	}

	public void PacketChange(){
		InitParams ();
		ShowParams (0);
		switch(dropdown.value){
		case 1:
			descriptions [0].text = "Account";
			HideParams (1);
			break;
		case 2:
			descriptions[0].text = "Name";
			descriptions[1].text = "Gender";
			HideParams (2);
			break;
		case 3:
			descriptions[0].text = "BuildingID";
			HideParams (1);
			break;
		case 4:
			descriptions[0].text = "MarchID";
			descriptions[1].text = "HeroID";
			HideParams (2);
			break;
		case 5:
			descriptions[0].text = "Queueindex";
			descriptions[1].text = "BuildID";
			descriptions[2].text = "TroopType";
			descriptions[3].text = "Count";
			HideParams (4);
			break;
		}
	}

	public void Send(){
		switch(dropdown.value){
		case 1:
			SendCGLogin ();
			break;
		case 2:
			SendCGCreateRole ();
			break;
		case 3:
			SendCGBuildingLevelUp ();
			break;
		case 4:
			SendCGAssignHero ();
			break;
		case 5:
			SendCGTroopTrain ();
			break;
		}
	}

	//发送登录请求
	public void SendCGLogin(){
		CG_LOGIN packet = (CG_LOGIN)PacketDistributed.CreatePacket(MessageID.PACKET_CG_LOGIN);
		packet.SetAccount (values [0].text);
		packet.SetMaxpacketid ((int)MessageID.PACKET_SIZE);
		packet.SendPacket();
	}

	//发送创建角色请求
	public void SendCGCreateRole(){
		CG_CREATEROLE packet = (CG_CREATEROLE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_CREATEROLE);
		packet.SetName (values [0].text);
		packet.SetGender (int.Parse(values [1].text));
		packet.SendPacket();
	}

	public void SendCGBuildingLevelUp(){
		CG_Building_LevelUp packet = (CG_Building_LevelUp)PacketDistributed.CreatePacket(MessageID.PACKET_CG_BUILDING_LEVELUP);
		packet.SetBuildingID (long.Parse(values [0].text));
		packet.SendPacket();
	}

	public void SendCGAssignHero(){
		CG_ASSIGN_HERO packet = (CG_ASSIGN_HERO)PacketDistributed.CreatePacket(MessageID.PACKET_CG_ASSIGN_HERO);
		packet.SetMarchid (long.Parse(values [0].text));
		packet.SetHeroid (long.Parse(values [1].text));
		packet.SendPacket();
	}

	public void SendCGTroopTrain(){
		CG_Troop_Train packet = (CG_Troop_Train)PacketDistributed.CreatePacket(MessageID.PACKET_CG_TROOP_TRAIN);
		packet.SetQueueindex (int.Parse(values [0].text));
		packet.SetBuildid (long.Parse(values [1].text));
		packet.SetTrooptype (int.Parse(values [2].text));
		packet.SetCount (int.Parse(values [3].text));
		packet.SendPacket();
	}

	void OnApplicationQuit(){
		NetManager.GetInstance().DisconnectServer();
	}
}
