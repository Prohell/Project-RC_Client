//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{
public class Tab_SceneClass
{ private const string TAB_FILE_DATA = "Tables/SceneClass.txt";
 enum _ID
 {
 INVLAID_INDEX=-1,
ID_SCENEID,
ID_NAME=2,
ID_RESNAME,
ID_TYPE,
ID_LENGTH,
ID_WIDTH,
ID_OBSTACLE,
ID_BGMUSIC,
ID_COPYSCENEID,
ID_SCENEMAPTEXTURE,
ID_SCENEMAPWIDTH,
ID_SCENEMAPHEIGHT,
ID_SCENEMAPLOGICWIDTH,
ID_PLAYERVIEWRADIUS,
ID_ATTACKPOSX1,
ID_ATTACKPOSZ1,
ID_ATTACKPOSX2,
ID_ATTACKPOSZ2,
ID_ATTACKPOSX3,
ID_ATTACKPOSZ3,
ID_ATTACKPOSX4,
ID_ATTACKPOSZ4,
ID_ATTACKPOSX5,
ID_ATTACKPOSZ5,
ID_ATTACKPOSX6,
ID_ATTACKPOSZ6,
ID_ATTACKPOSX7,
ID_ATTACKPOSZ7,
ID_ATTACKPOSX8,
ID_ATTACKPOSZ8,
ID_ATTACKPOSX9,
ID_ATTACKPOSZ9,
ID_ATTACKPOSX10,
ID_ATTACKPOSZ10,
ID_ATTACKPOSX11,
ID_ATTACKPOSZ11,
ID_DEFENCEPOSX1,
ID_DEFENCEPOSZ1,
ID_DEFENCEPOSX2,
ID_DEFENCEPOSZ2,
ID_DEFENCEPOSX3,
ID_DEFENCEPOSZ3,
ID_DEFENCEPOSX4,
ID_DEFENCEPOSZ4,
ID_DEFENCEPOSX5,
ID_DEFENCEPOSZ5,
ID_DEFENCEPOSX6,
ID_DEFENCEPOSZ6,
ID_DEFENCEPOSX7,
ID_DEFENCEPOSZ7,
ID_DEFENCEPOSX8,
ID_DEFENCEPOSZ8,
ID_DEFENCEPOSX9,
ID_DEFENCEPOSZ9,
ID_DEFENCEPOSX10,
ID_DEFENCEPOSZ10,
ID_DEFENCEPOSX11,
ID_DEFENCEPOSZ11,
ID_EDGELENGTH,
MAX_RECORD
 }
 public static string GetInstanceFile(){return TAB_FILE_DATA; }

public int getAttackPosXCount() { return 11; } 
 private float[] m_AttackPosX = new float[11];
 public float GetAttackPosXbyIndex(int idx) {
 if(idx>=0 && idx<11) return m_AttackPosX[idx];
 return 0.0f;
 }

public int getAttackPosZCount() { return 11; } 
 private float[] m_AttackPosZ = new float[11];
 public float GetAttackPosZbyIndex(int idx) {
 if(idx>=0 && idx<11) return m_AttackPosZ[idx];
 return 0.0f;
 }

private int m_BGMusic;
 public int BGMusic { get{ return m_BGMusic;}}

private int m_CopySceneID;
 public int CopySceneID { get{ return m_CopySceneID;}}

public int getDefencePosXCount() { return 11; } 
 private float[] m_DefencePosX = new float[11];
 public float GetDefencePosXbyIndex(int idx) {
 if(idx>=0 && idx<11) return m_DefencePosX[idx];
 return 0.0f;
 }

public int getDefencePosZCount() { return 11; } 
 private float[] m_DefencePosZ = new float[11];
 public float GetDefencePosZbyIndex(int idx) {
 if(idx>=0 && idx<11) return m_DefencePosZ[idx];
 return 0.0f;
 }

private int m_EdgeLength;
 public int EdgeLength { get{ return m_EdgeLength;}}

private int m_Length;
 public int Length { get{ return m_Length;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private string m_Obstacle;
 public string Obstacle { get{ return m_Obstacle;}}

private int m_PlayerViewRadius;
 public int PlayerViewRadius { get{ return m_PlayerViewRadius;}}

private string m_ResName;
 public string ResName { get{ return m_ResName;}}

private int m_SceneID;
 public int SceneID { get{ return m_SceneID;}}

private int m_SceneMapHeight;
 public int SceneMapHeight { get{ return m_SceneMapHeight;}}

private int m_SceneMapLogicWidth;
 public int SceneMapLogicWidth { get{ return m_SceneMapLogicWidth;}}

private string m_SceneMapTexture;
 public string SceneMapTexture { get{ return m_SceneMapTexture;}}

private int m_SceneMapWidth;
 public int SceneMapWidth { get{ return m_SceneMapWidth;}}

private int m_Type;
 public int Type { get{ return m_Type;}}

private int m_Width;
 public int Width { get{ return m_Width;}}

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
 Tab_SceneClass _values = new Tab_SceneClass();
 _values.m_AttackPosX [ 0 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX1] as string );
_values.m_AttackPosX [ 1 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX2] as string );
_values.m_AttackPosX [ 2 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX3] as string );
_values.m_AttackPosX [ 3 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX4] as string );
_values.m_AttackPosX [ 4 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX5] as string );
_values.m_AttackPosX [ 5 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX6] as string );
_values.m_AttackPosX [ 6 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX7] as string );
_values.m_AttackPosX [ 7 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX8] as string );
_values.m_AttackPosX [ 8 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX9] as string );
_values.m_AttackPosX [ 9 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX10] as string );
_values.m_AttackPosX [ 10 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSX11] as string );
_values.m_AttackPosZ [ 0 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ1] as string );
_values.m_AttackPosZ [ 1 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ2] as string );
_values.m_AttackPosZ [ 2 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ3] as string );
_values.m_AttackPosZ [ 3 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ4] as string );
_values.m_AttackPosZ [ 4 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ5] as string );
_values.m_AttackPosZ [ 5 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ6] as string );
_values.m_AttackPosZ [ 6 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ7] as string );
_values.m_AttackPosZ [ 7 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ8] as string );
_values.m_AttackPosZ [ 8 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ9] as string );
_values.m_AttackPosZ [ 9 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ10] as string );
_values.m_AttackPosZ [ 10 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_ATTACKPOSZ11] as string );
_values.m_BGMusic =  Convert.ToInt32(valuesList[(int)_ID.ID_BGMUSIC] as string);
_values.m_CopySceneID =  Convert.ToInt32(valuesList[(int)_ID.ID_COPYSCENEID] as string);
_values.m_DefencePosX [ 0 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX1] as string );
_values.m_DefencePosX [ 1 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX2] as string );
_values.m_DefencePosX [ 2 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX3] as string );
_values.m_DefencePosX [ 3 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX4] as string );
_values.m_DefencePosX [ 4 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX5] as string );
_values.m_DefencePosX [ 5 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX6] as string );
_values.m_DefencePosX [ 6 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX7] as string );
_values.m_DefencePosX [ 7 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX8] as string );
_values.m_DefencePosX [ 8 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX9] as string );
_values.m_DefencePosX [ 9 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX10] as string );
_values.m_DefencePosX [ 10 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSX11] as string );
_values.m_DefencePosZ [ 0 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ1] as string );
_values.m_DefencePosZ [ 1 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ2] as string );
_values.m_DefencePosZ [ 2 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ3] as string );
_values.m_DefencePosZ [ 3 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ4] as string );
_values.m_DefencePosZ [ 4 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ5] as string );
_values.m_DefencePosZ [ 5 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ6] as string );
_values.m_DefencePosZ [ 6 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ7] as string );
_values.m_DefencePosZ [ 7 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ8] as string );
_values.m_DefencePosZ [ 8 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ9] as string );
_values.m_DefencePosZ [ 9 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ10] as string );
_values.m_DefencePosZ [ 10 ] =  Convert.ToSingle(valuesList[(int)_ID.ID_DEFENCEPOSZ11] as string );
_values.m_EdgeLength =  Convert.ToInt32(valuesList[(int)_ID.ID_EDGELENGTH] as string);
_values.m_Length =  Convert.ToInt32(valuesList[(int)_ID.ID_LENGTH] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_Obstacle =  valuesList[(int)_ID.ID_OBSTACLE] as string;
_values.m_PlayerViewRadius =  Convert.ToInt32(valuesList[(int)_ID.ID_PLAYERVIEWRADIUS] as string);
_values.m_ResName =  valuesList[(int)_ID.ID_RESNAME] as string;
_values.m_SceneID =  Convert.ToInt32(valuesList[(int)_ID.ID_SCENEID] as string);
_values.m_SceneMapHeight =  Convert.ToInt32(valuesList[(int)_ID.ID_SCENEMAPHEIGHT] as string);
_values.m_SceneMapLogicWidth =  Convert.ToInt32(valuesList[(int)_ID.ID_SCENEMAPLOGICWIDTH] as string);
_values.m_SceneMapTexture =  valuesList[(int)_ID.ID_SCENEMAPTEXTURE] as string;
_values.m_SceneMapWidth =  Convert.ToInt32(valuesList[(int)_ID.ID_SCENEMAPWIDTH] as string);
_values.m_Type =  Convert.ToInt32(valuesList[(int)_ID.ID_TYPE] as string);
_values.m_Width =  Convert.ToInt32(valuesList[(int)_ID.ID_WIDTH] as string);

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

