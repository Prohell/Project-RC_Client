using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using GCGame.Table;

public enum TroopState
{
	Null,
	NeedTrain,
	Training,
	TrainFull
}

public class TroopTrainUIMediator : IUIMediator, IMediator {
	LuaTable IUIMediator.m_View { get; set; }
	string IUIMediator.m_UIName { get; set; }

	//建筑类型
	public const int BuildingType = 9;
	//当前建筑槽位
	public int curSlot = -1;
	//当前建筑的位置是同类型建筑的第几个
	public int curSlotIndex = -1;
	//当前建筑的Guid
	public long curBuildingGuid = -1;
	//当前队伍的列表位置
	private int marchIndex = -1;
	//当前英雄的列表位置
	private int heroIndex = -1;

	private PlayerProxy _playerProxy = null;
	private PlayerProxy playerProxy{
		get{ 
			if(_playerProxy == null){
				_playerProxy = GameFacade.GetProxy<PlayerProxy> ();
			}
			return _playerProxy;
		}
	}

	public void OnInit() {
		EventManager.GetInstance ().AddEventListener (EventId.AssignHero, AssignHero);
		EventManager.GetInstance ().AddEventListener (EventId.TroopTrain, TroopTrain);
		EventManager.GetInstance ().AddEventListener (EventId.TroopTrainOver, TroopTrainOver);
	}

	public void OnDestroy() {
	}

	//接收服务器切换英雄回调
	private void AssignHero(object obj) {
		SetCurHeroIndex ();
		EventManager.GetInstance ().SendEvent ("Private_TroopTrainRefreshAll");
	}

	//接收服务器切换开始训练回调
	private void TroopTrain(object obj){
		startTrainReceive = true;
		EventManager.GetInstance ().SendEvent ("Private_TroopTrainRefreshTroops");
	}

	private void TroopTrainOver(object obj){
		GC_TroopTrain_Over data = (GC_TroopTrain_Over)obj;
		//

		EventManager.GetInstance ().SendEvent ("Private_TroopTrainRefreshTroops");
	}


	//通过Slot设置兵营数据位置
	public void SetBarrackData(int slot){
		curSlot = slot;
		//获取当前兵营Slot在所有兵营中的index
		Dictionary<int,List<Tab_CityBuildingSlot>> slotDic = TableManager.GetCityBuildingSlot ();
		int indx = -1;
		foreach(KeyValuePair<int,List<Tab_CityBuildingSlot>> k in slotDic){
			if(k.Value[0].BuildingType == slotDic[slot][0].BuildingType){
				indx++;
				if(k.Key == slot){
					break;
				}
			}
		}
		curSlotIndex = indx;

		//获取当前槽位建筑物的Guid
		for(int i = 0;i < playerProxy.city.buildList.Count;i++){
			BuildingVo data = playerProxy.city.buildList [i];
			if(data.slot == curSlot){
				curBuildingGuid = data.guid;
				break;
			}
		}

		SetCurTroopIndex ();
		SetCurHeroIndex ();

		//更新界面
		EventManager.GetInstance ().SendEvent ("Private_TroopTrainRefreshAll");
	}

	private void SetCurTroopIndex(){
		//获取当前队伍信息位置
		marchIndex = -1;
		for (int i = 0; i < playerProxy.marchList.Count; i++) {
			if(playerProxy.marchList [i].buildId == curBuildingGuid){
				marchIndex = i;
				break;
			}
		}
	}

	private void SetCurHeroIndex(){
		//获取当前英雄信息位置
		heroIndex = -1;
		for (int i = 0; i < playerProxy.heroList.Count; i++) {
			if(playerProxy.heroList [i].marchId == playerProxy.marchList[marchIndex].marchId){
				heroIndex = i;
				break;
			}
		}
	}

	//获取当前英雄头像
	public string GetCurHeroIcon() {
		if(heroIndex != -1){
			HeroVo hero = playerProxy.heroList [heroIndex];
			List<Tab_Hero> heroTempList = TableManager.GetHeroByID (hero.type);
			return heroTempList [0].Portrait;
		}
		return "";
	}

	//获取当前部队 各个单位的头像
	public string GetTroopIcon(int index) {
		MarchVo march = playerProxy.marchList [marchIndex];
		//临时处理
		TroopVo troop = null;
		if (march.troopList.Count <= index) {
			if(addTrainList.Count > index - march.troopList.Count){
				troop = addTrainList[index - march.troopList.Count];
			}
		} else {
			troop = march.troopList [index];
		}
		if(troop != null){
			List<Tab_Troop> troopTempList = TableManager.GetTroopByID (troop.type);
			return troopTempList [0].Portrait;
		}
		return "";
	}

	//获取当前队伍中单位数量
	public int GetCurTroopCount(){
		return playerProxy.marchList [marchIndex].troopList.Count + addTrainList.Count;
	}

	//获取当前队伍中第i个单位数据
	public TroopVo GetCurTroopData(int i){
		return playerProxy.marchList [marchIndex].troopList[i];
	}

	//获取训练信息
	public TrainVo GetCurTrainData(long bID, int qIndex){
		for (int i = 0; i <	playerProxy.city.trainList.Count; i++) {
			if(playerProxy.city.trainList[i].buildId == bID){
				if(playerProxy.city.trainList [i].queueIndex == qIndex){
					return playerProxy.city.trainList [i];
				}
			}
		}
		return null;
	}


