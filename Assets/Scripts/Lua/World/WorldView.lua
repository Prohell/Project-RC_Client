WorldView = 
{
	MarchList = {},
	WorldController
}

function WorldView:OnMarchListClick(index)
	self.WorldController:MarchListClick(index)
end

function WorldView:SetMarchObject(paras)
	self.MarchList[paras[0]]:Set(paras[1], paras[2])
	self.MarchList[paras[0]]:StartCountDown()
end

function WorldView:ShowMarchObject(paras)
	self.MarchList[paras[0]].gameObject:SetActive(true)
end

function WorldView:HideMarchObject(paras)
	self.MarchList[paras[0]].gameObject:SetActive(false)
end

function WorldView:HideAllButtons()
	self.MoveToggle.value = false
	self.AttackToggle.value = false
	self.GoOutToggle.value = false
	self.OccupyToggle.value = false
	self.AssistanceToggle.value = false
	self.InfoToggle.value = false
	self.FocusToggle.value = false
	self.RecallToggle.value = false
	self.EvacuateToggle.value = false
	self.TradeToggle.value = false
end

function WorldView:OnMoveClick()
	self.WorldController:MoveClick()
end

function WorldView:ShowMoveButton()
	self.MoveToggle.value = true
end

function WorldView:OnAttackClick()
	self.WorldController:AttackClick()
end

function WorldView:ShowAttackButton()
	self.AttackToggle.value = true
end

function WorldView:OnGoOutClick()
	self.WorldController:GoOutClick()
end

function WorldView:ShowGoOutButton()
	self.GoOutToggle.value = true
end

function WorldView:OnOccupyClick()
	
end

function WorldView:ShowOccupyButton()
	self.OccupyToggle.value = true
end

function WorldView:OnAssistanceClick()
	
end

function WorldView:ShowAssistanceButton()
	self.AssistanceToggle.value = true
end

function WorldView:OnInfoClick()
	
end

function WorldView:ShowInfoButton()
	self.InfoToggle.value = true
end

function WorldView:OnFocusClick()
	
end

function WorldView:ShowFocusButton()
	self.FocusToggle.value = true
end

function WorldView:OnRecallClick()
	
end

function WorldView:ShowRecallButton()
	self.RecallToggle.value = true
end

function WorldView:OnEvacuateClick()
	
end

function WorldView:ShowEvacuateButton()
	self.EvacuateToggle.value = true
end

function WorldView:OnTradeClick()
	
end

function WorldView:ShowTradeButton()
	self.TradeToggle.value = true
end

function WorldView:ShowMarchInfo(info)
	self.MarchInfoObject:SetActive(true)
	self.MarchInfoLabel.text = info
end

function WorldView:HideMarchInfo()
	self.MarchInfoObject:SetActive(false)
end

function WorldView:AddListener()
	--Add listener
	EventDelegate.Add(self.MoveToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnMoveClick"], self))
	EventDelegate.Add(self.AttackToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnAttackClick"], self))
	EventDelegate.Add(self.GoOutToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnGoOutClick"], self))
	EventDelegate.Add(self.OccupyToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnOccupyClick"], self))
	EventDelegate.Add(self.AssistanceToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnAssistanceClick"], self))
	EventDelegate.Add(self.InfoToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnInfoClick"], self))
	EventDelegate.Add(self.FocusToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnFocusClick"], self))
	EventDelegate.Add(self.RecallToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnRecallClick"], self))
	EventDelegate.Add(self.EvacuateToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnEvacuateClick"], self))
	EventDelegate.Add(self.TradeToggle:GetComponent(TypeHelper.GetTypeInNGUI("UIButton")).onClick, EventDelegate.Callback(WorldView["OnTradeClick"], self))
end

function WorldView:Init()
	-- Add to list.
	self.MarchList[0] = self.March1.m_LuaTable
	self.MarchList[0].index = 0
	self.MarchList[1] = self.March2.m_LuaTable
	self.MarchList[1].index = 1
	self.MarchList[2] = self.March3.m_LuaTable
	self.MarchList[2].index = 2
	self.MarchList[3] = self.March4.m_LuaTable
	self.MarchList[3].index = 3

	-- Bind click.
	for i = 0, table.getn(self.MarchList) do
		self.MarchList[i].WorldUI = self
		self.MarchList[i]:AddListener()
	end
	self:AddListener()

	-- Add mvc
	self.WorldModel = ProxyManager.GetInstance():Get("WorldProxy")
	self.WorldController = WorldController.GetInstance()
end