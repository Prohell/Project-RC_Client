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
ID_SQUADL=2,
ID_SQUADH,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Id;
 public int Id { get{ return m_Id;}}

private float m_SquadH;
 public float SquadH { get{ return m_SquadH;}}

private float m_SquadL;
 public float SquadL { get{ return m_SquadL;}}

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
_values.m_SquadH =  Convert.ToSingle(valuesList[(int)_ID.ID_SQUADH] as string );
_values.m_SquadL =  Convert.ToSingle(valuesList[(int)_ID.ID_SQUADL] as string );

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

