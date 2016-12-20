//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_UnitTemplate
{ private const string TAB_FILE_DATA = "Tables/UnitTemplate.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_UNITTYPE=2,
ID_UNITVIEW,
ID_UNITATTACKRANGE,
ID_UNITMAXHP,
ID_ATTACKSPACETIME,
ID_UNITPATH,
ID_ATTACKTYPE,
ID_ATTACK,
ID_UNITRADIUS,
ID_UNITSPACEL,
ID_UNITSPACEH,
ID_UNITSKILLPATH,
ID_UNITARROWPATH,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private float m_Attack;
 public float Attack { get{ return m_Attack;}}

private float m_AttackSpaceTime;
 public float AttackSpaceTime { get{ return m_AttackSpaceTime;}}

private int m_AttackType;
 public int AttackType { get{ return m_AttackType;}}

private int m_Id;
 public int Id { get{ return m_Id;}}

private string m_UnitArrowPath;
 public string UnitArrowPath { get{ return m_UnitArrowPath;}}

private float m_UnitAttackRange;
 public float UnitAttackRange { get{ return m_UnitAttackRange;}}

private float m_UnitMaxHP;
 public float UnitMaxHP { get{ return m_UnitMaxHP;}}

private string m_UnitPath;
 public string UnitPath { get{ return m_UnitPath;}}

private float m_UnitRadius;
 public float UnitRadius { get{ return m_UnitRadius;}}

private string m_UnitSkillPath;
 public string UnitSkillPath { get{ return m_UnitSkillPath;}}

private float m_UnitSpaceH;
 public float UnitSpaceH { get{ return m_UnitSpaceH;}}

private float m_UnitSpaceL;
 public float UnitSpaceL { get{ return m_UnitSpaceL;}}

private int m_UnitType;
 public int UnitType { get{ return m_UnitType;}}

private float m_UnitView;
 public float UnitView { get{ return m_UnitView;}}

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
 Tab_UnitTemplate _values = new Tab_UnitTemplate();
 _values.m_Attack =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACK] as string );
_values.m_AttackSpaceTime =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKSPACETIME] as string );
_values.m_AttackType =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTACKTYPE] as string);
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_UnitArrowPath =  valuesList[(int)_ID.ID_UNITARROWPATH] as string;
_values.m_UnitAttackRange =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITATTACKRANGE] as string );
_values.m_UnitMaxHP =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITMAXHP] as string );
_values.m_UnitPath =  valuesList[(int)_ID.ID_UNITPATH] as string;
_values.m_UnitRadius =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITRADIUS] as string );
_values.m_UnitSkillPath =  valuesList[(int)_ID.ID_UNITSKILLPATH] as string;
_values.m_UnitSpaceH =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITSPACEH] as string );
_values.m_UnitSpaceL =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITSPACEL] as string );
_values.m_UnitType =  Convert.ToInt32(valuesList[(int)_ID.ID_UNITTYPE] as string);
_values.m_UnitView =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITVIEW] as string );

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

