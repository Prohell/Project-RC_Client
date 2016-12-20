//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_CityBuilding
{ private const string TAB_FILE_DATA = "Tables/CityBuilding.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_BUILDINGTYPE=2,
ID_NAME,
ID_LEVEL,
ID_DESCRIPTION,
ID_ICON,
ID_RES,
ID_PROSPERITY,
ID_NEEDBUILDING1,
ID_NEEDBUILDING2,
ID_NEEDBUILDING3,
ID_NEEDPROSPERITY,
ID_NEEDTIME,
ID_NEEDSTONE,
ID_NEEDGOLD,
ID_NEEDIRON,
ID_NEEDFOOD,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BuildingType;
 public int BuildingType { get{ return m_BuildingType;}}

private string m_Description;
 public string Description { get{ return m_Description;}}

private string m_Icon;
 public string Icon { get{ return m_Icon;}}

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_Level;
 public int Level { get{ return m_Level;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

public int getNeedBuildingCount() { return 3; } 
 private string[] m_NeedBuilding = new string[3];
 public string GetNeedBuildingbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_NeedBuilding[idx];
 return "";
 }

private int m_NeedFood;
 public int NeedFood { get{ return m_NeedFood;}}

private int m_NeedGold;
 public int NeedGold { get{ return m_NeedGold;}}

private int m_NeedIron;
 public int NeedIron { get{ return m_NeedIron;}}

private int m_NeedProsperity;
 public int NeedProsperity { get{ return m_NeedProsperity;}}

private int m_NeedStone;
 public int NeedStone { get{ return m_NeedStone;}}

private int m_NeedTime;
 public int NeedTime { get{ return m_NeedTime;}}

private int m_Prosperity;
 public int Prosperity { get{ return m_Prosperity;}}

private string m_Res;
 public string Res { get{ return m_Res;}}

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
 Tab_CityBuilding _values = new Tab_CityBuilding();
 _values.m_BuildingType =  Convert.ToInt32(valuesList[(int)_ID.ID_BUILDINGTYPE] as string);
_values.m_Description =  valuesList[(int)_ID.ID_DESCRIPTION] as string;
_values.m_Icon =  valuesList[(int)_ID.ID_ICON] as string;
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_Level =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVEL] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_NeedBuilding [ 0 ] =  valuesList[(int)_ID.ID_NEEDBUILDING1] as string;
_values.m_NeedBuilding [ 1 ] =  valuesList[(int)_ID.ID_NEEDBUILDING2] as string;
_values.m_NeedBuilding [ 2 ] =  valuesList[(int)_ID.ID_NEEDBUILDING3] as string;
_values.m_NeedFood =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDFOOD] as string);
_values.m_NeedGold =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDGOLD] as string);
_values.m_NeedIron =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDIRON] as string);
_values.m_NeedProsperity =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDPROSPERITY] as string);
_values.m_NeedStone =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDSTONE] as string);
_values.m_NeedTime =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDTIME] as string);
_values.m_Prosperity =  Convert.ToInt32(valuesList[(int)_ID.ID_PROSPERITY] as string);
_values.m_Res =  valuesList[(int)_ID.ID_RES] as string;

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

