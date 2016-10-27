using System;
using System.Collections.Generic;
using UnityEngine;

public class MapProxy : IProxy
{
    // this array is arranged by coordinate
    private uint[,] _tiles = new uint[MapConst.MAP_WIDTH, MapConst.MAP_HEIGHT];
    /// <summary>
    /// 不同区域之间的通路，先保留
    /// </summary>
    private List<MapTileVO> _wayPoints;
    /// <summary>
    /// 按阵营和等级划分区域存储，快速索引区域，先保留
    /// </summary>
    private Dictionary<int, Rect> _regions = new Dictionary<int, Rect>();
    HashSet<uint> visibleBlockIds = new HashSet<uint>();
    public IMapAStar mapAStar;

    public void OnDestroy()
    {
    }

    public void OnInit()
    {
        TextAsset ta;
        // Source Map Bytes
        if (MapView.Current != null && MapView.Current.EditMode == true)
        {
            ta = Resources.Load("mapsource", typeof(TextAsset)) as TextAsset; // Final Map Bytes
        }
        else
        {
            ta = Resources.Load("mapuint16", typeof(TextAsset)) as TextAsset; // Final Map Bytes
        }

        if (ta)
        {
            byte[] byteTiles = ta.bytes;
            for (int y = 0; y < MapConst.MAP_HEIGHT; ++y)
            {
                for (int x = 0; x < MapConst.MAP_WIDTH; ++x)
                {
                    uint tileBytes = GetTileBytes(x, y, byteTiles);
                    _tiles[y, x] = tileBytes;
                    // 按照等级划分
                    //if ((x & 0x7) == 0 && (y & 0x7) == 0)
                    //{
                    //    using (MapTileVO tile = GetTile(x, y, byteTiles))
                    //    {
                    //        MarkTileDirectionAndLevel(tile);
                    //    }
                    //}
                }
            }
            _wayPoints = GetWayPoints(byteTiles);
        }
        else
        {
            Debug.LogError("Can't Load mapsource");
        }

        //mapAStar = new MapAStarLua();
        //mapAStar = new MapAStar();
        //mapAStar = new MapAStarCompact();
    }

    public MapTileVO[] GetWayPointsByCamp()
    {
        MapTileVO[] wayPoints = new MapTileVO[4];
        foreach (MapTileVO wayPoint in _wayPoints)
        {
            wayPoints[wayPoint.camp] = wayPoint;
        }
        return wayPoints;
    }

    uint GetTileBytes(int x, int y, byte[] bytes)
    {
        int idx = y * MapConst.MAP_HEIGHT + x;
        uint data = 0;
        data |= Convert.ToUInt32(Convert.ToUInt32(bytes[4 * idx + 3]) << 24);
        data |= Convert.ToUInt32(Convert.ToUInt32(bytes[4 * idx + 2]) << 16);
        data |= Convert.ToUInt32(Convert.ToUInt32(bytes[4 * idx + 1]) << 8);
        data |= Convert.ToUInt32(Convert.ToUInt32(bytes[4 * idx]) << 0);
        return data;
    }

    // coordinate x/y and data x/y is opposite because:
    // when encoding to binary, the 1st demension of array represents line while the 2nd represents col, that is,
    // when we say (x, y) in binary data, we mean the xth line and yth col.
    // in the other hand, when we get tile by coord from binary, for example, coord is c,
    // in this case, c.x represents col and c.y represents line
    MapTileVO GetTile(int x, int y, byte[] bytes)
    {
        uint data = GetTileBytes(x, y, bytes);
        MapTileVO re = MapTileVO.TileDecodeLocal(data, new Coord(x, y));
        return re;
    }

    void MarkTileDirectionAndLevel(MapTileVO vo)
    {
        int dl = 0;

        if (vo.camp != 0)
            dl = vo.camp * 10 + vo.level;

        if (_regions.ContainsKey(dl))
        {
            if (_regions[dl].Contains(new Vector2(vo.coord.x, vo.coord.y)))
                ;
            else
            {
                Rect current = _regions[dl];
                if (vo.coord.x < current.x)
                    current.x = vo.coord.x;
                else if (vo.coord.x > current.x + current.width)
                    current.width = vo.coord.x - current.x;
                if (vo.coord.y < current.y)
                    current.y = vo.coord.y;
                else if (vo.coord.y > current.y + current.height)
                    current.height = vo.coord.y - current.y;

                _regions[dl] = current;
            }
        }
        else
        {
            _regions.Add(dl, new Rect(vo.coord.x, vo.coord.y, 1, 1));
        }
    }

