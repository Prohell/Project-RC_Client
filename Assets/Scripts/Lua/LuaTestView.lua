LuaTestView = {}
local this = LuaTestView

local Logger = Debugger

local TempDataList = {}

function LuaTestView.New()
	assert(LuaTestView ~= nil)

	LuaTestView.__index = LuaTestView

	local tb = {}
	setmetatable(tb, LuaTestView)

	return tb
end

function LuaTestView.BindLua(outlet)
	local ins = LuaTestView.New()
	outlet:BindFromLua(ins)
end

function LuaTestView:UpdateItems()
	for i = 0, table.getn(TempDataList) - 1 do
		
	end
end