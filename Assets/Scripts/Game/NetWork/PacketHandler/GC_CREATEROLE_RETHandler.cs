//This code create by CodeEngine
using System;
using Google.ProtocolBuffers;
using System.Collections;

namespace SPacket.SocketInstance
 {
	 public class GC_CREATEROLE_RETHandler : Ipacket
	 {
		 public uint Execute(PacketDistributed ipacket)
		 {
		 	GC_CREATEROLE_RET packet = (GC_CREATEROLE_RET)ipacket;
		 	if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			 //enter your logic
			EventManager.GetInstance().SendEvent(EventId.CreateRole,null);
		 	return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
	 	}
	 }
 }
