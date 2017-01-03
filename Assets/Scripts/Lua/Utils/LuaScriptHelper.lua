WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
Type = System.Type
DoTweenEase = DG.Tweening.Ease
DoTweenCallback = DG.Tweening.TweenCallback
serpent = require "serpent"

Logger = {}

function Logger.Log(info)
	Debugger.Log(tostring(info).."\n"..debug.traceback())
end

function Logger.LogWarn(info)
	Debugger.LogWarn(tostring(info).."\n"..debug.traceback())
end

function Logger.LogError(info)
	Debugger.LogError(tostring(info).."\n"..debug.traceback())
end

TypeHelper = {}

function TypeHelper.GetTypeInUnityEngine(name)
	return Type.GetType("UnityEngine."..name..", UnityEngine")
end

function TypeHelper.GetTypeInNGUI(name)
	return Type.GetType(name..", Assembly-CSharp-firstpass")
end

LuaScriptHelper = {}

function LuaScriptHelper.NewTable()
	local newTable = {}
	return newTable
end

function LuaScriptHelper.StartDebugger()
	local debugger = require "mobdebug"
	debugger.start()
	Logger.Log("Lua debugger start.")
end

function LuaScriptHelper.Output(object)
	Logger.Log("Output value: "..tostring(object)..", serialize: "..serpent.dump(object))
end