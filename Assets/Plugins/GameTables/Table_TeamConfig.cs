//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_TeamConfig
{ private const string TAB_FILE_DATA = "Tables/TeamConfig.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_SQUADSPACE=2,
ID_UNITSPACE,
ID_UNITNUMBERS,
ID_SQUADROWCOUNT,
ID_SQUADNUMBERS,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_SquadNumbers;
 public int SquadNumbers { get{ return m_SquadNumbers;}}

private int m_SquadRowCount;
 public int SquadRowCount { get{ return m_SquadRowCount;}}

private float m_SquadSpace;
 public float SquadSpace { get{ return m_SquadSpace;}}

private int m_UnitNumbers;
 public int UnitNumbers { get{ return m_UnitNumbers;}}

private float m_UnitSpace;
 public float UnitSpace { get{ return m_UnitSpace;}}

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
 Tab_TeamConfig _values = new Tab_TeamConfig();
 _values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_SquadNumbers =  Convert.ToInt32(valuesList[(int)_ID.ID_SQUADNUMBERS] as string);
_values.m_SquadRowCount =  Convert.ToInt32(valuesList[(int)_ID.ID_SQUADROWCOUNT] as string);
_values.m_SquadSpace =  Convert.ToSingle(valuesList[(int)_ID.ID_SQUADSPACE] as string );
_values.m_UnitNumbers =  Convert.ToInt32(valuesList[(int)_ID.ID_UNITNUMBERS] as string);
_values.m_UnitSpace =  Convert.ToSingle(valuesList[(int)_ID.ID_UNITSPACE] as string );

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

