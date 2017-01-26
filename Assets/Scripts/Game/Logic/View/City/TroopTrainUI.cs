using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TroopTrainUI : MonoBehaviour {

	public class BuildTime{
		public TroopState state;
		public int endTime;
		public int startTime;
	}

	//兵营的标签页
	public UIToggle[] barrackToggles;
	//小兵的ScrollView
	public UIScrollView characterScroll;
	//ScrollView的滚动条
	public UIProgressBar scrollProgress;
	//英雄gameObject
	public GameObject heroObj;
	//英雄头像
	private UISprite heroSprite;
	//所有操作按钮
	public GameObject oprBtns;
	//造兵队列
	public GameObject[] buildSlot;
	//造兵的头像
	private UISprite[] troopSprites;
	//造兵的时间文本
	private UILabel[] troopLabels;
	//造兵的时间进度条
	private UIProgressBar[] troopProgressBars;

	private BuildTime[] buildTimes;

	TroopTrainUIMediator _meditor;
	TroopTrainUIMediator meditor{
		get{ 
			if(_meditor == null){
				_meditor = GameFacade.GetMediator<TroopTrainUIMediator> ();
			}
			return _meditor;
		}
	}

	void Awake(){
		EventManager.GetInstance ().AddEventListener ("Private_TroopTrainRefreshAll", RefreshAll);
		EventManager.GetInstance ().AddEventListener ("Private_TroopTrainRefreshHero", RefreshHero);
		EventManager.GetInstance ().AddEventListener ("Private_TroopTrainRefreshTroops", RefreshTrolls);

		_meditor = new TroopTrainUIMediator ();
		MediatorManager.GetInstance ().Add (_meditor);

		buildTimes = new BuildTime[buildSlot.Length];
		for (int i = 0; i < buildTimes.Length; i++) {
			buildTimes [i] = new BuildTime ();
			buildTimes [i].state = TroopState.Null;
			buildTimes [i].endTime = 0;
		}
	}
	// Use this for initialization
	void Start () {
		//获取英雄头像Sprite
		Transform heroPortrait = heroObj.transform.FindChild ("Img_HeroPortrait");
		heroSprite = heroObj.transform.FindChild("Img_HeroPortrait").GetComponent<UISprite> ();

		troopSprites = new UISprite[buildSlot.Length];
		troopLabels = new UILabel[buildSlot.Length];
		troopProgressBars = new UIProgressBar[buildSlot.Length];
		//获取造兵的现实对象
		for(int i = 0;i < troopSprites.Length;i++){
			troopSprites[i] = buildSlot [i].transform.FindChild ("Img_Soldier").GetComponent<UISprite>();
			troopLabels[i] = buildSlot [i].transform.FindChild ("Time_Pro").FindChild("Lab_Time").GetComponent<UILabel>();
			troopProgressBars[i] = buildSlot [i].transform.FindChild ("Time_Pro").FindChild("ProgressBar").GetComponent<UIProgressBar>();
		}
	}

	void FixedUpdate(){
		for(int i = 0;i < buildTimes.Length;i++){
			if(buildTimes[i].state == TroopState.Training){
				System.DateTime dt1 = System.Convert.ToDateTime("1970-1-1 00:00:00");
				long now = System.DateTime.Now.ToFileTimeUtc() - dt1.ToFileTimeUtc();
				float a = now / 10000000;
				int aa = Mathf.FloorToInt (a);

				if(aa > buildTimes[i].endTime){
					int end = aa - buildTimes [i].endTime;
					int start = aa - buildTimes [i].startTime;
					Transform trans = buildSlot [i].transform;
					Transform timeTrans = trans.FindChild ("Time_Pro");
					Transform labTime = timeTrans.FindChild ("Lab_Time");
					Transform progressbar = timeTrans.FindChild ("ProgressBar");

					UILabel label = labTime.GetComponent<UILabel> ();
					label.text = end.ToString();
					UIProgressBar progress = progressbar.GetComponent<UIProgressBar> ();
					progress.value = 1f - end / start;
				}
			}
		}
	}

	bool buildEnable = true;
	void BuildSequenceDisable(){
		buildEnable = false;
		Disable (heroObj.transform);
		Disable (oprBtns.transform);
		for(int i = 0;i < buildSlot.Length;i++){
			GameObject obj = buildSlot [i];
			Disable (obj.transform);
		}
	}

	void BuildSequenceEnable(){
		buildEnable = true;
		Enable (heroObj.transform);
		Enable (oprBtns.transform);
		for(int i = 0;i < buildSlot.Length;i++){
			GameObject obj = buildSlot [i];
			Enable (obj.transform);
		}
	}
		
	void Disable(Transform trans){
		UISprite mySprite = trans.GetComponent<UISprite> ();
		if(mySprite != null){
			mySprite.color = new Color (0f,mySprite.color.g,mySprite.color.b,mySprite.color.a);
		}

		if(trans.childCount > 0){
			for(int i = 0;i < trans.childCount;i++){
				Transform childTrans = trans.GetChild (i);
				Disable (childTrans);
			}
		}
	}

	void Enable(Transform trans){
		UISprite mySprite = trans.GetComponent<UISprite> ();
		if(mySprite != null){
			mySprite.color = new Color (1f,mySprite.color.g,mySprite.color.b,mySprite.color.a);
		}

		if(trans.childCount > 0){
			for(int i = 0;i < trans.childCount;i++){
				Transform childTrans = trans.GetChild (i);
				Disable (childTrans);
			}
		}
	}

	//下一个兵种
	public void ScrollNext(){
		DOTween.To (() => scrollProgress.value, x => scrollProgress.value = x, 1f, 0.5f).SetEase(Ease.OutCirc);
	}
	//前一个兵种
	public void ScrollPrev(){
		DOTween.To (() => scrollProgress.value, x => scrollProgress.value = x, 0f, 0.5f).SetEase(Ease.OutCirc);
	}
	//点击兵种
	public void ClickCharacter(string id){
		if(!buildEnable){
			return;
		}

		if(id == "Item1"){
			BuildCharacter (1);
		} else if (id == "Item2"){
			BuildCharacter (2);
		}
	}
	//建造兵种
	public void BuildCharacter(int id){
		meditor.AddToBuildSequence (id);
	}
	//更换英雄
	public void ClickChangeHero(){
		if(!buildEnable){
			return;
		}
		meditor.ChangeHero ();
	}
	//开始训练
	public void ClickStartTrain(){
		if(!buildEnable){
			return;
		}
		meditor.StartTrain ();
	}
	//暂停训练
	public void StopTrain(){
		if(!buildEnable){
			return;
		}
	}
	//布阵
	public void Embattle(){
		if(!buildEnable){
			return;
		}
	}
	//出征
	public void ToBattle(){
		if(!buildEnable){
			return;
		}
        meditor.ToBattle();
    }

    //将建造队列还原为初始置空状态
    void InitSLots(){
		for(int i = 0;i < buildSlot.Length;i++){
			GameObject obj = buildSlot [i];
			for(int j = 0;j < obj.transform.childCount;j++){
				Transform child = obj.transform.GetChild (j);
				if (child.gameObject.name == "Img_None") {
					child.gameObject.SetActive (true);
				} else {
					child.gameObject.SetActive (false);
				}
			}
		}
	}

	//更新界面所有内容
	private void RefreshAll(object obj = null){
		//设置标签页
		barrackToggles [meditor.curSlotIndex].value = true;

		//设置已解锁兵种  目前写死
		//

		RefreshHero ();
		RefreshTrolls ();
	}

	//刷新英雄
	public void RefreshHero(object obj = null){
		heroSprite.spriteName = meditor.GetCurHeroIcon ();
	}

	//刷新训练队列
	public void RefreshTrolls(object obj = null){
		InitSLots ();
		int num = meditor.GetCurTroopCount ();
		for (int i = 0; i < num; i++) {
			TroopState state = meditor.GetTroopState (i);
			int queueIndex = meditor.GetTroopQueueIndex(i);

			troopSprites[queueIndex].spriteName = meditor.GetTroopIcon(i);

			Transform trans = buildSlot [queueIndex].transform;
			trans.FindChild ("Img_None").gameObject.SetActive(false);
			trans.FindChild ("Btn_Delete").gameObject.SetActive(true);
			trans.FindChild ("Img_Soldier").gameObject.SetActive(true);

			Transform timeTrans = trans.FindChild ("Time_Pro");
			Transform labTime = timeTrans.FindChild ("Lab_Time");
			Transform progressbar = timeTrans.FindChild ("ProgressBar");
			//训练队列中 正在训练中 训练满
			if (state == TroopState.NeedTrain) {
//				timeTrans.gameObject.SetActive (true);
//				labTime.gameObject.SetActive (false);
//				progressbar.gameObject.SetActive (false);
				//临时处理
				timeTrans.gameObject.SetActive (false);
				labTime.gameObject.SetActive (false);
				progressbar.gameObject.SetActive (false);
				Transform labDone = trans.FindChild ("Lab_Done");
				labDone.gameObject.SetActive (true);
				labDone.GetComponent<UILabel> ().text = "等待训练";

				buildTimes [i].state = TroopState.NeedTrain;
			} else if (state == TroopState.Training) {
//				timeTrans.gameObject.SetActive (true);
//				labTime.gameObject.SetActive (true);
//				progressbar.gameObject.SetActive (true);
//				UILabel timeLabel = labTime.GetComponent<UILabel> ();
//				UIProgressBar progress = timeTrans.FindChild ("ProgressBar").GetComponent<UIProgressBar>();
//				var trainData = meditor.GetCurTrainData (meditor.curBuildingGuid, queueIndex);
//				buildTimes [i].endTime = trainData.endTime;
				//临时处理
				timeTrans.gameObject.SetActive (false);
				labTime.gameObject.SetActive (false);
				progressbar.gameObject.SetActive (false);
				Transform labDone = trans.FindChild ("Lab_Done");
				labDone.gameObject.SetActive (true);
				labDone.GetComponent<UILabel> ().text = "训练中";

				buildTimes [i].state = TroopState.Training;
			} else if (state == TroopState.TrainFull) {
				timeTrans.gameObject.SetActive (false);
				labTime.gameObject.SetActive (false);
				progressbar.gameObject.SetActive (false);
				Transform labDone = trans.FindChild ("Lab_Done");
				labDone.gameObject.SetActive (true);
				labDone.GetComponent<UILabel> ().text = "训练完成";
				buildTimes [i].state = TroopState.TrainFull;
			}
		}
	}
		

	public void ClickBarrackToggle(UIToggle toggle){
		if(toggle.value){
			int index = -1;
			for(int i = 0;i < barrackToggles.Length;i++){
				if(barrackToggles[i] == toggle){
					index = i + 1;
					break;
				}
			}

			if(index != -1){
				//临时处理 锁定兵营数据为29
				index = 29;

				meditor.SetBarrackData (index);
			}

		}
	}


	public void CloseClick(){
		UIManager.GetInstance ().CloseUI ("TroopTrainUI", false);
		CityMediator cityMediator = GameFacade.GetMediator<CityMediator> ();
		cityMediator.view.ShowCurSlotCenter ();
		cityMediator.view.ShowBtns ();
	}
}
