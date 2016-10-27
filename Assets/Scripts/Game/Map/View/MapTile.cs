using UnityEngine;

public class MapTile : MonoBehaviour
{
    public TextMesh lblLevel;
    public TextMesh lblTitle;
    public TextMesh lblCooldown;
    public SpriteRenderer sprLevelBg;
    public SpriteRenderer sprSelfFlag;
    public Coord coord;
    public Color myColor;
    public Color enemyColor;

    public GameObject allianceInfo;
    public TextMesh lblAllianceTag;

    ulong mEndAt;

    Color[] campColor = new Color[]{
        new Color(245f/255, 255f/255, 146f/255),
        new Color(245f/255, 255f/255, 146f/255),
        new Color(245f/255, 255f/255, 146f/255),
        new Color(245f/255, 255f/255, 146f/255)
    };

    public void SetPitch(float eulurX)
    {
        transform.localEulerAngles = new Vector3(eulurX, 0, 0);
    }

    public void SetData(string title,
                        int level,
                        int camp,
                        string allianceTag = null,
                        string allianceBanner = null,
                        long defenseUntil = -1,
                        MapTileType type = MapTileType.Normal,
                        bool hasPlayer = true)
    {
    }
}