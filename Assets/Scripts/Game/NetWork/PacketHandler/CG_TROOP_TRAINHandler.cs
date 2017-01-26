//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class CG_TROOP_TRAINHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 CG_Troop_Train packet = (CG_Troop_Train )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
