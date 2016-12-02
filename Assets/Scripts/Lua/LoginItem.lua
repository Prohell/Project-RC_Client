
LoginItem = {}
local this = LoginItem

local Logger = Debugger

function LoginItem.New()
	assert(LoginItem ~= nil)

	LoginItem.__index = LoginItem

	local tb = {}
	setmetatable(tb, LoginItem)

	Logger.Log(tb)
	return tb
end

function LoginItem:Init(num)
	self.LoginItemLabel.text = tostring(num)
end