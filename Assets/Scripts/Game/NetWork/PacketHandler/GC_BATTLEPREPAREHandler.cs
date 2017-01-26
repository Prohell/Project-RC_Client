//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_BATTLEPREPAREHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_BATTLEPREPARE packet = (GC_BATTLEPREPARE )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
