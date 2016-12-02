#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using GCGame.Table;

public class MapTileGeneratorRun : EditorWindow
{


    //public static MapTileVO[,] GeneratePVETiles(MapTileVO[,] tileVOs){
    //       // Generate PvETiles from GDS
    //       var pveStats = TableManager.GetPVETile();
    //       // Generate tileVOs Mapping
    //       Dictionary<int, Dictionary<int, List<string>>> tileMapping = new Dictionary<int, Dictionary<int, List<string>>>(); // TileType: TileCamp -> x_y

    //	for (int y=0; y<MapConst.MAP_HEIGHT; ++y) {
    //		for (int x=0; x<MapConst.MAP_WIDTH; ++x) {
    //			var tileVO = tileVOs[y, x] as MapTileVO;
    //			var tileType = Convert.ToInt32(tileVO.type);
    //			var tileLevel = Convert.ToInt32(tileVO.level);
    //			if (tileVO.IsNeutral()) {
    //				tileType = 10;
    //				tileLevel = 0;
    //			}
    //			if(tileMapping.ContainsKey(tileType) == false) {
    //				tileMapping[tileType] = new Dictionary<int, List<string>>();
    //			}

    //			if (tileMapping[tileType].ContainsKey(tileLevel) == false) {
    //				tileMapping[tileType][tileLevel] = new List<string>();
    //			}

    //			tileMapping[tileType][tileLevel].Add(String.Join("_", new string[]{x.ToString(), y.ToString()}));
    //		}
    //	}


    //	// Generate PvE tile
    //	foreach(List<Tab_PVETile> pveTiles in pveStats.Values)
    //       {
    //           if (pveTiles.Count <= 0) continue;
    //           Tab_PVETile pveTile = pveTiles[0];
    //		System.Random random = new System.Random();

    //		List<string> newList = new List<string>();
    //		try{
    //			var tileType = Convert.ToInt32(pveTile.map_region);
    //			var tileLevel = Convert.ToInt32(pveTile.camp_region);
    //			if (tileType == 4) { // Former Neutral Tiles
    //				tileType = 10;
    //				tileLevel = 0;
    //			}
    //			foreach (string coordXY in tileMapping[tileType][tileLevel])
    //			{
    //				newList.Insert(random.Next(newList.Count), coordXY);
    //			}

    //			var totalQty = pveTile.total_quantity;
    //			int i = 0;
    //			while (i < totalQty) {
    //				string[] coords = newList[i].Split('_');
    //				if (tileVOs[Convert.ToInt32(coords[1]), Convert.ToInt32(coords[0])].type != MapTileType.PVE) {
    //					tileVOs[Convert.ToInt32(coords[1]), Convert.ToInt32(coords[0])].pveLevel = pveTile.pve_tile_level;
    //					tileVOs[Convert.ToInt32(coords[1]), Convert.ToInt32(coords[0])].type = MapTileType.PVE;
    //					tileVOs[Convert.ToInt32(coords[1]), Convert.ToInt32(coords[0])].camp1 = random.Next(3);
    //					i++;
    //				} else {
    //					i++;
    //					totalQty++;
    //				}
    //			}

    //		} catch(Exception e) {
    //			Debug.LogWarning(e);
    //			Debug.LogWarning(Newtonsoft.Json.JsonConvert.SerializeObject(pveTile));
    //			Debug.LogWarning(Newtonsoft.Json.JsonConvert.SerializeObject(tileMapping.Keys));
    //		}
    //	}

    //	return tileVOs;
    //}

