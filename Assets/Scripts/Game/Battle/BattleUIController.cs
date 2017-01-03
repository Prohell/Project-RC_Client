//----------------------------------------------
//	CreateTime  : 12/28/2016 4:03:22 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : BattleUIController
//	ChangeLog   : None
//----------------------------------------------

using UnityEngine;

public class BattleUIController : IMediator
{
    public BattleUIView View;

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

    }

    public void OnSurroundClick()
    {

    }

    public void OnLeaveClick()
    {

    }

    public void OnSkillClick(int i)
    {

    }

    private void AddListener()
    {
        View.AutoButton.onClick.AddListener(OnAutoClick);
        View.SurroundButton.onClick.AddListener(OnSurroundClick);
        View.LeaveButton.onClick.AddListener(OnLeaveClick);

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
                }));
            GameAssets.AddBundleRef(BattleUIView.BundleName);
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
