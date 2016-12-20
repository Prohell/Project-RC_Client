//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class CG_OBJPOSLISTHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 CG_OBJPOSLIST packet = (CG_OBJPOSLIST )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