    public static void GenerateMapTilesLocal(MapTileVO[,] tileVOs, List<MapTileVO> wayPoints)
    {
        // Rewrite Source Map Binary
        FileStream fs = File.Open(Application.dataPath + "/Resources/mapsource.bytes", FileMode.OpenOrCreate, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs, System.Text.Encoding.BigEndianUnicode);

        for (int y = 0; y < MapConst.MAP_HEIGHT; ++y)
        {
            for (int x = 0; x < MapConst.MAP_WIDTH; ++x)
            {
                // mat-height-TileType-TileCamp-TileCamp1-TileLevel-PvELevel-BlockType
                //  4b-  4b  -   4b   -   4b   -    4b   -   4b    -   4b   -   4b   
                try
                {
                    uint tileInfo = MapTileVO.TileEncodeLocal(tileVOs[y, x]);
                    bw.Write(tileInfo);
                }
                catch (Exception e)
                {
                    Debug.Log(tileVOs[y, x].camp);
                    Debug.Log((int)tileVOs[y, x].type);
                    Debug.Log(tileVOs[y, x].level);
                    throw e;
                }
            }
        }
        if (wayPoints != null)
        {
            foreach (MapTileVO wayPoint in wayPoints)
            {
                try
                {
                    uint tileInfo = MapTileVO.WayPointEncodeLocal(wayPoint);
                    bw.Write(tileInfo);
                }
                catch (Exception e)
                {
                    Debug.Log("writing waypoint error: " + wayPoint.coord.x + ", " + wayPoint.coord.y);
                    throw e;
                }
            }
        }
        bw.Close();

        // Rewrite Map Binary with PvE Tiles
        //tileVOs = GeneratePVETiles(tileVOs);

        fs = File.Open(Application.dataPath + "/Resources/mapuint16.bytes", FileMode.OpenOrCreate, FileAccess.Write);
        bw = new BinaryWriter(fs, System.Text.Encoding.BigEndianUnicode);

        for (int y = 0; y < MapConst.MAP_HEIGHT; ++y)
        {
            for (int x = 0; x < MapConst.MAP_WIDTH; ++x)
            {
                // TileType-TileCamp-TileCamp1-TileLevel-PvELevel-BlockType
                //  4b     -   4b   -    4b   -   2b    -   6b   -   2b    
                try
                {
                    UInt32 tileInfo = MapTileVO.TileEncodeLocal(tileVOs[y, x]);
                    bw.Write(tileInfo);
                }
                catch (Exception e)
                {
                    Debug.Log(tileVOs[y, x].camp);
                    Debug.Log((int)tileVOs[y, x].type);
                    Debug.Log(tileVOs[y, x].level);
                    throw e;
                }
            }
        }
        if (wayPoints != null)
        {
            foreach (MapTileVO wayPoint in wayPoints)
            {
                try
                {
                    UInt32 tileInfo = MapTileVO.WayPointEncodeLocal(wayPoint);
                    bw.Write(tileInfo);
                }
                catch (Exception e)
                {
                    Debug.Log("writing waypoint error: " + wayPoint.coord.x + ", " + wayPoint.coord.y);
                    throw e;
                }
            }
        }
        bw.Close();
        AssetDatabase.Refresh();
    }

    public static void DispMapStats(int[,] stats)
    {
        float total = MapConst.MAP_WIDTH * MapConst.MAP_HEIGHT;
        GUILayout.Label("camp\t |  level   |    count    | percent");
        for (int camp = 0; camp < 4; ++camp)
        {
            for (int level = 0; level < 3; ++level)
            {
                GUILayout.Label(string.Format("{0:D4}", camp) + " " + MapTileVO.Camp2String(camp).Substring(0, 4) + "\t" + " | " +
                                string.Format("{0:D5}", level) + " | " +
                                string.Format("{0:D7}", stats[camp, level]) + " | " +
                                stats[camp, level] / total * 100);
            }
        }
        GUILayout.Label("strait     | " +
                        string.Format("{0:D7}", stats[4, 0]) + " | " +
                        stats[4, 0] / total * 100);
        GUILayout.Label("block     | " +
                        string.Format("{0:D7}", stats[4, 1]) + " | " +
                        stats[4, 1] / total * 100);
        GUILayout.Label("neutral  | " +
                        string.Format("{0:D7}", stats[4, 2]) + " | " +
                        stats[4, 2] / total * 100);
    }

    public static int[,] GetMapTileStats(MapTileVO[,] vos)
    {
        int[,] stats = new int[5, 3];
        //for (int y = 0; y < MapConst.MAP_HEIGHT; ++y)
        //{
        //    for (int x = 0; x < MapConst.MAP_WIDTH; ++x)
        //    {
        //        MapTileVO tileVO = vos[y, x];
        //        if (tileVO.type == MapTileType.Camp && tileVO.camp == Convert.ToInt32(CampType.CAMP_TYPE_NEUTRAL))
        //        {
        //            stats[4, 2] += 1;
        //        }
        //        else if (tileVO.type == MapTileType.Block)
        //        {
        //            stats[4, 1] += 1;
        //        }
        //        else if (tileVO.type == MapTileType.Strait)
        //        {
        //            stats[4, 0] += 1;
        //        }
        //        else if (tileVO.type == MapTileType.Camp)
        //        {
        //            stats[tileVO.camp, tileVO.level] += 1;
        //        }
        //    }
        //}
        return stats;
    }
}
#endif