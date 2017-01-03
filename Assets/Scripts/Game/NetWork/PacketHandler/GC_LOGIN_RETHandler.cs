//This code create by CodeEngine
using System;
using Google.ProtocolBuffers;
using System.Collections;

namespace SPacket.SocketInstance
{
	 public class GC_LOGIN_RETHandler : Ipacket
	 {
		 public uint Execute(PacketDistributed ipacket)
		 {
			GC_LOGIN_RET packet = (GC_LOGIN_RET)ipacket;
			 if (null == packet) return (uint)PACKET_EXE.PACKET_EXE_ERROR;
			 //enter your logic
			PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy> ();
			switch(packet.Result){
				case 1:
					proxy.userid = packet.Userid;
					proxy.oid = packet.Oid;
					proxy.accesstoken = packet.Accesstoken;
					proxy.city = packet.City;
					proxy.playername = packet.Playername;
					proxy.level = packet.Level;
					proxy.heroList = packet.HeroList;
					proxy.marchlist = packet.Marchlist;
					EventManager.GetInstance().SendEvent(EventId.PlayerProxyUpdate, null);
					break;
				default:
					CG_CREATEROLE p = (CG_CREATEROLE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_CREATEROLE);
                    Random ran = new Random();
                    int RandKey = ran.Next(1, 999);
					p.SetName("TestRole"+ RandKey);
					p.SetGender(1);
					p.SendPacket();
					break;
			}

			return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
		 }
	 }
 }
