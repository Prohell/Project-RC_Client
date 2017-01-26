using UnityEngine;
public struct Coord
{
    public int x;
    public int y;

    public Coord(int ax, int ay)
    {
        x = ax;
        y = ay;
    }

    /// <summary>
    /// 用字符串创建坐标
    /// </summary>
    /// <param name="coordText">0,0</param>
    public Coord(string coordText)
    {
        string[] value = coordText.Split(',');
        if (value.Length == 2 || int.TryParse(value[0], out x) || !int.TryParse(value[1], out y))
            throw new UnityException("Error Coord Input : " + coordText);
    }

    public static Coord operator +(Coord lhs, Coord rhs)
    {
        return new Coord(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static Coord operator -(Coord lhs, Coord rhs)
    {
        return new Coord(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static bool operator <(Coord lhs, Coord rhs)
    {
        if (lhs.x != rhs.x)
            return (lhs.x < rhs.x);
        else
            return (lhs.y < rhs.y);
    }

    public static bool operator >(Coord lhs, Coord rhs)
    {
        if (lhs != rhs)
            return !(lhs < rhs);
        return false;
    }

    public static bool operator ==(Coord lhs, Coord rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(Coord lhs, Coord rhs)
    {
        return (lhs.x != rhs.x || lhs.y != rhs.y);
    }

    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }

    public override bool Equals(object other)
    {
        if (!(other is Coord))
        {
            return false;
        }
        Coord coord = (Coord)other;
        return this == coord;
    }

    public override int GetHashCode()
    {
        //return this.x.GetHashCode () ^ this.y.GetHashCode () << 2 ^ this.z.GetHashCode () >> 2;
        return (x << 16 + y);
    }

    public bool Near(Coord other, int measure = 100)
    {
        return (Mathf.Abs(x - other.x) + Mathf.Abs(y - other.y) < measure);
    }

    public static Coord RandomMapCoord()
    {
        Coord re = new Coord();
        re.x = Random.Range(0, MapConst.MAP_WIDTH - 1);
        re.y = Random.Range(0, MapConst.MAP_HEIGHT - 1);
        return re;
    }

    public Coord GetSubmapCoord()
    {
        Coord submapCoord = new Coord();
        submapCoord.x = Mathf.FloorToInt(x / MapConst.MAP_TILES_PER_SUB);
        submapCoord.y = Mathf.FloorToInt(y / MapConst.MAP_TILES_PER_SUB);
        return submapCoord;
    }
}
