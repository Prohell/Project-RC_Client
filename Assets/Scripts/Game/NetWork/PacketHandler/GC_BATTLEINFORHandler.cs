//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_BATTLEINFORHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_BATTLEINFOR packet = (GC_BATTLEINFOR )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetBattleInfor(proxy.BattleInfor, packet);
            EventManager.GetInstance().SendEvent(EventId.ReceiveBattleInfor, packet);
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
