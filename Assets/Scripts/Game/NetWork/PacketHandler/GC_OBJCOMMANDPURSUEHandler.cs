//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_OBJCOMMANDPURSUEHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_OBJCOMMANDPURSUE packet = (GC_OBJCOMMANDPURSUE )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            proxy.PursueInfor = packet;
            EventManager.GetInstance().SendEvent(EventId.ReceiveCommandPursue, packet);
 //enter your logic
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
