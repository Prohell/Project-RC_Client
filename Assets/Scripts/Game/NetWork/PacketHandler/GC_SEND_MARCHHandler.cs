//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_SEND_MARCHHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_SEND_MARCH packet = (GC_SEND_MARCH )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
