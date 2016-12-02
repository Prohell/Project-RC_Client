function Main()		
	LuaHelper.LoadLuaFile("Login")			
	LuaHelper.CallLuaFunction("Login.Init")				
end

function OnLevelWasLoaded(level)
	Time.timeSinceLevelLoad = 0
end