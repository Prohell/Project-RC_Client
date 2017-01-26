using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
public class CBattleInfor {
    public int sceneId;            //场景ID
    public List<CObjInfor> objList = new List<CObjInfor>(); //场景OBJ列表
    public int camp;               // 阵营
    public int currentState;	    //当前战场状态
}
public class CPursueInfor
{
    public int sceneId; //场景ID
    public int objId;       //战斗场景中OBJID
    public int aimObjId;	//目标OBJID
}
public class CPrepareForAttackInfor
{
    public int sceneId; //场景ID
    public int objId;       //战斗场景中OBJID
    public int aimObjId;	//目标OBJID
}
public class CObjectGetHurt
{
    public int sceneId;        // 场景ID
    public int objId;          //战斗场景中OBJID
    public int attackObjId;    //伤害发起者ID
    public int damage;         //造成的伤害
    public int objDead;        //改OBJ是否死亡
    public int deathNumber;	//死亡人数
}
public class CCurrentObjPos
{
    public int sceneId;             //场景ID
    public List<CObjPos>objPosList=new List<CObjPos>();	//场景OBJ位置信息列表
}
public class CFightInfor
{
    public int ret; // 战斗的返回值 0成功 1失败
    public long marchId;    // marchId
    public int sceneId; // 场景ID
    public int sceneclass ; // 场景类型
}
public class CUseSkill {
    public int skillId;
    public int senderId;
    public int targetId;
    public int skillfailType;//技能使用失败的原因
    public string skillname;
    public int sceneId; // 场景ID
}

public class CBattleEnd
{
    public int sceneId;     //场景ID
    public int camp;       //胜利阵营
}
public class CSendMarch
{
    public long marchId;    // marchid
    public int ret;     // 返回值
    public int sceneId;     // 进入的场景ID
    public int sceneClass; 	//进入的场景类型 
}
public class CObjInfor
{
    public int id;  //战斗场景中OBJID
    public int unitDataId;  //每个队伍小兵的模板ID
    public List<int> skilldataid	=new List<int>();   //技能模板id
    public int camp;    //c场景中obj阵营
    public int unitcount;   //每个队伍中小兵的人数
    public int hp;  //血量
    public int maxhp;   //最大血量
    public int attack;  //攻击能力
    public int defence; //防御能力
    public int sp;  //技能蓄力
    public int level;   //等级
    public int posx;    //坐标x
    public int posz;    //坐标z	
    public int arrangeindex;	//阵型位置
}
public class CObjPos {
    public int objId;   //战斗场景中OBJID
    public int posX;    //坐标x
    public int posZ;    //坐标z
    public int hp;  //血量	
    public int targetId;    //目标Id
    public int objState;	//当前OBJ状态
}



public class BattleProxy : IProxy {
    //public GC_BATTLEINFOR BattleInfor { get; set; }
    //public GC_OBJCOMMANDPURSUE PursueInfor { get; set; }
    //public GC_OBJPREPAREFORATTACK PrepareForAttackInfor { get; set; }
    //public GC_OBJGETHURT ObjectGetHurt { get; set; }
    //public GC_OBJPOSLIST CurrentObjPos { get; set; }
    //public GC_FIGHT FightInfor { get; set; }
    //public GC_RET_USE_SKILL UseSkill{ get; set; }
    //public GC_BATTLEEND BattleEnd { get; set; }
    //public GC_SEND_MARCH SendMarch { get; set; }

    public CBattleInfor BattleInfor { get; set; }
    public CPursueInfor PursueInfor { get; set; }
    public CPrepareForAttackInfor PrepareForAttackInfor { get; set; }
    public CObjectGetHurt ObjectGetHurt { get; set; }
    public CCurrentObjPos CurrentObjPos { get; set; }
    public CFightInfor FightInfor { get; set; }
    public CUseSkill UseSkill { get; set; }
    public CBattleEnd BattleEnd { get; set; }
    public CSendMarch SendMarch { get; set; }

