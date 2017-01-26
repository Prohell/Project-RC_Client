WorldMarchView = 
{
	index,
	state,
	time
}

function WorldMarchView:Set(state, time)
	self.state = state
	self.time = time
	self.State.text = self.state
	self.RemainingTime.text = self.time:ToString()
end

function WorldMarchView:StartCountDown()
	if TimeHelper.Instance:IsTimeCalcKeyExist("WorldMarchView"..tostring(self.index)) then
		TimeHelper.Instance:RemoveFromTimeCalc("WorldMarchView"..tostring(self.index))
	end

	TimeHelper.Instance:AddEveryDelegateToTimeCalc("WorldMarchView"..tostring(self.index), self.time:ToSeconds(), DelegateHelper.IntDelegate(WorldMarchView["UpdateCountDown"], self))
end

function WorldMarchView:UpdateCountDown(elapseTime)
	if self.gameObject == nil then
		TimeHelper.Instance:RemoveFromTimeCalc("WorldMarchView"..tostring(self.index))
		return
	end

	local remainingTime = self.time:ToSeconds() - elapseTime

	if remainingTime >= 0 then
		self.RemainingTime.text = TimeHelper.SecondToClockTime(remainingTime):ToString()
	else
		TimeHelper.Instance:RemoveFromTimeCalc("WorldMarchView"..tostring(self.index))
	end
end

function WorldMarchView:OnMarchClick()
	self.WorldUI:OnMarchListClick(self.index)
end

function WorldMarchView:AddListener()
	--Add listener
	EventDelegate.Add(self.ClickButton.onClick, EventDelegate.Callback(WorldMarchView["OnMarchClick"], self))
end

function WorldMarchView:Init(index)
	self.index = index
	self:AddListener()
end