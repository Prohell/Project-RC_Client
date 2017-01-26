//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
	 public class GC_ASSIGN_HEROHandler : Ipacket
	 {
		 public uint Execute(PacketDistributed ipacket)
		 {
			 GC_ASSIGN_HERO packet = (GC_ASSIGN_HERO )ipacket;
			 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			 //enter your logic
			long marchID = 0; 
			if (packet.Ret == 0) {
				PlayerProxy playerProxy = GameFacade.GetProxy<PlayerProxy> ();
				for (int i = 0; i < playerProxy.heroList.Count; i++) {
					var data = playerProxy.heroList [i];
					if (data.guid == packet.HeroId) {
						marchID = packet.Marchid;
						data.marchId = packet.Marchid;
					} else if (data.marchId == packet.Marchid) {
						data.marchId = 0;
					}
				}

				EventManager.GetInstance ().SendEvent (EventId.AssignHero, marchID);
			} else {
				UnityEngine.Debug.Log ("数据包报错了！！！");
			}
			 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		 }
	 }
 }
