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
private static Dictionary<int, List<Tab_BattleSceneConfig> > g_BattleSceneConfig = new Dictionary<int, List<Tab_BattleSceneConfig> >(); 
 public static bool InitTable_BattleSceneConfig()
 {
 g_BattleSceneConfig.Clear();
 Dictionary<int, List<object> > tmps = new Dictionary<int, List<object> >();
 if (!Tab_BattleSceneConfig.LoadTable(tmps)) return false;
 foreach (KeyValuePair<int, List<object> > kv in tmps)
 {
 List<Tab_BattleSceneConfig> values = new List<Tab_BattleSceneConfig>();
 foreach (object subit in kv.Value)
 {
 values.Add((Tab_BattleSceneConfig)subit);
 }
 g_BattleSceneConfig.Add(kv.Key, values);
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

bRet &= InitTable_BattleSceneConfig();

bRet &= InitTable_PVETile();

bRet &= InitTable_TeamConfig();

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

public static List<Tab_BattleSceneConfig> GetBattleSceneConfigByID(int nKey)
 {
 if(g_BattleSceneConfig.Count==0)
 {
 InitTable_BattleSceneConfig();
 }
 if( g_BattleSceneConfig.ContainsKey(nKey))
 {
 return g_BattleSceneConfig[nKey];
 }
 return null;
 }
 public static Tab_BattleSceneConfig GetBattleSceneConfigByID(int nKey, int nIndex)
 {
 if(g_BattleSceneConfig.Count==0)
 {
 InitTable_BattleSceneConfig();
 }
 if( g_BattleSceneConfig.ContainsKey(nKey))
 {
 if(nIndex>=0 && nIndex<g_BattleSceneConfig[nKey].Count)
 return g_BattleSceneConfig[nKey][nIndex];
 }
 return null;
 }
 public static Dictionary<int, List<Tab_BattleSceneConfig> > GetBattleSceneConfig()
 {
 if(g_BattleSceneConfig.Count==0)
 {
 InitTable_BattleSceneConfig();
 }
 return g_BattleSceneConfig;
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

