using System;
using System.Collections.Generic;

public class MapAStarCompact : IMapAStar
{
    private List<Coord>[,] _waypoints = new List<Coord>[5, 5];
    private uint[,] _cellmasks = new uint[MapConst.MAP_WIDTH, MapConst.MAP_HEIGHT];
    private Dictionary<uint, float> _footprints = new Dictionary<uint, float>();

    public void ResetContext()
    {
        Array.Clear(_cellmasks, 0, _cellmasks.Length);
        _footprints.Clear();
    }

    public MapAStarCompact()
    {
        var waypointTiles = GameFacade.GetProxy<MapProxy>().GetWayPointsByCamp();
        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 5; ++j)
            {
                if (i == j)
                {
                    continue;
                }
                if (_waypoints[i, j] == null)
                    _waypoints[i, j] = new List<Coord>();
                if (i != 4)
                {
                    _waypoints[i, j].Add(waypointTiles[i].coord);
                }
                if (j != 4)
                {
                    _waypoints[i, j].Add(waypointTiles[j].coord);
                }
            }
        }
    }

    public List<Coord> CalcPath(Coord src, Coord dst)
    {
        bool reverse = dst < src;
        if (reverse)
        {
            Coord temp = src;
            src = dst;
            dst = temp;
        }
        MapTileVO srcTile = GameFacade.GetProxy<MapProxy>().GetTile(src);
        MapTileVO dstTile = GameFacade.GetProxy<MapProxy>().GetTile(dst);
        int srcCamp = srcTile.camp;
        int dstCamp = dstTile.camp;

        List<Coord> transitions = new List<Coord>();
        transitions.Add(src);
        if (_waypoints[srcCamp, dstCamp] != null)
        {
            transitions.AddRange(_waypoints[srcCamp, dstCamp]);
        }
        transitions.Add(dst);

        List<Coord> paths = new List<Coord>();
        // merge transitions near each other
        while (transitions.Count > 2)
        {
            bool merged = false;
            for (int i = 0; i + 1 < transitions.Count; ++i)
            {
                if (transitions[i].Near(transitions[i + 1]))
                {
                    if (i + 1 == transitions.Count - 1)
                    {
                        // the last transition
                        merged = true;
                        transitions.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        merged = true;
                        transitions.RemoveAt(i + 1);
                        break;
                    }
                }
            }
            if (!merged)
                break;
        }
        for (int i = 0; i + 1 < transitions.Count; ++i)
        {
            var subPath = CalcPathImp(transitions[i], transitions[i + 1]);
            if (null != subPath)
            {
                paths.AddRange(subPath);
            }
            else
            {
                paths = null;
                break;
            }
        }
        ResetContext();
        if (reverse)
            paths.Reverse();
        return paths;
    }

    private float GetFootprintLen(uint mask)
    {
        uint key = 0;
        key |= (mask & COORDX_MASK);
        key |= (mask & COORDY_MASK);
        return _footprints[key];
    }

    private void SetFootprintLen(uint mask, float value)
    {
        uint key = 0;
        key |= (mask & COORDX_MASK);
        key |= (mask & COORDY_MASK);
        if (_footprints.ContainsKey(key))
        {
            _footprints[key] = value;
        }
        else
        {
            _footprints.Add(key, value);
        }
    }

    private int ReversePredecessor(int p)
    {
        p = (p + 3) % 6;
        p = (0 == p) ? 6 : p;
        return p;
    }

    public List<Coord> CalcPathImp(Coord src, Coord dst)
    {
        ResetContext();

        var sharedLayout = new MapLayout(1f / 1.732f);
        Func<int, int, float> distanceCalculator = (int x, int y) =>
        {
            return (sharedLayout.HexCenter(new Coord(x, y)) - sharedLayout.HexCenter(dst)).magnitude;
        };

        MinHeap<uint> opened = new MinHeap<uint>((uint lhr, uint rhr) =>
        {
            int lhr_x = 0, lhr_y = 0, rhr_x = 0, rhr_y = 0;
            ExtractCoord(lhr, out lhr_x, out lhr_y);
            ExtractCoord(rhr, out rhr_x, out rhr_y);
            var lhr_dis = GetFootprintLen(lhr) + distanceCalculator.Invoke(lhr_x, lhr_y);
            var rhr_dis = GetFootprintLen(rhr) + distanceCalculator.Invoke(rhr_x, rhr_y);
            return lhr_dis < rhr_dis;
        });

        Func<int, int, int, bool> adjacentChecker = (int x, int y, int radition) =>
        {
            MapTileVO tileVO = GameFacade.GetProxy<MapProxy>().GetTile(NextX(x, y, radition, true), NextY(x, y, radition, true));
            return (tileVO != null && tileVO.type != MapTileType.Block);
        };

        Func<int, int, int, bool> leapfrogChecker = (int x, int y, int leap) =>
        {
            int p1 = (leap + 4) % 6;
            p1 = (0 == p1) ? 6 : p1;
            int p2 = (leap + 5) % 6;
            p2 = (0 == p2) ? 6 : p2;

            return adjacentChecker.Invoke(NextX(x, y, leap, false), NextY(x, y, leap, false), 0)
                && adjacentChecker.Invoke(x, y, p1)
                && adjacentChecker.Invoke(x, y, p2);
        };

        Utils.Assert(adjacentChecker.Invoke(dst.x, dst.y, 0));
        SetActiveMask(src.x, src.y, true);
        opened.Insert(GetCellMask(src.x, src.y));
        SetOpenMask(src.x, src.y, true);
        SetFootprintLen(GetCellMask(src.x, src.y), 0f);

        while (opened.HeapLen > 0)
        {
            uint curMask = opened.ExtractMin();
            int curX = 0, curY = 0;
            ExtractCoord(curMask, out curX, out curY);

            for (int i = 1; i <= 6; ++i)
            {
                int adjacentX = NextX(curX, curY, i, true);
                int adjacentY = NextY(curX, curY, i, true);
                if (adjacentX < 0 || adjacentX >= MapConst.MAP_WIDTH
                    || adjacentY < 0 || adjacentY >= MapConst.MAP_HEIGHT)
                {
                    continue;
                }
                uint adjacentMask = GetCellMask(adjacentX, adjacentY);
                if (IsActive(adjacentMask) && !isOpened(adjacentMask))
                {
                    continue;
                }
                if (adjacentX == dst.x && adjacentY == dst.y)
                {
                    curMask = SetPredecessor(adjacentX, adjacentY, ReversePredecessor(i), true);
                    return ConstructPath(adjacentX, adjacentY);
                }
                if (!adjacentChecker.Invoke(curX, curY, i))
                {
                    continue;
                }
                TouchNext(opened, curMask, GetCellMask(adjacentX, adjacentY), ReversePredecessor(i), true);
            }

            for (int i = 1; i <= 6; ++i)
            {
                int leapfrogX = NextX(curX, curY, i, false);
                int leapfrogY = NextY(curX, curY, i, false);
                if (leapfrogX < 0 || leapfrogX >= MapConst.MAP_WIDTH
                    || leapfrogY < 0 || leapfrogY >= MapConst.MAP_HEIGHT)
                {
                    continue;
                }
                uint leapfrogMask = GetCellMask(leapfrogX, leapfrogY);
                if (IsActive(leapfrogMask) && !isOpened(leapfrogMask))
                {
                    continue;
                }
                if (leapfrogX == dst.x && leapfrogY == dst.y)
                {
                    curMask = SetPredecessor(leapfrogX, leapfrogY, ReversePredecessor(i), false);
                    return ConstructPath(leapfrogX, leapfrogY);
                }
                if (!leapfrogChecker.Invoke(curX, curY, i))
                {
                    continue;
                }
                TouchNext(opened, curMask, GetCellMask(leapfrogX, leapfrogY), ReversePredecessor(i), false);
            }
            curMask = SetOpenMask(curMask, false);
        }

        return null;
    }

    private void TouchNext(MinHeap<uint> opened, uint curMask, uint nextMask, int predecessor, bool adjacent)
    {
        bool check1 = false;
        int check2 = GetPredecessor(curMask, out check1);
        var distance = adjacent ? 1f : 1.732f;
        if (check1 == adjacent && check2 == predecessor)
        {
            distance *= 0.8f;
        }
        if (!IsActive(nextMask))
        {
            nextMask = SetActiveMask(nextMask, true);
            nextMask = SetOpenMask(nextMask, true);
            nextMask = SetPredecessor(nextMask, predecessor, adjacent);
            SetFootprintLen(nextMask, GetFootprintLen(curMask) + distance);
            opened.Insert(nextMask);
        }
        else if (isOpened(nextMask))
        {
            if (GetFootprintLen(nextMask) > (GetFootprintLen(curMask) + distance))
            {
                SetFootprintLen(nextMask, GetFootprintLen(curMask) + distance);
                nextMask = SetPredecessor(nextMask, predecessor, adjacent);
                opened.Heapify();
            }
        }
    }

    private List<Coord> ConstructPath(int dstX, int dstY)
    {
        uint curMask = GetCellMask(dstX, dstY);
        List<Coord> ret = new List<Coord>();
        List<bool> tmp = new List<bool>();
        ret.Add(new Coord(dstX, dstY));
        bool adjacent = false;
        int predecessor = 0;
        do
        {
            int curX, curY;
            ExtractCoord(curMask, out curX, out curY);
            predecessor = GetPredecessor(curMask, out adjacent);
            if (predecessor != 0)
            {
                int prevX = NextX(curX, curY, predecessor, adjacent);
                int prevY = NextY(curX, curY, predecessor, adjacent);
                curMask = GetCellMask(prevX, prevY);
                ret.Add(new Coord(prevX, prevY));
                tmp.Add(adjacent);
            }
        }
        while (predecessor > 0);
        ret.Reverse();
        return ret;
    }

    static int[] adjacentOffsetX = new int[7] { 0, -1, -1, 0, 1, 0, -1 };
    static int[] adjacentOffsetY = new int[7] { 0, 0, -1, -1, 0, 1, 1 };
    static int[] leapfrogOffsetX = new int[7] { 0, 0, -2, -2, 0, 1, 1 };
    static int[] leapfrogOffsetY = new int[7] { 0, 2, 1, -1, -2, -1, 1 };
    private int NextX(int curX, int curY, int code, bool adjacent)
    {
        if (0 == code) return curX;
        if (adjacent)
        {
            int ret = curX + adjacentOffsetX[code];
            if (0 == (curY % 2) && 0 != adjacentOffsetY[code])
            {
                ret += 1;
            }
            return ret;
        }
        else
        {
            int ret = curX + leapfrogOffsetX[code];
            if (0 == (curY % 2) && 0 != leapfrogOffsetX[code])
            {
                ret += 1;
            }
            return ret;
        }
    }
    private int NextY(int curX, int curY, int code, bool adjacent)
    {
        if (0 == code) return curY;
        if (adjacent)
        {
            return curY + adjacentOffsetY[code];
        }
        else
        {
            return curY + leapfrogOffsetY[code];
        }
    }

    private const uint ACTIVE_MASK = 0x00000001;
    private const uint OPENED_MASK = 0x00000002;
    private const uint PREDECESSOR_MASK = 0x70000000;
    private const uint PREDECESSOR_TYPE_MASK = 0x80000000;
    private const uint COORDX_MASK = 0x0FFC0000;
    private const uint COORDY_MASK = 0x0003FF00;

    private bool IsActive(uint mask)
    {
        return (mask & ACTIVE_MASK) > 0;
    }

    private uint SetActiveMask(int x, int y, bool isActive)
    {
        uint tmp = _cellmasks[x, y];
        tmp &= ~ACTIVE_MASK;
        if (isActive)
        {
            tmp |= ACTIVE_MASK;
        }
        _cellmasks[x, y] = tmp;
        return tmp;
    }

    private uint SetActiveMask(uint mask, bool isActive)
    {
        int x = 0, y = 0;
        ExtractCoord(mask, out x, out y);
        return SetActiveMask(x, y, isActive);
    }

    private uint GetCellMask(int x, int y)
    {
        uint tmp = _cellmasks[x, y];
        tmp |= (uint)(x << 18);
        tmp |= (uint)(y << 8);
        _cellmasks[x, y] = tmp;
        return tmp;
    }

    private void ExtractCoord(uint mask, out int x, out int y)
    {
        x = (int)((mask & COORDX_MASK) >> 18);
        y = (int)((mask & COORDY_MASK) >> 8);
    }

    private bool isOpened(uint mask)
    {
        return (mask & OPENED_MASK) > 0;
    }

    private uint SetOpenMask(uint mask, bool isOpen)
    {
        int x = 0, y = 0;
        ExtractCoord(mask, out x, out y);
        return SetOpenMask(x, y, isOpen);
    }

    private uint SetOpenMask(int x, int y, bool isOpen)
    {
        uint tmp = _cellmasks[x, y];
        tmp &= ~OPENED_MASK;
        if (isOpen)
        {
            tmp |= OPENED_MASK;
        }
        _cellmasks[x, y] = tmp;
        return tmp;
    }

    private uint SetPredecessor(int x, int y, int p, bool adjacent)
    {
        if (!adjacent)
        {
            int a = 0;
        }
        uint tmp = _cellmasks[x, y];
        tmp &= ~PREDECESSOR_MASK;
        tmp |= ((uint)p << 28);
        if (adjacent)
        {
            tmp &= ~PREDECESSOR_TYPE_MASK;
        }
        else
        {
            tmp |= PREDECESSOR_TYPE_MASK;
        }
        _cellmasks[x, y] = tmp;

        return tmp;
    }

    private uint SetPredecessor(uint mask, int p, bool adjacent)
    {
        int x = 0, y = 0;
        ExtractCoord(mask, out x, out y);
        return SetPredecessor(x, y, p, adjacent);
    }

    private int GetPredecessor(uint mask, out bool adjacent)
    {
        adjacent = 0 == (mask & PREDECESSOR_TYPE_MASK);
        return (int)((mask & PREDECESSOR_MASK) >> 28);
    }

    private int GetPredecessor(int x, int y, out bool adjacent)
    {
        return GetPredecessor(GetCellMask(x, y), out adjacent);
    }
}