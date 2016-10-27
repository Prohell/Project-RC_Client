public class MapConst
{
    public const int MAP_WIDTH = 1024;
    public const int MAP_HEIGHT = 1024;
    /// <summary>
    /// ��ͼ��λ�߶ȣ�����1�߶����߻������ݶ�����
    /// </summary>
    public const float MAP_HEIGHT_UNIT = 0.5f;
    public const byte MAP_HEIGHT_HORIZON = 8;
}

public enum MapTileType
{
    Normal = 0,// �����ߣ��ɽ���
    Block = 1,// �������ߣ����ɽ���
    Walkable = 2// �����ߣ����ɽ���
}

public enum MapBlockType
{
    None = 0,
    Mountain = 1,
    Forest = 2,
    Water = 3
}

public enum CampType
{
    CAMP_TYPE_N = 0,

    CAMP_TYPE_W = 1,

    CAMP_TYPE_E = 2,

    CAMP_TYPE_S = 3,

    CAMP_TYPE_NEUTRAL = 4
}