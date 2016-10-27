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
        re.x = UnityEngine.Random.Range(0, MapConst.MAP_WIDTH - 1);
        re.y = UnityEngine.Random.Range(0, MapConst.MAP_HEIGHT - 1);
        return re;
    }
}
