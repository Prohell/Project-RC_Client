using UnityEngine;
using System.Collections;

public class MapViewLod1 : MonoBehaviour
{
    [HideInInspector]
    public Camera MapCamera;
    public HexMapLayout Layout;

    public GameObject Background;
    public Vector2 ContentRate;

    public GameObject MyCityFlag;

    public IEnumerator Init()
    {
        ResetBackground();
        SyncMyCityFlagPos();
        yield break;
    }

#if UNITY_EDITOR
    Vector2 preContentRate = Vector2.one;
    void Update()
    {
        if (Layout != null && preContentRate != ContentRate)
        {
            ResetBackground();
            preContentRate = ContentRate;
        }
    }
#endif

    Vector3 GetSize()
    {
        return new Vector3(-Layout.HexTileWidth * MapConst.MAP_WIDTH / 2f / ContentRate.x,
                             1,
                             Layout.HexTileHeight * MapConst.MAP_HEIGHT / 2f / ContentRate.y);
    }

    Vector3 GetPosition()
    {
        return new Vector3(Layout.HexTileWidth * MapConst.MAP_WIDTH / 2f,
                            0,
                            Layout.HexTileHeight * MapConst.MAP_HEIGHT / 2f);
    }

    void ResetBackground()
    {
        Background.transform.localScale = GetSize();
        Background.transform.localPosition = GetPosition();
    }

    public void SyncMyCityFlagPos()
    {
        Coord c = GameFacade.GetProxy<CityProxy>().myCityCoord;
        MyCityFlag.transform.position = Layout.HexCenter(c);
    }

    void OnDrawGizmos()
    {
        if (Layout != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Vector3 center = GetPosition();
            Vector3 size = Vector3.Scale(GetSize(), new Vector3(ContentRate.x, 0, ContentRate.y)) * 2f;
            Gizmos.DrawWireCube(center, size);
        }
    }
}
