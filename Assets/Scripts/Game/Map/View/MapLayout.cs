using UnityEngine;
using System.Collections.Generic;

public class MapLayout
{

    public const float Sqrt3 = 1.7320508f;

    private float _hexEdgeLen = 1f;
    private Vector3[] _hexVertices;
    private Vector2[] _hexUvs;
    private int[] _triangles;

    private float _offsetX;
    private float _offsetZ;
    private float _stepX;
    private float _stepZ;

    public enum Direction
    {
        BottomLeft,
        Left,
        TopLeft,
        TopRight,
        Right,
        BottomRight,
        Count
    }

    /// 
    /// 3 /\ 
    /// 2 || 4
    /// 1 \/ 5
    ///   0
    public MapLayout(float hexEdgeLen)
    {
        _hexEdgeLen = hexEdgeLen;
        _hexVertices = new Vector3[6]{
            new Vector3(0, 0, -1f) * _hexEdgeLen,
            new Vector3(-Sqrt3/2f, 0, -0.5f) * _hexEdgeLen,
            new Vector3(-Sqrt3/2f, 0, 0.5f) * _hexEdgeLen,
            new Vector3(0, 0, 1f) * _hexEdgeLen,
            new Vector3(Sqrt3/2f, 0, 0.5f) * _hexEdgeLen,
            new Vector3(Sqrt3/2f, 0, -0.5f) * _hexEdgeLen
        };

        _hexUvs = new Vector2[6]{
            new Vector2(0, -1f),
            new Vector2(-Sqrt3/2f, -0.5f),
            new Vector2(-Sqrt3/2f, 0.5f),
            new Vector2(0, 1f),
            new Vector2(Sqrt3/2f, 0.5f),
            new Vector2(Sqrt3/2f, -0.5f)
        };

        _triangles = new int[]{
            0, 1, 5,
            1, 4, 5,
            1, 2, 4,
            2, 3, 4,
        };

        _offsetX = Sqrt3 / 2f * _hexEdgeLen;
        _offsetZ = _hexEdgeLen;
        // 以下两个值需要修改
        //_stepX = Sqrt3 * _hexEdgeLen;
        //_stepZ = 1.5f * _hexEdgeLen;
        // fixed
        _stepX = Sqrt3 * _hexEdgeLen + 0.5f;
        _stepZ = 1.5f * _hexEdgeLen + (Sqrt3 / 4);
    }

    public static Coord NearBy(Coord coord, Direction dxt)
    {
        switch (dxt)
        {
            case Direction.BottomLeft:
                return BottomLeft(coord);

            case Direction.BottomRight:
                return BottomRight(coord);

            case Direction.Left:
                return Left(coord);

            case Direction.Right:
                return Right(coord);

            case Direction.TopLeft:
                return TopLeft(coord);

            case Direction.TopRight:
                return TopRight(coord);
        }
        return new Coord();
    }

    public static Coord Left(Coord c)
    {
        return new Coord(c.x - 1, c.y);
    }
    public static Coord Right(Coord c)
    {
        return new Coord(c.x + 1, c.y);
    }
    public static Coord TopLeft(Coord c)
    {
        return new Coord((c.y & 0x1) == 0 ? c.x : c.x - 1, c.y + 1);
    }
    public static Coord TopRight(Coord c)
    {
        return new Coord((c.y & 0x1) == 0 ? c.x + 1 : c.x, c.y + 1);
    }
    public static Coord BottomLeft(Coord c)
    {
        return new Coord((c.y & 0x1) == 0 ? c.x : c.x - 1, c.y - 1);
    }
    public static Coord BottomRight(Coord c)
    {
        return new Coord((c.y & 0x1) == 0 ? c.x + 1 : c.x, c.y - 1);
    }

    public static List<Coord> Nearby(Coord c)
    {
        List<Coord> re = new List<Coord>();
        re.Add(new Coord(c.x - 1, c.y));
        re.Add(new Coord(c.x + 1, c.y));
        re.Add(new Coord(c.x, c.y - 1));
        re.Add(new Coord(c.x, c.y + 1));

        int offset = (c.y & 0x1) == 0 ? 1 : -1;
        re.Add(new Coord(c.x + offset, c.y - 1));
        re.Add(new Coord(c.x + offset, c.y + 1));

        return re;
    }

