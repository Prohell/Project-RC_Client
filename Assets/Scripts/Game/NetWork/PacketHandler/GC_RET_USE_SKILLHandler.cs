//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_RET_USE_SKILLHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_RET_USE_SKILL packet = (GC_RET_USE_SKILL )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetUseSkill(proxy.UseSkill, packet);
            EventManager.GetInstance().SendEvent(EventId.ReceiveSkill, packet);
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
