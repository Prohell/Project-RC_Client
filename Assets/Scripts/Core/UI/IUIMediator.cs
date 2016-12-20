using LuaInterface;

public interface IUIMediator
{
    LuaTable m_View { get; set; }
    string m_UIName { get; set; }
}
