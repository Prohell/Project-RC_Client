//----------------------------------------------
//	CreateTime  : 1/22/2017 5:30:43 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : WorldView
//	ChangeLog   : None
//----------------------------------------------

using System.Collections.Generic;
using UnityEngine;
public class WorldView : MonoBehaviour, IInit, IDestroy
{
    private WorldProxy m_WorldModel;

    public GameObject m_World3DContainer;
    private GameObject m_CityPrefab;
    private GameObject m_MarchPrefab;
    private GameObject m_SelectTagPrefab;

    private GameObject m_selectTagObject;

    private List<GameObject> MyMarchObjects = new List<GameObject>();
    private List<GameObject> OtherMatchObjects = new List<GameObject>();

    public void SetMarchView()
    {
        //MyMarchObjects.ForEach(item => Destroy(item));
        //MyMarchObjects.Clear();
        OtherMatchObjects.ForEach(item => Destroy(item));
        OtherMatchObjects.Clear();

        foreach (WorldProxy.MarchInfo item in m_WorldModel.MyOutMarchList)
        {
            if (item.MarchObject == null)
            {
                item.MarchObject = UnityEngine.Object.Instantiate(m_MarchPrefab);
                item.MarchObject.transform.parent = m_World3DContainer.transform;
                Utils.StandardizeObject(item.MarchObject);
            }

            item.MarchObject.transform.localPosition = MapHelper.CoordToVector3(item.Position);
            item.MarchObject.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        foreach (WorldProxy.MarchInfo item in m_WorldModel.OtherMarchList)
        {
            if (item.MarchObject == null)
            {
                item.MarchObject = UnityEngine.Object.Instantiate(m_MarchPrefab);
                OtherMatchObjects.Add(item.MarchObject);
                item.MarchObject.transform.parent = m_World3DContainer.transform;
                Utils.StandardizeObject(item.MarchObject);
            }

            item.MarchObject.transform.localPosition = MapHelper.CoordToVector3(item.Position);
            item.MarchObject.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void SetCityView()
    {
        //Set my city.
        if (m_WorldModel.MyCity.CityObject == null)
        {
            m_WorldModel.MyCity.CityObject = Instantiate(m_CityPrefab);
            Utils.StandardizeObject(m_WorldModel.MyCity.CityObject, m_World3DContainer.transform);
            m_WorldModel.MyCity.CityObject.transform.localScale = new Vector3(1, 5, 1);
        }
        m_WorldModel.MyCity.CityObject.transform.position = MapHelper.CoordToVector3(m_WorldModel.MyCity.Position);

        //Set other city.
        for (int i = 0; i < m_WorldModel.OtherCityList.Count; i++)
        {
            m_WorldModel.OtherCityList[i].CityObject = Instantiate(m_CityPrefab);
            Utils.StandardizeObject(m_WorldModel.OtherCityList[i].CityObject, m_World3DContainer.transform);
            m_WorldModel.OtherCityList[i].CityObject.transform.localScale = new Vector3(1, 5, 1);
            m_WorldModel.OtherCityList[i].CityObject.transform.position = MapHelper.CoordToVector3(m_WorldModel.OtherCityList[i].Position);
        }
    }

    public void SelectTagObject(Vector3 pos)
    {
        m_selectTagObject.transform.position = pos;
        m_selectTagObject.SetActive(true);
    }

    public void UnSelectTagObject()
    {
        m_selectTagObject.SetActive(false);
    }

    public void OnInit()
    {
        m_WorldModel = ProxyManager.GetInstance().Get<WorldProxy>();

        Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>("load_preload$s_building$worldcity.assetbundle",
                    "WorldCity",
                    (prefab) =>
                    {
                        m_CityPrefab = prefab;
                    }));
        Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>("load_preload$s_character$worldmarch.assetbundle",
                    "WorldMarch",
                    (prefab) =>
                    {
                        m_MarchPrefab = prefab;
                    }));
        Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>("load_preload$s_character$selecttag.assetbundle",
            "SelectTag",
            (prefab) =>
            {
                m_SelectTagPrefab = prefab;
                m_selectTagObject = UnityEngine.Object.Instantiate(prefab);
                Utils.StandardizeObject(m_selectTagObject, m_World3DContainer.transform);
                m_selectTagObject.transform.localEulerAngles = new Vector3(90, 0, 0);
                m_selectTagObject.SetActive(false);
            }));

        m_World3DContainer = new GameObject("World3DContainer");
    }

    public void OnDestroy()
    {

    }
}
