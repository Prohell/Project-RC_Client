//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_CityBuildingSlot
{ private const string TAB_FILE_DATA = "Tables/CityBuildingSlot.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_BUILDINGTYPE=2,
ID_UNLOCKLEVEL,
ID_EFFECT_1,
ID_EFFECT_2,
ID_EFFECT_3,
ID_EFFECT_4,
ID_EFFECT_5,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_BuildingType;
 public int BuildingType { get{ return m_BuildingType;}}

public int getEffectCount() { return 5; } 
 private int[] m_Effect = new int[5];
 public int GetEffectbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_Effect[idx];
 return -1;
 }

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_UnlockLevel;
 public int UnlockLevel { get{ return m_UnlockLevel;}}

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
 Tab_CityBuildingSlot _values = new Tab_CityBuildingSlot();
 _values.m_BuildingType =  Convert.ToInt32(valuesList[(int)_ID.ID_BUILDINGTYPE] as string);
_values.m_Effect [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_1] as string);
_values.m_Effect [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_2] as string);
_values.m_Effect [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_3] as string);
_values.m_Effect [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_4] as string);
_values.m_Effect [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_EFFECT_5] as string);
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_UnlockLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_UNLOCKLEVEL] as string);

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

