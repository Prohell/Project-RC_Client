//This code create by CodeEngine

using System;
 using Google.ProtocolBuffers;
 using System.Collections;
namespace SPacket.SocketInstance
 {
 public class GC_UPDATE_MARCHHandler : Ipacket
 {
 public uint Execute(PacketDistributed ipacket)
 {
 GC_Update_March packet = (GC_Update_March )ipacket;
 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
            //enter your logic
            PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy>();
            for (int i = 0; i < proxy.marchlist.marchlistList.Count; i++)
            {
                if(proxy.marchlist.marchlistList[i].Marchid == packet.Data.Marchid)
                {
                    proxy.marchlist.marchlistList[i] = packet.Data;
                }
            }
            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
 }
 }
 }
