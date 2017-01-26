using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CityVo {
	public int tileId;
	public long cityId;
	public int level;
	public long food;
	public long stone;
	public long iron;
    public Coord WorldPos;
	public List<BuildingVo> buildList = new List<BuildingVo>();
	public List<TrainVo> trainList = new List<TrainVo>();
}

public class BuildingVo {
	public long guid;
	public int type;
	public int slot;
	public int level;
}

public class TrainVo {
	public long queueid;
	public long buildId;
	public int troopType;
	public int health;
	public int beginTime;
	public int endTime;
	public int queueIndex;
}

public class HeroVo {
	public long guid;
	public long marchId;
	public int type;
	public int level;
	public int state;
	public int health;
	public int mana;
	public int arrangeIndex;
	public List<int> skillIdList = new List<int>();
	public List<CoolDownVo> cooldownList = new List<CoolDownVo>();
}

public class CoolDownVo {
	public long id;
	public int cdTime;
	public int elapsed;
}

public class MarchVo {
	public long marchId;
	public long playerId;
	public long cityId;
	public long fightId;
	public long buildId;
	public int sceneId;
	public int beginTime;
	public int status;
	public int speed;
	public HeroVo hero = new HeroVo();
	public List<TroopVo> troopList = new List<TroopVo>();
}

public class TroopVo {
	public int type;
	public int level;
	public int health;
	public int mana;
	public int arrangeIndex;
	public long marchId;
	public int queueIndex;
	public List<CoolDownVo> cooldownList = new List<CoolDownVo>();
	public List<int> skillIdList = new List<int>();
}

public class PlayerProxy : IProxy {
	public long userid;
	public string oid;
	public string accesstoken;
	public string playername;
	public int  level;
	public CityVo city = new CityVo();
	public List<HeroVo> heroList = new List<HeroVo>();
	public List<MarchVo> marchList = new List<MarchVo>();

	public void OnDestroy()
	{
	}

	public void OnInit()
	{
	}


	static public void SetBuildVo(GC_BuildingData data, BuildingVo vo){
		vo.guid = data.Guid;
		vo.type = data.Type;
		vo.slot = data.Slot;
		vo.level = data.Level;
	}

	static public void SetTrainVo(GC_TrainData data, TrainVo vo){
		vo.queueid = data.Queueid;
		vo.buildId = data.Buildid;
		vo.troopType = data.Trooptype;
		vo.health = data.Hp;
		vo.beginTime = data.Begintime;
		vo.endTime = data.Completime;
		vo.queueIndex = data.Queueindex;
	}

	static public void SetHeroVo(GC_HeroData data, HeroVo vo){
		vo.guid = data.Guid;
		vo.marchId = data.MarchId;
		vo.type = data.Type;
		vo.level = data.Level;
		vo.state = data.State;
		vo.health = data.Hp;
		vo.mana = data.Mp;
		vo.arrangeIndex = data.Arrangeindex;
		vo.skillIdList.Clear ();
		for (int j = 0; j < data.skillCount; j++) {
			vo.skillIdList.Add(data.GetSkill(j));
		}
		vo.cooldownList.Clear ();
		for (int i = 0; i < data.cooldownCount; i++) {
			GC_CoolDownInfo coolDown = data.GetCooldown (i);
			CoolDownVo cVo = new CoolDownVo ();
			cVo.cdTime = coolDown.Cdtime;
			cVo.elapsed = coolDown.Elapsed;
			cVo.id = coolDown.Id;
			vo.cooldownList.Add (cVo);
		}
	}

	static public void SetMarchVo(GC_MarchData data, MarchVo vo){
		vo.marchId = data.Marchid;
		vo.playerId = data.Playerid;
		vo.cityId = data.CityId;
		vo.fightId = data.Fightid;
		vo.buildId = data.Buildid;
		vo.sceneId = data.Sceneid;
		vo.beginTime = data.Begintime;
		vo.status = data.Status;
		vo.speed = data.Speed;

		vo.hero = new HeroVo ();
		SetHeroVo (data.Hero, vo.hero);

		vo.troopList.Clear ();
		for (int i = 0; i < data.troopCount; i++) {
			GC_TroopData tData = data.GetTroop (i);
			TroopVo tVo = new TroopVo();
			SetTroopVo (tData, tVo);
			vo.troopList.Add (tVo);
		}
	}

	static public void SetTroopVo(GC_TroopData data, TroopVo vo){
		vo.type = data.Type;
		vo.level = data.Level;
		vo.health = data.Hp;
		vo.mana = data.Mp;
		vo.arrangeIndex = data.Arrangeindex;
		vo.marchId = data.Marchid;
		vo.queueIndex = data.Queueindex;

		vo.skillIdList.Clear ();
		for (int i = 0; i < data.skillCount; i++) {
			int skill = data.GetSkill (i);
			vo.skillIdList.Add(skill);
		}

		vo.cooldownList.Clear ();
		for (int i = 0; i < data.cooldownCount; i++) {
			GC_CoolDownInfo coolDown = data.GetCooldown (i);
			CoolDownVo cVo = new CoolDownVo ();
			cVo.cdTime = coolDown.Cdtime;
			cVo.elapsed = coolDown.Elapsed;
			cVo.id = coolDown.Id;
			vo.cooldownList.Add (cVo);
		}
	}
}