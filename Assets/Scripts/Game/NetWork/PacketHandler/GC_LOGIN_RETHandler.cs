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

            UnityEngine.Debug.Log("packet.Result : " + packet.Result);
            switch (packet.Result)
            {
                case 1:
                    SetProxy(packet);
                    EventManager.GetInstance().SendEvent(EventId.PlayerProxyUpdate, null);
                    break;
                case 3:
                    LogModule.ErrorLog("壮哥，3了！");
                    break;
                case 8:
                    CG_CREATEROLE p = (CG_CREATEROLE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_CREATEROLE);
                    Random ran = new Random();
                    int RandKey = ran.Next(1, 999);
                    p.SetName("TestRole" + RandKey);
                    p.SetGender(0);
                    p.SendPacket();
                    break;
            }

            return (uint)PACKET_EXE.PACKET_EXE_CONTINUE;
        }

        private void SetProxy(GC_LOGIN_RET packet)
        {
            PlayerProxy proxy = GameFacade.GetProxy<PlayerProxy>();
            proxy.userid = packet.Userid;
            proxy.oid = packet.Oid;
            proxy.accesstoken = packet.Accesstoken;
            proxy.playername = packet.Playername;
            proxy.level = packet.Level;

            //城市数据
            proxy.city.cityId = packet.City.CityId;
            proxy.city.tileId = packet.City.TileId;
            proxy.city.level = packet.City.Level;
            proxy.city.food = packet.City.Food;
            proxy.city.stone = packet.City.Stone;
            proxy.city.iron = packet.City.Iron;
            proxy.city.WorldPos = new Coord(packet.City.Posx, packet.City.Posz);

            proxy.city.buildList.Clear();
            for (int i = 0; i < packet.City.buildlistCount; i++)
            {
                GC_BuildingData data = packet.City.GetBuildlist(i);
                BuildingVo vo = new BuildingVo();
                PlayerProxy.SetBuildVo(data, vo);
                proxy.city.buildList.Add(vo);
            }

            proxy.city.trainList.Clear();
            for (int i = 0; i < packet.City.trainListCount; i++)
            {
                GC_TrainData data = packet.City.GetTrainList(i);
                TrainVo vo = new TrainVo();
                PlayerProxy.SetTrainVo(data, vo);
                proxy.city.trainList.Add(vo);
            }

            //英雄数据
            proxy.heroList.Clear();
            for (int i = 0; i < packet.HeroList.heroListCount; i++)
            {
                GC_HeroData data = packet.HeroList.GetHeroList(i);
                HeroVo vo = new HeroVo();
                PlayerProxy.SetHeroVo(data, vo);
                proxy.heroList.Add(vo);
            }

            //队伍数据
            proxy.marchList.Clear();
            for (int i = 0; i < packet.Marchlist.marchlistCount; i++)
            {
                GC_MarchData data = packet.Marchlist.GetMarchlist(i);
                MarchVo vo = new MarchVo();
                PlayerProxy.SetMarchVo(data, vo);
                proxy.marchList.Add(vo);
            }
        }


    }


}
