//This code create by CodeEngine Author:Wendy ,don't modify

namespace SPacket.SocketInstance
 {
 public class PacketFactoryManagerInstance : PacketFactoryManager
 {
 public override bool Init ()
 {
 AddFactory(new GC_LOGIN_RET_PF());
AddFactory(new GC_CONNECTED_HEARTBEAT_PF());
AddFactory(new GC_NEAR_MARCHLIST_PF());
AddFactory(new GC_CHAT_PF());
AddFactory(new GC_NOTICE_PF());
AddFactory(new GC_UPDATE_SCENE_INSTACTIVATION_PF());
AddFactory(new GC_MOVE_PF());
AddFactory(new GC_MARCH_MOVE_PF());
AddFactory(new GC_STOP_PF());
AddFactory(new GC_TELEMOVE_PF());
AddFactory(new GC_RET_USE_SKILL_PF());
AddFactory(new GC_UPDATE_ANIMATION_STATE_PF());
AddFactory(new GC_DELETE_OBJ_PF());
AddFactory(new GC_ATTACKFLY_PF());
AddFactory(new GC_FORCE_SETPOS_PF());
AddFactory(new GC_UPDATE_NEEDIMPACTINFO_PF());
AddFactory(new GC_PLAY_EFFECT_PF());
AddFactory(new GC_REMOVEEFFECT_PF());
AddFactory(new GC_FIGHT_PF());
AddFactory(new GC_ROBOT_OPEN_PF());
AddFactory(new GC_ASSIGN_HERO_PF());
AddFactory(new GC_SEND_MARCH_PF());
AddFactory(new GC_CREATEROLE_RET_PF());
AddFactory(new GC_LOGIN_QUEUE_STATUS_PF());
AddFactory(new GC_BATTLEINFOR_PF());
AddFactory(new GC_OBJPOSLIST_PF());
AddFactory(new GC_OBJCOMMANDPURSUE_PF());
AddFactory(new GC_OBJPREPAREFORATTACK_PF());
AddFactory(new GC_OBJGETHURT_PF());
AddFactory(new GC_BUILDING_LEVELUP_PF());
AddFactory(new GC_UPDATE_MARCH_PF());
AddFactory(new GC_TROOP_TRAIN_PF());
AddFactory(new GC_TROOPTRAIN_OVER_PF());
AddFactory(new GC_BATTLEPREPARE_PF());
AddFactory(new GC_BATTLEEND_PF());
AddFactory(new GC_BATTLESTART_PF());
AddFactory(new CG_LOGIN_PF());
AddFactory(new CG_CONNECTED_HEARTBEAT_PF());
AddFactory(new CG_REQ_NEAR_LIST_PF());
AddFactory(new CG_CHAT_PF());
AddFactory(new CG_MOVE_PF());
AddFactory(new CG_MARCH_MOVE_PF());
AddFactory(new CG_SKILL_USE_PF());
AddFactory(new CG_FIGHT_PF());
AddFactory(new CG_LEAVE_COPYSCENE_PF());
AddFactory(new CG_ROBOT_OPEN_PF());
AddFactory(new CG_ASSIGN_HERO_PF());
AddFactory(new CG_SEND_MARCH_PF());
AddFactory(new CG_CREATEROLE_PF());
AddFactory(new CG_BATTLEINFOR_PF());
AddFactory(new CG_OBJPOSLIST_PF());
AddFactory(new CG_BUILDING_LEVELUP_PF());
AddFactory(new CG_TROOP_TRAIN_PF());
AddFactory(new CG_BATTLEPREPARE_PF());
 AddPacketHander(MessageID.PACKET_GC_LOGIN_RET, new GC_LOGIN_RETHandler());
AddPacketHander(MessageID.PACKET_GC_CONNECTED_HEARTBEAT, new GC_CONNECTED_HEARTBEATHandler());
AddPacketHander(MessageID.PACKET_GC_NEAR_MARCHLIST, new GC_NEAR_MARCHLISTHandler());
AddPacketHander(MessageID.PACKET_GC_CHAT, new GC_CHATHandler());
AddPacketHander(MessageID.PACKET_GC_NOTICE, new GC_NOTICEHandler());
AddPacketHander(MessageID.PACKET_GC_UPDATE_SCENE_INSTACTIVATION, new GC_UPDATE_SCENE_INSTACTIVATIONHandler());
AddPacketHander(MessageID.PACKET_GC_MOVE, new GC_MOVEHandler());
AddPacketHander(MessageID.PACKET_GC_MARCH_MOVE, new GC_MARCH_MOVEHandler());
AddPacketHander(MessageID.PACKET_GC_STOP, new GC_STOPHandler());
AddPacketHander(MessageID.PACKET_GC_TELEMOVE, new GC_TELEMOVEHandler());
AddPacketHander(MessageID.PACKET_GC_RET_USE_SKILL, new GC_RET_USE_SKILLHandler());
AddPacketHander(MessageID.PACKET_GC_UPDATE_ANIMATION_STATE, new GC_UPDATE_ANIMATION_STATEHandler());
AddPacketHander(MessageID.PACKET_GC_DELETE_OBJ, new GC_DELETE_OBJHandler());
AddPacketHander(MessageID.PACKET_GC_ATTACKFLY, new GC_ATTACKFLYHandler());
AddPacketHander(MessageID.PACKET_GC_FORCE_SETPOS, new GC_FORCE_SETPOSHandler());
AddPacketHander(MessageID.PACKET_GC_UPDATE_NEEDIMPACTINFO, new GC_UPDATE_NEEDIMPACTINFOHandler());
AddPacketHander(MessageID.PACKET_GC_PLAY_EFFECT, new GC_PLAY_EFFECTHandler());
AddPacketHander(MessageID.PACKET_GC_REMOVEEFFECT, new GC_REMOVEEFFECTHandler());
AddPacketHander(MessageID.PACKET_GC_FIGHT, new GC_FIGHTHandler());
AddPacketHander(MessageID.PACKET_GC_ROBOT_OPEN, new GC_ROBOT_OPENHandler());
AddPacketHander(MessageID.PACKET_GC_ASSIGN_HERO, new GC_ASSIGN_HEROHandler());
AddPacketHander(MessageID.PACKET_GC_SEND_MARCH, new GC_SEND_MARCHHandler());
AddPacketHander(MessageID.PACKET_GC_CREATEROLE_RET, new GC_CREATEROLE_RETHandler());
AddPacketHander(MessageID.PACKET_GC_LOGIN_QUEUE_STATUS, new GC_LOGIN_QUEUE_STATUSHandler());
AddPacketHander(MessageID.PACKET_GC_BATTLEINFOR, new GC_BATTLEINFORHandler());
AddPacketHander(MessageID.PACKET_GC_OBJPOSLIST, new GC_OBJPOSLISTHandler());
AddPacketHander(MessageID.PACKET_GC_OBJCOMMANDPURSUE, new GC_OBJCOMMANDPURSUEHandler());
AddPacketHander(MessageID.PACKET_GC_OBJPREPAREFORATTACK, new GC_OBJPREPAREFORATTACKHandler());
AddPacketHander(MessageID.PACKET_GC_OBJGETHURT, new GC_OBJGETHURTHandler());
AddPacketHander(MessageID.PACKET_GC_BUILDING_LEVELUP, new GC_BUILDING_LEVELUPHandler());
AddPacketHander(MessageID.PACKET_GC_UPDATE_MARCH, new GC_UPDATE_MARCHHandler());
AddPacketHander(MessageID.PACKET_GC_TROOP_TRAIN, new GC_TROOP_TRAINHandler());
AddPacketHander(MessageID.PACKET_GC_TROOPTRAIN_OVER, new GC_TROOPTRAIN_OVERHandler());
AddPacketHander(MessageID.PACKET_GC_BATTLEPREPARE, new GC_BATTLEPREPAREHandler());
AddPacketHander(MessageID.PACKET_GC_BATTLEEND, new GC_BATTLEENDHandler());
AddPacketHander(MessageID.PACKET_GC_BATTLESTART, new GC_BATTLESTARTHandler());
AddPacketHander(MessageID.PACKET_CG_LOGIN, new CG_LOGINHandler());
AddPacketHander(MessageID.PACKET_CG_CONNECTED_HEARTBEAT, new CG_CONNECTED_HEARTBEATHandler());
AddPacketHander(MessageID.PACKET_CG_REQ_NEAR_LIST, new CG_REQ_NEAR_LISTHandler());
AddPacketHander(MessageID.PACKET_CG_CHAT, new CG_CHATHandler());
AddPacketHander(MessageID.PACKET_CG_MOVE, new CG_MOVEHandler());
AddPacketHander(MessageID.PACKET_CG_MARCH_MOVE, new CG_MARCH_MOVEHandler());
AddPacketHander(MessageID.PACKET_CG_SKILL_USE, new CG_SKILL_USEHandler());
AddPacketHander(MessageID.PACKET_CG_FIGHT, new CG_FIGHTHandler());
AddPacketHander(MessageID.PACKET_CG_LEAVE_COPYSCENE, new CG_LEAVE_COPYSCENEHandler());
AddPacketHander(MessageID.PACKET_CG_ROBOT_OPEN, new CG_ROBOT_OPENHandler());
AddPacketHander(MessageID.PACKET_CG_ASSIGN_HERO, new CG_ASSIGN_HEROHandler());
AddPacketHander(MessageID.PACKET_CG_SEND_MARCH, new CG_SEND_MARCHHandler());
AddPacketHander(MessageID.PACKET_CG_CREATEROLE, new CG_CREATEROLEHandler());
AddPacketHander(MessageID.PACKET_CG_BATTLEINFOR, new CG_BATTLEINFORHandler());
AddPacketHander(MessageID.PACKET_CG_OBJPOSLIST, new CG_OBJPOSLISTHandler());
AddPacketHander(MessageID.PACKET_CG_BUILDING_LEVELUP, new CG_BUILDING_LEVELUPHandler());
AddPacketHander(MessageID.PACKET_CG_TROOP_TRAIN, new CG_TROOP_TRAINHandler());
AddPacketHander(MessageID.PACKET_CG_BATTLEPREPARE, new CG_BATTLEPREPAREHandler());
 return true;
 } 
 }
 

 

public class GC_ASSIGN_HERO_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_ASSIGN_HERO;
 }
 }
