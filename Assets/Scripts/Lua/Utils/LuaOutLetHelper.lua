LuaOutLetHelper = {}

function LuaOutLetHelper.New(sourceTable)
	assert(sourceTable ~= nil)
	sourceTable.__index = sourceTable
	sourceTable.__serialize = function(t) return serpent.dump(sourceTable) end
	return setmetatable({}, sourceTable)
end

function LuaOutLetHelper.BindLua(outlet, sourceTable)
	outlet:BindFromLua(LuaOutLetHelper.New(sourceTable))
end