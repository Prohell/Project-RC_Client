//This code create by CodeEngine ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;
 using UnityEngine;
 using System.IO;

namespace GCGame.Table{
public delegate void SerializableTable(string[] valuesList, int skey, Dictionary<int, List<object> > _hash);
 public class TableManager
 {
 private static string GetLoadPath(string localName)
 {
 string localPath = Application.persistentDataPath + "/Tables/" + localName + ".txt";
 if (File.Exists(localPath))
 {
 return localPath;
 }
 #if UNITY_EDITOR
 return Application.dataPath + "/Resources/Build/Load_Table/S_Table/" + localName + ".txt";
 #endif
 return null;
 }
 private static string[] MySplit(string str, string[] nTypeList, string regix)
 {
 if (string.IsNullOrEmpty(str))
 {
 return null;
 }
 String[] content = new String[nTypeList.Length];
 int nIndex = 0;
 int nstartPos = 0;
 while (nstartPos <= str.Length)
 {
 int nsPos = str.IndexOf(regix, nstartPos);
 if (nsPos < 0)
 {
 String lastdataString = str.Substring(nstartPos);
 if (string.IsNullOrEmpty(lastdataString) && nTypeList[nIndex].ToLower() != "string")
 {
 content[nIndex++] = "--";
 }
 else
 {
 content[nIndex++] = lastdataString;
 }
 break;
 }
 else
 {
 if (nstartPos == nsPos)
 {
 if (nTypeList[nIndex].ToLower() != "string")
 {
 content[nIndex++] = "--";
 }
 else
 {
 content[nIndex++] = "";
 }
 }
 else
 {
 content[nIndex++] = str.Substring(nstartPos, nsPos - nstartPos);
 }
 nstartPos = nsPos + 1;
 }
 }
 return content;
 }
 public static bool ReaderPList(String xmlFile, SerializableTable _fun, Dictionary<int, List<object> > _hash)
 {
 string m_Key="";
 string[] m_Value=null;
 string[] list= xmlFile.Split('.'); 
 string relTablePath = list[0].Substring(7);
 string tableFilePath = GetLoadPath(relTablePath);
 string[] alldataRow;
 if (File.Exists(tableFilePath))
 {
 StreamReader sr = null;
 sr = File.OpenText(tableFilePath);
 string tableData = sr.ReadToEnd();
 sr.Close();
 alldataRow = tableData.Split('\n');
 sr.Dispose(); }
 else
 {
 TextAsset testAsset = Resources.Load(list[0], typeof(TextAsset)) as TextAsset;
 alldataRow = testAsset.text.Split('\n');
 }
 //skip fort three
 int skip = 0;
 string[] typeList = null;
 foreach (string line in alldataRow)
 {
 int nKey = -1;
 if (skip == 1)
 {
 string sztemp = line;
 if (sztemp.Length >= 1)
 {
 if (sztemp[sztemp.Length - 1] == '\r')
 {
 sztemp = sztemp.TrimEnd('\r');
 }
 }
 typeList = line.Split('\t');
 m_Value = new string[typeList.Length];
 ++skip;
 continue;
 }
 if (++skip < 4) continue;
 if (String.IsNullOrEmpty(line)) continue;
 if(line[0] == '#')continue;
 string szlinetemp = line;
 if (szlinetemp.Length >= 1)
 {
 if (szlinetemp[szlinetemp.Length - 1] == '\r')
 {
 szlinetemp = szlinetemp.TrimEnd('\r');
 }
 }
 string[] strCol = MySplit(szlinetemp, typeList, "\t");
 if (strCol.Length == 0) continue;
 string skey = strCol[0];
 string[] valuesList = new string[strCol.Length];

 if (string.IsNullOrEmpty(skey) || skey.Equals("--"))
 {
 skey = m_Key;
 nKey = Int32.Parse(skey); 
 valuesList[0] = skey;
 for (int i = 1; i < strCol.Length; ++i)
 {
 if (String.IsNullOrEmpty(strCol[i]) || strCol[i]=="--")
 { valuesList[i] = m_Value[i];
 }
 else
 {
 valuesList[i] = strCol[i];
 m_Value[i] = strCol[i];
 }
 }

 } else
 {
 m_Key = skey; 
 nKey = Int32.Parse(skey); 
 
 for (int i = 0; i < strCol.Length; ++i)

 { if (strCol[i] == "--")
 {
 valuesList[i] = "0";
 m_Value[i] = "0";
 }
 else
 {
 valuesList[i] = strCol[i];
 m_Value[i] = strCol[i];
 }
 }
 }
 _fun(valuesList, nKey, _hash);
 }
 return true;
 }

private static Dictionary<int, List<Tab_AnimationEvent> > g_AnimationEvent = new Dictionary<int, List<Tab_AnimationEvent> >(); 
 public static bool InitTable_AnimationEvent()
 {
 g_AnimationEvent.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_AnimationEvent.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_AnimationEvent> values = new List<Tab_AnimationEvent>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_AnimationEvent)subit);
 }
 g_AnimationEvent.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_BuildBarracks> > g_BuildBarracks = new Dictionary<int, List<Tab_BuildBarracks> >(); 
 public static bool InitTable_BuildBarracks()
 {
 g_BuildBarracks.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_BuildBarracks.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_BuildBarracks> values = new List<Tab_BuildBarracks>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_BuildBarracks)subit);
 }
 g_BuildBarracks.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_CityBuildingDefault> > g_CityBuildingDefault = new Dictionary<int, List<Tab_CityBuildingDefault> >(); 
 public static bool InitTable_CityBuildingDefault()
 {
 g_CityBuildingDefault.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_CityBuildingDefault.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_CityBuildingDefault> values = new List<Tab_CityBuildingDefault>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_CityBuildingDefault)subit);
 }
 g_CityBuildingDefault.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_CityBuildingLevel> > g_CityBuildingLevel = new Dictionary<int, List<Tab_CityBuildingLevel> >(); 
 public static bool InitTable_CityBuildingLevel()
 {
 g_CityBuildingLevel.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_CityBuildingLevel.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_CityBuildingLevel> values = new List<Tab_CityBuildingLevel>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_CityBuildingLevel)subit);
 }
 g_CityBuildingLevel.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_CityBuildingSlot> > g_CityBuildingSlot = new Dictionary<int, List<Tab_CityBuildingSlot> >(); 
 public static bool InitTable_CityBuildingSlot()
 {
 g_CityBuildingSlot.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_CityBuildingSlot.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_CityBuildingSlot> values = new List<Tab_CityBuildingSlot>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_CityBuildingSlot)subit);
 }
 g_CityBuildingSlot.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_Hero> > g_Hero = new Dictionary<int, List<Tab_Hero> >(); 
 public static bool InitTable_Hero()
 {
 g_Hero.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_Hero.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_Hero> values = new List<Tab_Hero>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_Hero)subit);
 }
 g_Hero.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_PVETile> > g_PVETile = new Dictionary<int, List<Tab_PVETile> >(); 
 public static bool InitTable_PVETile()
 {
 g_PVETile.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_PVETile.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_PVETile> values = new List<Tab_PVETile>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_PVETile)subit);
 }
 g_PVETile.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_RoleBaseAttr> > g_RoleBaseAttr = new Dictionary<int, List<Tab_RoleBaseAttr> >(); 
 public static bool InitTable_RoleBaseAttr()
 {
 g_RoleBaseAttr.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_RoleBaseAttr.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_RoleBaseAttr> values = new List<Tab_RoleBaseAttr>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_RoleBaseAttr)subit);
 }
 g_RoleBaseAttr.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_SceneClass> > g_SceneClass = new Dictionary<int, List<Tab_SceneClass> >(); 
 public static bool InitTable_SceneClass()
 {
 g_SceneClass.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_SceneClass.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_SceneClass> values = new List<Tab_SceneClass>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_SceneClass)subit);
 }
 g_SceneClass.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_SkillEx> > g_SkillEx = new Dictionary<int, List<Tab_SkillEx> >(); 
 public static bool InitTable_SkillEx()
 {
 g_SkillEx.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_SkillEx.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_SkillEx> values = new List<Tab_SkillEx>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_SkillEx)subit);
 }
 g_SkillEx.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_SkillTemplate> > g_SkillTemplate = new Dictionary<int, List<Tab_SkillTemplate> >(); 
 public static bool InitTable_SkillTemplate()
 {
 g_SkillTemplate.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_SkillTemplate.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_SkillTemplate> values = new List<Tab_SkillTemplate>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_SkillTemplate)subit);
 }
 g_SkillTemplate.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_TeamConfig> > g_TeamConfig = new Dictionary<int, List<Tab_TeamConfig> >(); 
 public static bool InitTable_TeamConfig()
 {
 g_TeamConfig.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_TeamConfig.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_TeamConfig> values = new List<Tab_TeamConfig>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_TeamConfig)subit);
 }
 g_TeamConfig.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_Troop> > g_Troop = new Dictionary<int, List<Tab_Troop> >(); 
 public static bool InitTable_Troop()
 {
 g_Troop.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_Troop.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_Troop> values = new List<Tab_Troop>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_Troop)subit);
 }
 g_Troop.Add(kv.Key, values);
 }
 return true;
 }
