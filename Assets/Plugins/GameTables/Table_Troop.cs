//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_Troop
{ private const string TAB_FILE_DATA = "Tables/Troop.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_ID,
ID_MODELID=2,
ID_PORTRAIT,
ID_MODELSIZE,
ID_SIZE,
ID_ACTIONID,
ID_VONETYID,
ID_TYPE,
ID_DATAID1,
ID_DATAID2,
ID_DATAID3,
ID_DATAID4,
ID_DATAID5,
ID_DATAID6,
ID_DATAID7,
ID_DATAID8,
ID_DATAID9,
ID_DATAID10,
ID_SOLDIERCPUNT,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ActionId;
 public int ActionId { get{ return m_ActionId;}}

public int getDataIDCount() { return 10; } 
 private int[] m_DataID = new int[10];
 public int GetDataIDbyIndex(int idx) {
 if(idx>=0 && idx<10) return m_DataID[idx];
 return -1;
 }

private int m_Id;
 public int Id { get{ return m_Id;}}

private int m_ModelID;
 public int ModelID { get{ return m_ModelID;}}

private int m_ModelSize;
 public int ModelSize { get{ return m_ModelSize;}}

private string m_Portrait;
 public string Portrait { get{ return m_Portrait;}}

private int m_Size;
 public int Size { get{ return m_Size;}}

private int m_SoldierCpunt;
 public int SoldierCpunt { get{ return m_SoldierCpunt;}}

private int m_Type;
 public int Type { get{ return m_Type;}}

private int m_VonetyID;
 public int VonetyID { get{ return m_VonetyID;}}

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
 Tab_Troop _values = new Tab_Troop();
 _values.m_ActionId =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIONID] as string);
_values.m_DataID [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID1] as string);
_values.m_DataID [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID2] as string);
_values.m_DataID [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID3] as string);
_values.m_DataID [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID4] as string);
_values.m_DataID [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID5] as string);
_values.m_DataID [ 5 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID6] as string);
_values.m_DataID [ 6 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID7] as string);
_values.m_DataID [ 7 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID8] as string);
_values.m_DataID [ 8 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID9] as string);
_values.m_DataID [ 9 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DATAID10] as string);
_values.m_Id =  Convert.ToInt32(valuesList[(int)_ID.ID_ID] as string);
_values.m_ModelID =  Convert.ToInt32(valuesList[(int)_ID.ID_MODELID] as string);
_values.m_ModelSize =  Convert.ToInt32(valuesList[(int)_ID.ID_MODELSIZE] as string);
_values.m_Portrait =  valuesList[(int)_ID.ID_PORTRAIT] as string;
_values.m_Size =  Convert.ToInt32(valuesList[(int)_ID.ID_SIZE] as string);
_values.m_SoldierCpunt =  Convert.ToInt32(valuesList[(int)_ID.ID_SOLDIERCPUNT] as string);
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);
_values.m_VonetyID =  Convert.ToInt32(valuesList[(int)_ID.ID_VONETYID] as string);

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

