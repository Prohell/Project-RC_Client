//最后编辑 张羽乔 2016年10月13日

//UnityEngine.UI.Text.Text由于保护属性而不允许被访问，因此，无法在C#脚本中创建UnityEngine.UI.Text对象
//需要事先定义标签CY_NameLabel_Unused_Text和CY_NameLabel_Occupied_Text
//在存放UI元素的Canvas中预分配一定量的UnityEngine.UI.Text对象并预设置标签为CY_NameLabel_Unused_Text
//将所有预分配的UnityEngine.UI.Text对象的字符串预设置为空串确保其不会被显示
//值得注意的是，未使用的UnityEngine.UI.Text对象在字符串为空串的情况下，并不占用GPU资源（纹理或Draw命令），一般情况下，完全可以预分配足够多的对象
//一般情况下，预分配的UnityEngine.UI.Text对象的Paragraph/Alignment属性应当设置为居中且偏上
//在运行时，游戏中的UnityEngine.UI.Text对象的个数将保持不变，并且游戏对象会自动复用被释放的UnityEngine.UI.Text对象

public class CYNamePanel : UnityEngine.MonoBehaviour
{
	public string _String = null;
	public UnityEngine.Vector3 _Offset = new UnityEngine.Vector3 (0, 15, 0);

	private UnityEngine.GameObject _TextObject = null;//表示对UnityEngine.UI.Text对象的占用
    private UnityEngine.UI.Text _TextString = null;//缓存
	private UnityEngine.RectTransform _TextTransform = null;//缓存

	void Update () {
		if (_TextObject == null) {
            //在每一帧进行尝试，因为之前被其它游戏对象占用的UnityEngine.UI.Text对象现在可能已经被释放，应当立即复用
            _TextObject = UnityEngine.GameObject.FindGameObjectWithTag ("CY_NameLabel_Unused_Text");
			if (_TextObject == null) {
                //游戏中的UnityEngine.UI.Text对象的个数不足，当前游戏对象的名字板将不显示
                UnityEngine.Debug.LogWarning ("游戏中的UnityEngine.UI.Text对象的个数不足！");
				return;
			}
			_TextObject.tag = "CY_NameLabel_Occupied_Text";//标识该UnityEngine.UI.Text对象已经被占用，在此，我们假定UnityEngine.UI.Text对象的标签不会被并发访问，这取决于Unity3D的内部机制
            _TextString = _TextObject.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
			_TextTransform = _TextObject.GetComponent<UnityEngine.RectTransform> () as UnityEngine.RectTransform;
		}

        //_String可能已经被改变
        if (!_TextString.text.Equals (_String))
			_TextString.text = _String;//生成纹理

        //根据游戏对象的位置计算名字板的位置并考虑用户自定义的偏移（在实际开发中，偏移可以由上层模块的脚本根据某种符合开发者需求的规则计算得出，并传入到本模块）
		_TextTransform.position = UnityEngine.Camera.main.WorldToScreenPoint (this.gameObject.transform.position) + _Offset;
	}

	void OnDisable()
	{
		if (_TextObject != null) {
			_TextObject.tag = "CY_NameLabel_Unused_Text";//标识该UnityEngine.UI.Text对象已经被释放，在此，我们假定UnityEngine.UI.Text对象的标签不会被并发访问，这取决于Unity3D的内部机制
            _TextObject = null;
			_TextString.text = "";//将字符串预设置为空串确保不会显示
			_TextString = null;
			_TextTransform = null;
		}
	}
}