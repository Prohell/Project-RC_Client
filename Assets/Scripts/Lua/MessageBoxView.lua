require "define"

local LuaOutLetBase = import("LuaOutLetBase")

MessageBoxView = {
	InfoStr,
	OKCallBack,
	CancelCallBack
}
MessageBoxView.__index = MessageBoxView

function MessageBoxView.New()
	return LuaOutLetBase.New(MessageBoxView)
end

function MessageBoxView.BindLua(outlet)
	return LuaOutLetBase.BindLua(outlet, MessageBoxView)
end

function MessageBoxView:Init()
	self.InfoLabel.text = self.InfoStr

	--Add listener
	EventDelegate.Add(self.OKButton.onClick, EventDelegate.Callback(MessageBoxView["OK"], self))
	EventDelegate.Add(self.CancelButton.onClick, EventDelegate.Callback(MessageBoxView["Cancel"], self))
	EventDelegate.Add(self.CloseButton.onClick, EventDelegate.Callback(MessageBoxView["Close"], self))

	if self.OKCallBack ~= nil then
		EventDelegate.Add(self.OKButton.onClick, self.OKCallBack)
	end

	if self.CancelCallBack ~= nil then
		EventDelegate.Add(self.CancelButton.onClick, self.CancelCallBack)
	end
end

function MessageBoxView:OK()
	self:Close()
end

function MessageBoxView:Cancel()
	self:Close()
end

function MessageBoxView:Close()
	UIManager.GetInstance():DestroyUI(self.UIName)
end