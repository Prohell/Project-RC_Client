//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_DELETE_OBJHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_DELETE_OBJ packet = (GC_DELETE_OBJ )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
