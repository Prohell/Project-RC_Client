//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_OBJGETHURTHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_OBJGETHURT packet = (GC_OBJGETHURT )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            proxy.ObjectGetHurt = packet;
            EventManager.GetInstance().SendEvent(EventId.ReceiveObjGetHurt, packet);
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
