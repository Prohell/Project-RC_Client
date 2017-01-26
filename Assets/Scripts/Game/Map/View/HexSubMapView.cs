using UnityEngine;
using System.Collections.Generic;

public class HexSubMapView : MonoBehaviour
{
    public bool inUse = false;
    public int xIdx = 0;
    public int yIdx = 0;
    public GameObject mapHexBg;
    public GameObject mapWaterBg;
	public GameObject mapEdgeLeft;
    public GameObject mapEdgePrev;
    public GameObject mapEdgeDiag;
    public HEX hex = null;
    public HexMapLayout layout = null;
    public Dictionary<string, MapTile> tileTmpDict = null;
    public Transform blockRoot;

    public THEXGON mHexBg = null;
    public THEXWATER mHexWater = null;
	public THEXBLENDL mHexBlendL = null;
    public THEXBLENDP mHexBlendP = null;
    public THEXBLENDD mHexBlendD = null;

    public HexSubMapView left = null;
	public HexSubMapView prev = null;
	public HexSubMapView diag = null;

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

		if(mapEdgeLeft==null && left!=null)
		{
            if (xIdx>0)
            {
                //Debug.Log("kk");
                mapEdgeLeft = new GameObject();
                mapEdgeLeft.name = "mapEdgeLeft";
                mapEdgeLeft.transform.parent = mapHexBg.transform.parent;
                mapEdgeLeft.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                mapEdgeLeft.transform.localPosition = new Vector3(0f, 8f, 0f);
                mapEdgeLeft.transform.localScale = new Vector3(1, 1, 1);
                //mapBlendBg = Instantiate (mapHexBg);
            }
        }

        if (mapEdgePrev == null && prev!=null)
        {
            if (yIdx>0)
            {
                mapEdgePrev = new GameObject();
                mapEdgePrev.name = "mapEdgePrev";
                mapEdgePrev.transform.parent = mapHexBg.transform.parent;
                mapEdgePrev.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                mapEdgePrev.transform.localPosition = new Vector3(0f, 8f, 0f);
                mapEdgePrev.transform.localScale = new Vector3(1, 1, 1);
                //mapBlendBg = Instantiate (mapHexBg);
            }
        }

