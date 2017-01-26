//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_OBJPOSLISTHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_OBJPOSLIST packet = (GC_OBJPOSLIST )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetCurrentObjPos(proxy.CurrentObjPos, packet);
            EventManager.GetInstance().SendEvent(EventId.ReceiveSquadPosInfor, packet);            
 return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
