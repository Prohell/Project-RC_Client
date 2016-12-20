//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_BuildBarracks
{ private const string TAB_FILE_DATA = "Tables/BuildBarracks.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_NAME=2,
ID_ISUPGRADE,
ID_MAXLEVEL,
ID_UNLOCKLEVEL,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_IsUpgrade;
 public int IsUpgrade { get{ return m_IsUpgrade;}}

private int m_MaxLevel;
 public int MaxLevel { get{ return m_MaxLevel;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

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
 Tab_BuildBarracks _values = new Tab_BuildBarracks();
 _values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_IsUpgrade =  Convert.ToInt32(valuesList[(int)_ID.ID_ISUPGRADE] as string);
_values.m_MaxLevel =  Convert.ToInt32(valuesList[(int)_ID.ID_MAXLEVEL] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
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

