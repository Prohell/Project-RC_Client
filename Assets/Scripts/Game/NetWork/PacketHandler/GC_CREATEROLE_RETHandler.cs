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
			//临时处理:创建角色后 再次发送Login协议
			CG_LOGIN p = (CG_LOGIN)PacketDistributed.CreatePacket(MessageID.PACKET_CG_LOGIN);
			p.SetAccount ("TestAccount1");
			p.SendPacket();
		 	return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
	 	}
	 }
 }
