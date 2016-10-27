/// <summary>
/// 平台相关
/// by TT
/// 2016-07-04
/// </summary>
public class PlatformHelper
{
    public static bool IsEnableDebugMode()
    {
        return true;
    }

    public static string GetCurrentTime()
    {
        System.DateTime now = System.DateTime.Now;
        return now.ToString("yyyy-MM-dd HH:mm:SS");
    }
}
