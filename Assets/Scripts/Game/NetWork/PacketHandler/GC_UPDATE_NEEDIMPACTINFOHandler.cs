//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_UPDATE_NEEDIMPACTINFOHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_UPDATE_NEEDIMPACTINFO packet = (GC_UPDATE_NEEDIMPACTINFO )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }