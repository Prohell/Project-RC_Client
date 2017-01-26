
#define HEXGON0

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class THEXGON : MonoBehaviour
{
    public List<int> spriteIds = new List<int>();
    /// <summary>
    /// 是否有高度
    /// </summary>
    public bool isGenHeight = true;
	public bool isBlend = false;
    
    //Unity3D中包围盒
    private Bounds _bound;
    private Mesh mPlan;

    //在HexSubMapView.cs中的HexSubMapView.InitBg中被引用
    //初始化 _bound
    public void SetBounds(Vector3 min, Vector3 max)
    {
        Vector3 e = (max - min) * 0.5f;
        Vector3 c = e + min;
        _bound = new Bounds(c, e * 2.0f);
    }

    //来自Hex
    private System.WeakReference _weak_hex;
    //private WeakReference _weak_mat;
    //private WeakReference _weak_mesh;
    public int _x;
    public int _y;
    public int _xTile;
    public int _yTile;

	public HEX GetHex()
	{
		var _hex = _weak_hex.Target as HEX;
		return (HEX)_hex;
	}

    /*
    private Dictionary<Camera, Camera> m_ReflectionCameras = new Dictionary<Camera, Camera>(); // Camera -> Camera table
    private Dictionary<Camera, Camera> m_RefractionCameras = new Dictionary<Camera, Camera>(); // Camera -> Camera table
    private RenderTexture m_ReflectionTexture;
    public int textureSize = 256;
    private int m_OldReflectionTextureSize;
    private static bool s_InsideWater;

    public LayerMask reflectLayers = -1;
    public float clipPlaneOffset = 0.07f;*/


    //在HexSubMapView.cs中的HexSubMapView.InitBg中被引用
    //初始化来自Hex的数据
    public void Init(HEX hex, int x, int y)
    {
        _weak_hex = new System.WeakReference(hex);
        _x = x;
        _y = y;
        int z = (x + 1) * hex.xTile - hex.xMax;
        int w = (y + 1) * hex.yTile - hex.yMax;
        _xTile = z > 0 ? hex.xTile - z : hex.xTile;
        _yTile = w > 0 ? hex.xTile - w : hex.yTile;
        GenMeshWithBound(hex);
        GenHexMesh(hex);
    }

    public void ReplaceMesh()
    {
        var mf = GetComponent<MeshFilter>();
        if (mf)
        {
            mf.sharedMesh = mPlan;
        }
    }

    public void ReInit(int x, int y)
    {
        if (!_weak_hex.IsAlive)
        {
            return;
        }

        var _hex = _weak_hex.Target as HEX;
        _x = x;
        _y = y;
        int z = (x + 1) * _hex.xTile - _hex.xMax;
        int w = (y + 1) * _hex.yTile - _hex.yMax;
        _xTile = z > 0 ? _hex.xTile - z : _hex.xTile;
        _yTile = w > 0 ? _hex.xTile - w : _hex.yTile;
        GenHexMesh(_hex);
    }

    bool IsValid(int x, int y)
    {
        int xMin = 0, yMin = 0;

        if (_x > 0)
            xMin = -1;
        if (_y > 0)
            yMin = -1;
        if (x < xMin || y < yMin)
            return false;
        if (x >= _xTile || y >= _yTile)
            return false;
        return true;
    }

    private MaterialPropertyBlock _block = null;
    public MaterialPropertyBlock UpdateBlock()
    {
        if (_block == null)
        {
            _block = new MaterialPropertyBlock();   // make it lazy
        }
        HEX hex = _weak_hex.Target as HEX;
        Dictionary<int, int> TSet = new Dictionary<int, int>();
        List<Vector4> TArray = new List<Vector4>();
        for (int i = 0; i < spriteIds.Count; i++){
			Vector4 pos = hex.GetSpritePosInfoByName(spriteIds[i]);
			TArray.Add(pos);
		}	
		if(TArray.Count!=0){
			_block.SetVectorArray("_ARRAY", TArray.ToArray());
		}

		_block.SetTexture("_ATLAS", hex.altasTexture);

        var mr = gameObject.GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.SetPropertyBlock(_block);
        }

        return _block;
    }

    static private Vector3[] tri_ver;
    static private int[] tri_idx = new int[] { 0, 3, 2, 3, 0, 1 };
    void GenMeshWithBound(HEX _hex)
    {
        if (tri_ver == null)
        {
            tri_ver = new Vector3[] { new Vector3(_bound.min.x, _bound.min.y, 0), new Vector3(_bound.max.x, _bound.min.y, 0), new Vector3(_bound.min.x, _bound.max.y, 0), new Vector3(_bound.max.x, _bound.max.y, 0) };
        }
        mPlan = new Mesh();
        mPlan.vertices = tri_ver;
        mPlan.triangles = tri_idx;
        mPlan.colors = null;
        mPlan.tangents = null;
        mPlan.uv = null;
        mPlan.bounds = _bound;
        //计算法线
        mPlan.RecalculateNormals();

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        mf.sharedMesh = mPlan;

        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
		mr.sharedMaterials = _hex.GetTerrainMats();
        //UpdateBlock();
        mr.SetPropertyBlock(_block);

        //_weak_mat = new WeakReference(mr.material);
        //_weak_mesh = new WeakReference(mf.mesh);

        //BoxCollider pb = gameObject.AddComponent<BoxCollider>();
        //pb.center = _bound.center;
        //pb.size = _bound.size;

    }

    class element
    {
        public List<Vector3> vHex = new List<Vector3>();
		public List<Vector3> vNor = new List<Vector3>();
        public List<Color> vClr = new List<Color>();
        public List<Vector4> vTan = new List<Vector4>();
        public List<Vector2> vTex = new List<Vector2>();
        public List<Vector2> vNorTex = new List<Vector2>();
    };
	private static float Tx = Mathf.Sin(Mathf.PI / 3.0f);
	private static float Ty = Mathf.Cos(Mathf.PI / 3.0f);
	private static int[] lst = null;

    private delegate int _reindex(ref element e, int ver, int col, int row, float normalz, Vector2 uv2);
	private delegate bool _recheck(int ver, int col, int row);

    float CompareHeight(int thisHeight, int neighborHeight)
    {
        if (neighborHeight == -1)
            return 4.5f;
        else if (thisHeight == neighborHeight)
            return 4.5f;
        else if (thisHeight < neighborHeight)
            return 3.5f;
        else if (thisHeight > neighborHeight)
            return 5.5f;
        return 4.5f;
    }

    Coord GetNeighborCoord(int thisCoordX, int thisCoordY, int neighborInd)
    {
        int[,,] neighborOffset = new int[,,]
        {
            {
                { 1, 1 },
                { 1, 0 },
                { 1, -1 },
                { 0, -1 },
                { -1, 0 },
                { 0, 1 },
            },
            {
                { 0, 1 },
                { 1, 0 },
                { 0, -1 },
                { -1, -1 },
                { -1, 0 },
                { -1, 1 },
            }
        };

        Coord c_neighbor;
        if (thisCoordY % 2 == 0)
        {
            c_neighbor = new Coord(thisCoordX + neighborOffset[1, neighborInd, 0], thisCoordY + neighborOffset[1, neighborInd, 1]);
        }
        else
        {
            c_neighbor = new Coord(thisCoordX + neighborOffset[0, neighborInd, 0], thisCoordY + neighborOffset[0, neighborInd, 1]);
        }
        return c_neighbor;
    }

    int GetNeighborHeight(int thisCoordX, int thisCoordY, int neighborInd)
    {
        int height_neighbor = -1;
        Coord c_neighbor= GetNeighborCoord(thisCoordX, thisCoordY, neighborInd);
        MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();
        MapTileVO t_neighbor = mapProxy.GetTile(c_neighbor);
        if (t_neighbor != null)
        {
            height_neighbor = t_neighbor.height;
        }
        return height_neighbor;
    }

    int CompareHeight_Tri0(int height5, int height4, int thisheight)
    { // smaller number means higher
        if(height5>height4 && height5>thisheight)
        {
            return 0;
        }
        else if(height5>height4 && thisheight>height4)
        {
            return 1;
        }
        else if (height5>thisheight && height4>thisheight)
        {
            return 2;
        }
        else if (height4>height5 && thisheight>height5)
        {
            return 3;
        }
        else if (thisheight>height4 && thisheight>height5)
        {
            return 4;
        }
        else if (height4>height5 && height4>thisheight)
        {
            return 5;
        }
        else
        {
            return -1;
        }
    }

