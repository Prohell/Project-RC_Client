//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class CG_BATTLEINFORHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 CG_BATTLEINFOR packet = (CG_BATTLEINFOR )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
