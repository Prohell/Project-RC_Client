BagController = {}
local this = BagController

local Logger = Debugger
local Model
local View

local BagItemDataList = {}

function BagController.New()
	assert(BagController ~= nil)

	BagController.__index = BagController

	local tb = {}
	setmetatable(tb, BagController)

	return tb
end

function BagController:Init(paras)
	Model = paras[0]
	View = paras[1]

	Model.OnUpdateData = self.UpdateView
	-- LuaHelper.AddListener(Model.OnUpdateData, UpdateView)
	Logger.Log("Inited")
end

function BagController:UpdateView()
	View.BagLabel.text = self.BagLabelText
end