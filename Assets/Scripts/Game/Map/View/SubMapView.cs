using UnityEngine;
using System.Collections.Generic;

public class SubMapView : MonoBehaviour
{
    public bool inUse = false;
    public const int xTileCnt = 16;
    public const int yTileCnt = 16;
    public int xIdx = 0;
    public int yIdx = 0;
    public MapLayout layout = null;
    public Dictionary<string, MapTile> tileTmpDict = null;

    MapHexBg hexBg = null;
    public Sprite2DList mapSprites;

    Dictionary<Coord, MapTile> tiles = new Dictionary<Coord, MapTile>();

    public Transform blockRoot;
    public SpriteRenderer blockTmp;
    Dictionary<Coord, SpriteRenderer> blocks = new Dictionary<Coord, SpriteRenderer>();

    ~SubMapView()
    {
        Debug.Log("SubMapView Destruct (" + xIdx + " : " + yIdx + ")");
    }

    void OnDestory()
    {
        DestroyTiles();
        tiles = null;

        DestroyBlocks();
        blocks = null;
    }

    public void InitPos()
    {
        transform.localPosition = new Vector3(
            layout.HexTileWidth * xIdx * xTileCnt,
            0,
            layout.HexTileHeight * yIdx * yTileCnt);
    }

    public void InitBg()
    {
        hexBg = gameObject.GetComponentInChildren<MapHexBg>();
        hexBg.layout = layout;
        hexBg.xMin = xIdx * xTileCnt;
        hexBg.width = xTileCnt;
        hexBg.yMin = yIdx * yTileCnt;
        hexBg.height = yTileCnt;
        hexBg.Init();
    }

    //MapTile CreateCityTile(MapTileVO tileVO, proto_structs.MapCity cityData)
    //   {
    //	MapTile tile = App.GameObjectPool.SpawnGo(tileTmpDict[MapPrefabDef.MAP_TILE_CITY].gameObject).GetComponent<MapTile>();
    //	tile.gameObject.name = MapPrefabDef.MAP_TILE_CITY;

    //	tile.coord = new Coord((int)cityData.x, (int)cityData.y);
    //	var title = cityData.playerName;
    //	title = title.Replace("Player_P_", "");
    //	tile.SetData(title, (int)cityData.cityLevel, tileVO.camp, cityData.allianceTag, cityData.allianceFlag, cityData.defenseUntil, MapTileType.Camp, true, 
    //	             TempRelationFunc.GetRelation(cityData.playerId, cityData.allianceId, proto_structs.CampType.CAMP_TYPE_E));

    //	return tile;
    //}

    //MapTile CreateResourceTile(proto_structs.MapResource resourceData)
    //   {
    //	string prefabName = MapPrefabDef.GetMapTileResourceByLevel (resourceData.resourceType, resourceData.resourceLevel);
    //	MapTile template = tileTmpDict[prefabName];
    //	MapTile tile = App.GameObjectPool.SpawnGo(template.gameObject).GetComponent<MapTile>();
    //	tile.gameObject.name = prefabName;
    //	tile.coord = new Coord((int)resourceData.x, (int)resourceData.y);

    //	if (resourceData.owner != null) {
    //		tile.SetData ("", (int)resourceData.resourceLevel, 0, null, null, -1, MapTileType.Camp, true
    //		              , TempRelationFunc.GetRelation(resourceData.owner.playerId, resourceData.owner.allianceId, proto_structs.CampType.CAMP_TYPE_E));
    //	} 
    //	else {
    //		tile.SetData ("", (int)resourceData.resourceLevel, 0, null, null, -1, MapTileType.Camp, false);
    //	}
    //	return tile;
    //}

    ////TODO. Dummy Implement
    //MapTile CreatePVETile(MapTileVO pvetile)
    //{
    //	MapTile tile = App.GameObjectPool.SpawnGo(tileTmpDict[MapPrefabDef.MAP_TILE_PVE].gameObject).GetComponent<MapTile>();
    //	tile.gameObject.name = MapPrefabDef.MAP_TILE_PVE;
    //	tile.coord = pvetile.coord;
    //	//tile.SetData(DI18n.T("pvetile.pvetile" + (pvetile.camp1+1) + ".name"), pvetile.pveLevel, 0);
    //	tile.SetData("", pvetile.pveLevel, 0);
    //	ulong endAt = 0;
    //	if(App.ProxyMgr.RaidRecordProxy.IsInCoolDown((uint)pvetile.coord.x, (uint)pvetile.coord.y, pvetile.pveLevel, out endAt))
    //	{
    //		tile.ShowRuinCooldown(endAt);
    //	}
    //	else{
    //		tile.HideCooldown();
    //	}
    //	return tile;
    //}

    public void InitTiles()
    {
#if UNITY_EDITOR
        if (MapView.Current.EditMode)
        {
            return;
        }
#endif
        for (int x = 0; x < xTileCnt; ++x)
        {
            for (int y = 0; y < yTileCnt; ++y)
            {
                InitTile(new Coord(x + xTileCnt * xIdx, y + yTileCnt * yIdx));
            }
        }
    }

