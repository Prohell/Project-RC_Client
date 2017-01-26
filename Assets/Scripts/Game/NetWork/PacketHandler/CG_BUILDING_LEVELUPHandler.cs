//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
	public class CG_BUILDING_LEVELUPHandler : Ipacket
	{
		public uint Execute(PacketDistributed ipacket)
		{
			CG_Building_LevelUp packet = (CG_Building_LevelUp )ipacket;
			if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			//enter your logic


			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		}
	}
}
