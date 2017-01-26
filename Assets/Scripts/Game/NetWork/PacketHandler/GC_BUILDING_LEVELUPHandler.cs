//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
	public class GC_BUILDING_LEVELUPHandler : Ipacket
	{
		public uint Execute(PacketDistributed ipacket)
		{
			GC_Building_LevelUp packet = (GC_Building_LevelUp )ipacket;
			if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			//enter your logic
			if(packet.Ret == 0){
				PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();

				for(int i = 0;i < proxy.city.buildList.Count;i++){
					BuildingVo buildingData = proxy.city.buildList[i];
					if(buildingData.guid == packet.BuildingID){
						buildingData.level = packet.Level;
						EventManager.GetInstance ().SendEvent (EventId.BuildingLevelUp, buildingData.guid);
						break;
					}
				}
			}
			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		}
	}
}
