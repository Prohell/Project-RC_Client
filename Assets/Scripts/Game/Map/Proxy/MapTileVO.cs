using UnityEngine;
using System;
using System.Collections.Generic;
/// <summary>
/// 地块数据结构
/// </summary>
public class MapTileVO : IDisposable
{
    /// <summary>
    /// 坐标
    /// </summary>
    public Coord coord;
    /// <summary>
    /// 地表贴图
    /// </summary>
    public byte mat;
    /// <summary>
    /// 地表高度
    /// </summary>
    public byte height;
    /// <summary>
    /// 地块类型，标识地块行走、建城属性
    /// </summary>
    public MapTileType type;
    /// <summary>
    /// 障碍物类型
    /// </summary>
    public MapBlockType blockType;
    /// <summary>
    /// 区域索引
    /// </summary>
    public int camp; // if block type is forest, specify the camp around the forest
    /// <summary>
    /// 区域等级
    /// </summary>
    public int level;
    /// <summary>
    /// pve等级，暂时不用
    /// </summary>
    public int pveLevel;
    /// <summary>
    /// 区域等级相关索引，暂时不用
    /// </summary>
    public int camp1; // if block type is mountain, specify the camp at the other side of the mountain, 4 for neuatral

    public void Dispose() { }

    public int GetTileType()
    {
        return (int)type;
    }

    #region Encode & Decode
    static bool mDecodeTestMode = true;
    public static uint WayPointEncodeLocal(MapTileVO tile)
    {
        uint re = 0;
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.coord.x) << 16);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.coord.y) << 4);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.camp) << 0);
        return re;
    }

    public static MapTileVO WayPointDecodeLocal(uint bin)
    {
        MapTileVO tile = new MapTileVO();
        tile.camp = Convert.ToInt32((bin & 0xf) >> 0);
        Coord coord = new Coord();
        coord.y = Convert.ToInt32((bin & 0xfff0) >> 4);
        coord.x = Convert.ToInt32((bin & 0xfff0000) >> 16);
        tile.coord = coord;
        return tile;
    }

    public static uint TileEncodeLocal(MapTileVO tile)
    {
        // mat-height-TileType-TileCamp-TileCamp1-TileLevel-PvELevel-BlockType
        //  4b-  4b  -   4b   -   4b   -    4b   -   4b    -   4b   -   4b   
        uint re = 0;
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.mat) << 28);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.height) << 24);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.type) << 20);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.camp) << 16);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.camp1) << 12);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.level) << 8);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.pveLevel) << 4);
        re |= Convert.ToUInt32(Convert.ToUInt32(tile.blockType) << 0);
        return re;
    }

    public static MapTileVO TileDecodeLocal(uint bin, Coord coord)
    {
        // mat-height-TileType-TileCamp-TileCamp1-TileLevel-PvELevel-BlockType
        //  4b-  4b  -   4b   -   4b   -    4b   -   4b    -   4b   -   4b   
        MapTileVO tile = new MapTileVO();
        if(mDecodeTestMode)
        {
            tile.mat = 1;
            tile.height = MapConst.MAP_HEIGHT_HORIZON;
            tile.type = MapTileType.Normal;
            tile.camp = 0;
            tile.camp1 = 0;
            tile.level = 0;
            tile.pveLevel = 0;
            tile.blockType = MapBlockType.None;
        }
        else
        {
            tile.mat = Convert.ToByte((bin & 0xf0000000) >> 28);
            tile.height = Convert.ToByte((bin & 0xf000000) >> 24);
            tile.type = (MapTileType)Convert.ToInt32((bin & 0xf00000) >> 20);
            tile.camp = Convert.ToInt32((bin & 0xf0000) >> 16);
            tile.camp1 = Convert.ToInt32((bin & 0xf000) >> 12);
            tile.level = Convert.ToInt32((bin & 0xf00) >> 8);
            tile.pveLevel = Convert.ToInt32((bin & 0xf0) >> 4);
            tile.blockType = (MapBlockType)Convert.ToInt32((bin & 0xf) >> 0);
        }
        tile.coord = coord;
        return tile;
    }

    public static Color ColorDecode(ushort bin)
    {
        Color re = new Color();
        re.r = ((bin & Convert.ToInt16("1111100000000000", 2)) >> 11) / 31f;
        re.g = ((bin & Convert.ToInt16("0000011111000000", 2)) >> 6) / 31f;
        re.b = ((bin & Convert.ToInt16("0000000000111110", 2)) >> 1) / 31f;
        re.a = 1f;
        return re;
    }

    public static ushort ColorEncode(Color color)
    {
        int r = Mathf.Clamp(Mathf.RoundToInt(color.r * 31), 0, 31);
        int g = Mathf.Clamp(Mathf.RoundToInt(color.g * 31), 0, 31);
        int b = Mathf.Clamp(Mathf.RoundToInt(color.b * 31), 0, 31);
        return (ushort)((r << 11) | (g << 6) | (b << 1));
    }
    #endregion
    //	public const int MAP_CMAP_N = 0;
    //	public const int MAP_CMAP_W = 1;
    //	public const int MAP_CMAP_E = 2;
    //	public const int MAP_CMAP_S = 3;
    public static readonly List<int> OpenedCamp = new List<int>() { 0, 1, 2, 3, 4 };
    public static readonly string[] CampName = new string[] { "north", "west", "east", "south" };
    public static string Camp2String(int camp)
    {
        if (camp >= 0 && camp < 4)
        {
            return CampName[camp];
        }
        else
        {
            Debug.Log("camp = " + camp);
            return "north";
        }
    }
}
