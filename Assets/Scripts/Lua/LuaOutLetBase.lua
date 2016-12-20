LuaOutLetBase = {}
LuaOutLetBase.__index = LuaOutLetBase

function LuaOutLetBase.New(sourceTable)
	assert(sourceTable ~= nil)

	sourceTable.__index = sourceTable

	local tb = {}
	setmetatable(tb, sourceTable)

	return tb
end

function LuaOutLetBase.BindLua(outlet, sourceTable)
	local ins = sourceTable.New()
	outlet:BindFromLua(ins)
end

return LuaOutLetBase