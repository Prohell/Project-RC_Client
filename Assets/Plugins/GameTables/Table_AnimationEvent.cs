//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_AnimationEvent
{ private const string TAB_FILE_DATA = "Tables/AnimationEvent.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_FUNCTIONNAME=2,
ID_ANIMATIONCLIPNAME,
ID_PREFABNAME,
ID_TIME,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_AnimationClipName;
 public string AnimationClipName { get{ return m_AnimationClipName;}}

private string m_FunctionName;
 public string FunctionName { get{ return m_FunctionName;}}

private int m_Id;
 public int Id { get{ return m_Id;}}

private string m_PrefabName;
 public string PrefabName { get{ return m_PrefabName;}}

private float m_Time;
 public float Time { get{ return m_Time;}}

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
 Tab_AnimationEvent _values = new Tab_AnimationEvent();
 _values.m_AnimationClipName =  valuesList[(int)_ID.ID_ANIMATIONCLIPNAME] as string;
_values.m_FunctionName =  valuesList[(int)_ID.ID_FUNCTIONNAME] as string;
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_PrefabName =  valuesList[(int)_ID.ID_PREFABNAME] as string;
_values.m_Time =  Convert.ToSingle(valuesList[(int)_ID.ID_TIME] as string );

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

