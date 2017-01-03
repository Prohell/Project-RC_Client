using UnityEngine;
using System.Collections.Generic;

public class HexSubMapView : MonoBehaviour
{
    public bool inUse = false;
    public int xIdx = 0;
    public int yIdx = 0;
    public GameObject mapHexBg;
    public GameObject mapWaterBg;
	public GameObject mapBlendBg;
    public HEX hex = null;
    public HexMapLayout layout = null;
    public Dictionary<string, MapTile> tileTmpDict = null;
    public Transform blockRoot;

    public THEXGON mHexBg = null;
    public THEXWATER mHexWater = null;
	public THEXBLEND mBlendBg = null;

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

		if(mapBlendBg==null)
		{
			mapBlendBg = new GameObject();
			mapBlendBg.name = "MapBlendBg";
			mapBlendBg.transform.parent = mapHexBg.transform.parent;
			//mapBlendBg = Instantiate (mapHexBg);
		}
		mapBlendBg.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		mapBlendBg.transform.localPosition = new Vector3(0f, 8f, 0f);
		mapBlendBg.transform.localScale = new Vector3 (1, 1, 1);
    }

	public void UpdateSprList()
    {
        if (mapProxy == null)
        {
            mapProxy = GameFacade.GetProxy<MapProxy>();
        }
        List<int> sprs = mapProxy.GetSprLists(xIdx, yIdx);

		mHexBg.spriteId.Clear();
		mHexBg.spriteId.AddRange(sprs);
        
        mHexWater.spriteId.Clear();
        mHexWater.spriteId.AddRange(sprs);
    }
  
    public void UpdateInterSprList()
    {
        if (mBlendBg)
        {
            MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();

            MapTileVO theTile = mapProxy.GetTile(mHexBg._xTile * mHexBg._x, mHexBg._yTile * mHexBg._y);
            int theMat = theTile.mat;
            if (theMat > 3)
                theMat = 0;
            mBlendBg.spriteId.Clear();
            mBlendBg.spriteId.Add(mHexBg.spriteId[theMat]);
            mBlendBg.spriteId.Add(0);
            mBlendBg.spriteId.Add(0);
            mBlendBg.spriteId.Add(0);

            int leftMat = -1;
            int prevMat = -1;
            int diagMat = -1;

            if(left!=null)
            {
                MapTileVO leftTile = mapProxy.GetTile(left.mHexBg._xTile * left.mHexBg._x, left.mHexBg._yTile * left.mHexBg._y);
                leftMat = leftTile.mat;
                if (leftMat > 3)
                    leftMat = 0;
            }
            
            if(prev!=null)
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
            if (left != null && left.mHexBg.spriteId.Count > 0)
            {
                hasLeft = true;
            }

            bool hasPrev = false;
            if (prev != null && prev.mHexBg.spriteId.Count > 0)
            {
                hasPrev = true;
            }

            mBlendBg.left = hasLeft;
            mBlendBg.prev = hasPrev;
       
            if (hasLeft && !hasPrev)
            {
                mBlendBg.spriteId[1] = left.mHexBg.spriteId[leftMat];
            }

            if (hasPrev && !hasLeft)
            {
                mBlendBg.spriteId[2] = prev.mHexBg.spriteId[prevMat];
            }

            if (hasLeft && hasPrev)
            {
                mBlendBg.spriteId[1] = left.mHexBg.spriteId[leftMat];
                mBlendBg.spriteId[2] = prev.mHexBg.spriteId[prevMat];
                if (diag != null && diag.mHexBg != null)
                {
                    mBlendBg.spriteId[3] = diag.mHexBg.spriteId[diagMat];
                }
            }
        }
    }

    public void InitBg(bool isGenheight = true)
    {
		if (mHexBg == null) {
			mHexBg = mapHexBg.AddComponent<THEXGON> ();
			mHexBg.isGenHeight = isGenheight;
			mHexBg.SetBounds (
				new Vector3 (-layout.HexTileWidth, -layout.HexTileWidth, -10),
				new Vector3 (layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
			mHexBg.Init(hex, xIdx, yIdx);
		} else {
			mHexBg.isGenHeight = isGenheight;
			mHexBg.ReInit(xIdx, yIdx);
		}

		if (mHexWater == null) {
			mHexWater = mapWaterBg.AddComponent<THEXWATER> ();
			mHexWater.SetBounds (
				new Vector3 (-layout.HexTileWidth, -layout.HexTileWidth, -10),
				new Vector3 (layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
			mHexWater.Init (hex, xIdx, yIdx);
		} else {
			mHexWater.ReInit(xIdx, yIdx);
		}

		if (mBlendBg == null) {
			if (left != null || prev != null) {
				mBlendBg = mapBlendBg.AddComponent<THEXBLEND> ();
				mBlendBg.isGenHeight = isGenheight;
				mBlendBg.SetBounds (
					new Vector3 (-layout.HexTileWidth, -layout.HexTileWidth, -10),
					new Vector3 (layout.HexTileWidth * hex.xTile, layout.HexTileWidth * hex.yTile, 10));
				mBlendBg.Init(hex, xIdx, yIdx);
			}
		} else {
			if (left != null || prev != null) {
				mBlendBg.ReInit (xIdx, yIdx);
			}
		}

        UpdateSprList();
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