    void InitTile(Coord c)
    {
        //      MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();
        //MapTileVO tileVo = mapProxy.GetTile(c);
        //if (tileVo != null)
        //      {
        //MapTile tile = null;
        // TileData是玩家信息，从服务器获取
        //MapProxy.TileData data = null;
        //if (null != MapView.Current && !MapView.Current.EditMode){
        //	data = mapProxy.GetTileData(tileVo.coord);
        //}

        //if (null != data){
        //	if (null != data.cityData){
        //		tile = CreateCityTile(tileVo, data.cityData);
        //		Utils.AddChild(gameObject, tile.gameObject);
        //		tile.transform.localPosition = layout.HexCenter(tileVo.coord) - this.transform.localPosition;
        //		tiles.Add(c, tile);
        //	}else if (null != data.resourceData){
        //		//Debug.LogError("InitTile  null != data.resourceData " + c.ToString() + " subViewName:" + gameObject.name);
        //		tile = CreateResourceTile(data.resourceData);
        //		Utils.AddChild(gameObject, tile.gameObject);
        //		tile.transform.localPosition = layout.HexCenter(tileVo.coord) - this.transform.localPosition;
        //		tiles.Add(c, tile);
        //	}else{
        //		Debug.LogError("illegal map tile data!");
        //	}
        //}
        //else
        //         {
        //if (tileVo.type == MapTileType.PVE){
        //	tile = CreatePVETile(tileVo);
        //	Utils.AddChild(gameObject, tile.gameObject);
        //	tile.transform.localPosition = layout.HexCenter(tileVo.coord) - this.transform.localPosition;
        //	tiles.Add(c, tile);
        //}
        //	}
        //}
    }

    public void DestroyTiles()
    {
        foreach (var item in tiles)
        {
            if (item.Value != null)
            {
                //item.Value.OnRecycle();
                GameObjectPool.GetInstance().RecycleGo(item.Value.gameObject.name, item.Value.gameObject);
            }
        }
        tiles.Clear();
    }

    SpriteRenderer CreateBlock(MapTileVO tile)
    {
        SpriteRenderer block = GameObjectPool.GetInstance().SpawnGo(blockTmp.gameObject).GetComponent<SpriteRenderer>();
        block.gameObject.name = blockTmp.gameObject.name;

        int blockIdx = Mathf.Clamp(tile.camp1, 0, 2);
        if (tile.blockType == MapBlockType.Mountain)
        {
            block.sprite = mapSprites.Get(MapTileVO.Camp2String(tile.camp) + "_mountain_" + blockIdx);
        }
        else if (tile.blockType == MapBlockType.Forest)
        {
            block.sprite = mapSprites.Get(MapTileVO.Camp2String(tile.camp) + "_tree_" + blockIdx);
        }

        return block;
    }

    public void InitBlocks()
    {
        for (int x = 0; x < xTileCnt; ++x)
        {
            for (int y = 0; y < yTileCnt; ++y)
            {
                Coord c = new Coord(x + xTileCnt * xIdx, y + yTileCnt * yIdx);
                MapTileVO tileVo = GameFacade.GetProxy<MapProxy>().GetTile(c);
                if (tileVo != null)
                {
                    if (!tileVo.IsWater())
                    {
                        SpriteRenderer block = CreateBlock(tileVo);
                        block.transform.parent = blockRoot;
                        block.gameObject.SetActive(true);
                        block.transform.localPosition = layout.HexCenter(tileVo.coord) - this.transform.localPosition;
                        blocks.Add(c, block);
                    }
                }
            }
        }
    }

    public void DestroyBlocks()
    {
        foreach (var item in blocks)
        {
            if (item.Value != null)
            {
                GameObjectPool.GetInstance().RecycleGo(blockTmp.name, item.Value.gameObject);
            }
        }
        blocks.Clear();
    }

    public void SetPitch(float eulurX)
    {
        foreach (var item in tiles)
        {
            if (item.Value != null)
            {
                item.Value.SetPitch(eulurX);
            }
        }

        foreach (var item in blocks)
        {
            if (item.Value != null)
            {
                item.Value.transform.localEulerAngles = new Vector3(eulurX, 0, 0);
            }
        }
    }

    #region relocate effect
    public GameObject DetachSingleTile(Coord c)
    {
        MapTile tile = null;
        tiles.TryGetValue(c, out tile);
        if (null != tile)
        {
            tiles.Remove(c);
            tile.gameObject.transform.parent = null;
            return tile.gameObject;
        }
        return null;
    }

    public void DestroySingleTile(Coord c)
    {
        MapTile tile = null;
        tiles.TryGetValue(c, out tile);
        if (null != tile)
        {
            tiles.Remove(c);
            GameObject.DestroyImmediate(tile.gameObject);
        }
    }

    public void RecreateSingleTile(Coord c)
    {
        DestroySingleTile(c);
        InitTile(c);
    }
    #endregion
}
