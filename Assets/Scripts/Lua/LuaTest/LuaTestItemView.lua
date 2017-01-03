LuaTestItemView = {}

function LuaTestItemView:SetData(data)
	self.NumData = data
	self.LuaTestItemLabel.text = tostring(self.NumData)
end

function LuaTestItemView:OnClick()
	self.MainView.Mediator:UseItem(self.NumData)
	GameObject.Destroy(self.gameObject)
end