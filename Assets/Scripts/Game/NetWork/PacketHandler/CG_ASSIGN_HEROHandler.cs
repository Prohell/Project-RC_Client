using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
	public class CG_ASSIGN_HEROHandler : Ipacket
	{
		public uint Execute(PacketDistributed ipacket)
		{
			CG_ASSIGN_HERO packet = (CG_ASSIGN_HERO )ipacket;
			if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			//enter your logic
			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		}
	}
}
