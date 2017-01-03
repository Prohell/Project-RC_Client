//This code create by CodeEngine ,don't modify

using System;
 public enum MessageID :ushort
 {
 PACKET_NONE = 0 , // 0£¬¿Õ
 PACKET_CG_LOGIN = 1, //client ask login 
 PACKET_GC_LOGIN_RET =2, //client login result 
 PACKET_CG_CONNECTED_HEARTBEAT,	//client connected heartbeat
PACKET_GC_CONNECTED_HEARTBEAT,	//server connected heartbeat
PACKET_CG_REQ_NEAR_LIST,	//client ask nearlist
PACKET_GC_NEAR_MARCHLIST,	//server send nearby player list to client
PACKET_CG_CHAT,	//Client send chat info
PACKET_GC_CHAT,	//Server send chat info
PACKET_GC_NOTICE,	//notice from server
PACKET_GC_UPDATE_SCENE_INSTACTIVATION,	//Update Scene InstActivation
PACKET_CG_MOVE,	//Player Move
PACKET_GC_MOVE,	//Notify Character Move
PACKET_GC_STOP,	//Notify Character Stop
PACKET_GC_TELEMOVE,	//TeleMove
PACKET_CG_SKILL_USE,	//client send use skill
PACKET_GC_RET_USE_SKILL,	//Use Skill ret
PACKET_GC_UPDATE_ANIMATION_STATE,	//Update Animation state
PACKET_GC_DELETE_OBJ,	//Delete Player
PACKET_GC_ATTACKFLY,	//Attak fly
PACKET_GC_FORCE_SETPOS,	//Server Force Set Player Pos
PACKET_GC_UPDATE_NEEDIMPACTINFO,	//syn need Impact Info
PACKET_GC_PLAY_EFFECT,	//Server Send Player Use Tool
PACKET_GC_REMOVEEFFECT,	//remove Effect
PACKET_CG_FIGHT,	//Send Open Fight Req to Server
PACKET_CG_LEAVE_COPYSCENE,	// Client Leave Copyscene
PACKET_CG_ROBOT_OPEN,	// Send Rotbot Open
PACKET_GC_ROBOT_OPEN,	// Ret Rotbot Open
PACKET_CG_ASSIGN_HERO,	// Send Assign Hero
PACKET_GC_ASSIGN_HERO,	// Ret Assign Hero
PACKET_CG_SEND_MARCH,	// SEND March From City
PACKET_GC_SEND_MARCH,	// Ret March From City
PACKET_CG_CREATEROLE,	//client send createRole
PACKET_GC_CREATEROLE_RET,	//server send create role result
PACKET_GC_LOGIN_QUEUE_STATUS,	//login queue status
PACKET_CG_BATTLEINFOR,	//
PACKET_GC_OBJINFOR,	//
PACKET_GC_BATTLEINFOR,	//
PACKET_GC_OBJPOSLIST,	//
PACKET_CG_OBJPOSLIST,	//
PACKET_GC_OBJCOMMANDPURSUE,	//
PACKET_GC_OBJPREPAREFORATTACK,	//
PACKET_GC_OBJGETHURT,	//
PACKET_CG_BUILDING_LEVELUP,	// Send Building LevelUp
PACKET_GC_BUILDING_LEVELUP,	// Ret Building LevelUp

 PACKET_SIZE
 }
