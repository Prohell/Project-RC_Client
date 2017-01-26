//----------------------------------------------
//	CreateTime  : 1/20/2017 4:54:27 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : MapHelper
//	ChangeLog   : None
//----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MapHelper
{
    public const float Sqrt3 = HexMapLayout.Sqrt3;
    public const float HexEdgeLen = 1f;
    public const float StepX = Sqrt3 * HexEdgeLen + 0.5f;
    public const float StepZ = 1.5f * HexEdgeLen + Sqrt3 / 4;
    public const float OddEvenOffsetX = StepX / 2f;
    public const float StepY = 0.5f;
    public const int BaseYPosition = 8;

    public static Vector3 CoordToVector3(Coord p_co)
    {
        var tile = ProxyManager.GetInstance().Get<MapProxy>().GetTile(p_co);
        bool isEven = p_co.y % 2 == 0;
        return new Vector3(p_co.x * StepX + (isEven ? 0 : OddEvenOffsetX), (BaseYPosition - tile.height) * StepY, p_co.y * StepZ);
    }

    public enum CoordBorder
    {
        TopRight,
        Right,
        ButtomRight,
        ButtomLeft,
        Left,
        TopLeft
    }

    public enum CoordVertex
    {
        TopRight,
        Top,
        ButtomRight,
        ButtomLeft,
        Buttom,
        TopLeft
    }

    public static Vector3 GetBorderVector3OfCoord(Coord p_co, CoordBorder p_border)
    {
        var vec3 = CoordToVector3(p_co);

        switch (p_border)
        {
            case CoordBorder.TopRight:
                {
                    return new Vector3(vec3.x + Sqrt3 / 4, vec3.y, vec3.z + 0.75f);
                }
            case CoordBorder.Right:
                {
                    return new Vector3(vec3.x + Sqrt3 / 2, vec3.y, vec3.z);
                }
            case CoordBorder.ButtomRight:
                {
                    return new Vector3(vec3.x + Sqrt3 / 4, vec3.y, vec3.z - 0.75f);
                }
            case CoordBorder.ButtomLeft:
                {
                    return new Vector3(vec3.x - Sqrt3 / 4, vec3.y, vec3.z - 0.75f);
                }
            case CoordBorder.Left:
                {
                    return new Vector3(vec3.x - Sqrt3 / 2, vec3.y, vec3.z);
                }
            case CoordBorder.TopLeft:
                {
                    return new Vector3(vec3.x - Sqrt3 / 4, vec3.y, vec3.z + 0.75f);
                }
            default:
                {
                    LogModule.ErrorLog(string.Format("Error Border type:{0}", p_border));
                    return Vector3.zero;
                }
        }
    }

    public static Vector3 GetVertexVector3OfCoord(Coord p_co, CoordVertex p_vertex)
    {
        var vec3 = CoordToVector3(p_co);

        switch (p_vertex)
        {
            case CoordVertex.TopRight:
                {
                    return new Vector3(vec3.x + Sqrt3 / 2, vec3.y, vec3.z + 0.5f);
                }
            case CoordVertex.Buttom:
                {
                    return new Vector3(vec3.x, vec3.y, vec3.z - 1);
                }
            case CoordVertex.ButtomRight:
                {
                    return new Vector3(vec3.x + Sqrt3 / 2, vec3.y, vec3.z - 0.5f);
                }
            case CoordVertex.ButtomLeft:
                {
                    return new Vector3(vec3.x - Sqrt3 / 2, vec3.y, vec3.z - 0.5f);
                }
            case CoordVertex.Top:
                {
                    return new Vector3(vec3.x, vec3.y, vec3.z + 1);
                }
            case CoordVertex.TopLeft:
                {
                    return new Vector3(vec3.x - Sqrt3 / 2, vec3.y, vec3.z + 0.5f);
                }
            default:
                {
                    LogModule.ErrorLog(string.Format("Error vertex type:{0}", p_vertex));
                    return Vector3.zero;
                }
        }
    }

    public static List<Vector3> ConvertCoordPathToVec(List<Coord> p_co)
    {
        List<Vector3> VecPath = new List<Vector3>();

        for (int i = 0; i < p_co.Count - 1; i++)
        {
            var path = CalcVecPath(p_co[i], p_co[i + 1]);
            path.RemoveAt(path.Count - 1);
            VecPath.AddRange(path);
        }
        VecPath.Add(CoordToVector3(p_co.Last()));

        return VecPath;
    }

    private static List<Vector3> CalcVecPath(Coord p_co1, Coord p_co2)
    {
        List<Vector3> VecPath = new List<Vector3>();

        bool IsEven = p_co1.y % 2 == 0;
        Coord OffsetCoord = p_co2 - p_co1;

        if (IsEven)
        {
            switch (OffsetCoord.y)
            {
                case -2:
                    {
                        switch (OffsetCoord.x)
                        {
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.Buttom));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.Top));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case -1:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.ButtomLeft));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.TopRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.ButtomLeft));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.TopRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.ButtomRight));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.TopLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 1:
                            case 2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.ButtomRight));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.TopLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 0:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.Left));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.Right));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    break;
                                }
                            case 1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.Right));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.Left));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.TopLeft));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.ButtomRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.TopLeft));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.ButtomRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.TopRight));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.ButtomLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 1:
                            case 2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.TopRight));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.ButtomLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (OffsetCoord.x)
                        {
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.Top));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.Buttom));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                default:
                    {
                        LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                        return null;
                    }
            }
        }
        else
        {
            switch (OffsetCoord.y)
            {
                case -2:
                    {
                        switch (OffsetCoord.x)
                        {
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.Buttom));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.Top));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case -1:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -2:
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.ButtomLeft));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.TopRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.ButtomLeft));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.TopRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.ButtomRight));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.TopLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.ButtomRight));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.TopLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 0:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.Left));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.Right));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    break;
                                }
                            case 1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.Right));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.Left));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch (OffsetCoord.x)
                        {
                            case -2:
                            case -1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.TopLeft));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.ButtomRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.TopLeft));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.ButtomRight));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 1:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co1, CoordBorder.TopRight));
                                    VecPath.Add(GetBorderVector3OfCoord(p_co2, CoordBorder.ButtomLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            case 2:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.TopRight));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.ButtomLeft));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (OffsetCoord.x)
                        {
                            case 0:
                                {
                                    VecPath.Add(CoordToVector3(p_co1));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co1, CoordVertex.Top));
                                    VecPath.Add(GetVertexVector3OfCoord(p_co2, CoordVertex.Buttom));
                                    VecPath.Add(CoordToVector3(p_co2));
                                    break;
                                }
                            default:
                                {
                                    LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                                    return null;
                                }
                        }
                        break;
                    }
                default:
                    {
                        LogModule.ErrorLog(string.Format("Coor not near {0}, {1}", p_co1, p_co2));
                        return null;
                    }
            }
        }

        return VecPath;
    }

    public static void MoveMapCamera(Coord c)
    {
        Camera MapCamera = MapView.Current.MapCamera;

        Vector3 startPos = MapCamera.transform.localPosition;
        Vector3 endPos = startPos + (MapView.Current.Layout.HexCenter(c) - MapView.Current.Layout.ScreenPos2WorldPos(MapCamera, MapView.MapInfoHighlightPos));

        MapCamera.transform.DOMove(endPos, 1f).SetEase(Ease.Linear);
    }
}
