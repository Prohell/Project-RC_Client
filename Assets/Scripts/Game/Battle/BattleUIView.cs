//----------------------------------------------
//	CreateTime  : 12/28/2016 4:03:22 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : BattleUIView
//	ChangeLog   : None
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class BattleUIView : MonoBehaviour
{
    public const string BundleName = "load_preload$s_ui$battleui.assetbundle";
    public const string AssetName = "BattleUI";

    public List<GameObject> SkillObjects = new List<GameObject>();
    public List<Button> SkillButtons = new List<Button>();

    public Button AutoButton;
    public GameObject AutoObject;

    public Button LeaveButton;
    public GameObject LeaveObject;

    public Button SurroundButton;
    public GameObject SurroundObject;

    public Text CountDownText;
    public GameObject CountDownObject;

    public Text SelfNameText;
    public Text TargetNameText;

    public Image SelfBloodImage;
    public Image TargetBloodImage;

    public Button DimmerButton;

    public void StartCountDown(int sec)
    {
        CountDownObject.SetActive(true);

        StartCoroutine(UpdateCountDown(sec));
    }

    IEnumerator UpdateCountDown(int sec)
    {
        if (sec <= 0)
        {
            EndCountDown();
            yield break;
        }

        CountDownText.text = string.Format("{0} 秒", sec);
        yield return new WaitForSeconds(1);

        if (gameObject != null && gameObject.activeInHierarchy)
        {
            StartCoroutine(UpdateCountDown(--sec));
        }
        else
        {
            EndCountDown();
        }
    }

    void EndCountDown()
    {
        CountDownObject.SetActive(false);
    }

    void Awake()
    {
        if (!SkillObjects.Any())
        {
            SkillObjects = SkillButtons.Select(item => item.gameObject).ToList();
        }
    }
}
