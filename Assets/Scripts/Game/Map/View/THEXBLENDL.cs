
//#define HEXGON0

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class THEXBLENDL : MonoBehaviour
{
    /// <summary>
    /// 是否有高度
    /// </summary>
    public bool isGenHeight = true;
    
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
    private int _xTile;
    private int _yTile;

    public bool left = false;
    public List<int> spriteIds = new List<int>();
    public List<int> thisSprites = null;
    public List<int> leftSprites = null;

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
        if(x==0 && y==0)
        {
            Debug.Log("kk");
        }

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
		if (_block == null) {
			_block = new MaterialPropertyBlock ();   // make it lazy
		}
		HEX hex = _weak_hex.Target as HEX;
		Dictionary<int, int> TSet = new Dictionary<int, int> ();
		List<Vector4> TArray = new List<Vector4> ();

        for (int i = 0; i < spriteIds.Count; i++)
        {
            Vector4 pos = hex.GetSpritePosInfoByName(spriteIds[i]);
            TArray.Add(pos);
        }

        if (TArray.Count > 0)
        {
            _block.SetVectorArray ("_ARRAY", TArray.ToArray ());
		}

		_block.SetTexture ("_ATLAS", hex.altasTexture);

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

        MeshFilter mf = gameObject.GetComponent<MeshFilter>();
        if(mf==null)
            mf = gameObject.AddComponent<MeshFilter>();
        mf.sharedMesh = mPlan;

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        if(mr==null)
            mr = gameObject.AddComponent<MeshRenderer>();
		mr.sharedMaterial = _hex.GetTerrainBlendMat();
        //UpdateBlock();
        //mr.SetPropertyBlock(_block);

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
    };

	private static float Tx = Mathf.Sin(Mathf.PI / 3.0f);
	private static float Ty = Mathf.Cos(Mathf.PI / 3.0f);
	private static int[] lst = null;

    private delegate int _reindex(ref element e, int ver, int col, int row);
	private delegate int _reindex_Blend(ref element e, int ver, int col, int row, Color BlendColor, Vector4 BlendTangent);
	private delegate bool _recheck(int ver, int col, int row);

    int FindInSpriteIds(List<int> Ids, int aId)
    {
        for(int i=0; i<Ids.Count; i++)
        {
            if (Ids[i] == aId)
                return i;
        }
        return -1;
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

    int GetMatNameInd(int thisCoordX, int thisCoordY, int neighborInd)
    {
        MapProxy mapProxy = GameFacade.GetProxy<MapProxy>();

        if(neighborInd>5 || neighborInd<0)
        {
            Coord thisCoord = new Coord(thisCoordX, thisCoordY);
            byte thisMat = mapProxy.GetTile(thisCoord).mat;
            if (thisMat == 8)
                thisMat = 0;
            int thisMatNameNum = thisSprites[thisMat];
            int thisMatNameInd = FindInSpriteIds(spriteIds, thisMatNameNum);
            return thisMatNameInd;
        }

        Coord c_neighbor = GetNeighborCoord(thisCoordX, thisCoordY, neighborInd);
        MapTileVO t_neighbor = mapProxy.GetTile(c_neighbor);
        if(t_neighbor==null)
        {
            return -1;
        }

        byte m_neighbor = t_neighbor.mat;
        if (m_neighbor == 8)
            m_neighbor = 0;
        int neighborMatNameNum = -1;
        if(neighborInd==4)
        {
            neighborMatNameNum = leftSprites[m_neighbor];
        }
        else
        {
            if (thisCoordY % 2 == 0)
            {
                neighborMatNameNum = leftSprites[m_neighbor];
            }
            else
            {
                neighborMatNameNum = thisSprites[m_neighbor];
            }
        }
        
        int neighborMatNameInd = FindInSpriteIds(spriteIds, neighborMatNameNum);
        return neighborMatNameInd;
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

		_reindex_Blend reindex_Blend = (ref element _elm, int _ver, int _col, int _row, Color BlendColor, Vector4 BlendTangent) =>
		{
			MapTileVO tileVo = mapProxy.GetTile(_col + _hex.xTile * _x, _row + _hex.yTile * _y);
			int loc = ((_row + 1) * (_hex.xTile + 1) + (_col + 1)) * 8 + _ver;
			int idx = lst[loc];
			//if (idx >= 0)
			//	return idx;

			idx = _elm.vHex.Count;
			lst[loc] = idx;

			Vector3 _v = Vector3.zero;
			Vector3 _n = Vector3.zero;
			Vector4 _t = Vector4.zero;
			Vector2 _u = Vector2.zero;
			Color _c;
			float _R = Tx * 2.0f + _hex.fvHex.x;

			int m = tileVo.mat;
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

			_elm.vClr.Add(BlendColor);
			_elm.vTan.Add(BlendTangent);

			_elm.vTex.Add(_u);
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
        
		Color C1000=new Color(1,0,0,0);
		Color C0000=new Color(0,0,0,0);
		Color C0001=new Color(0,0,0,1);
		Color C0010=new Color(0,0,1,0);
		Color C0100=new Color(0,1,0,0);
		Color C1010=new Color(1,0,1,0);
		Color C0110=new Color(0,1,1,0);
		Color C1111=new Color(1,1,1,1);

		for (int y = 0; y < _yTile; y++)
		{
            for (int x = 0; x < _xTile; x++)
            {
                /*if (!(_x == 1 && _y == 0 && x == 0 && (y >= 11 && y < 15)))
                {
                    continue;
                }*/
                /*if (!(_x == 1 && _y == 1 && x == 0 && (y >= 1 && y < 5)))
                {
                    continue;
                }*/

                int thisCoordX = x + _xTile * _x;
                int thisCoordY = y + _yTile * _y;
                int thisMatNameInd = GetMatNameInd(thisCoordX, thisCoordY, -1);

                // west -> x-1
                // south west -> odd : x, y-1  even: x-1, y-1
                // south -> odd: x+1, y-1 even: x, y-1

                int t = y % 2;
				int ww = x - 1;                 // y
				int sw = t > 0 ? x : x - 1;     // y-1
				int nw = t > 0 ? x : x - 1;		// y+1

				if (IsValid(ww, y))
				{
                    // block 4
                    int neighbor4MatNameInd = GetMatNameInd(thisCoordX, thisCoordY, 4);
                    Vector4 tangent = new Vector4(thisMatNameInd * 0.1f, neighbor4MatNameInd * 0.1f, -1, -1);

                    pass = true;
					pass &= recheck(edx[0][0] - nIndexOffset, x, y);
					pass &= recheck(edx[0][1] - nIndexOffset, x, y);
					pass &= recheck(edx[0][2] - nIndexOffset, ww, y);

					bool bBld=false;
					if(x==0)
						bBld=true;

					if( pass == true && bBld)
					{
						vBld.Add( reindex_Blend(ref e, edx[0][0] - nIndexOffset, x, y, C1000, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[0][1] - nIndexOffset, x, y, C1000, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[0][2] - nIndexOffset, ww, y, C0100, tangent) );
					}

					pass = true;
					pass &= recheck(edx[0][3] - nIndexOffset, ww, y);
					pass &= recheck(edx[0][4] - nIndexOffset, ww, y);
					pass &= recheck(edx[0][5] - nIndexOffset, x, y);

					if( pass == true && bBld)
                    {
						vBld.Add( reindex_Blend(ref e, edx[0][3] - nIndexOffset, ww, y, C0100, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[0][4] - nIndexOffset, ww, y, C0100, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[0][5] - nIndexOffset, x, y, C1000, tangent) );
					}

                    // tri 4
                    int neighbor5MatNameInd = GetMatNameInd(thisCoordX, thisCoordY, 5);
                    tangent = new Vector4(thisMatNameInd * 0.1f, neighbor4MatNameInd * 0.1f, neighbor5MatNameInd * 0.1f, -1);

                    if ( IsValid(nw, y+1) )
					{
						pass = true;
						pass &= recheck(edx[3][0] - nIndexOffset, x, y);
						pass &= recheck(edx[3][1] - nIndexOffset, ww, y);
						pass &= recheck(edx[3][2] - nIndexOffset, nw, y+1);

						if ( pass == true && bBld)
                        {
                            vBld.Add(reindex_Blend(ref e, edx[3][0] - nIndexOffset, x, y, C1000, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[3][1] - nIndexOffset, ww, y, C0100, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[3][2] - nIndexOffset, nw, y + 1, C0010, tangent));
                        }
					}

                    // tri 3
                    int neighbor3MatNameInd = GetMatNameInd(thisCoordX, thisCoordY, 3);
                    tangent = new Vector4(thisMatNameInd * 0.1f, neighbor3MatNameInd * 0.1f, neighbor4MatNameInd * 0.1f, -1);

                    if (IsValid(sw, y - 1))
					{
						pass = true;
						pass &= recheck(edx[3][3] - nIndexOffset, x, y);
						pass &= recheck(edx[3][4] - nIndexOffset, sw, y - 1);
						pass &= recheck(edx[3][5] - nIndexOffset, ww, y);            

						if ( pass == true && bBld)
                        {
							if(x==0 && y==0)
                            {
                                // diag tri 3
                            }
							else
                            {
                                vBld.Add(reindex_Blend(ref e, edx[3][3] - nIndexOffset, x, y, C1000, tangent));
                                vBld.Add(reindex_Blend(ref e, edx[3][4] - nIndexOffset, sw, y - 1, C0100, tangent));
                                vBld.Add(reindex_Blend(ref e, edx[3][5] - nIndexOffset, ww, y, C0010, tangent));
                            }
						}
					}
				}

				if (IsValid(sw, y - 1))
				{
                    // block 3
                    int neighbor3MatNameInd = GetMatNameInd(thisCoordX, thisCoordY, 3);
                    Vector4 tangent = new Vector4(thisMatNameInd * 0.1f, neighbor3MatNameInd * 0.1f, -1, -1);

                    bool bBld =false;
					if(x==0 && y%2==0)
						bBld=true;

					pass = true;
					pass &= recheck(edx[1][0] - nIndexOffset, x, y);
					pass &= recheck(edx[1][1] - nIndexOffset, x, y);
					pass &= recheck(edx[1][2] - nIndexOffset, sw, y - 1);

					if ( pass == true && bBld)
                    {
						if(x==0 && y==0)
						{
                            // diag block 3
						}
						else
						{
                            vBld.Add(reindex_Blend(ref e, edx[1][0] - nIndexOffset, x, y, C1000, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[1][1] - nIndexOffset, x, y, C1000, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[1][2] - nIndexOffset, sw, y - 1, C0100, tangent));
                        }

					}

					pass = true;
					pass &= recheck(edx[1][3] - nIndexOffset, sw, y-1);
					pass &= recheck(edx[1][4] - nIndexOffset, sw, y-1);
					pass &= recheck(edx[1][5] - nIndexOffset, x, y);

					if ( pass == true && bBld)
                    {
						if(x==0 && y==0)
                        {
                            // diag block 3
                        }
						else
						{
                            vBld.Add(reindex_Blend(ref e, edx[1][3] - nIndexOffset, sw, y - 1, C0100, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[1][4] - nIndexOffset, sw, y - 1, C0100, tangent));
                            vBld.Add(reindex_Blend(ref e, edx[1][5] - nIndexOffset, x, y, C1000, tangent));
                        }
					}
				}

				if (IsValid(nw, y + 1))
				{
                    // block 5
                    int neighbor5MatNameInd = GetMatNameInd(thisCoordX, thisCoordY, 5);
                    Vector4 tangent = new Vector4(thisMatNameInd * 0.1f, neighbor5MatNameInd * 0.1f, -1, -1);

                    bool bBld=false;
					if(x==0 && y%2==0)
						bBld=true;

					pass = true;
					pass &= recheck(edx[2][0] - nIndexOffset, x, y);
					pass &= recheck(edx[2][1] - nIndexOffset, x, y);
					pass &= recheck(edx[2][2] - nIndexOffset, nw, y + 1);

					if( pass == true && bBld)
                    {
						vBld.Add( reindex_Blend(ref e, edx[2][0] - nIndexOffset, x, y, C1000, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[2][1] - nIndexOffset, x, y, C1000, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[2][2] - nIndexOffset, nw, y+1, C0100, tangent) );	
					}

					pass = true;
					pass &= recheck(edx[2][3] - nIndexOffset, nw, y+1);
					pass &= recheck(edx[2][4] - nIndexOffset, nw, y+1);
					pass &= recheck(edx[2][5] - nIndexOffset, x, y);

					if( pass == true && bBld)
                    {
						vBld.Add( reindex_Blend(ref e, edx[2][3] - nIndexOffset, nw, y+1, C0100, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[2][4] - nIndexOffset, nw, y+1, C0100, tangent) );
						vBld.Add( reindex_Blend(ref e, edx[2][5] - nIndexOffset, x, y, C1000, tangent) );
					}
				}
			}
		}

        Mesh me = new Mesh(); 
		me.subMeshCount = 1;
		me.vertices = e.vHex.ToArray();
		//me.SetTriangles(vIdx.ToArray(), 0);
		//me.SetTriangles(vSub.ToArray(), 1);
		me.SetTriangles(vBld.ToArray(), 0);
		me.normals = e.vNor.ToArray();
		me.colors = e.vClr.ToArray();
		me.tangents = e.vTan.ToArray(); 
		me.uv = e.vTex.ToArray();
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
		mr.sharedMaterial =  _hex.GetTerrainBlendMat();
		//mr.SetPropertyBlock(UpdateBlock());
    }

    private void OnDrawGizmos()
    {
        Bounds bound = gameObject.GetComponent<MeshRenderer>().bounds;
       // Gizmos.DrawWireCube(bound.center, bound.size);
    }

}