    public Vector3 HexCenter(Coord coord)
    {
        Vector3 re = new Vector3(coord.x * _stepX + _offsetX, 0, coord.y * _stepZ + _offsetZ);
        if ((coord.y & 0x1) == 0)
        {
            re.x += _stepX / 2f;
        }
        return re;
    }

    public float HexEdgeLen
    {
        get
        {
            return _hexEdgeLen;
        }
    }

    public float HexTileWidth
    {
        get { return _stepX; }
    }

    public float HexTileHeight
    {
        get { return _stepZ; }
    }

    public Vector3[] HexVertices
    {
        get
        {
            return _hexVertices;
        }
    }

    public Vector2[] HexUvs
    {
        get
        {
            return _hexUvs;
        }
    }

    public int[] Triangles
    {
        get
        {
            return _triangles;
        }
    }

    //TODO: it is not a good method
    public Coord ScreenPos2Coord(Camera camera, Vector2 screenPos)
    {

        Vector3 worldPos = ScreenPos2WorldPos(camera, screenPos);
        int x = Mathf.FloorToInt(worldPos.x / HexTileWidth);
        int y = Mathf.FloorToInt(worldPos.z / HexTileHeight);

        float minDis = float.MaxValue;
        int reX = x;
        int reY = y;
        for (int row = y - 1; row < y + 1; row++)
        {
            for (int col = x - 1; col < x + 1; col++)
            {
                float dis = (HexCenter(new Coord(col, row)) - worldPos).magnitude;
                if (dis < minDis)
                {
                    minDis = dis;
                    reX = col;
                    reY = row;
                }
            }
        }
        reX = Mathf.Clamp(reX, 0, MapConst.MAP_WIDTH - 1);
        reY = Mathf.Clamp(reY, 0, MapConst.MAP_HEIGHT - 1);
        return new Coord(reX, reY);
    }

    public Vector2 Coord2ScreenPos(Camera camera, Coord coord)
    {
        return camera.WorldToScreenPoint(HexCenter(coord)).xy();
    }

    // Calc the screen pos ray cast pos in map plane, consider the camera pos as zero
    public Vector3 ScreenPos2WorldPos(Camera camera, Vector2 screenPos)
    {
        float z = (Screen.height / 2f) / Mathf.Tan(camera.fieldOfView / 2f * Mathf.Deg2Rad);
        Vector3 localPos = new Vector3(screenPos.x - Screen.width / 2f, screenPos.y - Screen.height / 2f, z);
        Matrix4x4 matrCamera = camera.transform.localToWorldMatrix.RS();
        Vector3 worldPos = matrCamera.MultiplyPoint(localPos);
        Ray ray = new Ray(Vector3.zero, worldPos);
        Plane plane = new Plane(Vector3.up, new Vector3(0, -camera.transform.localPosition.y, 0));
        float dis = 0;
        if (plane.Raycast(ray, out dis))
        {
            return ray.GetPoint(dis) + camera.transform.localPosition - new Vector3(_offsetX, 0, 0);
        }
        else
        {
            return Vector3.zero;
        }
    }

    //计算相机的位置，使屏幕上的某个点(screenPos)，对准世界坐标系中某个位置(worldPosition)
    public Vector3 LookAt(Camera camera, Vector3 worldPosition, Vector2 screenPos, float cameraHeight)
    {

        //当前屏幕点对应的世界位置
        Vector3 curWorldPos = ScreenPos2WorldPos(camera, screenPos);
        Vector3 offset = worldPosition - curWorldPos;
        offset.y = 0;

        //在和当前相机同样高度的情况下，对准worldPosition的相机位置
        Vector3 sameYDst = camera.transform.localPosition + offset;

        Vector3 dst = (sameYDst - worldPosition) / camera.transform.localPosition.y * cameraHeight + worldPosition;
        return dst;
    }

    private const int MAX_SUBVIEW_X = MapConst.MAP_WIDTH / SubMapView.xTileCnt - 1;
    private const int MAX_SUBVIEW_Y = MapConst.MAP_HEIGHT / SubMapView.yTileCnt - 1;

