using UnityEngine;
using System.Collections;
using LuaInterface;

public class DelegateHelper
{
    public delegate void VoidDelegate();
    public delegate void IntDelegate(int p_int);
    public delegate void FloatDelegate(float p_float);
    public delegate void StringDelegate(string p_str);
    public delegate void TableDelegate(LuaTable p_table);
}
