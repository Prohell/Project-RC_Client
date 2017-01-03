MainUIView = 
{
	AnimFuncList = {},
	isDisableDrawer = false
}

local AnimFuncDuration = 0.5

function MainUIView:Init()
	self.AnimFuncList[0] = LuaHelper.GetLuaComponent(self.Btn_Bag.gameObject, "MainUIItemView")
	self.AnimFuncList[1] = LuaHelper.GetLuaComponent(self.Btn_Task.gameObject, "MainUIItemView")
	self.AnimFuncList[2] = LuaHelper.GetLuaComponent(self.Btn_League.gameObject, "MainUIItemView")
	self.AnimFuncList[0].m_MainUIView = self
	self.AnimFuncList[1].m_MainUIView = self
	self.AnimFuncList[2].m_MainUIView = self
	for i = 0, table.getn(self.AnimFuncList) do
		self.AnimFuncList[i]:SetPos()
	end

	self:AddListener()
	self:OpenFunc()
end

function MainUIView:ShowCastleButton()
	self.Btn_Castle.gameObject:SetActive(true)
	self.Btn_World.gameObject:SetActive(false)
end

function MainUIView:ShowWorldButton()
	self.Btn_Castle.gameObject:SetActive(false)
	self.Btn_World.gameObject:SetActive(true)
end

function MainUIView:AddListener()
	--Add listener
	EventDelegate.Add(self.Btn_Open.onClick, EventDelegate.Callback(MainUIView["OpenFunc"], self))
	EventDelegate.Add(self.Btn_Close.onClick, EventDelegate.Callback(MainUIView["CloseFunc"], self))
	EventDelegate.Add(self.Btn_Bag.onClick, EventDelegate.Callback(MainUIView["OpenBag"], self))
	EventDelegate.Add(self.Btn_Task.onClick, EventDelegate.Callback(MainUIView["OpenTask"], self))
	EventDelegate.Add(self.Btn_League.onClick, EventDelegate.Callback(MainUIView["OpenLeague"], self))
	EventDelegate.Add(self.Btn_Castle.onClick, EventDelegate.Callback(MainUIView["OpenCastle"], self))
	EventDelegate.Add(self.Btn_World.onClick, EventDelegate.Callback(MainUIView["OpenWorld"], self))
end

function MainUIView:OpenFunc()
	if self.isDisableDrawer == true then
		return
	end

	for i = 0, table.getn(self.AnimFuncList) do
		self.AnimFuncList[i].gameObject:SetActive(true)
		self.AnimFuncList[i].transform.localPosition = self.Btn_Close.transform.localPosition
		self.AnimFuncList[i].transform:DOLocalMove(self.AnimFuncList[i].originalPos, AnimFuncDuration, false):SetEase(DoTweenEase.OutBack):OnComplete(DoTweenCallback(MainUIItemView.OpenComplete, self.AnimFuncList[i], true));
	end
	self.Btn_Open.gameObject:SetActive(false)
	self.Btn_Close.gameObject:SetActive(true)

	self:DisableDrawer()
end

function MainUIView:CloseFunc()
	if self.isDisableDrawer == true then
		return
	end

	for i = 0, table.getn(self.AnimFuncList) do
		self.AnimFuncList[i].transform.localPosition = self.AnimFuncList[i].originalPos
		self.AnimFuncList[i].transform:DOLocalMove(self.Btn_Close.transform.localPosition, AnimFuncDuration, false):SetEase(DoTweenEase.InBack):OnComplete(DoTweenCallback(MainUIItemView.CloseComplete, self.AnimFuncList[i], true));
	end
	self.Btn_Open.gameObject:SetActive(true)
	self.Btn_Close.gameObject:SetActive(false)

	self:DisableDrawer()
end

function MainUIView:EnableDrawer()
	self.isDisableDrawer = false
end

function MainUIView:DisableDrawer()
	self.isDisableDrawer = true
end

function MainUIView:OpenBag()
	
end

function MainUIView:OpenTask()
	
end

function MainUIView:OpenLeague()
	
end

function MainUIView:OpenCastle()
	MySceneManager.GetInstance():SwitchToScene(SceneId.Castle, nil);
end

function MainUIView:OpenWorld()
	MySceneManager.GetInstance():SwitchToScene(SceneId.BattleTest, nil);
end
