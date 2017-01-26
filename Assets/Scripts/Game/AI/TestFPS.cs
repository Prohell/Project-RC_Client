using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestFPS : MonoBehaviour {

    private string mUnitNumbers;
    public InputField mInputField;
    public InputField mInputField2;

    // Use this for initialization
    void Awake () {
       // EventManager.GetInstance().AddEventListener("Prepare", StartBattle);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void LoadSquad()
    {
        EventManager.GetInstance().SendEvent(EventId.LoadSquad, mInputField.text);
    }
    public void StartBattle()
    {
        EventManager.GetInstance().SendEvent(EventId.StartBattle);
    }
    public void StartFight()
    {
        EventManager.GetInstance().SendEvent(EventId.StartFight, mInputField2.text);
    }
    public void SendMarch()
    {
        EventManager.GetInstance().SendEvent(EventId.SendMarch);
    }
    public void UseSkill()
    {
        EventManager.GetInstance().SendEvent(EventId.UseSkill);
    }
    public void AutoFight()
    {
        EventManager.GetInstance().SendEvent(EventId.AutoFight);
    }
}
