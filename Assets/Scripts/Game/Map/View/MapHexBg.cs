/// <summary>
/// 世界地图，六边形地面网格
/// </summary>

using UnityEngine;
using System.Collections.Generic;

public class MapHexBg : MonoBehaviour
{

    public int xMin = -1;
    public int width = -1;
    public int yMin = -1;
    public int height = -1;

    public MapLayout layout = null;
    public Vector3 offset = Vector3.zero;
    public UIAtlas mapTileAtlas = null;

    bool isInited = false;

    public void Init()
    {
        MeshFilter mf = gameObject.GetOrAddComponent<MeshFilter>();
        if (!isInited)
        {
            mf.mesh = CreateHexagonMeshWithoutUVColor();
            mf.mesh.uv = GetMeshUVS();
            mf.mesh.colors = GeteMeshColors();
            mf.mesh.RecalculateBounds();
            isInited = true;
        }
        else
        {
            mf.mesh.uv = GetMeshUVS();
        }
    }

    Mesh CreateHexagonMeshWithoutUVColor()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Hexagon";

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int col = 0; col < width; ++col)
        {
            for (int row = 0; row < height; ++row)
            {
                Coord local = new Coord(col, row);
                Coord world = new Coord(col + xMin, row + yMin);
                MapTileVO tileVo = GameFacade.GetProxy<MapProxy>().GetTile(world);
                if (tileVo != null)
                {
                    AddHexagonVertices(vertices, triangles, layout.HexCenter(local));
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        return mesh;
    }

    Vector2[] GetMeshUVS()
    {
        RefreshUVS();
        return uvs;
    }

    Vector2[] uvs = null;
    void RefreshUVS()
    {
        if (null == uvs)
        {
            uvs = new Vector2[width * height * layout.HexUvs.Length];
        }
        for (int col = 0; col < width; ++col)
        {
            for (int row = 0; row < height; ++row)
            {
                Coord world = new Coord(col + xMin, row + yMin);
                MapTileVO tileVo = GameFacade.GetProxy<MapProxy>().GetTile(world);
                if (null != tileVo)
                {
                    AddHexagonUV((height * col + row) * layout.HexUvs.Length, tileVo);
                }
            }
        }
    }

    Color[] GeteMeshColors()
    {
        List<Color> colors = new List<Color>();

        for (int col = 0; col < width; ++col)
        {
            for (int row = 0; row < height; ++row)
            {
                Coord world = new Coord(col + xMin, row + yMin);
                MapTileVO tileVo = GameFacade.GetProxy<MapProxy>().GetTile(world);
                if (null != tileVo)
                {
                    AddHexagonColor(colors, tileVo);
                }
            }
        }

        return colors.ToArray();
    }

    void AddHexagonColor(List<Color> colors, MapTileVO tile)
    {
        for (int i = 0; i < layout.HexVertices.Length; ++i)
        {
            colors.Add(Color.white);
        }
    }

    void AddHexagonVertices(List<Vector3> vertices, List<int> triangles, Vector3 center)
    {

        int idxBegin = vertices.Count;
        foreach (Vector3 ver in layout.HexVertices)
        {
            vertices.Add(center + ver);
        }

        foreach (int tri in layout.Triangles)
        {
            triangles.Add(idxBegin + tri);
        }
    }

    private static Dictionary<int, UISpriteData> spdDataMap = new Dictionary<int, UISpriteData>();

    void AddHexagonUV(int startIdx, MapTileVO tile)
    {
        int key = ((int)tile.type << 24) + ((int)tile.blockType << 16) + ((int)tile.camp << 8) + tile.level + (tile.camp1 << 28);
        UISpriteData spd = null;
        if (!spdDataMap.TryGetValue(key, out spd))
        {
            string name = SpriteNameInAtlas(tile);
            spd = mapTileAtlas.GetSprite(name);
            if (spd == null)
            {
                Debug.LogError("# Cant Get Sprite " + name + " in Atlas " + mapTileAtlas.name);
            }
            spdDataMap.Add(key, spd);
        }

        float atlasSide = mapTileAtlas.texture.height;
        float uvl = 0f;
        //if (spd.rotated){
        //	uvl = spd.width / atlasSide / 2f; 
        //}else{
        uvl = spd.height / atlasSide / 2f;
        //}

        uvl = 256f / atlasSide / 2f;

        float cx = (spd.x + spd.width / 2f) / atlasSide;
        float cy = 1f - (spd.y + spd.height / 2f) / atlasSide;
        Vector2 cen = new Vector2(cx, cy);

        for (int i = 0; i < layout.HexUvs.Length; ++i)
        {
            var hexUV = layout.HexUvs[i];
            //if (spd.rotated){
            //	Vector2 rotatedUV = new Vector2(hexUV.y, - hexUV.x);
            //	uvs[startIdx + i] = cen + rotatedUV * uvl;
            //}else{
            uvs[startIdx + i] = cen + hexUV * uvl;
            //}
        }
    }

    string SpriteNameInAtlas(MapTileVO tile)
    {
        return "west_0";
        //switch (tile.type)
        //{
        //    case MapTileType.Block:
        //        {
        //            if (tile.blockType == MapBlockType.Sea)
        //            {
        //                return "sea_" + Mathf.Clamp(tile.camp1, 0, 4);
        //            }
        //            else if (tile.blockType == MapBlockType.Empty)
        //            {
        //                return MapTileVO.Camp2String(tile.camp) + "_empty";
        //            }
        //            else
        //            {
        //                return MapTileVO.Camp2String(tile.camp) + "_block";
        //            }
        //        }
        //    case MapTileType.Camp:
        //    case MapTileType.PVE:
        //        {
        //            return MapTileVO.Camp2String(tile.camp) + "_" + tile.level;
        //        }
        //    case MapTileType.Strait:
        //        {
        //            return "sea_" + Mathf.Clamp(tile.camp1, 0, 4);
        //        }
        //    default:
        //        return "";
        //}
    }
}