	//获取当前队伍某位置的单位的训练状态
	public TroopState GetTroopState(int index) {
		MarchVo march = playerProxy.marchList [marchIndex];
		//临时处理
		TroopVo troop = null;
		if (march.troopList.Count <= index) {
			if (addTrainList.Count > index - march.troopList.Count) {
				troop = addTrainList [index - march.troopList.Count];
			}
		} else {
			troop = march.troopList [index];
		}

		List<Tab_RoleBaseAttr> attrList = TableManager.GetRoleBaseAttrByID(troop.type);
		Tab_RoleBaseAttr roleAttr = attrList [troop.level - 1];
		int totalHP = roleAttr.MaxHP * 18;
		if(troop.health < totalHP){
			for(int i = 0;i < playerProxy.city.trainList.Count;i++){
				if(playerProxy.city.trainList[i].queueIndex == troop.queueIndex){
					if(playerProxy.city.trainList[i].troopType <= 0){
						break;
					}
					//类型为0说明没有训练了 时间到了等于训练结束了
					System.DateTime dt1 = System.Convert.ToDateTime("1970-1-1 00:00:00");
					long now = System.DateTime.Now.ToFileTimeUtc() - dt1.ToFileTimeUtc();
					float a = now/10000000;
					int aa = Mathf.FloorToInt (a);
					//等于0也算是空闲
					if (playerProxy.city.trainList[i].troopType >= 0) {
						if (playerProxy.city.trainList[i].endTime <= aa) {
							return TroopState.Training;//训练中
						}
					}
				}
			}
			return TroopState.NeedTrain;
		}
			
		return TroopState.TrainFull;
	}

	public int GetTroopQueueIndex(int index){
		MarchVo march = playerProxy.marchList [marchIndex];
		//临时处理
		TroopVo troop = null;
		if (march.troopList.Count <= index) {
			if (addTrainList.Count > index - march.troopList.Count) {
				troop = addTrainList [index - march.troopList.Count];
			}
		} else {
			troop = march.troopList [index];
		}
		if(troop == null){
			return -1;
		}

		return troop.queueIndex;
	}

	private List<TroopVo> addTrainList = new List<TroopVo>();
	//临时处理 前端直接处理新增的Troop
	public void AddToBuildSequence(int id) {
		MarchVo march = playerProxy.marchList [marchIndex];
		if(march.troopList.Count + addTrainList.Count < 5){
			TroopVo troop = new TroopVo ();
			troop.type = id;
			troop.health = 0;
			troop.level = 1;
			troop.queueIndex = march.troopList.Count + addTrainList.Count;
			addTrainList.Add (troop);
			//更新造兵队列
			EventManager.GetInstance ().SendEvent ("Private_TroopTrainRefreshTroops");
		}
	}

	//开始训练
	private bool startTrainReceive = true;
	public void StartTrain(){
		if (startTrainReceive) {
			MarchVo march = playerProxy.marchList [marchIndex];
			bool find = false;
			for (int i = 0; i < march.troopList.Count; i++) {
				if (GetTroopState(i) == TroopState.NeedTrain) {
					find = true;
					startTrainReceive = false;
					SendCGTroopTrain (march.troopList [i].queueIndex, curBuildingGuid, march.troopList [i].type, 20);
					break;
				}
			}

			//临时处理 队伍列表里没找到可以训练的  那就看看新添加的列表有没有  
			if(!find){
				if(addTrainList.Count > 0){
					SendCGTroopTrain (addTrainList[0].queueIndex, curBuildingGuid, addTrainList[0].type, 20);
					addTrainList.RemoveAt (0);
				}
			}
		}
	}



	//暂停训练
	public void PauseTrain(){
	
	}

	//删除部队
	public void DeleteTroop(){
	
	}

	//切换英雄
	private bool changeHeroReceive = true;
	public void ChangeHero(){
		if(changeHeroReceive){
			for(int i = 0;i < playerProxy.heroList.Count;i++){
				if(playerProxy.heroList[i].marchId <= 0){
					changeHeroReceive = false;
					SendCGAssignHero (playerProxy.marchList[marchIndex].marchId, playerProxy.heroList[i].guid);
					break;
				}
			}
		}
	}

	//出征
	public void ToBattle(){
        SendSendMarchMsg(playerProxy.marchList[marchIndex].marchId);
	}

	//发送切换英雄协议
	private void SendCGAssignHero(long marchID, long heroID){
		CG_ASSIGN_HERO packet = (CG_ASSIGN_HERO)PacketDistributed.CreatePacket(MessageID.PACKET_CG_ASSIGN_HERO);
		packet.SetMarchid (marchID);
		packet.SetHeroid (heroID);
		packet.SendPacket();
	}

	//发送练兵协议
	private void SendCGTroopTrain(int queue, long buildID, int troopType, int count){
		CG_Troop_Train packet = (CG_Troop_Train)PacketDistributed.CreatePacket(MessageID.PACKET_CG_TROOP_TRAIN);
		packet.SetQueueindex (queue);
		packet.SetBuildid (buildID);
		packet.SetTrooptype (troopType);
		packet.SetCount (count);
		packet.SendPacket();
	}

    public void SendSendMarchMsg(long p_marchID)
    {
        CG_SEND_MARCH march = (CG_SEND_MARCH)PacketDistributed.CreatePacket(MessageID.PACKET_CG_SEND_MARCH);
        march.SetMarchid(p_marchID);
        march.SendPacket();
    }

}
