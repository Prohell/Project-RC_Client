using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

public class BattleProxy : IProxy {
    public GC_BATTLEINFOR BattleInfor { get; set; }
    public GC_OBJCOMMANDPURSUE PursueInfor { get; set; }
    public GC_OBJPREPAREFORATTACK PrepareForAttackInfor { get; set; }
    public GC_OBJGETHURT ObjectGetHurt { get; set; }
    public GC_OBJPOSLIST CurrentObjPos { get; set; }
    public void OnDestroy()
    {
    }

    public void OnInit()
    {

    }
    public void GetBattlerInfor(int tSceneID)
    {
        CG_BATTLEINFOR battlerInforPacket = (CG_BATTLEINFOR)PacketDistributed.CreatePacket(MessageID.PACKET_CG_BATTLEINFOR);
        battlerInforPacket.SetSceneId(tSceneID);
        battlerInforPacket.SendPacket();
    }
    public void GetPosList(int tSceneID)
    {
        CG_OBJPOSLIST objListPacket = (CG_OBJPOSLIST)PacketDistributed.CreatePacket(MessageID.PACKET_GC_OBJPOSLIST);
        objListPacket.SetSceneId(tSceneID);
        objListPacket.SendPacket();
    }
}
