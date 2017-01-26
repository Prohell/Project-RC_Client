//----------------------------------------------
//	CreateTime  : 1/23/2017 5:26:01 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : CityController
//	ChangeLog   : None
//----------------------------------------------

using UnityEngine;
public class CityController : MonoBehaviour
{
    public GameObject SelectTag { get; private set; }

    public void Select()
    {
        SelectTag.SetActive(true);
    }

    public void UnSelect()
    {
        SelectTag.SetActive(false);
    }

    void Awake()
    {
        if (SelectTag == null)
        {
            Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>("load_preload$s_character$selecttag.assetbundle", "SelectTag",
                (prefab) =>
                {
                    SelectTag = Instantiate(prefab);
                    Utils.StandardizeObject(SelectTag, transform);

                    UnSelect();
                }));
        }
    }
}
