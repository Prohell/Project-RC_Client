//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_BattleSceneConfig
{ private const string TAB_FILE_DATA = "Tables/BattleSceneConfig.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_ABORNPOSX=2,
ID_ABORNPOSY,
ID_ABORNPOSZ,
ID_BBORNPOSX,
ID_BBORNPOSY,
ID_BBORNPOSZ,
ID_STARTTIME,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private float m_ABornPosX;
 public float ABornPosX { get{ return m_ABornPosX;}}

private float m_ABornPosY;
 public float ABornPosY { get{ return m_ABornPosY;}}

private float m_ABornPosZ;
 public float ABornPosZ { get{ return m_ABornPosZ;}}

private float m_BBornPosX;
 public float BBornPosX { get{ return m_BBornPosX;}}

private float m_BBornPosY;
 public float BBornPosY { get{ return m_BBornPosY;}}

private float m_BBornPosZ;
 public float BBornPosZ { get{ return m_BBornPosZ;}}

private int m_Id;
 public int Id { get{ return m_Id;}}

private float m_StartTime;
 public float StartTime { get{ return m_StartTime;}}

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
 Tab_BattleSceneConfig _values = new Tab_BattleSceneConfig();
 _values.m_ABornPosX =  Convert.ToSingle(valuesList[(int)_ID.ID_ABORNPOSX] as string );
_values.m_ABornPosY =  Convert.ToSingle(valuesList[(int)_ID.ID_ABORNPOSY] as string );
_values.m_ABornPosZ =  Convert.ToSingle(valuesList[(int)_ID.ID_ABORNPOSZ] as string );
_values.m_BBornPosX =  Convert.ToSingle(valuesList[(int)_ID.ID_BBORNPOSX] as string );
_values.m_BBornPosY =  Convert.ToSingle(valuesList[(int)_ID.ID_BBORNPOSY] as string );
_values.m_BBornPosZ =  Convert.ToSingle(valuesList[(int)_ID.ID_BBORNPOSZ] as string );
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_StartTime =  Convert.ToSingle(valuesList[(int)_ID.ID_STARTTIME] as string );

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

