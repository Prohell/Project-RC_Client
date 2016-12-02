
Login = {}
local this = Login

local GameObject = UnityEngine.GameObject
local Instantiate = GameObject.Instantiate
local Vector3 = UnityEngine.Vector3
local Logger = Debugger

local parent
local ItemManagerList = {}

function Login.Init()					
	local prefab = LuaHelper.LoadGB("LuaTest/Login")
	local ins = Instantiate(prefab)
	ins.transform.parent = GameObject.Find("Camera").transform
	ins.transform.localScale = Vector3.one

	local loginOutlet = ins:GetComponent("UILuaOutlet")
	loginOutlet:SetLua("Login", this)
	loginOutlet:BindLua()

	this.LoginLabel.text = "Setted by lua."

	LuaHelper.LoadLuaFile("LoginItem")

	local itemPrefab = LuaHelper.LoadGB("LuaTest/LoginItem")
	for i = 0, 9 do
		local itemIns = Instantiate(itemPrefab)
		itemIns.transform.parent = this.LoginGrid.transform
		itemIns.transform.localScale = Vector3.one

		ItemManagerList[i] = itemIns:GetComponent("UILuaOutlet")
		local currentItemTable = LoginItem.New(LoginItem)
		ItemManagerList[i]:SetLua("LoginItem", currentItemTable)
		ItemManagerList[i]:BindLua()

		currentItemTable:Init(i)
	end

	this.LoginGrid.arrangement = UIGrid.Arrangement.Vertical
	this.LoginGrid.cellHeight = 50
	this.LoginGrid:Reposition()

	Logger.Log(collectgarbage("count"))
	Logger.Log(collectgarbage("collect"))
end