    List<MapTileVO> GetWayPoints(byte[] bytes)
    {
        List<MapTileVO> wayPoints = new List<MapTileVO>();
        int idx = 4 * MapConst.MAP_HEIGHT * MapConst.MAP_WIDTH;
        int realBytesLen = MapConst.MAP_HEIGHT * MapConst.MAP_WIDTH * 4 + 4 * 4;
        while (idx + 3 < bytes.Length && idx + 3 < realBytesLen)
        {
            uint data = 0;
            data |= Convert.ToUInt32(Convert.ToUInt32(bytes[idx]) << 0);
            data |= Convert.ToUInt32(Convert.ToUInt32(bytes[idx + 1]) << 8);
            data |= Convert.ToUInt32(Convert.ToUInt32(bytes[idx + 2]) << 16);
            data |= Convert.ToUInt32(Convert.ToUInt32(bytes[idx + 3]) << 24);
            wayPoints.Add(MapTileVO.WayPointDecodeLocal(data));
            idx += 4;
        }
        return wayPoints;
    }

#if UNITY_EDITOR
    private bool useEditorDataSource = false;
    private MapTileVO[,] _editorTiles = null;
    public MapTileVO[,] EditorTiles
    {
        get
        {
            if (null == _editorTiles)
            {
                _editorTiles = new MapTileVO[MapConst.MAP_WIDTH, MapConst.MAP_HEIGHT];
                for (int x = 0; x < MapConst.MAP_WIDTH; ++x)
                {
                    for (int y = 0; y < MapConst.MAP_HEIGHT; ++y)
                    {
                        _editorTiles[y, x] = MapTileVO.TileDecodeLocal(_tiles[y, x], new Coord(x, y));
                    }
                }
                useEditorDataSource = true;
            }
            return _editorTiles;
        }
        set
        {
            _editorTiles = value;
        }
    }
    
    public void SeedTile(MapTileVO tile)
    {
        EditorTiles[tile.coord.y, tile.coord.x] = tile;
        // 刷新地块
        MapView.Current.Lod0.ForceRereshSubMapView(tile.coord);
    }

    /// <summary>
    /// 保存地图配置
    /// </summary>
    public void Export()
    {
        MapTileGeneratorRun.GenerateMapTilesLocal(_editorTiles, _wayPoints);
    }
#endif

    public MapTileVO GetTile(int x, int y)
    {
        if (x >= 0 && x < MapConst.MAP_WIDTH && y >= 0 && y < MapConst.MAP_HEIGHT)
        {
#if UNITY_EDITOR
            if (!useEditorDataSource)
            {
                return MapTileVO.TileDecodeLocal(_tiles[y, x], new Coord(x, y));
            }
            else
            {
                return _editorTiles[y, x];
            }
#else
			return MapTileVO.TileDecodeLocal(_tiles[y, x], new Coord(x, y));
#endif
        }
        else
        {
            return null;
        }
    }

    public MapTileVO GetTile(Coord coord)
    {
        return GetTile(coord.x, coord.y);
    }

    public void SetVisibleBlocks(List<Coord> coords)
    {
        visibleBlockIds.Clear();
        foreach (Coord c in coords)
        {
            visibleBlockIds.Add((uint)c.y * (MapConst.MAP_WIDTH / MapServerBlock.WIDTH) + (uint)c.x);
        }
    }

    #region MapServerBlock
    class MapServerBlock
    {
        public const int WIDTH = 16;
        public const int HEIGHT = 16;
        public const float EXPIRE_TIME = 60f; // 1 min
        uint blockId;
        public uint BlockId
        {
            get { return blockId; }
        }

        //public TileData GetTileData(int x, int y)
        //{
        //    UInt64 key = GetKey((uint)x, (uint)y);
        //    TileData ret = null;
        //    dataDict.TryGetValue(key, out ret);
        //    return ret;
        //}

        public void ClearAll()
        {
            blockId = 0;
            //dataDict.Clear();
        }

        //public void setData(proto_structs.RpMap data)
        //{
        //    ClearAll();
        //    blockId = data.blockId;
        //    foreach (proto_structs.MapCity c in data.mapCities)
        //    {
        //        UInt64 key = GetKey(c.x, c.y);
        //        TileData d = new TileData();
        //        d.cityData = c;
        //        dataDict.Add(key, d);
        //    }
        //    foreach (proto_structs.MapResource r in data.mapResources)
        //    {
        //        UInt64 key = GetKey(r.x, r.y);
        //        TileData d = new TileData();
        //        d.resourceData = r;
        //        dataDict.Add(key, d);
        //    }
        //}


        //Dictionary<UInt64, TileData> dataDict = new Dictionary<UInt64, TileData>();
        ulong GetKey(uint x, uint y)
        {
            return ((ulong)x << 32) + y;
        }
    }
    #endregion
}