//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_MARCH_MOVEHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_MARCH_MOVE packet = (GC_MARCH_MOVE )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