private static Dictionary<int, List<Tab_UnitTemplate> > g_UnitTemplate = new Dictionary<int, List<Tab_UnitTemplate> >(); 
 public static bool InitTable_UnitTemplate()
 {
 g_UnitTemplate.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_UnitTemplate.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_UnitTemplate> values = new List<Tab_UnitTemplate>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_UnitTemplate)subit);
 }
 g_UnitTemplate.Add(kv.Key, values);
 }
 return true;
 }
public bool InitTable()
 {
 bool bRet=true;
 bRet &= InitTable_AnimationEvent();

bRet &= InitTable_BuildBarracks();

bRet &= InitTable_CityBuildingDefault();

bRet &= InitTable_CityBuildingLevel();

bRet &= InitTable_CityBuildingSlot();

bRet &= InitTable_Hero();

bRet &= InitTable_PVETile();

bRet &= InitTable_RoleBaseAttr();

bRet &= InitTable_SceneClass();

bRet &= InitTable_SkillEx();

bRet &= InitTable_SkillTemplate();

bRet &= InitTable_TeamConfig();

bRet &= InitTable_Troop();

bRet &= InitTable_UnitTemplate();


 return bRet;
 }

public static List<Tab_AnimationEvent> GetAnimationEventByID(int nKey)
 {
 if(g_AnimationEvent.Count==0)
 {
 InitTable_AnimationEvent();
 }
 if( g_AnimationEvent.ContainsKey(nKey))
 {
 return g_AnimationEvent[nKey];
 }
 return null;
 }
 public static Tab_AnimationEvent GetAnimationEventByID(int nKey, int nIndex)
 {
 if(g_AnimationEvent.Count==0)
 {
 InitTable_AnimationEvent();
 }
 if( g_AnimationEvent.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_AnimationEvent[nKey].Count)
 return g_AnimationEvent[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_AnimationEvent> > GetAnimationEvent()
 {
 if(g_AnimationEvent.Count==0)
 {
 InitTable_AnimationEvent();
 }
 return g_AnimationEvent;
 }

public static List<Tab_BuildBarracks> GetBuildBarracksByID(int nKey)
 {
 if(g_BuildBarracks.Count==0)
 {
 InitTable_BuildBarracks();
 }
 if( g_BuildBarracks.ContainsKey(nKey))
 {
 return g_BuildBarracks[nKey];
 }
 return null;
 }
 public static Tab_BuildBarracks GetBuildBarracksByID(int nKey, int nIndex)
 {
 if(g_BuildBarracks.Count==0)
 {
 InitTable_BuildBarracks();
 }
 if( g_BuildBarracks.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_BuildBarracks[nKey].Count)
 return g_BuildBarracks[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_BuildBarracks> > GetBuildBarracks()
 {
 if(g_BuildBarracks.Count==0)
 {
 InitTable_BuildBarracks();
 }
 return g_BuildBarracks;
 }

public static List<Tab_CityBuildingDefault> GetCityBuildingDefaultByID(int nKey)
 {
 if(g_CityBuildingDefault.Count==0)
 {
 InitTable_CityBuildingDefault();
 }
 if( g_CityBuildingDefault.ContainsKey(nKey))
 {
 return g_CityBuildingDefault[nKey];
 }
 return null;
 }
 public static Tab_CityBuildingDefault GetCityBuildingDefaultByID(int nKey, int nIndex)
 {
 if(g_CityBuildingDefault.Count==0)
 {
 InitTable_CityBuildingDefault();
 }
 if( g_CityBuildingDefault.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_CityBuildingDefault[nKey].Count)
 return g_CityBuildingDefault[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_CityBuildingDefault> > GetCityBuildingDefault()
 {
 if(g_CityBuildingDefault.Count==0)
 {
 InitTable_CityBuildingDefault();
 }
 return g_CityBuildingDefault;
 }

public static List<Tab_CityBuildingLevel> GetCityBuildingLevelByID(int nKey)
 {
 if(g_CityBuildingLevel.Count==0)
 {
 InitTable_CityBuildingLevel();
 }
 if( g_CityBuildingLevel.ContainsKey(nKey))
 {
 return g_CityBuildingLevel[nKey];
 }
 return null;
 }
 public static Tab_CityBuildingLevel GetCityBuildingLevelByID(int nKey, int nIndex)
 {
 if(g_CityBuildingLevel.Count==0)
 {
 InitTable_CityBuildingLevel();
 }
 if( g_CityBuildingLevel.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_CityBuildingLevel[nKey].Count)
 return g_CityBuildingLevel[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_CityBuildingLevel> > GetCityBuildingLevel()
 {
 if(g_CityBuildingLevel.Count==0)
 {
 InitTable_CityBuildingLevel();
 }
 return g_CityBuildingLevel;
 }

public static List<Tab_CityBuildingSlot> GetCityBuildingSlotByID(int nKey)
 {
 if(g_CityBuildingSlot.Count==0)
 {
 InitTable_CityBuildingSlot();
 }
 if( g_CityBuildingSlot.ContainsKey(nKey))
 {
 return g_CityBuildingSlot[nKey];
 }
 return null;
 }
 public static Tab_CityBuildingSlot GetCityBuildingSlotByID(int nKey, int nIndex)
 {
 if(g_CityBuildingSlot.Count==0)
 {
 InitTable_CityBuildingSlot();
 }
 if( g_CityBuildingSlot.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_CityBuildingSlot[nKey].Count)
 return g_CityBuildingSlot[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_CityBuildingSlot> > GetCityBuildingSlot()
 {
 if(g_CityBuildingSlot.Count==0)
 {
 InitTable_CityBuildingSlot();
 }
 return g_CityBuildingSlot;
 }

public static List<Tab_Hero> GetHeroByID(int nKey)
 {
 if(g_Hero.Count==0)
 {
 InitTable_Hero();
 }
 if( g_Hero.ContainsKey(nKey))
 {
 return g_Hero[nKey];
 }
 return null;
 }
 public static Tab_Hero GetHeroByID(int nKey, int nIndex)
 {
 if(g_Hero.Count==0)
 {
 InitTable_Hero();
 }
 if( g_Hero.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_Hero[nKey].Count)
 return g_Hero[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_Hero> > GetHero()
 {
 if(g_Hero.Count==0)
 {
 InitTable_Hero();
 }
 return g_Hero;
 }

public static List<Tab_PVETile> GetPVETileByID(int nKey)
 {
 if(g_PVETile.Count==0)
 {
 InitTable_PVETile();
 }
 if( g_PVETile.ContainsKey(nKey))
 {
 return g_PVETile[nKey];
 }
 return null;
 }
 public static Tab_PVETile GetPVETileByID(int nKey, int nIndex)
 {
 if(g_PVETile.Count==0)
 {
 InitTable_PVETile();
 }
 if( g_PVETile.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_PVETile[nKey].Count)
 return g_PVETile[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_PVETile> > GetPVETile()
 {
 if(g_PVETile.Count==0)
 {
 InitTable_PVETile();
 }
 return g_PVETile;
 }

public static List<Tab_RoleBaseAttr> GetRoleBaseAttrByID(int nKey)
 {
 if(g_RoleBaseAttr.Count==0)
 {
 InitTable_RoleBaseAttr();
 }
 if( g_RoleBaseAttr.ContainsKey(nKey))
 {
 return g_RoleBaseAttr[nKey];
 }
 return null;
 }
 public static Tab_RoleBaseAttr GetRoleBaseAttrByID(int nKey, int nIndex)
 {
 if(g_RoleBaseAttr.Count==0)
 {
 InitTable_RoleBaseAttr();
 }
 if( g_RoleBaseAttr.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_RoleBaseAttr[nKey].Count)
 return g_RoleBaseAttr[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_RoleBaseAttr> > GetRoleBaseAttr()
 {
 if(g_RoleBaseAttr.Count==0)
 {
 InitTable_RoleBaseAttr();
 }
 return g_RoleBaseAttr;
 }

public static List<Tab_SceneClass> GetSceneClassByID(int nKey)
 {
 if(g_SceneClass.Count==0)
 {
 InitTable_SceneClass();
 }
 if( g_SceneClass.ContainsKey(nKey))
 {
 return g_SceneClass[nKey];
 }
 return null;
 }
 public static Tab_SceneClass GetSceneClassByID(int nKey, int nIndex)
 {
 if(g_SceneClass.Count==0)
 {
 InitTable_SceneClass();
 }
 if( g_SceneClass.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_SceneClass[nKey].Count)
 return g_SceneClass[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_SceneClass> > GetSceneClass()
 {
 if(g_SceneClass.Count==0)
 {
 InitTable_SceneClass();
 }
 return g_SceneClass;
 }

public static List<Tab_SkillEx> GetSkillExByID(int nKey)
 {
 if(g_SkillEx.Count==0)
 {
 InitTable_SkillEx();
 }
 if( g_SkillEx.ContainsKey(nKey))
 {
 return g_SkillEx[nKey];
 }
 return null;
 }
 public static Tab_SkillEx GetSkillExByID(int nKey, int nIndex)
 {
 if(g_SkillEx.Count==0)
 {
 InitTable_SkillEx();
 }
 if( g_SkillEx.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_SkillEx[nKey].Count)
 return g_SkillEx[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_SkillEx> > GetSkillEx()
 {
 if(g_SkillEx.Count==0)
 {
 InitTable_SkillEx();
 }
 return g_SkillEx;
 }

public static List<Tab_SkillTemplate> GetSkillTemplateByID(int nKey)
 {
 if(g_SkillTemplate.Count==0)
 {
 InitTable_SkillTemplate();
 }
 if( g_SkillTemplate.ContainsKey(nKey))
 {
 return g_SkillTemplate[nKey];
 }
 return null;
 }
 public static Tab_SkillTemplate GetSkillTemplateByID(int nKey, int nIndex)
 {
 if(g_SkillTemplate.Count==0)
 {
 InitTable_SkillTemplate();
 }
 if( g_SkillTemplate.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_SkillTemplate[nKey].Count)
 return g_SkillTemplate[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_SkillTemplate> > GetSkillTemplate()
 {
 if(g_SkillTemplate.Count==0)
 {
 InitTable_SkillTemplate();
 }
 return g_SkillTemplate;
 }

public static List<Tab_TeamConfig> GetTeamConfigByID(int nKey)
 {
 if(g_TeamConfig.Count==0)
 {
 InitTable_TeamConfig();
 }
 if( g_TeamConfig.ContainsKey(nKey))
 {
 return g_TeamConfig[nKey];
 }
 return null;
 }
 public static Tab_TeamConfig GetTeamConfigByID(int nKey, int nIndex)
 {
 if(g_TeamConfig.Count==0)
 {
 InitTable_TeamConfig();
 }
 if( g_TeamConfig.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_TeamConfig[nKey].Count)
 return g_TeamConfig[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_TeamConfig> > GetTeamConfig()
 {
 if(g_TeamConfig.Count==0)
 {
 InitTable_TeamConfig();
 }
 return g_TeamConfig;
 }

public static List<Tab_Troop> GetTroopByID(int nKey)
 {
 if(g_Troop.Count==0)
 {
 InitTable_Troop();
 }
 if( g_Troop.ContainsKey(nKey))
 {
 return g_Troop[nKey];
 }
 return null;
 }
 public static Tab_Troop GetTroopByID(int nKey, int nIndex)
 {
 if(g_Troop.Count==0)
 {
 InitTable_Troop();
 }
 if( g_Troop.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_Troop[nKey].Count)
 return g_Troop[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_Troop> > GetTroop()
 {
 if(g_Troop.Count==0)
 {
 InitTable_Troop();
 }
 return g_Troop;
 }

public static List<Tab_UnitTemplate> GetUnitTemplateByID(int nKey)
 {
 if(g_UnitTemplate.Count==0)
 {
 InitTable_UnitTemplate();
 }
 if( g_UnitTemplate.ContainsKey(nKey))
 {
 return g_UnitTemplate[nKey];
 }
 return null;
 }
 public static Tab_UnitTemplate GetUnitTemplateByID(int nKey, int nIndex)
 {
 if(g_UnitTemplate.Count==0)
 {
 InitTable_UnitTemplate();
 }
 if( g_UnitTemplate.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_UnitTemplate[nKey].Count)
 return g_UnitTemplate[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_UnitTemplate> > GetUnitTemplate()
 {
 if(g_UnitTemplate.Count==0)
 {
 InitTable_UnitTemplate();
 }
 return g_UnitTemplate;
 }


}
}

