//This code create by CodeEngine

using System;
using Google.ProtocolBuffers;
using System.Collections;
namespace SPacket.SocketInstance
{
    public class GC_FIGHTHandler : Ipacket
    {
        public uint Execute(PacketDistributed ipacket)
        {
            GC_FIGHT packet = (GC_FIGHT)ipacket;
            if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            BattleProxy proxy = GameFacade.GetProxy<BattleProxy>();
            BattleProxy.SetFightInfor(proxy.FightInfor, packet);
            EventManager.GetInstance().SendEvent(EventId.ReceiveFight, packet);
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
        }
    }
}
