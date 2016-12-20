require "define"

local LuaOutLetBase = import("LuaOutLetBase")

CastleLogic = 
{
	TempDataList = {},
	UpdateCount = 0,
	ItemPrefab
}
CastleLogic.__index = CastleLogic

function CastleLogic.New()
	return LuaOutLetBase.New(CastleLogic)
end

function CastleLogic.BindLua(outlet)
	return LuaOutLetBase.BindLua(outlet, CastleLogic)
end

function CastleLogic:AAA()
	Logger.Log(self.BBB)
end


function CastleLogic:Start()
	self:AAA()
end
