using UnityEngine;
using System.Collections.Generic;

public class HexSubMapView : MonoBehaviour
{
    public bool inUse = false;
    public int xIdx = 0;
    public int yIdx = 0;
    public GameObject mapHexBg;
    public GameObject mapWaterBg;
    public HEX hex = null;
    public HexMapLayout layout = null;
    public Dictionary<string, MapTile> tileTmpDict = null;
    public Transform blockRoot;

    THEXGON mHexBg = null;
    THEXWATER mHexWater = null;
    Dictionary<Coord, MapTile> mTiles = new Dictionary<Coord, MapTile>();
    Dictionary<Coord, SpriteRenderer> mBlocks = new Dictionary<Coord, SpriteRenderer>();
    MapProxy mapProxy;

    void OnDestory()
    {
        DestroyTiles();
        mTiles = null;

        DestroyBlocks();
        mBlocks = null;
    }

    public void InitPos()
    {
        transform.localPosition = new Vector3(
            layout.HexTileWidth * xIdx * hex.xTile,
            0,
            layout.HexTileHeight * yIdx * hex.yTile);
        transform.localScale = Vector3.one;
        mapHexBg.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        mapHexBg.transform.localPosition = new Vector3(0f, 8f, 0f);
        mapWaterBg.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        mapWaterBg.transform.localPosition = new Vector3(0f, 8f, 0f);
    }

    void UpdateSprList()
    {
        mHexBg.spriteId.Clear();
        if (mapProxy == null)
        {
            mapProxy = GameFacade.GetProxy<MapProxy>();
        }
        List<int> sprs = mapProxy.GetSprLists(xIdx, yIdx);
        mHexBg.spriteId.AddRange(sprs);
        mHexWater.spriteId.Clear();
        mHexWater.spriteId.AddRange(sprs);
    }

    public void InitBg(bool isGenheight = true)
    {
        if(mHexBg == null)
        {
            mHexBg = mapHexBg.AddComponent<THEXGON>();
            mHexBg.isGenHeight = isGenheight;
            mHexBg.SetBounds(
            new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
            new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));

            mHexWater = mapWaterBg.AddComponent<THEXWATER>();
            mHexWater.SetBounds(
                new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));

            UpdateSprList();

            mHexBg.Init(hex, xIdx, yIdx);
            mHexWater.Init(hex, xIdx, yIdx);
        }
        else
        {
            mHexBg.isGenHeight = isGenheight;
            UpdateSprList();
            mHexBg.ReInit(xIdx, yIdx);
            mHexWater.ReInit(xIdx, yIdx);
        } 
    }

#if UNITY_EDITOR
    SpriteRenderer[][] mMarks;
    public Transform markRoot;
    public GameObject markTemplate;
    public void ShowMarks(MapEditorEntry.EDIT_OP op, Color[] color)
    {
        if(mMarks == null)
        {
            mMarks = new SpriteRenderer[hex.xTile][];
        }
        for (int x = 0; x < hex.xTile; ++x)
        {
            if (mMarks[x] == null)
            {
                mMarks[x] = new SpriteRenderer[hex.yTile];
            }
            for (int y = 0; y < hex.yTile; ++y)
            {
                Coord c = new Coord(x + hex.xTile * xIdx, y + hex.yTile * yIdx);
                if (mapProxy == null)
                {
                    mapProxy = GameFacade.GetProxy<MapProxy>();
                }
                MapTileVO tileVo = mapProxy.GetTile(c);
                if (tileVo != null)
                {
                    int index;
                    if(op == MapEditorEntry.EDIT_OP.EDIT_AREA)
                    {
                        index = tileVo.camp;
                    }
                    else if(op == MapEditorEntry.EDIT_OP.EDIT_LV)
                    {
                        index = tileVo.level;
                    }
                    else if (op == MapEditorEntry.EDIT_OP.EDIT_TILETYPE)
                    {
                        index = (int)tileVo.type;
                    }
                    else
                    {
                        Debug.LogError("Error Op.");
                        return;
                    }
                    if (index >= color.Length)
                    {
                        Debug.LogError("No defined color of this index.");
                        return;
                    }
                    SpriteRenderer mark;
                    if (mMarks[x][y] != null)
                    {
                        mMarks[x][y].color = color[index];
                        continue;
                    }
                    mark = GetAMark();
                    mark.color = color[index];
                    mark.transform.localPosition = layout.HexCenter(tileVo.coord) - transform.localPosition;
                    mark.transform.localScale = new Vector3(1.5f, 1.55f, 1f);
                    mark.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                    mMarks[x][y] = mark;
                }
            }
        }
        markRoot.gameObject.SetActive(true);
    }

    SpriteRenderer GetAMark()
    {
        SpriteRenderer mark = GameObjectPool.GetInstance().SpawnGo(markTemplate).GetComponent<SpriteRenderer>();
        mark.transform.parent = markRoot;
        mark.gameObject.SetActive(true);
        return mark;
    }

    public void HideMarks()
    {
        markRoot.gameObject.SetActive(false);
    }
#endif

    public void InitTiles()
    {
    }

    public void DestroyTiles()
    {
        //foreach (var item in mTiles)
        //{
        //    if (item.Value != null)
        //    {
        //        //item.Value.OnRecycle();
        //        GameObjectPool.GetInstance().RecycleGo(item.Value.gameObject.name, item.Value.gameObject);
        //    }
        //}
        //mTiles.Clear();
    }

    public void InitBlocks()
    {
        //for (int x = 0; x < xTileCnt; ++x)
        //{
        //    for (int y = 0; y < yTileCnt; ++y)
        //    {
        //        Coord c = new Coord(x + xTileCnt * xIdx, y + yTileCnt * yIdx);
        //        MapTileVO tileVo = GameFacade.GetProxy<MapProxy>().GetTile(c);
        //        if (tileVo != null)
        //        {
        //            if (tileVo.type == MapTileType.Block && tileVo.blockType != MapBlockType.Sea)
        //            {
        //                SpriteRenderer block = CreateBlock(tileVo);
        //                block.transform.parent = blockRoot;
        //                block.gameObject.SetActive(true);
        //                block.transform.localPosition = layout.HexCenter(tileVo.coord) - this.transform.localPosition;
        //                blocks.Add(c, block);
        //            }
        //        }
        //    }
        //}
    }

    public void DestroyBlocks()
    {
        //foreach (var item in blocks)
        //{
        //    if (item.Value != null)
        //    {
        //        GameObjectPool.GetInstance().RecycleGo(blockTmp.name, item.Value.gameObject);
        //    }
        //}
        //blocks.Clear();
    }

    public void ReplaceMesh()
    {
        mHexBg.ReplaceMesh();
    }

    public void SetPitch(float eulurX)
    {
        foreach (var item in mTiles)
        {
            if (item.Value != null)
            {
                item.Value.SetPitch(eulurX);
            }
        }

        foreach (var item in mBlocks)
        {
            if (item.Value != null)
            {
                item.Value.transform.localEulerAngles = new Vector3(eulurX, 0, 0);
            }
        }
    }
}