public class GC_ATTACKFLY_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_ATTACKFLY;
 }
 }
public class GC_BATTLEEND_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_BATTLEEND;
 }
 }
public class GC_BATTLEINFOR_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_BATTLEINFOR;
 }
 }
public class GC_BATTLEPREPARE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_BATTLEPREPARE;
 }
 }
public class GC_BATTLESTART_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_BATTLESTART;
 }
 }
public class GC_BUILDING_LEVELUP_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_BUILDING_LEVELUP;
 }
 }
public class GC_CHAT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_CHAT;
 }
 }
public class GC_CONNECTED_HEARTBEAT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_CONNECTED_HEARTBEAT;
 }
 }
public class GC_CREATEROLE_RET_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_CREATEROLE_RET;
 }
 }
public class GC_DELETE_OBJ_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_DELETE_OBJ;
 }
 }
public class GC_FIGHT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_FIGHT;
 }
 }
public class GC_FORCE_SETPOS_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_FORCE_SETPOS;
 }
 }
public class GC_LOGIN_QUEUE_STATUS_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_LOGIN_QUEUE_STATUS;
 }
 }
public class GC_LOGIN_RET_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_LOGIN_RET;
 }
 }
public class GC_MARCH_MOVE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_MARCH_MOVE;
 }
 }
