using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class HEX : MonoBehaviour
{
    public struct SprVO
    {
        public int id;
        public string name;
        public Vector4 info;
    }
    //在THEXGON.cs中的THEXGON.GenHexMesh中被引用
    public Vector4 fvHex = new Vector4(0.5f, 0.0f, 0.0f, 0.0f);
    public int xMax, yMax, xTile, yTile;

    //在THEXGON.cs中的THEXGON.GenMeshWithBound中被引用
	public List<Material> HexTerrainMaterials;
    public List<Material> HexWaterMaterials;

    private Dictionary<int, SprVO> spritePosList = null;
    struct TextureInfo
    {
        public string name;
        public Vector2 size;
        public int format;
        public int length;
        public int mipmapCount;
    };
    //private Dictionary<string, Vector4> spritePosList = new Dictionary<string, Vector4>();
    private TextureInfo textureInfo;
    public Texture2D altasTexture;

    //Unity3D回调
    void Start()
    {
        Init();
        //Hexing(true);
    }

    public List<string> GetSprNames()
    {
        List<string> ret = new List<string>();
        foreach (SprVO vo in spritePosList.Values)
        {
            ret.Add(vo.name);
        }
        return ret;
    }
    public Vector4 GetSpritePosInfoByName(int spriteName)
    {
        if (spritePosList.Count <= 0 || !spritePosList.ContainsKey(spriteName))
        {
            return Vector4.zero;
        }
        else
        {
            Vector4 posInfo = spritePosList[spriteName].info;
            return posInfo;
        }
    }



    private Texture2D LoadAltasAsTexture()
    {
        bool mipmap = textureInfo.mipmapCount > 1 ? true : false;
        altasTexture = new Texture2D((int)textureInfo.size.x, (int)textureInfo.size.y, (TextureFormat)textureInfo.format, mipmap);
        TextAsset ta = Resources.Load(MapPrefabDef.MapAtlas, typeof(TextAsset)) as TextAsset;
        altasTexture.LoadRawTextureData(ta.bytes);
        altasTexture.Apply();
        altasTexture.anisoLevel = 3;
        altasTexture.filterMode = FilterMode.Trilinear;
        altasTexture.wrapMode = TextureWrapMode.Clamp;
        return altasTexture;
    }

    private void IniSpritePosListAndTextureInfo()
    {
        if (spritePosList == null)
        {
            spritePosList = new Dictionary<int, SprVO>();
        }
        TextAsset ta = Resources.Load(MapPrefabDef.MapTerrainSprData, typeof(TextAsset)) as TextAsset;
        string[] alldataRow;
        alldataRow = ta.text.Split('\n');
        int index = 0;
        foreach (string line in alldataRow)
        {
            if (line == "" || line == "\r") continue;
            if(index == 0)
            {
                string[] atlasInfo = line.Split(' ');
                textureInfo.name = atlasInfo[0];
                textureInfo.size = new Vector2(int.Parse(atlasInfo[1]), int.Parse(atlasInfo[2]));
                textureInfo.format = int.Parse(atlasInfo[3]);
                textureInfo.length = int.Parse(atlasInfo[4]);
                textureInfo.mipmapCount = int.Parse(atlasInfo[5]);
            }
            else
            {
                string[] info = line.Split(' ');
                string spriteName = info[0];
                float v1 = float.Parse(info[1]);
                float v2 = float.Parse(info[2]);
                float v3 = float.Parse(info[3]);
                float v4 = float.Parse(info[4]);

                SprVO vo;
                vo.id = int.Parse(spriteName.Split('_')[0]);
                vo.name = spriteName;
                vo.info = new Vector4(v1, v2, v3, v4);
                spritePosList.Add(vo.id, vo);
            }
            index++;
        }
        LoadAltasAsTexture();
    }

    private string MyReadLine(StreamReader sr)
    {
        string strLine = sr.ReadLine();
        if (strLine == null)
        {
            return string.Empty;
        }
        else
        {
            if (strLine == "")
            {
                strLine = sr.ReadLine();
            }
            return strLine;
        }


    }

    private void Init()
    {
        //Unity3D中世界坐标系 Y轴向上 左手系
        gameObject.transform.localScale = new Vector3(1.0f, MapConst.MAP_HEIGHT_UNIT, 1.0f);
        IniSpritePosListAndTextureInfo();
        //transform.Rotate(90, 0, 0);
        return;
    }

	bool ModiRq = false;
	public Material[] GetWaterMats()
	{
		if (HexWaterMaterials.Count < 2)
			return new Material[2];

		if (ModiRq == false ){
			if (HexWaterMaterials [0] != null && HexWaterMaterials [1] != null) {
				HexWaterMaterials [0].renderQueue = HexWaterMaterials [0].shader.renderQueue;
				HexWaterMaterials [1].renderQueue = HexWaterMaterials [0].renderQueue + 1;
			}
			ModiRq = true;
		}
		return HexWaterMaterials.ToArray ();
	}

	bool ModiRq2 = false;
	public Material[] GetTerrainMats()
	{
		if (HexTerrainMaterials.Count < 2)
			return new Material[2];

		if (ModiRq2 == false ){
			if (HexTerrainMaterials [0] != null && HexTerrainMaterials [1] != null) {
				HexTerrainMaterials [0].renderQueue = HexTerrainMaterials [0].shader.renderQueue;
				HexTerrainMaterials [1].renderQueue = HexTerrainMaterials [0].renderQueue + 1;
			}
			ModiRq2 = true;
		}
		return HexTerrainMaterials.ToArray ();
	}

    //未被使用
    //private HexData[][] _hexdata = null;

    //未被使用
    //private HexData hDefault = new HexData();

    //未被使用
    //public THEXGON[][] _tile = null;

    //未被使用
    //private float Tx = Mathf.Sin(Mathf.PI / 3.0f);

    //未被使用
    //private int[] _lim = new int[2];

    //未被使用
    //private int Dx, Dy;

    /*未被使用
    public void SetData(HexData[][] _data)
    {
        _hexdata = _data;
    }
	*/

    /*未被使用
    public void Hexing(bool bUseRandom)
    {
        hDefault.nLev = 0;
        hDefault.nMat = 0;

        if (bUseRandom)
        {
            _hexdata = new HexData[yMax][];
            Random.InitState(19000);
            int nLev, nMat;
            for (int y = 0; y < yMax; y++)
            {
                _hexdata[y] = new HexData[xMax];
                for (int x = 0; x < xMax; x++)
                {
                    var r = Random.Range(0.0f, 1.0f) * 7;
                    r += Random.Range(0.0f, 1.0f) * 11;
                    r *= Random.Range(0.0f, 1.0f) * 17;
                    r += Random.Range(0.0f, 1.0f) * 29;
                    r = r - Mathf.Floor(r);

                    nMat = (ushort)Random.Range(0, 8);
                    if (x == 0 && y == 0)
                    {
                        nLev = (int)(r * 10);
                        _lim[0] = nLev;
                        _lim[1] = nLev;
                    }
                    else
                    {
                        // 相邻高度限制，可修改为按奇偶行数，取限制3边
                        int l0, l1, l2, l3;
                        l0 = l1 = l2 = l3 = -1000;
                        if (x > 0)
                        {
                            l0 = _hexdata[y][x - 1].nLev;
                        }
                        if (y > 0)
                        {
                            l2 = _hexdata[y - 1][x].nLev;
                            if (x > 0)
                                l1 = _hexdata[y - 1][x - 1].nLev;
                            if (x < xMax - 1)
                                l3 = _hexdata[y - 1][x + 1].nLev;
                        }
                        int xl = Mathf.Max(Mathf.Max(Mathf.Max(l0, l1), l2), l3);
                        if (l0 == -1000)
                            l0 = 1000;
                        if (l1 == -1000)
                            l1 = 1000;
                        if (l2 == -1000)
                            l2 = 1000;
                        if (l3 == -1000)
                            l3 = 1000;
                        int il = Mathf.Min(Mathf.Min(Mathf.Min(l0, l1), l2), l3);

                        if (il == xl)
                        {
                            nLev = r < 0.333f ? il : r < 0.666f ? il - 1 : il + 1;
                        }
                        else
                        {
                            if (xl - il > 1)
                                nLev = xl - 1;
                            else
                                nLev = r < 0.5f ? il : xl;
                        }

                        nLev = nLev < 0 ? 0 : nLev > 255 ? 255 : nLev;

                        _lim[0] = Mathf.Min(nLev, _lim[0]);
                        _lim[1] = Mathf.Max(nLev, _lim[1]);
                        HexData dat;
                        dat.nMat = (byte)nMat;
                        dat.nLev = (byte)nLev;
                        _hexdata[y][x] = dat;
                    }
                }
            }
        }

        if (_hexdata == null)
            return;

        // all boundry and tiling number request to be even, automatic shift if not
        if (xMax % 2 > 0) xMax++;
        if (yMax % 2 > 0) yMax++;
        if (xTile % 2 > 0) xTile++;
        if (yTile % 2 > 0) yTile++;

        Dx = Mathf.CeilToInt((float)xMax / xTile);
        Dy = Mathf.CeilToInt((float)yMax / yTile);

        _tile = new THEXGON[Dy][];
        //float R = Tx * 2.0f + fvHex.x;

        //if (IsTest)
        //{
        //    _tile[indexY] = new THEXGON[Dx];
        //    string s = string.Format("HexTile_{0}_{1}", indexY, indexX);
        //    GameObject obj = new GameObject();
        //    obj.name = s;
        //    obj.transform.parent = this.transform;
        //    obj.transform.Translate(new Vector3(R * indexX * xTile, R * Tx * indexY * yTile, 0));
        //    _tile[indexY][indexX] = obj.AddComponent<THEXGON>();
        //    _tile[indexY][indexX].Init(this, indexX, indexY);
        //    indexX++;
        //}
        //else
        //{
        //    for (; indexY < Dy; indexY++)
        //    {
        //        _tile[indexY] = new THEXGON[Dx];
        //        for (; indexX < Dx; indexX++)
        //        {
        //            string s = string.Format("HexTile_{0}_{1}", indexX, indexY);
        //            GameObject obj = new GameObject();
        //            obj.name = s;
        //            obj.transform.parent = this.transform;
        //            obj.transform.Translate(new Vector3(R * indexX * xTile, R * Tx * indexY * yTile, 0));
        //            _tile[indexY][indexX] = obj.AddComponent<THEXGON>();
        //            _tile[indexY][indexX].Init(this, indexX, indexY);
        //        }
        //    }
        //}

        transform.localScale = new Vector3(1.0f, 1.0f, 0.25f);
        transform.Rotate(90, 0, 0);
        return;
    }
    */

    /*未被使用
    public HexData GetData(int x, int y)
    {
        if (_hexdata == null)
            return hDefault;

        if (x < 0 || x >= xMax)
            return hDefault;
        if (y < 0 || y >= yMax)
            return hDefault;

        return _hexdata[y][x];
    }
    */

    //int indexY = 0;
    //int indexX = 0;
    /*未被使用
    public GameObject AddBlock(int indexX, int indexY)
    {
        float R = Tx * 2.0f + fvHex.x;
        if (indexY >= Dy) return null;

        if (_tile[indexY] == null)
        {
            _tile[indexY] = new THEXGON[Dx];
        }
        string s = string.Format("HexTile_{0}_{1}", indexX, indexY);
        GameObject obj = new GameObject();
        obj.name = s;
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(R * indexX * xTile, R * Tx * indexY * yTile, 0);
        obj.transform.localScale = Vector3.one;
        obj.transform.Rotate(90, 0, 0);
        _tile[indexY][indexX] = obj.AddComponent<THEXGON>();
        _tile[indexY][indexX].Init(this, indexX, indexY);
        //if (indexX == Dx - 1)
        //{
        //    indexX = 0;
        //    indexY++;
        //}
        //else
        //{
        //    indexX++;
        //}
        return obj;
    }
    */
}