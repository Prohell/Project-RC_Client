WWW = UnityEngine.WWW;
GameObject = UnityEngine.GameObject;
Type = System.Type

Logger = {}
Logger.__index = Logger

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
TypeHelper.__index = TypeHelper

function TypeHelper.GetTypeInUnityEngine(name)
    return Type.GetType("UnityEngine."..name..", UnityEngine")
end

function TypeHelper.GetTypeInNGUI(name)
    return Type.GetType(name..", Assembly-CSharp-firstpass")
end

