//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_RoleBaseAttr
{ private const string TAB_FILE_DATA = "Tables/RoleBaseAttr.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_T,
ID_CHARMODELID=2,
ID_NAME,
ID_SEX,
ID_LEVEL,
ID_ATTACKDISTYPE,
ID_SKILLSTRATEGYINDEX,
ID_MAXHP,
ID_MAXXP,
ID_XPSPEED,
ID_DEFENSE,
ID_ATTACK,
ID_HIT,
ID_ATTACKTIME,
ID_MOVESPEED,
ID_CAMP,
ID_SELECTRADIUS,
ID_CORPSETIME,
ID_EXP,
ID_ISATTACKFLY,
ID_DIEEFFECTID,
ID_RAMPTYPE,
ID_ENHANCEID,
ID_UNITDATAID,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Attack;
 public int Attack { get{ return m_Attack;}}

private int m_AttackDisType;
 public int AttackDisType { get{ return m_AttackDisType;}}

private int m_AttackTime;
 public int AttackTime { get{ return m_AttackTime;}}

private int m_Camp;
 public int Camp { get{ return m_Camp;}}

private int m_CharModelID;
 public int CharModelID { get{ return m_CharModelID;}}

private int m_CorpseTime;
 public int CorpseTime { get{ return m_CorpseTime;}}

private int m_Defense;
 public int Defense { get{ return m_Defense;}}

private int m_DieEffectID;
 public int DieEffectID { get{ return m_DieEffectID;}}

private int m_EnhanceID;
 public int EnhanceID { get{ return m_EnhanceID;}}

private int m_Exp;
 public int Exp { get{ return m_Exp;}}

private int m_Hit;
 public int Hit { get{ return m_Hit;}}

private int m_IsAttackFly;
 public int IsAttackFly { get{ return m_IsAttackFly;}}

private int m_Level;
 public int Level { get{ return m_Level;}}

private int m_MaxHP;
 public int MaxHP { get{ return m_MaxHP;}}

private int m_MaxXP;
 public int MaxXP { get{ return m_MaxXP;}}

private int m_MoveSpeed;
 public int MoveSpeed { get{ return m_MoveSpeed;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_RampType;
 public int RampType { get{ return m_RampType;}}

private float m_SelectRadius;
 public float SelectRadius { get{ return m_SelectRadius;}}

private int m_Sex;
 public int Sex { get{ return m_Sex;}}

private int m_SkillstrategyIndex;
 public int SkillstrategyIndex { get{ return m_SkillstrategyIndex;}}

private int m_UnitDataID;
 public int UnitDataID { get{ return m_UnitDataID;}}

private int m_XpSpeed;
 public int XpSpeed { get{ return m_XpSpeed;}}

private int m_T;
 public int T { get{ return m_T;}}

public static bool LoadTable(Dictionary<int, List<object> > _tab)
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }
 public static void SerializableTable(string[] valuesList,int skey,Dictionary<int, List<object> > _hash)
 {
 if ((int)_ID.MAX_RECORD!=valuesList.Length)
 {
 throw TableException.ErrorReader("Load {0} error as CodeSize:{1} not Equal DataSize:{2}", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Length);
 }
 Tab_RoleBaseAttr _values = new Tab_RoleBaseAttr();
 _values.m_Attack =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACK] as string);
_values.m_AttackDisType =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACKDISTYPE] as string);
_values.m_AttackTime =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACKTIME] as string);
_values.m_Camp =  Convert.ToInt32(valuesList[(int)_ID.ID_CAMP] as string);
_values.m_CharModelID =  Convert.ToInt32(valuesList[(int)_ID.ID_CHARMODELID] as string);
_values.m_CorpseTime =  Convert.ToInt32(valuesList[(int)_ID.ID_CORPSETIME] as string);
_values.m_Defense =  Convert.ToInt32(valuesList[(int)_ID.ID_DEFENSE] as string);
_values.m_DieEffectID =  Convert.ToInt32(valuesList[(int)_ID.ID_DIEEFFECTID] as string);
_values.m_EnhanceID =  Convert.ToInt32(valuesList[(int)_ID.ID_ENHANCEID] as string);
_values.m_Exp =  Convert.ToInt32(valuesList[(int)_ID.ID_EXP] as string);
_values.m_Hit =  Convert.ToInt32(valuesList[(int)_ID.ID_HIT] as string);
_values.m_IsAttackFly =  Convert.ToInt32(valuesList[(int)_ID.ID_ISATTACKFLY] as string);
_values.m_Level =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVEL] as string);
_values.m_MaxHP =  Convert.ToInt32(valuesList[(int)_ID.ID_MAXHP] as string);
_values.m_MaxXP =  Convert.ToInt32(valuesList[(int)_ID.ID_MAXXP] as string);
_values.m_MoveSpeed =  Convert.ToInt32(valuesList[(int)_ID.ID_MOVESPEED] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_RampType =  Convert.ToInt32(valuesList[(int)_ID.ID_RAMPTYPE] as string);
_values.m_SelectRadius =  Convert.ToSingle(valuesList[(int)_ID.ID_SELECTRADIUS] as string );
_values.m_Sex =  Convert.ToInt32(valuesList[(int)_ID.ID_SEX] as string);
_values.m_SkillstrategyIndex =  Convert.ToInt32(valuesList[(int)_ID.ID_SKILLSTRATEGYINDEX] as string);
_values.m_UnitDataID =  Convert.ToInt32(valuesList[(int)_ID.ID_UNITDATAID] as string);
_values.m_XpSpeed =  Convert.ToInt32(valuesList[(int)_ID.ID_XPSPEED] as string);
_values.m_T =  Convert.ToInt32(valuesList[(int)_ID.ID_T] as string);

 if (_hash.ContainsKey(skey))
 {
 List< object> tList =_hash[skey];
 tList.Add(_values);
 }
 else
 {
 List<object> tList = new List<object>();
 tList.Add(_values); 
 _hash.Add(skey, (List<object>)tList);
 }
 }


}
}

