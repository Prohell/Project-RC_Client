//----------------------------------------------
//	CreateTime  : 1/16/2017 4:01:45 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : BattleBloodBarView
//	ChangeLog   : None
//----------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBloodBarView : MonoBehaviour
{
    public enum Profession
    {
        Archer,
        Artillery,
        Cavalry,
        Hypaspist,
        Infantry
    }

    public Image BloodFillImage;
    public Image MagicFillImage;

    public List<Image> ProfessionImageList = new List<Image>();

    public float BloodPercent
    {
        get { return BloodFillImage.fillAmount; }
        set { BloodFillImage.fillAmount = value; }
    }

    public float MagicPercent
    {
        get { return MagicFillImage.fillAmount; }
        set { MagicFillImage.fillAmount = value; }
    }

    public Profession m_Profession
    {
        get { return m_profession; }
        set
        {
            m_profession = value;
            switch (value)
            {
                case Profession.Archer:
                    {
                        ProfessionImageList.ForEach(item => item.gameObject.SetActive(false));
                        ProfessionImageList[0].gameObject.SetActive(true);
                        break;
                    }
                case Profession.Artillery:
                    {
                        ProfessionImageList.ForEach(item => item.gameObject.SetActive(false));
                        ProfessionImageList[1].gameObject.SetActive(true);
                        break;
                    }
                case Profession.Cavalry:
                    {
                        ProfessionImageList.ForEach(item => item.gameObject.SetActive(false));
                        ProfessionImageList[2].gameObject.SetActive(true);
                        break;
                    }
                case Profession.Hypaspist:
                    {
                        ProfessionImageList.ForEach(item => item.gameObject.SetActive(false));
                        ProfessionImageList[3].gameObject.SetActive(true);
                        break;
                    }
                case Profession.Infantry:
                    {
                        ProfessionImageList.ForEach(item => item.gameObject.SetActive(false));
                        ProfessionImageList[4].gameObject.SetActive(true);
                        break;
                    }
                default:
                    {
                        LogModule.ErrorLog("Cannot set profession icon {0}", value.ToString());
                        break;
                    }
            }
        }
    }

    private Profession m_profession;
}
