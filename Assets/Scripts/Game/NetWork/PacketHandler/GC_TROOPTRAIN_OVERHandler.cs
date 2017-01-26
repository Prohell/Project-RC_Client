//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
using System.Collections.Generic;

namespace SPacket.SocketInstance
{
	public class GC_TROOPTRAIN_OVERHandler : Ipacket
	{
		public uint Execute(PacketDistributed ipacket)
		{
			GC_TroopTrain_Over packet = (GC_TroopTrain_Over )ipacket;
			if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			//enter your logic
			if(packet.Ret == 0){
				int queueIndex = -1;
				PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();
				for(int i = 0;i < proxy.city.trainList.Count;i++){
					if(proxy.city.trainList[i].queueid == packet.Queueid){
						queueIndex = proxy.city.trainList [i].queueIndex;
						proxy.city.trainList.RemoveAt (i);
					}
				}
				//设置队伍中的单位信息
				for (int i = 0; i < proxy.marchList.Count; i++) {
					if(proxy.marchList[i].buildId == packet.Buildid){
						List<TroopVo> list = proxy.marchList [i].troopList;
						for (int j = 0; j < list.Count;j++) {
							if(list[j].queueIndex == queueIndex){
								list [j].health = packet.Hp;
							}
						}
					}
				}
				EventManager.GetInstance ().SendEvent (EventId.TroopTrainOver, null);
			} else {
				UnityEngine.Debug.Log ("数据包报错了！！！");
			}


			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		}
	}
}