void GenHexMesh(HEX _hex)
    {
        MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();
        Vector3[] hexgon = {
#if HEXGON0
            new Vector3(0,0,0),         // 0
#endif
            new Vector3(0,+1,0),        // 1
            new Vector3(+Tx, +Ty, 0),   // 2
            new Vector3(+Tx, -Ty, 0),   // 3
            new Vector3(0,-1,0),        // 4
            new Vector3(-Tx, -Ty, 0),   // 5
            new Vector3(-Tx, +Ty, 0),   // 6
        };

        Vector2[] water = {
            new Vector2(0,0),
            new Vector2(1,0),
        };

        Color[] sc = {
            new Color(0,0,0,0),
            new Color(1,0,0,0),
            new Color(0,1,0,0),
            new Color(0,0,1,0),
            new Color(0,0,0,1),
        };

        Vector4[] vc = {
            new Vector4(0,0,0,0),
            new Vector4(1,0,0,0),
            new Vector4(0,1,0,0),
            new Vector4(0,0,1,0),
            new Vector4(0,0,0,1),
        };

        int siz = (_hex.yTile + 1) * (_hex.yTile + 1) * 8;
        if (lst == null)
        {
            lst = new int[siz];
        }

        for (int i = 0; i < siz; i++) { lst[i] = -1; }

        element e = new element();

		_reindex reindex = (ref element _elm, int _ver, int _col, int _row, float tmpindex, Vector2 uv2) =>
        {
            MapTileVO tileVo = mapProxy.GetTile(_col + _hex.xTile * _x, _row + _hex.yTile * _y);
            int loc = ((_row + 1) * (_hex.xTile + 1) + (_col + 1)) * 8 + _ver;
            int idx = lst[loc];
            //if (idx >= 0)
            //    return idx;

            idx = _elm.vHex.Count;
            lst[loc] = idx;

			Vector3 _v = Vector3.zero;
			Vector3 _n = Vector3.zero;
			Vector4 _t = Vector4.zero;
			Vector2 _u = Vector2.zero;
            Color _c;
            float _R = Tx * 2.0f + _hex.fvHex.x;

            int m = tileVo.mat;

            /*
			// sz test
			if(_x==0 && _y==0)
			{
				m=0;
			}
			else if(_x==1 && _y==0)
			{
				m=0;
			}
			else if(_x==0 && _y==1)
			{
				m=0;
			}
			else if(_x==1 && _y==1)
			{
				m=0;
			}
            // sz test
            */
			
            int n = isGenHeight ? tileVo.height : MapConst.MAP_HEIGHT_HORIZON;
            if (m == 8)
            {
				_c = sc[0];
                _t = vc[0];
            }
            else
            {
				_c = sc[m <= 3 ? m + 1 : 0];
                _t = vc[m >= 4 ? m - 3 : 0];
            }
            
            _v.x = _R * _col;
            _v.y = _R * Tx * _row;
            _v.z = n;
			_n.z = m;
            if (_row % 2 == 0)
            {
                _v.x += 0.0f;
            }
            else
            {
                _v.x += Tx + Ty * _hex.fvHex.x;
            }

            _elm.vHex.Add(hexgon[_ver] + _v);
			_elm.vNor.Add (_n);

			_elm.vClr.Add(_c);
            
            if(tmpindex<100)
            {
                _t.z = tmpindex;
            }
            else
            {
                _t.w = (int)tmpindex/100;
                _t.z = tmpindex-_t.w*100;
            }
            _elm.vTan.Add(_t);

            _elm.vTex.Add(_u);
            _elm.vNorTex.Add(uv2);
            return idx;
        };

        _recheck recheck = (int _ver, int _col, int _row) =>
        {
            MapTileVO tileVo = mapProxy.GetTile(_col + _hex.xTile * _x, _row + _hex.yTile * _y);
            //if (tileVo.mat < 1)
            if(tileVo.IsWater())
            {
                return false;
            }
            return true;
        };

        int nHexIndices, nIndexOffset;

#if HEXGON0
        int[] tdx =
        {
            0,1,2, 0,2,3, 0,3,4,
            0,4,5, 0,5,6, 0,6,1,
        };
        nHexIndices = 18;
        nIndexOffset = 0;
#else
        int[] tdx =
        {
            0,1,5, 5,1,4, 2,4,1, 3,4,2
        };
        nHexIndices = 12;
        nIndexOffset = 1;
#endif

        int[][] edx =
        {
            new int[] {6,5,2, 3,2,5},
            new int[] {5,4,1, 2,1,4},
            new int[] {1,6,3, 4,3,6},
            new int[] {6,2,4, 5,1,3},
        };

        List<int> vIdx = new List<int>();
		List<int> vSub = new List<int>();
		List<int> vBld = new List<int>();
        bool pass = true;
        
		Color R=new Color(1,0,0,1);
		Color G=new Color(0,1,0,1);
		Color B=new Color(0,0,1,1);
		Color C1=new Color(1,1,1,1);
		Color C0=new Color(0,0,0,0);
		Vector4 V1=new Vector4(1,1,1,1);
		Vector4 V0=new Vector4(0,0,0,0);
        Vector2 uv2_zero = new Vector2(0,0);

        /*Vector2 uv2_normal_block0_6 = new Vector2(0.251f, 0.643f);
        Vector2 uv2_normal_block0_5 = new Vector2(0.743f, 0.643f);
        Vector2 uv2_normal_block0_2 = new Vector2(0.259f, 0.357f);
        Vector2 uv2_normal_block0_3 = new Vector2(0.755f, 0.357f);
        Vector2 uv2_normal_hex_0 = new Vector2(0.5f, 1.0f);
        Vector2 uv2_normal_hex_5 = new Vector2(0.746f, 0.643f);
        Vector2 uv2_normal_hex_6 = new Vector2(0.251f, 0.643f);
        Vector2 uv2_normal_hex_00 = new Vector2(0.5f, 0.0f);
        Vector2 uv2_normal_hex_2 = new Vector2(0.259f, 0.357f);
        Vector2 uv2_normal_hex_3 = new Vector2(0.755f, 0.357f);
        Vector2 uv2_normal_tri_6 = new Vector2(0.251f, 0.643f);
        Vector2 uv2_normal_tri_2 = new Vector2(0.259f, 0.357f);
        Vector2 uv2_normal_tri_4 = new Vector2(0.003f, 0.523f);
        Vector2 uv2_normal_tri_5 = new Vector2(0.746f, 0.643f);
        Vector2 uv2_normal_tri_1 = new Vector2(0.998f, 0.509f);
        Vector2 uv2_normal_tri_3 = new Vector2(0.755f, 0.357f);*/

        Vector2 uv2_normal_block0_6 = new Vector2(0.324f, 0.670f);
        Vector2 uv2_normal_block0_5 = new Vector2(0.662f, 0.670f);
        Vector2 uv2_normal_block0_2 = new Vector2(0.329f, 0.249f);
        Vector2 uv2_normal_block0_3 = new Vector2(0.668f, 0.250f);
        Vector2 uv2_normal_hex_0 = new Vector2(0.5f, 1.0f);
        Vector2 uv2_normal_hex_5 = new Vector2(0.662f, 0.670f);
        Vector2 uv2_normal_hex_6 = new Vector2(0.324f, 0.670f);
        Vector2 uv2_normal_hex_00 = new Vector2(0.5f, 0.0f);
        Vector2 uv2_normal_hex_2 = new Vector2(0.329f, 0.249f);
        Vector2 uv2_normal_hex_3 = new Vector2(0.668f, 0.250f);
        Vector2 uv2_normal_tri_6 = new Vector2(0.324f, 0.670f);
        Vector2 uv2_normal_tri_2 = new Vector2(0.329f, 0.249f);
        Vector2 uv2_normal_tri_4 = new Vector2(0.154f, 0.494f);
        Vector2 uv2_normal_tri_5 = new Vector2(0.662f, 0.670f);
        Vector2 uv2_normal_tri_1 = new Vector2(0.843f, 0.485f);
        Vector2 uv2_normal_tri_3 = new Vector2(0.668f, 0.250f);

        /*Vector2 uv2_normal_block0_6 = new Vector2(0.326f, 0.668f);
        Vector2 uv2_normal_block0_5 = new Vector2(0.660f, 0.668f);
        Vector2 uv2_normal_block0_2 = new Vector2(0.331f, 0.251f);
        Vector2 uv2_normal_block0_3 = new Vector2(0.666f, 0.252f);
        Vector2 uv2_normal_hex_0 = new Vector2(0.5f, 1.0f);
        Vector2 uv2_normal_hex_5 = new Vector2(0.662f, 0.670f);
        Vector2 uv2_normal_hex_6 = new Vector2(0.324f, 0.670f);
        Vector2 uv2_normal_hex_00 = new Vector2(0.5f, 0.0f);
        Vector2 uv2_normal_hex_2 = new Vector2(0.329f, 0.249f);
        Vector2 uv2_normal_hex_3 = new Vector2(0.668f, 0.250f);
        Vector2 uv2_normal_tri_6 = new Vector2(0.326f, 0.672f);
        Vector2 uv2_normal_tri_2 = new Vector2(0.331f, 0.247f);
        Vector2 uv2_normal_tri_4 = new Vector2(0.152f, 0.494f);
        Vector2 uv2_normal_tri_5 = new Vector2(0.660f, 0.668f);
        Vector2 uv2_normal_tri_1 = new Vector2(0.845f, 0.485f);
        Vector2 uv2_normal_tri_3 = new Vector2(0.666f, 0.252f);*/


        for (int y = 0; y < _yTile; y++)
        {
            for (int x = 0; x < _xTile; x++)
            {
                /*if(!(x==1 && y==1 || x== && y==1))
                {
                    continue;
                }*/
                /*if (_x == 1 && _y == 1 && (x < 11 || y < 6))
                {
                    continue;
                }*/

                // get height info
                int thisCoordX = x + _xTile * _x;
                int thisCoordY = y + _yTile * _y;
                Coord c = new Coord(thisCoordX, thisCoordY);
                int thisHeight = mapProxy.GetTile(c).height;
                int height_neighbor0 = GetNeighborHeight(thisCoordX, thisCoordY, 0);
                int height_neighbor1 = GetNeighborHeight(thisCoordX, thisCoordY, 1);
                int height_neighbor2 = GetNeighborHeight(thisCoordX, thisCoordY, 2);
                int height_neighbor3 = GetNeighborHeight(thisCoordX, thisCoordY, 3);
                int height_neighbor4 = GetNeighborHeight(thisCoordX, thisCoordY, 4);
                int height_neighbor5 = GetNeighborHeight(thisCoordX, thisCoordY, 5);

                // block5 left
                Vector2 uv2_hex0_0 = new Vector2(0.0f, 0.5f);
                Vector2 uv2_hex0_1 = new Vector2(0.375f, 0.211f);
                Vector2 uv2_hex0_2 = new Vector2(0.375f, 0.785f);
                // block0 right
                Vector2 uv2_hex3_0 = new Vector2(1.0f, 0.5f);
                Vector2 uv2_hex3_4 = new Vector2(0.621f, 0.785f);
                Vector2 uv2_hex3_5 = new Vector2(0.621f, 0.211f);

                float[] hex_index1 =
                {
                    1.5f,1.5f,1.5f,  0.5f,0.5f,0.5f,  2.5f,2.5f,2.5f,
                    1.5f,1.5f,1.5f,  0.5f,0.5f,0.5f,  2.5f,2.5f,2.5f,
                };

                Vector2[] hex_index2 =
                {
                    uv2_hex0_0, uv2_hex0_1, uv2_hex0_1,
                    uv2_hex0_0, uv2_hex0_1, uv2_hex0_1,
                    uv2_hex0_0, uv2_hex0_1, uv2_hex0_1,
                    uv2_hex3_0, uv2_hex3_4, uv2_hex3_5,
                    uv2_hex3_0, uv2_hex3_4, uv2_hex3_5,
                    uv2_hex3_0, uv2_hex3_4, uv2_hex3_5,
                };

                float heightCompare0 = CompareHeight(thisHeight, height_neighbor0);
                float heightCompare1 = CompareHeight(thisHeight, height_neighbor1);
                float heightCompare2 = CompareHeight(thisHeight, height_neighbor2);
                float heightCompare3 = CompareHeight(thisHeight, height_neighbor3);
                float heightCompare4 = CompareHeight(thisHeight, height_neighbor4);
                float heightCompare5 = CompareHeight(thisHeight, height_neighbor5);

                float[] hex_index3 =
                {
                    thisHeight*100+heightCompare0, thisHeight*100+heightCompare0, thisHeight*100+heightCompare0,
                    thisHeight*100+heightCompare1, thisHeight*100+heightCompare1, thisHeight*100+heightCompare1,
                    thisHeight*100+heightCompare2, thisHeight*100+heightCompare2, thisHeight*100+heightCompare2,
                    thisHeight*100+heightCompare3, thisHeight*100+heightCompare3, thisHeight*100+heightCompare3,
                    thisHeight*100+heightCompare4, thisHeight*100+heightCompare4, thisHeight*100+heightCompare4,
                    thisHeight*100+heightCompare5, thisHeight*100+heightCompare5, thisHeight*100+heightCompare5,
                };

                Vector2[] hex_index4 =
                {
                    uv2_normal_hex_00, uv2_normal_hex_2, uv2_normal_hex_3,
                    uv2_normal_hex_00, uv2_normal_hex_2, uv2_normal_hex_3,
                    uv2_normal_hex_00, uv2_normal_hex_2, uv2_normal_hex_3,
                    uv2_normal_hex_0, uv2_normal_hex_5, uv2_normal_hex_6,
                    uv2_normal_hex_0, uv2_normal_hex_5, uv2_normal_hex_6,
                    uv2_normal_hex_0, uv2_normal_hex_5, uv2_normal_hex_6,
                };

                // hexgon
                for (int i = 0; i < nHexIndices; i++)
                {
                    if (recheck(tdx[i], x, y) == true)
                    {
                        //vIdx.Add(reindex(ref e, tdx[i], x, y, hex_index3[i], hex_index4[i]));
                        vIdx.Add(reindex(ref e, tdx[i], x, y, 4.5f, hex_index4[i]));
                    }
                }        

                // west -> x-1
                // south west -> odd : x, y-1  even: x-1, y-1
                // south -> odd: x+1, y-1 even: x, y-1

                int t = y % 2;
                int ww = x - 1;                 // y
                int sw = t > 0 ? x : x - 1;     // y-1
                int nw = t > 0 ? x : x - 1;		// y+1

                if (IsValid(ww, y))
                {
                    Vector2 uv2_block0_6 = new Vector2(0.621f, 0.211f);
                    Vector2 uv2_block0_5 = new Vector2(0.621f, 0.785f);
                    Vector2 uv2_block0_3 = new Vector2(0.375f, 0.785f);
                    Vector2 uv2_block0_2 = new Vector2(0.375f, 0.211f);

                    Vector2 uv2_tri0_6 = new Vector2(0.621f, 0.211f);
                    Vector2 uv2_tri0_2 = new Vector2(0.375f, 0.211f);
                    Vector2 uv2_tri0_4 = new Vector2(0.5f, 0.0f);

                    Vector2 uv2_tri5_5 = new Vector2(0.621f, 0.785f);
                    Vector2 uv2_tri5_1 = new Vector2(0.5f, 1.0f);
                    Vector2 uv2_tri5_3 = new Vector2(0.375f, 0.785f);

                    // block 4
                    pass = true;
                    pass &= recheck(edx[0][0] - nIndexOffset, x, y);
                    pass &= recheck(edx[0][1] - nIndexOffset, x, y);
                    pass &= recheck(edx[0][2] - nIndexOffset, ww, y);
					
					bool bBld=false;
					if(x==0 && isBlend)
						bBld=true;

					if( pass == true && !bBld)
					{
                        /*vSub.Add( reindex(ref e, edx[0][0] - nIndexOffset, x, y, 0.5f, uv2_block0_6) );
                        vSub.Add( reindex(ref e, edx[0][1] - nIndexOffset, x, y, 0.5f, uv2_block0_5) );
                        vSub.Add( reindex(ref e, edx[0][2] - nIndexOffset, ww, y, 0.5f, uv2_block0_2) );*/
                        vSub.Add(reindex(ref e, edx[0][0] - nIndexOffset, x, y, heightCompare4, uv2_normal_block0_6));
                        vSub.Add( reindex(ref e, edx[0][1] - nIndexOffset, x, y, heightCompare4, uv2_normal_block0_5) );
                        vSub.Add( reindex(ref e, edx[0][2] - nIndexOffset, ww, y, heightCompare4, uv2_normal_block0_2) );
                    }

                    pass = true;
                    pass &= recheck(edx[0][3] - nIndexOffset, ww, y);
                    pass &= recheck(edx[0][4] - nIndexOffset, ww, y);
                    pass &= recheck(edx[0][5] - nIndexOffset, x, y);

					if( pass == true && !bBld)
					{
                        /*vSub.Add(reindex(ref e, edx[0][3] - nIndexOffset, ww, y, 0.5f, uv2_block0_3));
                        vSub.Add(reindex(ref e, edx[0][4] - nIndexOffset, ww, y, 0.5f, uv2_block0_2));
                        vSub.Add(reindex(ref e, edx[0][5] - nIndexOffset, x, y, 0.5f, uv2_block0_5));*/
                        vSub.Add(reindex(ref e, edx[0][3] - nIndexOffset, ww, y, heightCompare4, uv2_normal_block0_3));
                        vSub.Add(reindex(ref e, edx[0][4] - nIndexOffset, ww, y, heightCompare4, uv2_normal_block0_2));
                        vSub.Add(reindex(ref e, edx[0][5] - nIndexOffset, x, y, heightCompare4, uv2_normal_block0_5));
                    }

                    // tri 4
                    if ( IsValid(nw, y+1) )
					{
						pass = true;
						pass &= recheck(edx[3][0] - nIndexOffset, x, y);
						pass &= recheck(edx[3][1] - nIndexOffset, ww, y);
						pass &= recheck(edx[3][2] - nIndexOffset, nw, y+1);

                        Vector2 v0_uv = new Vector2();
                        Vector2 v1_uv = new Vector2();
                        Vector2 v2_uv = new Vector2();
                        int heightCompare_type = CompareHeight_Tri0(height_neighbor5, height_neighbor4, thisHeight);
                        float heightCompare_tri0=0;
                        switch (heightCompare_type)
                        {
                            case 0:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_3;
                                v1_uv = uv2_normal_tri_5;
                                v2_uv = uv2_normal_tri_1;
                                break;
                            case 1:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_5;
                                v1_uv = uv2_normal_tri_1;
                                v2_uv = uv2_normal_tri_3;
                                break;
                            case 2:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_2;
                                v1_uv = uv2_normal_tri_4;
                                v2_uv = uv2_normal_tri_6;
                                break;
                            case 3:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_6;
                                v1_uv = uv2_normal_tri_2;
                                v2_uv = uv2_normal_tri_4;
                                break;
                            case 4:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_2;
                                v1_uv = uv2_normal_tri_4;
                                v2_uv = uv2_normal_tri_6;
                                break;
                            case 5:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_5;
                                v1_uv = uv2_normal_tri_1;
                                v2_uv = uv2_normal_tri_3;
                                break;
                            default:
                                heightCompare_tri0 = 4.5f;
                                break;
                        }

                        if ( pass == true && !bBld)
						{
                            //if (heightCompare_type == 3)
                            {
                                /*vSub.Add(reindex(ref e, edx[3][0] - nIndexOffset, x, y, 0.5f, uv2_tri0_6));
                                vSub.Add(reindex(ref e, edx[3][1] - nIndexOffset, ww, y, 0.5f, uv2_tri0_2));
                                vSub.Add(reindex(ref e, edx[3][2] - nIndexOffset, nw, y+1, 0.5f, uv2_tri0_4));*/
                                vSub.Add(reindex(ref e, edx[3][0] - nIndexOffset, x, y, heightCompare_tri0, v0_uv));
                                vSub.Add(reindex(ref e, edx[3][1] - nIndexOffset, ww, y, heightCompare_tri0, v2_uv));
                                vSub.Add(reindex(ref e, edx[3][2] - nIndexOffset, nw, y + 1, heightCompare_tri0, v1_uv));
                            }
                        }
					}
				
					// tri 3
                    if (IsValid(sw, y - 1))
                    {
                        pass = true;
                        pass &= recheck(edx[3][3] - nIndexOffset, x, y);
                        pass &= recheck(edx[3][4] - nIndexOffset, sw, y - 1);
                        pass &= recheck(edx[3][5] - nIndexOffset, ww, y);

                        Vector2 v0_uv = new Vector2();
                        Vector2 v1_uv = new Vector2();
                        Vector2 v2_uv = new Vector2();
                        int heightCompare_type = CompareHeight_Tri0(height_neighbor3, thisHeight, height_neighbor4);
                        float heightCompare_tri0 = 0;
                        switch (heightCompare_type)
                        {
                            case 0:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_1;
                                v1_uv = uv2_normal_tri_3;
                                v2_uv = uv2_normal_tri_5;
                                break;
                            case 1:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_5;
                                v1_uv = uv2_normal_tri_1;
                                v2_uv = uv2_normal_tri_3;
                                break;
                            case 2:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_2;
                                v1_uv = uv2_normal_tri_4;
                                v2_uv = uv2_normal_tri_6;
                                break;
                            case 3:
                                heightCompare_tri0 = 5.5f;
                                v0_uv = uv2_normal_tri_6;
                                v1_uv = uv2_normal_tri_2;
                                v2_uv = uv2_normal_tri_4;
                                break;
                            case 4:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_2;
                                v1_uv = uv2_normal_tri_4;
                                v2_uv = uv2_normal_tri_6;
                                break;
                            case 5:
                                heightCompare_tri0 = 3.5f;
                                v0_uv = uv2_normal_tri_5;
                                v1_uv = uv2_normal_tri_1;
                                v2_uv = uv2_normal_tri_3;
                                break;
                            default:
                                heightCompare_tri0 = 4.5f;
                                break;
                        }

                        if (y==0 && isBlend)
							bBld=true;

                        float heightCompare_tri5 = 4.5f;
                        if(heightCompare3!=4.5f)
                        {
                            heightCompare_tri5 = heightCompare3;
                        }

                        if (heightCompare4 != 4.5f)
                        {
                            heightCompare_tri5 = heightCompare4;
                        }

                        if ( pass == true && !bBld)
						{
                            /*vSub.Add(reindex(ref e, edx[3][3] - nIndexOffset, x, y, 0.5f, uv2_tri5_5));
							vSub.Add(reindex(ref e, edx[3][4] - nIndexOffset, sw, y-1, 0.5f, uv2_tri5_1));
							vSub.Add(reindex(ref e, edx[3][5] - nIndexOffset, ww, y, 0.5f, uv2_tri5_3));*/
                            vSub.Add(reindex(ref e, edx[3][3] - nIndexOffset, x, y, heightCompare_tri5, v0_uv));
                            vSub.Add(reindex(ref e, edx[3][4] - nIndexOffset, sw, y - 1, heightCompare_tri5, v1_uv));
                            vSub.Add(reindex(ref e, edx[3][5] - nIndexOffset, ww, y, heightCompare_tri5, v2_uv));
                        }
                    }
                }

				if (IsValid(sw, y - 1))
				{
                    Vector2 uv2_block5_5 = new Vector2(0.621f, 0.211f);
                    Vector2 uv2_block5_4 = new Vector2(0.621f, 0.785f);
                    Vector2 uv2_block5_2 = new Vector2(0.375f, 0.785f);
                    Vector2 uv2_block5_1 = new Vector2(0.375f, 0.211f);

                    // block 3
                    pass = true;
					pass &= recheck(edx[1][0] - nIndexOffset, x, y);
					pass &= recheck(edx[1][1] - nIndexOffset, x, y);
					pass &= recheck(edx[1][2] - nIndexOffset, sw, y - 1);

					bool bBld=false;
					if( (x==0 && y%2==0 || y==0) && isBlend )
						bBld=true;
			
					if ( pass == true && !bBld)
					{
                        vSub.Add(reindex(ref e, edx[1][0] - nIndexOffset, x, y, heightCompare3, uv2_normal_block0_6));
                        vSub.Add(reindex(ref e, edx[1][1] - nIndexOffset, x, y, heightCompare3, uv2_normal_block0_5));
                        vSub.Add(reindex(ref e, edx[1][2] - nIndexOffset, sw, y - 1, heightCompare3, uv2_normal_block0_2));
                    }

					pass = true;
					pass &= recheck(edx[1][3] - nIndexOffset, sw, y-1);
					pass &= recheck(edx[1][4] - nIndexOffset, sw, y-1);
					pass &= recheck(edx[1][5] - nIndexOffset, x, y);

					if ( pass == true && !bBld)
					{
                        vSub.Add(reindex(ref e, edx[1][3] - nIndexOffset, sw, y - 1, heightCompare3, uv2_normal_block0_3));
                        vSub.Add(reindex(ref e, edx[1][4] - nIndexOffset, sw, y - 1, heightCompare3, uv2_normal_block0_2));
                        vSub.Add(reindex(ref e, edx[1][5] - nIndexOffset, x, y, heightCompare3, uv2_normal_block0_5));
                    }
				}

                if (IsValid(nw, y + 1))
                {
                    Vector2 uv2_block1_1 = new Vector2(0.621f, 0.211f);
                    Vector2 uv2_block1_6 = new Vector2(0.621f, 0.785f);
                    Vector2 uv2_block1_4 = new Vector2(0.375f, 0.785f);
                    Vector2 uv2_block1_3 = new Vector2(0.375f, 0.211f);

                    // block 5
                    pass = true;
                    pass &= recheck(edx[2][0] - nIndexOffset, x, y);
                    pass &= recheck(edx[2][1] - nIndexOffset, x, y);
                    pass &= recheck(edx[2][2] - nIndexOffset, nw, y + 1);

					bool bBld=false;
					if(x==0 && y%2==0 && isBlend)
						bBld=true;

                    if ( pass == true && !bBld)
					{
                        vSub.Add(reindex(ref e, edx[2][0] - nIndexOffset, x, y, heightCompare5, uv2_normal_block0_6));
                        vSub.Add(reindex(ref e, edx[2][1] - nIndexOffset, x, y, heightCompare5, uv2_normal_block0_5));
                        vSub.Add(reindex(ref e, edx[2][2] - nIndexOffset, nw, y + 1, heightCompare5, uv2_normal_block0_2));
                    }

					pass = true;
					pass &= recheck(edx[2][3] - nIndexOffset, nw, y+1);
					pass &= recheck(edx[2][4] - nIndexOffset, nw, y+1);
					pass &= recheck(edx[2][5] - nIndexOffset, x, y);

					if( pass == true && !bBld)
					{
                        vSub.Add(reindex(ref e, edx[2][3] - nIndexOffset, nw, y + 1, heightCompare5, uv2_normal_block0_3));
                        vSub.Add(reindex(ref e, edx[2][4] - nIndexOffset, nw, y + 1, heightCompare5, uv2_normal_block0_2));
                        vSub.Add(reindex(ref e, edx[2][5] - nIndexOffset, x, y, heightCompare5, uv2_normal_block0_5));
                    }
                }
			}
        }

		Mesh me = new Mesh(); 
		me.subMeshCount = 3;
		me.vertices = e.vHex.ToArray();
		me.SetTriangles(vIdx.ToArray(), 0);
		me.SetTriangles(vSub.ToArray(), 1);
		me.SetTriangles(vBld.ToArray(), 2);
		me.normals = e.vNor.ToArray();
		me.colors = e.vClr.ToArray();
		me.tangents = e.vTan.ToArray(); 
		me.uv = e.vTex.ToArray();
        me.uv2 = e.vNorTex.ToArray();
        me.bounds = _bound;

		var mf = gameObject.GetComponent<MeshFilter>();
		if(mf == null)
		{
			mf = gameObject.AddComponent<MeshFilter>();
		}
		mf.sharedMesh = me;

		var mr = gameObject.GetComponent<MeshRenderer>();
		if (mr == null)
		{
			mr = gameObject.AddComponent<MeshRenderer>();
		}
		mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		mr.sharedMaterials =  _hex.GetTerrainMats();
		//mr.SetPropertyBlock(UpdateBlock());
    }

    private void OnDrawGizmos()
    {
        Bounds bound = gameObject.GetComponent<MeshRenderer>().bounds;
       // Gizmos.DrawWireCube(bound.center, bound.size);
    }

