/// <summary>
/// 单例
/// by TT
/// 2016-06-17
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> where T : class, new()
{
    protected static T mInstance;
    private static readonly object mSyslock = new object();

    public static T GetInstance()
    {
        if (mInstance == null)
        {
            lock (mSyslock)
            {
                if (mInstance == null)
                {
                    mInstance = new T();
                }
            }
        }
        return mInstance;
    }
}