//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class CG_ROBOT_OPENHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 CG_ROBOT_OPEN packet = (CG_ROBOT_OPEN )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