/*    
    public void OnWillRenderObject()
    {
        if (!enabled || !GetComponent<Renderer>() || !GetComponent<Renderer>().sharedMaterial ||
                !GetComponent<Renderer>().enabled)
        {
            return;
        }

        Camera cam = Camera.current;
        if (!cam)
        {
            return;
        }

        // Safeguard from recursive water reflections.
        if (s_InsideWater)
        {
            return;
        }
        s_InsideWater = true;


        //创建反射相机
        Camera reflectionCamera;
        CreateWaterObjects(cam, out reflectionCamera);
        // find out the reflection plane: position and normal in world space
        Vector3 pos = transform.position;
        pos.x = textureSize / 2;
        pos.z = textureSize / 2;
        Vector3 normal = transform.up;


        UpdateCameraModes(cam, reflectionCamera);


        // Reflect camera around reflection plane
        float d = -Vector3.Dot(normal, pos) - clipPlaneOffset;
        Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

        Matrix4x4 reflection = Matrix4x4.zero;
        CalculateReflectionMatrix(ref reflection, reflectionPlane);
        Vector3 oldpos = cam.transform.position;
        Vector3 newpos = reflection.MultiplyPoint(oldpos);
        reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

        // Setup oblique projection matrix so that near plane is our reflection
        // plane. This way we clip everything below/above it for free.
        Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
        reflectionCamera.projectionMatrix = cam.CalculateObliqueMatrix(clipPlane);

        reflectionCamera.cullingMask = ~(1 << 4) & reflectLayers.value; // never render water layer
        reflectionCamera.targetTexture = m_ReflectionTexture;
        bool oldCulling = GL.invertCulling;
        GL.invertCulling = !oldCulling;
        reflectionCamera.transform.position = newpos;
        Vector3 euler = cam.transform.eulerAngles;
        reflectionCamera.transform.eulerAngles = new Vector3(-euler.x, euler.y, euler.z);
        reflectionCamera.Render();
        reflectionCamera.transform.position = oldpos;
        GL.invertCulling = oldCulling;
        GetComponent<Renderer>().sharedMaterial.SetTexture("_ReflectionTex", m_ReflectionTexture);


        s_InsideWater = false;
    }

    void CreateWaterObjects(Camera currentCamera, out Camera reflectionCamera)
    {
        reflectionCamera = null;
        // Reflection render texture
        if (!m_ReflectionTexture || m_OldReflectionTextureSize != textureSize)
        {
            if (m_ReflectionTexture)
            {
                DestroyImmediate(m_ReflectionTexture);
            }
            m_ReflectionTexture = new RenderTexture(textureSize, textureSize, 16);
            m_ReflectionTexture.name = "__WaterReflection" + GetInstanceID();
            m_ReflectionTexture.isPowerOfTwo = true;
            m_ReflectionTexture.hideFlags = HideFlags.DontSave;
            m_OldReflectionTextureSize = textureSize;
        }

        // Camera for reflection
        m_ReflectionCameras.TryGetValue(currentCamera, out reflectionCamera);
        if (!reflectionCamera) // catch both not-in-dictionary and in-dictionary-but-deleted-GO
        {
            GameObject go = new GameObject("Water Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
            reflectionCamera = go.GetComponent<Camera>();
            reflectionCamera.enabled = false;
            reflectionCamera.transform.position = transform.position;
            reflectionCamera.transform.rotation = transform.rotation;
            reflectionCamera.gameObject.AddComponent<FlareLayer>();
            go.hideFlags = HideFlags.HideAndDontSave;
            m_ReflectionCameras[currentCamera] = reflectionCamera;
        }
    }
    void UpdateCameraModes(Camera src, Camera dest)
    {
        if (dest == null)
        {
            return;
        }
        // set water camera to clear the same way as current camera
        dest.clearFlags = src.clearFlags;
        dest.backgroundColor = src.backgroundColor;
        if (src.clearFlags == CameraClearFlags.Skybox)
        {
            Skybox sky = src.GetComponent<Skybox>();
            Skybox mysky = dest.GetComponent<Skybox>();
            if (!sky || !sky.material)
            {
                mysky.enabled = false;
            }
            else
            {
                mysky.enabled = true;
                mysky.material = sky.material;
            }
        }
        // update other values to match current camera.
        // even if we are supplying custom camera&projection matrices,
        // some of values are used elsewhere (e.g. skybox uses far plane)
        dest.farClipPlane = src.farClipPlane;
        dest.nearClipPlane = src.nearClipPlane;
        dest.orthographic = src.orthographic;
        dest.fieldOfView = src.fieldOfView;
        dest.aspect = src.aspect;
        dest.orthographicSize = src.orthographicSize;
    }
    // Calculates reflection matrix around the given plane
    static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }
    // Given position/normal of the plane, calculates plane in camera space.
    Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        Vector3 offsetPos = pos + normal * clipPlaneOffset;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(offsetPos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
    }*/

}
