//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
	public class GC_TROOP_TRAINHandler : Ipacket
	{
		public uint Execute(PacketDistributed ipacket)
		{
			GC_Troop_Train packet = (GC_Troop_Train )ipacket;
			if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			//enter your logic
			if(packet.Ret == 0){
				PlayerProxy playerProxy = GameFacade.GetProxy<PlayerProxy> ();

				bool find = false;
				for(int i = 0;i < playerProxy.city.trainList.Count;i++){
					if(playerProxy.city.trainList[i].queueid == packet.Data.Queueid){
						find = true;
						PlayerProxy.SetTrainVo (packet.Data, playerProxy.city.trainList [i]);
					}
				}

				if(!find){
					TrainVo train = new TrainVo ();
					PlayerProxy.SetTrainVo (packet.Data, train);
					playerProxy.city.trainList.Add (train);
				}
					
				find = false;
				MarchVo march = null;
				for (int i = 0; i < playerProxy.marchList.Count; i++) {
					if (playerProxy.marchList [i].buildId == packet.Data.Buildid) {
						march = playerProxy.marchList [i];
						for (int j = 0; j < playerProxy.marchList [i].troopList.Count; j++) {
							TroopVo vo = playerProxy.marchList [i].troopList [j];
							if(vo.queueIndex == packet.Data.Queueindex){
								find = true;
							}
						}
					}
				}
				if(!find){
					TroopVo troop = new TroopVo ();
					troop.type = packet.Data.Trooptype;
					troop.health = 0;
					troop.level = 1;
					troop.queueIndex = packet.Data.Queueindex;
					march.troopList.Add (troop);
				}

				EventManager.GetInstance ().SendEvent (EventId.TroopTrain, null);
			} else {
				UnityEngine.Debug.Log ("数据包报错了！！！");
			}

			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		}
	}
}
