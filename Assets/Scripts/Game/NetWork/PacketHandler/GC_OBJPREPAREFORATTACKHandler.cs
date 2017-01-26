//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_OBJPREPAREFORATTACKHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_OBJPREPAREFORATTACK packet = (GC_OBJPREPAREFORATTACK )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetPrepareForAttackInfor(proxy.PrepareForAttackInfor,packet);
            EventManager.GetInstance().SendEvent(EventId.ReceivePrepareForAttack, packet);
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