    public static void SetBattleInfor(CBattleInfor battleInfor, GC_BATTLEINFOR gcBattleInfor)
    {
        battleInfor.sceneId = gcBattleInfor.SceneId;
        battleInfor.objList.Clear();
        for (int i = 0; i < gcBattleInfor.objListCount; i++)
        {
            CObjInfor objInfor = new CObjInfor();
            SetObjInfor(objInfor, gcBattleInfor.objListList[i]);
            battleInfor.objList.Add(objInfor);
        }
        battleInfor.camp = gcBattleInfor.Camp;
        battleInfor.currentState = gcBattleInfor.CurrentState;
    }
    public static void SetObjInfor(CObjInfor objInfor,GC_OBJINFOR gcObjInfor)
    {
        objInfor.id = gcObjInfor.Id;
        objInfor.unitDataId = gcObjInfor.UnitDataId;
        objInfor.skilldataid.Clear();
        for (int i = 0; i < gcObjInfor.skilldataidCount; i++)
        {
            objInfor.skilldataid.Add(gcObjInfor.skilldataidList[i]);
        }
        objInfor.camp = gcObjInfor.Camp;
        objInfor.unitcount = gcObjInfor.Unitcount;
        objInfor.hp = gcObjInfor.Hp;
        objInfor.maxhp = gcObjInfor.Maxhp;
        objInfor.attack = gcObjInfor.Attack;
        objInfor.defence = gcObjInfor.Defence;
        objInfor.sp = gcObjInfor.Sp;
        objInfor.level = gcObjInfor.Level;
        objInfor.posx = gcObjInfor.Posx;
        objInfor.posz = gcObjInfor.Posz;
        objInfor.arrangeindex = gcObjInfor.Arrangeindex;
    }
    public static void SetPursueInfor(CPursueInfor pursueInfor, GC_OBJCOMMANDPURSUE gcPursueInfor)
    {
        pursueInfor.sceneId = gcPursueInfor.SceneId;
        pursueInfor.objId = gcPursueInfor.ObjId;
        pursueInfor.aimObjId = gcPursueInfor.AimObjId;
    }
    public static void SetPrepareForAttackInfor(CPrepareForAttackInfor prepareForAttackInfor, GC_OBJPREPAREFORATTACK gcPrepareForAttackInfor)
    {
        prepareForAttackInfor.sceneId = gcPrepareForAttackInfor.SceneId;
        prepareForAttackInfor.objId = gcPrepareForAttackInfor.ObjId;
        prepareForAttackInfor.aimObjId = gcPrepareForAttackInfor.AimObjId;
    }
    public static void SetObjectGetHurt(CObjectGetHurt objectGetHurt, GC_OBJGETHURT gcObjectGetHurt)
    {
        objectGetHurt.sceneId = gcObjectGetHurt.SceneId;
        objectGetHurt.objId = gcObjectGetHurt.ObjId;
        objectGetHurt.attackObjId = gcObjectGetHurt.AttackObjId;
        objectGetHurt.damage = gcObjectGetHurt.Damage;
        objectGetHurt.objDead = gcObjectGetHurt.ObjDead;
        objectGetHurt.deathNumber = gcObjectGetHurt.DeathNumber;
    }
    public static void SetObjPos(CObjPos objPos, GC_OBJPOS gcObjPos)
    {
        objPos.hp = gcObjPos.Hp;
        objPos.objId = gcObjPos.ObjId;
        objPos.objState = gcObjPos.ObjState;
        objPos.posX = gcObjPos.PosX;
        objPos.posZ = gcObjPos.PosZ;
        objPos.targetId = gcObjPos.TargetId;
    }
    public static void SetCurrentObjPos(CCurrentObjPos currentObjPos,GC_OBJPOSLIST gcCurrentObjPos)
    {
        currentObjPos.sceneId = gcCurrentObjPos.SceneId;
        currentObjPos.objPosList.Clear();
        for (int i = 0; i < gcCurrentObjPos.objPosListCount; i++)
        {
            CObjPos objPos = new CObjPos();
            SetObjPos(objPos, gcCurrentObjPos.objPosListList[i]);
            currentObjPos.objPosList.Add(objPos);
        }
    }
    public static void SetFightInfor(CFightInfor fightInfor,GC_FIGHT gcFightInfor)
    {
        fightInfor.marchId = gcFightInfor.MarchId;
        fightInfor.ret = gcFightInfor.Ret;
        fightInfor.sceneclass = gcFightInfor.Sceneclass;
        fightInfor.sceneId = gcFightInfor.SceneId;
    }
    public static void SetUseSkill(CUseSkill useSkill,GC_RET_USE_SKILL gcUseSkill)
    {
        useSkill.sceneId = gcUseSkill.SceneId;
        useSkill.senderId = gcUseSkill.SenderId;
        useSkill.skillfailType = gcUseSkill.SkillfailType;
        useSkill.skillId = gcUseSkill.SkillId;
        useSkill.skillname = gcUseSkill.Skillname;
        useSkill.targetId = gcUseSkill.TargetId;
    }
    public static void SetBattleEnd(CBattleEnd battleEnd,GC_BATTLEEND gcBattleEnd)
    {
        battleEnd.camp = gcBattleEnd.Camp;
        battleEnd.sceneId = gcBattleEnd.SceneId;
    }
    public static void SetSendMarch(CSendMarch sendMarch,GC_SEND_MARCH gcSendMarch)
    {
        sendMarch.marchId = gcSendMarch.MarchId;
        sendMarch.ret = gcSendMarch.Ret;
        sendMarch.sceneClass = gcSendMarch.SceneClass;
        sendMarch.sceneId = gcSendMarch.SceneId;
    }

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