        if (mapEdgeDiag == null && left != null && prev != null)
        {
            if(xIdx>0 && yIdx>0)
            {
                mapEdgeDiag = new GameObject();
                mapEdgeDiag.name = "mapEdgeDiag";
                mapEdgeDiag.transform.parent = mapHexBg.transform.parent;
                mapEdgeDiag.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
                mapEdgeDiag.transform.localPosition = new Vector3(0f, 8f, 0f);
                mapEdgeDiag.transform.localScale = new Vector3(1, 1, 1);
                //mapBlendBg = Instantiate (mapHexBg);
            }
        }
    }

	public void UpdateSprList()
    {
        if (mapProxy == null)
        {
            mapProxy = GameFacade.GetProxy<MapProxy>();
        }
        List<int> sprs = mapProxy.GetSprLists(xIdx, yIdx);

		mHexBg.spriteIds.Clear();
		mHexBg.spriteIds.AddRange(sprs);
        
        mHexWater.spriteIds.Clear();
        mHexWater.spriteIds.AddRange(sprs);
    }
  
    int[] GetSameSpriteIds(List<int> Ids1, List<int> Ids2)
    {
        List<int> same = new List<int>();
        for(int i=0; i<Ids1.Count; i++)
        {
            for (int j = 0; j < Ids2.Count; j++)
            {
                if(Ids1[i]==Ids2[j])
                {
                    if(!same.Contains(Ids1[i]))
                    {
                        same.Add(Ids1[i]);
                    }
                }
            }
        }

        return same.ToArray();
    }

    public void UpdateInterSprList()
    {
        MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();

        MapTileVO theTile = mapProxy.GetTile(mHexBg._xTile * mHexBg._x, mHexBg._yTile * mHexBg._y);
        int theMat = theTile.mat;
        if (theMat > 3)
            theMat = 0;

        byte leftMat = 255;
        byte prevMat = 255;
        byte diagMat = 255;

        if (left != null)
        {
            MapTileVO leftTile = mapProxy.GetTile(left.mHexBg._xTile * left.mHexBg._x, left.mHexBg._yTile * left.mHexBg._y);
            leftMat = leftTile.mat;
            if (leftMat > 3)
                leftMat = 0;
        }

        if (prev != null)
        {
            MapTileVO prevTile = mapProxy.GetTile(prev.mHexBg._xTile * prev.mHexBg._x, prev.mHexBg._yTile * prev.mHexBg._y);
            prevMat = prevTile.mat;
            if (prevMat > 3)
                prevMat = 0;
        }

        if (diag != null)
        {
            MapTileVO diagTile = mapProxy.GetTile(diag.mHexBg._xTile * diag.mHexBg._x, diag.mHexBg._yTile * diag.mHexBg._y);
            diagMat = diagTile.mat;
            if (diagMat > 3)
                diagMat = 0;
        }

        bool hasLeft = false;
        if (left != null && left.mHexBg.spriteIds.Count > 0)
        {
            hasLeft = true;
        }

        bool hasPrev = false;
        if (prev != null && prev.mHexBg.spriteIds.Count > 0)
        {
            hasPrev = true;
        }

        if (mHexBlendL)
        {
            mHexBlendL.left = hasLeft;
            mHexBlendL.thisSprites = mHexBg.spriteIds;

            if (hasLeft)
            {
                //int[] same = GetSameSpriteIds(mHexBg.spriteIds, left.mHexBg.spriteIds);
                mHexBlendL.leftSprites = left.mHexBg.spriteIds;
                mHexBlendL.spriteIds.Clear();
                mHexBlendL.spriteIds.AddRange(mHexBg.spriteIds);
                mHexBlendL.spriteIds.AddRange(left.mHexBg.spriteIds);
            }   
        }

        if (mHexBlendP)
        {
            mHexBlendP.prev = hasPrev;
            mHexBlendP.thisSprites = mHexBg.spriteIds;

            if (hasPrev)
            {
                //int[] same = GetSameSpriteIds(mHexBg.spriteIds, prev.mHexBg.spriteIds);
                mHexBlendP.prevSprites = prev.mHexBg.spriteIds;
                mHexBlendP.spriteIds.Clear();
                mHexBlendP.spriteIds.AddRange(mHexBg.spriteIds);
                mHexBlendP.spriteIds.AddRange(prev.mHexBg.spriteIds);
            }  
        }

        if (mHexBlendD)
        {
            mHexBlendD.left = hasLeft;
            mHexBlendD.prev = hasPrev;
            mHexBlendD.thisSprites = mHexBg.spriteIds;

            if (hasLeft && hasPrev)
            {
                //int[] same = GetSameSpriteIds(mHexBg.spriteIds, prev.mHexBg.spriteIds);
                mHexBlendD.leftSprites = left.mHexBg.spriteIds;
                mHexBlendD.prevSprites = prev.mHexBg.spriteIds;
                mHexBlendD.diagSprites = diag.mHexBg.spriteIds;
                mHexBlendD.spriteIds.Clear();
                mHexBlendD.spriteIds.Add(0);
                mHexBlendD.spriteIds.Add(1);
                mHexBlendD.spriteIds.Add(2);
                mHexBlendD.spriteIds.Add(3);
            }
        }
    }

    public void InitBg(bool isGenheight = true)
    {
        if (mHexBg == null)
        {
            mHexBg = mapHexBg.AddComponent<THEXGON>();
            mHexBg.isGenHeight = isGenheight;
            mHexBg.SetBounds(
                new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
            mHexBg.Init(hex, xIdx, yIdx);
        }
        else
        {
            mHexBg.isGenHeight = isGenheight;
            mHexBg.ReInit(xIdx, yIdx);
        }

        if (mHexWater == null)
        {
            mHexWater = mapWaterBg.AddComponent<THEXWATER>();
            mHexWater.SetBounds(
                new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
            mHexWater.Init(hex, xIdx, yIdx);
        }
        else
        {
            mHexWater.ReInit(xIdx, yIdx);
        }

        UpdateSprList();
    }

    public void InitBlend(bool isGenheight = true)
    {
        bool initLeft = false;
		if (mHexBlendL == null && left != null)
        {
            if(xIdx==0 && yIdx==0)
            {
                Debug.Log("kk");
            }

            if (xIdx > 0)
            {
                mHexBlendL = mapEdgeLeft.AddComponent<THEXBLENDL>();
                mHexBlendL.isGenHeight = isGenheight;
                mHexBlendL.SetBounds(
                    new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                    new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
                initLeft = true;
            }
        }

        bool initPrev = false;
        if (mHexBlendP == null && prev != null)
        {
            if (xIdx == 0 && yIdx == 0)
            {
                Debug.Log("kk");
            }

            if (yIdx > 0)
            {
                mHexBlendP = mapEdgePrev.AddComponent<THEXBLENDP>();
                mHexBlendP.isGenHeight = isGenheight;
                mHexBlendP.SetBounds(
                    new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                    new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
                initPrev = true;
            } 
        }

        bool initDiag = false;
        if (mHexBlendD == null && left !=null && prev != null)
        {
            if (xIdx == 0 && yIdx == 0)
            {
                Debug.Log("kk");
            }

            if (xIdx > 0 && yIdx>0)
            {
                mHexBlendD = mapEdgeDiag.AddComponent<THEXBLENDD>();
                mHexBlendD.isGenHeight = isGenheight;
                mHexBlendD.SetBounds(
                    new Vector3(-layout.HexTileWidth, -layout.HexTileWidth, -10),
                    new Vector3(layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
                initDiag = true;
            }
        }

        UpdateInterSprList();

        if (initLeft)
        {
            mHexBlendL.Init(hex, xIdx, yIdx);
        }
        else if(mHexBlendL != null && left != null)
        {
            mHexBlendL.ReInit(xIdx, yIdx);
        }

        if (initPrev)
        {
            mHexBlendP.Init(hex, xIdx, yIdx);
        }
        else if (mHexBlendP != null && prev != null)
        {
            mHexBlendP.ReInit(xIdx, yIdx);
        }

        if (initDiag)
        {
            mHexBlendD.Init(hex, xIdx, yIdx);
        }
        else if (mHexBlendD != null && left!=null && prev != null)
        {
            mHexBlendD.ReInit(xIdx, yIdx);
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
