/// <summary>
/// 定时触发方法管理器
/// by TT
/// 2016-06-29
/// </summary>
public class UpdateManager : Singleton<UpdateManager>, IUpdate, IReset
{
    public delegate void UpdateDelegate();
    event UpdateDelegate mUpdateHandler;
	
	public void AddUpdate(UpdateDelegate updater)
    {
        mUpdateHandler += updater;
	}

	public void RemoveUpdate(UpdateDelegate updater)
    {
        mUpdateHandler -= updater;
	}
	
    public void OnUpdate()
    {
        if (mUpdateHandler != null)
        {
            mUpdateHandler.Invoke();
        }
    }

    public void OnReset()
    {
        mUpdateHandler = null;
    }
}
