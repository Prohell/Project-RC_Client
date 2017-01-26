//----------------------------------------------
//	CreateTime  : 1/16/2017 5:32:06 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : WorldProxy
//	ChangeLog   : None
//----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldProxy : IProxy
{
    public const int WorldSceneID = 0;

    public class CFightInfor
    {
        public int ret; // 战斗的返回值 0成功 1失败
        public long marchId;    // marchId
        public int sceneId; // 场景ID
        public int sceneclass; // 场景类型
    }
    public CFightInfor FightInfor { get; private set; }

    public void SetFightInfo(GC_FIGHT gcFightInfor)
    {
        FightInfor.marchId = gcFightInfor.MarchId;
        FightInfor.ret = gcFightInfor.Ret;
        FightInfor.sceneclass = gcFightInfor.Sceneclass;
        FightInfor.sceneId = gcFightInfor.SceneId;
    }

    public class CityInfo
    {
        public bool IsMine = false;
        public Coord Position;

        public CityVo CityData;

        public GameObject CityObject;
        public CityController CityController;
    }

    public CityInfo MyCity { get; private set; }

    public List<CityInfo> OtherCityList
    {
        get { return m_otherCityList; }
        private set { m_otherCityList = value; }
    }
    private List<CityInfo> m_otherCityList = new List<CityInfo>();

    public const int MarchListNum = 4;

    public class MarchInfo
    {
        public string Name;
        public string State;
        public TimeHelper.ClockTime Time;
        public Coord Position;

        public MarchVo MarchData = new MarchVo();

        public GameObject MarchObject;
        public MarchMoveController MarchMoveController;
        public MarchController MarchController;
        public MarchAnimController MarchAnimController;

        public bool IsMoving { get; set; }
        public bool IsOut { get; set; }

        public override string ToString()
        {
            return string.Format("[MarchInfo]Name:{0}, Position:{1}", Name, Position);
        }
    }

    public List<MarchInfo> MyMarchList
    {
        get { return m_myMarchList; }
        private set { m_myMarchList = value; }
    }

    public List<MarchInfo> MyOutMarchList
    {
        get { return m_myMarchList.Where(item => item.IsOut).ToList(); }
    }

    public List<MarchInfo> OtherMarchList
    {
        get { return m_otherMarchList; }
        private set { m_otherMarchList = value; }
    }

    private List<MarchInfo> m_myMarchList = new List<MarchInfo>();
    private List<MarchInfo> m_otherMarchList = new List<MarchInfo>();

    public void SelectMarch(int index)
    {
        if (index < MyOutMarchList.Count)
        {
            m_SelectedMyItem.SelectedMyMarch = MyOutMarchList[index];
        }
    }

    public class SelectedMyItem
    {
        public enum SelectedState
        {
            Null,
            MyMarch,
            MyCity,
        }
        public SelectedState m_SelectedState { get; private set; }

        public MarchInfo SelectedMyMarch
        {
            get { return m_selectedMyMarch; }
            set
            {
                m_selectedMyMarch = value;
                m_SelectedState = SelectedState.MyMarch;
                m_selectedMyCity = null;
            }
        }
        private MarchInfo m_selectedMyMarch;

        public CityInfo SelectedMyCity
        {
            get { return m_selectedMyCity; }
            set
            {
                m_selectedMyCity = value;
                m_SelectedState = SelectedState.MyCity;
                m_selectedMyMarch = null;
            }
        }
        private CityInfo m_selectedMyCity;
    }
    public SelectedMyItem m_SelectedMyItem = new SelectedMyItem();

    public class SelectedOtherItem
    {
        public enum SelectedState
        {
            Null,
            Block,
            OtherMarch,
        }

        public SelectedState m_SelectedState { get; private set; }

        public MapTileVO SelectedBlock
        {
            get { return m_selectedBlock; }
            set
            {
                m_selectedBlock = value;
                m_SelectedState = SelectedState.Block;
                m_selectOtherMarch = null;
            }
        }
        private MapTileVO m_selectedBlock;

        public MarchInfo SelectOtherMarch
        {
            get { return m_selectOtherMarch; }
            set
            {
                m_selectOtherMarch = value;
                m_SelectedState = SelectedState.OtherMarch;
                m_selectedBlock = null;
            }
        }
        private MarchInfo m_selectOtherMarch;
    }

    public SelectedOtherItem m_SelectedOtherItem = new SelectedOtherItem();

    public void InitMarchData()
    {
        for (int i = 0; i < GameFacade.GetProxy<PlayerProxy>().marchList.Count; i++)
        {
            MyMarchList.Add(new MarchInfo() { Name = "MyMarch" + i, State = "采集中", Time = new TimeHelper.ClockTime(29336), Position = new Coord(10 + i, 10 + i), MarchData = GameFacade.GetProxy<PlayerProxy>().marchList[i], IsOut = false });
        }
        //Set temp data.
        //for (int i = 0; i < 1; i++)
        //{
        //    OtherMarchList.Add(new MarchInfo() { Name = "TestOtherMarch" + i, Position = new Coord(5 + i, 8 + i) });
        //}

        SendQuestAllMarch();
    }

    public void InitCityData()
    {
        MyCity = new CityInfo()
        {
            IsMine = true,
            CityData = ProxyManager.GetInstance().Get<PlayerProxy>().city,
            Position = ProxyManager.GetInstance().Get<PlayerProxy>().city.WorldPos
        };

        //Set temp data.
        //for (int i = 0; i < 1; i++)
        //{
        //    OtherCityList.Add(new CityInfo()
        //    {
        //        IsMine = false,
        //        Position = new Coord(5 + i, 7 + i)
        //    });
        //}

        EventManager.GetInstance().SendEvent(EventId.WorldCityDataUpdate);
    }

    public void SendMarchMoveMsg(long p_id, Coord p_co)
    {
        CG_MARCH_MOVE packet = (CG_MARCH_MOVE)PacketDistributed.CreatePacket(MessageID.PACKET_CG_MARCH_MOVE);
        packet.SetMarchid(p_id);
        packet.SetPosx(p_co.x);
        packet.SetPosz(p_co.y);
        packet.SendPacket();
    }

    public void ReceiveMarchMoveMsg()
    {

    }

    public void SendSendMarchMsg(MarchInfo p_march)
    {
        CG_SEND_MARCH march = (CG_SEND_MARCH)PacketDistributed.CreatePacket(MessageID.PACKET_CG_SEND_MARCH);
        march.SetMarchid(p_march.MarchData.marchId);
        march.SendPacket();
    }

    public void ReceiveSendMarchMsg(object para)
    {
        var packet = (GC_SEND_MARCH)para;
        var marchs = MyMarchList.Where(item => item.MarchData.marchId == packet.MarchId).ToList();
        if (marchs.Any())
        {
            marchs.First().IsOut = true;
            EventManager.GetInstance().SendEvent(EventId.WorldMarchDataUpdate);
        }
    }

    public void ReceiveUpdateMarchMsg(object para)
    {
        GC_Update_March packet = (GC_Update_March)para;

        var marchs = MyMarchList.Concat(OtherMarchList).Where(item => item.MarchData.marchId == packet.Data.Marchid).ToList();
        if (marchs.Any())
        {
            PlayerProxy.SetMarchVo(packet.Data, marchs.First().MarchData);
        }
    }

    public void SendQuestAllMarch()
    {
        CG_REQ_NEAR_LIST packet = PacketDistributed.CreatePacket(MessageID.PACKET_CG_REQ_NEAR_LIST) as CG_REQ_NEAR_LIST;
        packet.SetSceneId(WorldSceneID);
        packet.SetBlockid(0);
        packet.SendPacket();
    }

    public void ReceiveQuestAllMarch(object para)
    {
        GC_NEAR_MARCHLIST packet = para as GC_NEAR_MARCHLIST;

        OtherMarchList.Clear();

        for (int i = 0; i < packet.GuidList.Count; i++)
        {
            var marchs = MyMarchList.Where(item => item.MarchData.marchId == packet.GuidList[i]).ToList();
            if (marchs.Any())
            {
                if (!marchs.First().IsOut)
                {
                    marchs.First().IsOut = true;
                    marchs.First().Position = new Coord(packet.posXList[i] / 100, packet.posZList[i] / 100);
                }
            }
            else
            {
                OtherMarchList.Add(new MarchInfo() { Name = "TestOtherMarch" + i, Position = new Coord(packet.posXList[i] / 100, packet.posZList[i] / 100) });
            }
        }

        EventManager.GetInstance().SendEvent(EventId.WorldMarchDataUpdate);
    }

    public void SendFightMsg()
    {
        LogModule.DebugLog("Send Get Fight Infor!");
        CG_FIGHT fight = (CG_FIGHT)PacketDistributed.CreatePacket(MessageID.PACKET_CG_FIGHT);
        fight.SetType(1);
        fight.SetAttackId(m_SelectedMyItem.SelectedMyMarch.MarchData.marchId);
        fight.SetDefenceId(m_SelectedOtherItem.SelectOtherMarch.MarchData.marchId);
        fight.SetSceneId(WorldSceneID);
        fight.SendPacket();
    }

    public void ReceiveFightMsg(object para)
    {
        SetFightInfo((GC_FIGHT)para);
        MySceneManager.GetInstance().SwitchToScene(SceneId.BattleTest);
    }

    public void OnDestroy()
    {

    }

    public void OnInit()
    {
        EventManager.GetInstance().AddEventListener(EventId.ReceiveFight, ReceiveFightMsg);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveSendMarchMsg, ReceiveSendMarchMsg);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveUpdateMarchMsg, ReceiveUpdateMarchMsg);
        EventManager.GetInstance().AddEventListener(EventId.ReceiveAllMarchMsg, ReceiveQuestAllMarch);

        var temp = WorldUnitTest.Instance;
    }
}
