//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_BATTLEENDHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_BATTLEEND packet = (GC_BATTLEEND )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetBattleEnd(proxy.BattleEnd, packet);
            EventManager.GetInstance().SendEvent(EventId.ReceiveBattleEnd, packet);
            //enter your logic
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
