//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_ATTACKFLYHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_ATTACKFLY packet = (GC_ATTACKFLY )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }