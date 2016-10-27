using System;
using UnityEngine;
using System.IO;
/// <summary>
/// 日志模块
/// by TT
/// 2016-06-17
/// </summary>
public class LogModule
{
    enum LOG_TYPE
    {
        DEGUG_LOG = 0,
        WARNING_LOG,
        ERROR_LOG
    }

    public delegate void OnOutputLog(string _msg);
    static public OnOutputLog onOutputLog = null;

    private static StreamWriter m_fileStream;

    static void InitLogFile()
    {
        string path = Application.persistentDataPath + "/log/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        m_fileStream = new StreamWriter(string.Format(path + "Game_{0}.log", DateTime.Now.ToString("yyyy-MM-dd-hhmmss")));
    }

    private static void WriteLog(string msg, LOG_TYPE type, bool _showInConsole = false)
    {
        try
        {
            if (m_fileStream == null)
            {
                InitLogFile();
            }

            switch (type)
            {
                case LOG_TYPE.DEGUG_LOG:
#if UNITY_EDITOR
                    Debug.Log(msg);
#endif
                    break;
                case LOG_TYPE.ERROR_LOG:
                    Debug.LogError(msg);
                    break;
                case LOG_TYPE.WARNING_LOG:
                    Debug.LogWarning(msg);
                    break;
            }

            m_fileStream.WriteLine(msg);
            m_fileStream.Flush();
        }
        catch (Exception e)
        {
            Debug.LogError("[Log] Exception occurred: " + e.GetType().Name + ", Message: " + e.Message);
        }
    }

    public static void ErrorLog(string fort, params object[] areges)
    {
        if (!PlatformHelper.IsEnableDebugMode()) return;
        if (areges.Length > 0)
        {
            string msg = string.Format(fort, areges);
            WriteLog(msg, LOG_TYPE.ERROR_LOG, true);
        }
        else
        {
            WriteLog(fort, LOG_TYPE.ERROR_LOG, true);
        }

    }
    public static void WarningLog(string fort, params object[] areges)
    {
        if (!PlatformHelper.IsEnableDebugMode()) return;
        if (areges.Length > 0)
        {
            string msg = string.Format(fort, areges);
            WriteLog(msg, LOG_TYPE.WARNING_LOG, true);
        }
        else
        {
            WriteLog(fort, LOG_TYPE.WARNING_LOG, true);
        }
    }
    public static void DebugLog(string fort, params object[] areges)
    {
        if (!PlatformHelper.IsEnableDebugMode()) return;
        if (areges.Length > 0)
        {
            string msg = string.Format(fort, areges);
            WriteLog(msg, LOG_TYPE.DEGUG_LOG, true);
        }
        else
        {
            WriteLog(fort, LOG_TYPE.DEGUG_LOG, true);
        }
    }

    private static void ErrorLog(string msg)
    {
        WriteLog(msg, LOG_TYPE.ERROR_LOG);
    }

    private static void WarningLog(string msg)
    {
        WriteLog(msg, LOG_TYPE.WARNING_LOG);
    }

    public static void DebugLog(string msg)
    {
        if (!PlatformHelper.IsEnableDebugMode()) return;
        WriteLog(msg, LOG_TYPE.DEGUG_LOG);
    }

    public static void Log(string logString, string stackTrace, LogType type)
    {
        if (!PlatformHelper.IsEnableDebugMode()) return;
        switch (type)
        {
            case LogType.Log:
                DebugLog(logString);
                break;
            case LogType.Warning:
                WarningLog(logString);
                break;
            case LogType.Error:
                ErrorLog(logString);
                break;
        }
    }

    public static void Assert(bool condition, string msg)
    {
        Debug.Assert(condition, msg);
    }
}
