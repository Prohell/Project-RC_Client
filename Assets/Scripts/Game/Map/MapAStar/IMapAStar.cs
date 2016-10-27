using System.Collections.Generic;

public interface IMapAStar
{
    List<Coord> CalcPath(Coord src, Coord dst);
}