public class GC_MOVE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_MOVE;
 }
 }
public class GC_NEAR_MARCHLIST_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_NEAR_MARCHLIST;
 }
 }
public class GC_NOTICE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_NOTICE;
 }
 }
public class GC_OBJCOMMANDPURSUE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_OBJCOMMANDPURSUE;
 }
 }
public class GC_OBJGETHURT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_OBJGETHURT;
 }
 }
public class GC_OBJPOSLIST_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_OBJPOSLIST;
 }
 }
public class GC_OBJPREPAREFORATTACK_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_OBJPREPAREFORATTACK;
 }
 }
public class GC_PLAY_EFFECT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_PLAY_EFFECT;
 }
 }
public class GC_REMOVEEFFECT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_REMOVEEFFECT;
 }
 }
public class GC_RET_USE_SKILL_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_RET_USE_SKILL;
 }
 }
public class GC_ROBOT_OPEN_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_ROBOT_OPEN;
 }
 }
public class GC_SEND_MARCH_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_SEND_MARCH;
 }
 }
public class GC_STOP_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_STOP;
 }
 }
public class GC_TELEMOVE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_TELEMOVE;
 }
 }
public class GC_TROOPTRAIN_OVER_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_TROOPTRAIN_OVER;
 }
 }
public class GC_TROOP_TRAIN_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_TROOP_TRAIN;
 }
 }
public class GC_UPDATE_ANIMATION_STATE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_UPDATE_ANIMATION_STATE;
 }
 }
public class GC_UPDATE_MARCH_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_UPDATE_MARCH;
 }
 }
public class GC_UPDATE_NEEDIMPACTINFO_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_UPDATE_NEEDIMPACTINFO;
 }
 }
public class GC_UPDATE_SCENE_INSTACTIVATION_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_GC_UPDATE_SCENE_INSTACTIVATION;
 }
 }
public class CG_ASSIGN_HERO_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_ASSIGN_HERO;
 }
 }
public class CG_BATTLEINFOR_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_BATTLEINFOR;
 }
 }
public class CG_BATTLEPREPARE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_BATTLEPREPARE;
 }
 }
public class CG_BUILDING_LEVELUP_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_BUILDING_LEVELUP;
 }
 }
public class CG_CHAT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_CHAT;
 }
 }
public class CG_CONNECTED_HEARTBEAT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_CONNECTED_HEARTBEAT;
 }
 }
public class CG_CREATEROLE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_CREATEROLE;
 }
 }
public class CG_FIGHT_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_FIGHT;
 }
 }
public class CG_LEAVE_COPYSCENE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_LEAVE_COPYSCENE;
 }
 }
public class CG_LOGIN_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_LOGIN;
 }
 }
public class CG_MARCH_MOVE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_MARCH_MOVE;
 }
 }
public class CG_MOVE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_MOVE;
 }
 }
public class CG_OBJPOSLIST_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_OBJPOSLIST;
 }
 }
public class CG_REQ_NEAR_LIST_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_REQ_NEAR_LIST;
 }
 }
public class CG_ROBOT_OPEN_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_ROBOT_OPEN;
 }
 }
public class CG_SEND_MARCH_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_SEND_MARCH;
 }
 }
public class CG_SKILL_USE_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_SKILL_USE;
 }
 }
public class CG_TROOP_TRAIN_PF : PacketFactory
 {
 public MessageID GetPacketID()
 {
 return MessageID.PACKET_CG_TROOP_TRAIN;
 }
 }
}
