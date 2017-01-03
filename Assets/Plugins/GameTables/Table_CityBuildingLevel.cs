//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_CityBuildingLevel
{ private const string TAB_FILE_DATA = "Tables/CityBuildingLevel.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_NUMBER=2,
ID_NAME,
ID_LEVEL,
ID_DESCRIPTION,
ID_ASSET,
ID_BUNDLE,
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

private string m_Asset;
 public string Asset { get{ return m_Asset;}}

private string m_Bundle;
 public string Bundle { get{ return m_Bundle;}}

private string m_Description;
 public string Description { get{ return m_Description;}}

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_Level;
 public int Level { get{ return m_Level;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

public int getNeedBuildingCount() { return 3; } 
 private int[] m_NeedBuilding = new int[3];
 public int GetNeedBuildingbyIndex(int idx) {
 if(idx>=0 && idx<3) return m_NeedBuilding[idx];
 return -1;
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

private int m_Number;
 public int Number { get{ return m_Number;}}

private int m_Prosperity;
 public int Prosperity { get{ return m_Prosperity;}}

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
 Tab_CityBuildingLevel _values = new Tab_CityBuildingLevel();
 _values.m_Asset =  valuesList[(int)_ID.ID_ASSET] as string;
_values.m_Bundle =  valuesList[(int)_ID.ID_BUNDLE] as string;
_values.m_Description =  valuesList[(int)_ID.ID_DESCRIPTION] as string;
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_Level =  Convert.ToInt32(valuesList[(int)_ID.ID_LEVEL] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_NeedBuilding [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDBUILDING1] as string);
_values.m_NeedBuilding [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDBUILDING2] as string);
_values.m_NeedBuilding [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDBUILDING3] as string);
_values.m_NeedFood =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDFOOD] as string);
_values.m_NeedGold =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDGOLD] as string);
_values.m_NeedIron =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDIRON] as string);
_values.m_NeedProsperity =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDPROSPERITY] as string);
_values.m_NeedStone =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDSTONE] as string);
_values.m_NeedTime =  Convert.ToInt32(valuesList[(int)_ID.ID_NEEDTIME] as string);
_values.m_Number =  Convert.ToInt32(valuesList[(int)_ID.ID_NUMBER] as string);
_values.m_Prosperity =  Convert.ToInt32(valuesList[(int)_ID.ID_PROSPERITY] as string);

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

