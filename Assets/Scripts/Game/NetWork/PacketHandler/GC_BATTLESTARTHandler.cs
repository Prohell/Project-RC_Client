//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_BATTLESTARTHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_BattleStart packet = (GC_BattleStart )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            EventManager.GetInstance().SendEvent(EventId.StartBattle, packet);
            //enter your logic
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
