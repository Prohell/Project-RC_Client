using UnityEngine;
using System.Collections.Generic;

public class HEX : MonoBehaviour
{
    //在THEXGON.cs中的THEXGON.GenHexMesh中被引用
    public Vector4 fvHex = new Vector4(0.5f, 0.0f, 0.0f, 0.0f);
    public int xMax, yMax, xTile, yTile;

    //在THEXGON.cs中的THEXGON.GenMeshWithBound中被引用
    public Material HexMaterial;

    public Material HexWaterMaterial;

    //在THEXGON.cs中的THEXGON.UpdateBlock中被引用
    public List<Sprite> HexSprites;

    //Unity3D回调
    void Start()
    {
        Init();
        //Hexing(true);
    }

    private void Init()
    {
        //Uber Shader(宏着色器)
        //HexMaterial.DisableKeyword("DEPTH_ON");
        //HexMaterial.EnableKeyword("DEPTH_OFF");
        
        //Unity3D中世界坐标系 Y轴向上 左手系
        gameObject.transform.localScale = new Vector3(1.0f, MapConst.MAP_HEIGHT_UNIT, 1.0f);
        
        //transform.Rotate(90, 0, 0);

        return;
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