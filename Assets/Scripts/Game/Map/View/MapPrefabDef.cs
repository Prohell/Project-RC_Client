public class MapPrefabDef
{
    public const string MAP_VIEW = "MapView";
    public const string MAP_TILE_CITY = "MapTileCity";
    public const string MAP_TILE_PVE = "MapTilePve";

    private const string MAP_TILE_SILVER = "MapTileSilver";
    private const string MAP_TILE_WOOD = "MapTileWood";
    private const string MAP_TILE_FOOD = "MapTileFood";

    public const string BATTLE_WIN_MARCH_LINE = "MapTroopWinnerMarchLine";
    public const string BATTLE_LOSE_MARCH_LINE = "MapTroopLoserMarchLine";
    public const string RESOURCE_FULL_MARCH_LINE = "MapTroopResourceFullMarchLine";
    public const string RESOURCE_EMPTY_MARCH_LINE = "MapTroopResourceEmptyMarchLine";
    public const string MAP_SUB_VIEW = "Map/HexSubMapView";
    public const string MAP_TILE_ATLAS = "map_tile_atlas";
    public const string MAP_HIGHLIGHT = "MapHighlight";

    private static string[] MapTileResourceFormat =
    {
        "_1",
        "_2",
        "_3",
        "_4",
    };

    public static int GetMapTileFormatCount()
    {
        return MapTileResourceFormat.Length;
    }
}
