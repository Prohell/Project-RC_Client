require "define"

local LuaOutLetBase = import("LuaOutLetBase")

LuaTestView = 
{
	TempDataList = {},
	UpdateCount = 0,
	ItemPrefab
}
LuaTestView.__index = LuaTestView

function LuaTestView.New()
	return LuaOutLetBase.New(LuaTestView)
end

function LuaTestView.BindLua(outlet)
	return LuaOutLetBase.BindLua(outlet, LuaTestView)
end

function LuaTestView:UpdatePanel(paras)
	self:UpdatePanelInternal(paras[0])
end

function LuaTestView:UpdateItems()
	if self.ItemPrefab == nil then
		return
	end

	self.TempDataList = self.Mediator:GetBagItemData()

	self.UpdateCount = self.UpdateCount + 1
	self.LuaTestLabel.text = self.UIName.."  Updated_"..self.UpdateCount

	while(self.LuaTestGrid.transform.childCount ~= 0) do
		local child = self.LuaTestGrid.transform:GetChild(0)
		GameObject.Destroy(child.gameObject)
		child.transform.parent = nil
	end

	for i = 0, table.getn(self.TempDataList) do
		local item = GameObject.Instantiate(self.ItemPrefab)
		item.transform.parent = self.LuaTestGrid.transform
		item.transform.localScale = Vector3.one

		local itemView = LuaHelper.GetComponent(item.gameObject, "LuaTestItemView")
		itemView["MainView"] = self
		itemView:SetData(self.TempDataList[i])
	end

	self.LuaTestGrid:Reposition()
end

function LuaTestView:UseAll(gb)
	while self.LuaTestGrid.transform.childCount ~= 0 do
		local child = self.LuaTestGrid.transform:GetChild(0)
		GameObject.Destroy(child.gameObject)
		child.transform.parent = nil
	end
	self.Mediator:UseAllItems()
end

function LuaTestView:Close(gb)
	UIManager.GetInstance():CloseUI(self.UIName)
end

function LuaTestView:UpdatePanelInternal(baseDepth)
	local panel = self.LuaTestScrollView:GetComponent(TypeHelper.GetTypeInNGUI("UIPanel"))
	
	panel.depth = baseDepth + 1
end

function LuaTestView:Init()
	-- Set panel
	local baseDepth = self.transform.parent:GetComponent(TypeHelper.GetTypeInNGUI("UIPanel")).depth
	self:UpdatePanelInternal(baseDepth)

	-- Add listener
	local listener = self.ClearButton.gameObject:AddComponent(TypeHelper.GetTypeInNGUI("UIEventListener"))
	listener.onClick = listener.onClick + UIEventListener.VoidDelegate(LuaTestView["UseAll"], self)

	local listener2 = self.CloseButton.gameObject:AddComponent(TypeHelper.GetTypeInNGUI("UIEventListener"))
	listener2.onClick = listener2.onClick + UIEventListener.VoidDelegate(LuaTestView["Close"], self)

	-- Set label
	self.LuaTestLabel.text = self.UIName

	-- Load prefab
	LuaHelper.LoadBundleGB("load_preload$g_luatest$ui.assetbundle", "LuaTestItem", self, self["StoreItemPrefab"])
end

function LuaTestView:StoreItemPrefab(gb)
	self.ItemPrefab = gb
end

function LuaTestView:Refresh()
	-- Identify UI	
	if self.UIName=="LuaTest1" then
		self.transform.localPosition = UnityEngine.Vector3.New(100, 20, 0)
	elseif self.UIName=="LuaTest2" then
		self.transform.localPosition = UnityEngine.Vector3.New(-200, 20, 0)
	elseif self.UIName=="LuaTest3" then
		self.transform.localPosition = UnityEngine.Vector3.New(200, -20, 0)
	elseif self.UIName=="LuaTest4" then
		self.transform.localPosition = UnityEngine.Vector3.New(-100, -20, 0)
	else	
		Logger.LogError("UIName: "..self.UIName.." not defined.")
	end
end