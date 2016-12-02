BagView = {}
local this = BagView

local Logger = Debugger
local Controller

function BagView.New()
	assert(BagView ~= nil)

	BagView.__index = BagView

	local tb = {}
	setmetatable(tb, BagView)

	return tb
end

function BagView.BindLua(outlet)
	local ins = BagView.New()
	outlet:BindFromLua(ins)
end

function BagView:Init(paras)
	Controller = paras[0]
end

function BagView:Awake()
	Logger.Log("Bag view awake")
end