    public List<Coord> CalcVisibleSubMaps(Camera camera)
    {
        Rect visRect = VisibleRect(camera);
        float coord_width_reverse = 1 / HexTileWidth;
        float coord_height_reverse = 1 / HexTileHeight;

        int maxX = (MAX_SUBVIEW_X + 1) * SubMapView.xTileCnt - 1;
        int maxY = (MAX_SUBVIEW_Y + 1) * SubMapView.yTileCnt - 1;

        int minVisibleX = Mathf.FloorToInt(visRect.xMin * coord_width_reverse);
        minVisibleX = Mathf.Max(0, minVisibleX - 1);
        int maxVisibleX = Mathf.FloorToInt(visRect.xMax * coord_width_reverse);
        maxVisibleX = Mathf.Min(maxVisibleX + 1, maxX);
        int minVisibleY = Mathf.FloorToInt(visRect.yMin * coord_height_reverse);
        minVisibleY = Mathf.Max(0, minVisibleY - 1);
        int maxVisibleY = Mathf.FloorToInt(visRect.yMax * coord_height_reverse);
        maxVisibleY = Mathf.Min(maxVisibleY + 1, maxY);
        int minSubViewVisibleX = minVisibleX / SubMapView.xTileCnt;
        int maxSubViewVisibleX = (maxVisibleX % SubMapView.xTileCnt == 0) ? maxVisibleX / SubMapView.xTileCnt - 1 : maxVisibleX / SubMapView.xTileCnt;
        int minSubViewVisibleY = minVisibleY / SubMapView.xTileCnt;
        int maxSubViewVisibleY = (maxVisibleY % SubMapView.xTileCnt == 0) ? maxVisibleY / SubMapView.xTileCnt - 1 : maxVisibleY / SubMapView.xTileCnt;
        List<Coord> re = new List<Coord>();
        for (int x = minSubViewVisibleX; x <= maxSubViewVisibleX; x++)
        {
            for (int y = minSubViewVisibleY; y <= maxSubViewVisibleY; y++)
            {
                re.Add(new Coord(x, y));
            }
        }
        return re;
    }

    public class VisibleArea
    {
        public float near;
        public float far;
        public float nearHalfWidth;
        public float farHalfWidth;
    }

    public static VisibleArea CalcRelativeVisibleArea(Camera camera)
    {
        float halfViewHeight = Screen.height / 2f;
        float halfViewWidth = halfViewHeight * camera.aspect;

        float vFov = camera.fieldOfView * Mathf.Deg2Rad;
        float z = halfViewHeight / Mathf.Tan(vFov / 2f);
        Vector3 localBottomLeft = new Vector3(-halfViewWidth, -halfViewHeight, z);
        Vector3 localTopLeft = new Vector3(-halfViewWidth, halfViewHeight, z);

        Matrix4x4 matrCamera = camera.transform.localToWorldMatrix.RS();
        Vector3 worldBottomLeft = matrCamera.MultiplyPoint(localBottomLeft);
        Vector3 worldTopLeft = matrCamera.MultiplyPoint(localTopLeft);

        Ray rayBottomLeft = new Ray(Vector3.zero, worldBottomLeft);
        Ray rayTopLeft = new Ray(Vector3.zero, worldTopLeft);

        Plane plane = new Plane(Vector3.up, new Vector3(0, -camera.transform.localPosition.y, 0));
        float dis = 0;

        Vector3 plBottomLeft;
        Vector3 plTopLeft;

        if (plane.Raycast(rayBottomLeft, out dis))
        {
            plBottomLeft = rayBottomLeft.GetPoint(dis);
        }
        else
        {
            plBottomLeft = Vector3.zero;
            Debug.LogError("rayBottomLeft can't cast ");
        }

        if (plane.Raycast(rayTopLeft, out dis))
        {
            plTopLeft = rayTopLeft.GetPoint(dis);
        }
        else
        {
            plTopLeft = Vector3.zero;
            Debug.LogError("plTopLeft can't cast ");
        }

        VisibleArea re = new VisibleArea();
        re.near = plBottomLeft.z;
        re.far = plTopLeft.z;
        re.nearHalfWidth = -plBottomLeft.x;
        re.farHalfWidth = -plTopLeft.x;

        return re;
    }

    public Rect VisibleRect(Camera camera)
    {
        VisibleArea va = MapLayout.CalcRelativeVisibleArea(camera);
        return new Rect(camera.transform.localPosition.x - va.farHalfWidth,
                        camera.transform.localPosition.z + va.near,
                        va.farHalfWidth * 2f,
                        va.far - va.near);
    }
}
