using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkTest : MonoBehaviour {

	public Dropdown dropdown;

	public Text IP;
	public Text Port;
	public Text Connect;
	public Text Account;

	public void ConnectToServer(){
		if(Connect.text == "Connect"){
			
			NetManager.GetInstance().ConnectToServer(IP.text, int.Parse(Port.text), ConnectHandle);
		}else if(Connect.text == "Diconnect"){
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

	public void Send(){
		switch(dropdown.value){
		case 0:
			SendCGLogin ();
			break;
		case 1:
			SendCGCreateRole ();
			break;
		}
	}

	//发送登录请求
	public void SendCGLogin(){
		CG_LOGIN packet = (CG_LOGIN)PacketDistributed.CreatePacket(MessageID.PACKET_CG_LOGIN);
		packet.SetAccount ("TestAccount2");
		packet.SetMaxpacketid ((int)MessageID.PACKET_SIZE);
		packet.SendPacket();
	}

	//发送创建角色请求
	public void SendCGCreateRole(){
		CG_CREATEROLE packet = (CG_CREATEROLE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_CREATEROLE);
		packet.SetName (Account.text);
		packet.SetGender (1);
		packet.SendPacket();
	}


	void OnApplicationQuit(){
		NetManager.GetInstance().DisconnectServer();
	}
}
