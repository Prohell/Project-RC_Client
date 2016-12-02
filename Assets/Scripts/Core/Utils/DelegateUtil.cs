using UnityEngine;
using System.Collections;
using LuaInterface;

public class DelegateUtil
{
    public delegate void VoidDelegate();
    public delegate void IntDelegate(int p_int);
    public delegate void FloatDelegate(int p_float);
    public delegate void StringDelegate(int p_str);
    public delegate void TableDelegate(LuaTable p_table);
}
