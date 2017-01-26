//----------------------------------------------
//	CreateTime  : 12/28/2016 4:03:22 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : BattleUIController
//	ChangeLog   : None
//----------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleUIController : IMediator
{
    public bool IsInited = false;
    public Dictionary<int, int> SkillIndexToTroopIndex = new Dictionary<int, int>();
    public BattleUIView View;

    public string SelfName
    {
        get { return View.SelfNameText.text; }
        set { View.SelfNameText.text = value; }
    }

    public string TargetName
    {
        get { return View.TargetNameText.text; }
        set { View.TargetNameText.text = value; }
    }

    public float SelfBloodPercent
    {
        get { return View.SelfBloodImage.fillAmount; }
        set { View.SelfBloodImage.fillAmount = value; }
    }

    public float TargetBloodPercent
    {
        get { return View.TargetBloodImage.fillAmount; }
        set { View.TargetBloodImage.fillAmount = value; }
    }

    public GameObject AutoObject
    {
        get { return View.AutoObject; }
    }

    public GameObject LeaveObject
    {
        get { return View.LeaveObject; }
    }

    public GameObject SurroundObject
    {
        get { return View.SurroundObject; }
    }

    private const int DefaultCountDownSec = 10;

    public void StartCountDown()
    {
        StartCountDown(DefaultCountDownSec);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sec">count down second</param>
    public void StartCountDown(int sec)
    {
        View.StartCountDown(sec);
    }

    public void OnAutoClick()
    {
        EventManager.GetInstance().SendEvent(EventId.AutoFight);
    }

    public void OnSurrenderClick()
    {
        EventManager.GetInstance().SendEvent(EventId.StartBattle);
    }

    public void OnLeaveClick()
    {
        EventManager.GetInstance().SendEvent(EventId.AutoFight);
    }

    public void OnSkillClick(int i)
    {
        if (SkillIndexToTroopIndex.ContainsKey(i))
        {
            int troopID = SkillIndexToTroopIndex[i];
            EventManager.GetInstance().SendEvent(EventId.UseSkill, troopID);
        }
    }

    private void AddListener()
    {
        View.AutoButton.onClick.AddListener(OnAutoClick);
        View.SurroundButton.onClick.AddListener(OnSurrenderClick);
        View.LeaveButton.onClick.AddListener(OnLeaveClick);
        View.DimmerButton.onClick.AddListener(StartCountDown);

        for (int i = 0; i < View.SkillButtons.Count; i++)
        {
            var index = i;
            View.SkillButtons[i].onClick.AddListener(() =>
            {
                OnSkillClick(index);
            });
        }
    }

    public void OnInit()
    {
        if (View == null)
        {
            Game.StartCoroutine(GameAssets.LoadAssetAsync<GameObject>(BattleUIView.BundleName, BattleUIView.AssetName,
                (prefab) =>
                {
                    var ins = Object.Instantiate(prefab);
                    Utils.StandardizeObject(ins);

                    if (View == null)
                    {
                        View = ins.GetComponent<BattleUIView>();
                    }

                    AddListener();

                    var troopList = GameFacade.GetProxy<BattleProxy>().BattleInfor.objList.Where(item => item.skilldataid != null && item.skilldataid.Any()).Select(item => item.id).ToList();
                    for (int i = 0; i < troopList.Count; i++)
                    {
                        SkillIndexToTroopIndex.Add(i, troopList[i]);
                    }

                    IsInited = true;
                }));
            GameAssets.AddBundleRef(BattleUIView.BundleName);
        }
        else
        {
            IsInited = true;
        }
    }

    public void OnDestroy()
    {
        if (View != null && View.gameObject != null)
        {
            Object.Destroy(View.gameObject);
        }

        GameAssets.SubBundleRef(BattleUIView.BundleName);
    }
}
