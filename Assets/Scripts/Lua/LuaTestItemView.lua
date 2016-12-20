require "define"

LuaTestItemView = {}
LuaTestItemView.__index = LuaTestItemView

local TempDataList = {}

function LuaTestItemView.New()
	return LuaOutLetBase.New(LuaTestItemView)
end

function LuaTestItemView.BindLua(outlet)
	return LuaOutLetBase.BindLua(outlet, LuaTestItemView)
end

function LuaTestItemView:SetData(data)
	self.NumData = data
	self.LuaTestItemLabel.text = tostring(self.NumData)
end

function LuaTestItemView:OnClick()
	self.MainView.Mediator:UseItem(self.NumData)
	GameObject.Destroy(self.gameObject)
end