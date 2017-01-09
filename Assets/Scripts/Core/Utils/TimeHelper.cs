//----------------------------------------------
//	CreateTime  : 1/9/2017 5:40:22 PM
//	Author      : Taylor Liang
//	Project     : RC
//	Company     : CYOU
//	Instruction : TimeHelper
//	ChangeLog   : None
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TimeHelper : Mono_Singleton<TimeHelper>
{
    #region ClockTime

    public struct ClockTime
    {
        public ClockTime(int l_hour, int l_minute, int l_second)
        {
            hour = l_hour;
            minute = l_minute;
            second = l_second;
        }

        public static ClockTime zero = new ClockTime(0, 0, 0);

        public int hour;
        public int minute;
        public int second;

        public static ClockTime Parse(string text)
        {
            try
            {
                var splited = text.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries).Select(item => int.Parse(item)).ToList();
                if (splited.Count == 0) throw new Exception("ClockTime length is 0, transfer fail.");
                if (splited.Count == 1) return new ClockTime(0, 0, splited[0]);
                if (splited.Count == 2) return new ClockTime(0, splited[1], splited[0]);
                if (splited.Count == 3) return new ClockTime(splited[2], splited[1], splited[0]);
                throw new Exception("ClockTime length larger than 3, transfer fail.");
            }
            catch (Exception e)
            {
                Debug.LogError("Transfer ClockTime fail, contact LiangXiao if you donot know how to solve this, exception:" + e.Source + "\nstackTrace:" + e.StackTrace);
                return zero;
            }
        }

        public override string ToString()
        {
            string hourStr = hour.ToString();
            while (hourStr.Length < 2)
            {
                hourStr = "0" + hourStr;
            }

            string minuteStr = minute.ToString();
            while (minuteStr.Length < 2)
            {
                minuteStr = "0" + minuteStr;
            }

            string secondStr = second.ToString();
            while (secondStr.Length < 2)
            {
                secondStr = "0" + secondStr;
            }

            if (hour == 0)
            {
                return minuteStr + ":" + secondStr;
            }
            else
            {
                return hourStr + ":" + minuteStr + ":" + secondStr;
            }
        }
    }

    public static ClockTime SecondToClockTime(int second)
    {
        if (second < 0)
        {
            return ClockTime.zero;
        }

        return new ClockTime(second / 3600, (second % 3600) / 60, second % 60);
    }

    public static int ClockTimeToSecond(ClockTime clockTime)
    {
        return clockTime.hour * 3600 + clockTime.minute * 60 + clockTime.second;
    }

    #endregion

    #region Time Calculate Module

    private class TimeCalc
    {
        public string key;

        public bool IsOverTime;
        public float StartTime;
        public float OverTimeDuration;
        /// <summary>
        /// 1: one deleagte, 2:per/s delegate, 3:per/frame delegate
        /// </summary>
        public int DelegateMode;
        public DelegateHelper.VoidDelegate m_TimeCalcVoidDelegate;
        public DelegateHelper.StringDelegate m_TimeCalcStringDelegate;
        public DelegateHelper.IntDelegate m_TimeCalcIntDelegate;
        public DelegateHelper.FloatDelegate m_TimeCalcFloatDelegate;

        public static bool ContainsKey(string key)
        {
            return m_timeCalcList.Any(item => item.key == key);
        }

        public static TimeCalc GetTimeCalc(string key)
        {
            if (!ContainsKey(key)) return null;

            return m_timeCalcList.Where(item => item.key == key).FirstOrDefault();
        }

        public static List<TimeCalc> m_timeCalcList = new List<TimeCalc>();
    }

    /// <summary>
    /// Add time calc dictionary item, delegate execute when time calc ends.
    /// </summary>
    /// <param name="key">item key</param>
    /// <param name="duration">time over duration</param>
    /// <param name="l_voidDelegate">delegate</param>
    /// <returns>is add succeed</returns>
    public bool AddOneDelegateToTimeCalc(string key, float duration, DelegateHelper.VoidDelegate l_voidDelegate = null)
    {
        if (TimeCalc.ContainsKey(key))
        {
            Debug.LogError("Cannot add key:" + key + " to time calc cause key already exist.");
            return false;
        }

        TimeCalc.m_timeCalcList.Add(new TimeCalc() { key = key, IsOverTime = true, StartTime = -1, OverTimeDuration = duration, m_TimeCalcVoidDelegate = l_voidDelegate, DelegateMode = 1 });

        StartCalc(key);
        return true;
    }

    /// <summary>
    /// Add time calc dictionary item, delegate execute when time calc ends.
    /// </summary>
    /// <param name="key">item key</param>
    /// <param name="duration">time over duration</param>
    /// <param name="l_timeCalcVoidDelegate">delegate</param>
    /// <returns>is add succeed</returns>
    public bool AddOneDelegateToTimeCalc(string key, float duration, DelegateHelper.StringDelegate l_stringDelegate = null)
    {
        if (TimeCalc.ContainsKey(key))
        {
            Debug.LogError("Cannot add key:" + key + " to time calc cause key already exist.");
            return false;
        }

        TimeCalc.m_timeCalcList.Add(new TimeCalc() { key = key, IsOverTime = true, StartTime = -1, OverTimeDuration = duration, m_TimeCalcStringDelegate = l_stringDelegate, DelegateMode = 2 });

        StartCalc(key);
        return true;
    }

    /// <summary>
    /// Add time calc dictionary item, delegate execute every second.
    /// </summary>
    /// <param name="key">item key</param>
    /// <param name="duration">time over duration</param>
    /// <param name="l_intDelegate">delegate</param>
    /// <returns>is add succeed</returns>
    public bool AddEveryDelegateToTimeCalc(string key, float duration, DelegateHelper.IntDelegate l_intDelegate = null)
    {
        if (TimeCalc.ContainsKey(key))
        {
            Debug.LogError("Cannot add key:" + key + " to time calc cause key already exist.");
            return false;
        }

        TimeCalc.m_timeCalcList.Add(new TimeCalc() { key = key, IsOverTime = true, StartTime = -1, OverTimeDuration = duration, m_TimeCalcIntDelegate = l_intDelegate, DelegateMode = 3 });

        StartCalc(key);
        return true;
    }

    /// <summary>
    /// Add time calc dictionary item, delegate execute every frame.
    /// </summary>
    /// <param name="key">item key</param>
    /// <param name="duration">time over duration</param>
    /// <param name="l_timeCalcIntDelegate">delegate</param>
    /// <returns>is add succeed</returns>
    public bool AddFrameDelegateToTimeCalc(string key, float duration, DelegateHelper.FloatDelegate l_floatDelegate = null)
    {
        if (TimeCalc.ContainsKey(key))
        {
            Debug.LogError("Cannot add key:" + key + " to time calc cause key already exist.");
            return false;
        }

        TimeCalc.m_timeCalcList.Add(new TimeCalc() { key = key, IsOverTime = true, StartTime = -1, OverTimeDuration = duration, m_TimeCalcFloatDelegate = l_floatDelegate, DelegateMode = 4 });

        StartCalc(key);
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool RemoveFromTimeCalcWhenDisable(string key)
    {
        if (Main.Instance.gameObject != null)
        {
            return TimeHelper.Instance.RemoveFromTimeCalc(key);
        }
        else
        {
            Debug.LogWarning("Cancel remove time calc key:" + key + " cause singleton has been destroyed.");
            return false;
        }
    }

    /// <summary>
    /// Remove time calc dictionary item.
    /// </summary>
    /// <param name="key">item key</param>
    /// <returns>is remove succeed</returns>
    public bool RemoveFromTimeCalc(string key)
    {
        StopCalc(key);

        if (!TimeCalc.ContainsKey(key))
        {
            Debug.LogError("Cannot remove key:" + key + " to time calc cause key not exist.");
            return false;
        }

        TimeCalc.m_timeCalcList.Remove(TimeCalc.GetTimeCalc(key));
        return true;
    }

    /// <summary>
    /// IsTimeCalcKeyExist
    /// </summary>
    /// <param name="key">item key</param>
    /// <returns>is key exist</returns>
    public bool IsTimeCalcKeyExist(string key)
    {
        return TimeCalc.ContainsKey(key);
    }

    /// <summary>
    /// Start time calc
    /// </summary>
    /// <param name="key">item key</param>
    private void StartCalc(string key)
    {
        if (!TimeCalc.ContainsKey(key))
        {
            Debug.LogError("key:" + key + " not exist.");
            return;
        }

        TimeCalc.GetTimeCalc(key).IsOverTime = false;
        TimeCalc.GetTimeCalc(key).StartTime = Time.realtimeSinceStartup;
    }

    /// <summary>
    /// Stop time calc
    /// </summary>
    /// <param name="key">item key</param>
    private void StopCalc(string key)
    {
        if (!TimeCalc.ContainsKey(key))
        {
            Debug.LogError("key:" + key + " not exist.");
            return;
        }

        TimeCalc.GetTimeCalc(key).IsOverTime = true;
        TimeCalc.GetTimeCalc(key).StartTime = -1;
    }

    /// <summary>
    /// Is over time
    /// </summary>
    /// <param name="key">item key</param>
    /// <returns>is over</returns>
    public bool IsCalcTimeOver(string key)
    {
        if (!TimeCalc.ContainsKey(key))
        {
            Debug.LogError("key:" + key + " not exist.");
            return false;
        }

        if (TimeCalc.GetTimeCalc(key).StartTime < 0)
        {
            Debug.LogError("Time calc key:" + key + " stopped or never start.");
            return false;
        }

        return TimeCalc.GetTimeCalc(key).IsOverTime;
    }

    public float GetCalcTime(string key)
    {
        if (!TimeCalc.ContainsKey(key))
        {
            Debug.LogError("key:" + key + " not exist.");
            return -1;
        }

        if (TimeCalc.GetTimeCalc(key).StartTime < 0)
        {
            Debug.LogError("Time calc key:" + key + " stopped or never start.");
            return -1;
        }

        return Time.realtimeSinceStartup - TimeCalc.GetTimeCalc(key).StartTime;
    }

    private float TimeCalcLastTime;

    // Update is called once per frame
    void Update()
    {
        //One delegate.
        var tempList = TimeCalc.m_timeCalcList.Where(item => (item.DelegateMode == 1) && (item.StartTime >= 0) && (!item.IsOverTime)).ToList();
        for (int i = 0; i < tempList.Count; i++)
        {
            if (Time.realtimeSinceStartup - tempList[i].StartTime > tempList[i].OverTimeDuration)
            {
                tempList[i].IsOverTime = true;

                if (tempList[i].m_TimeCalcVoidDelegate != null)
                {
                    tempList[i].m_TimeCalcVoidDelegate();
                }
            }
        }

        //One Key string delegate.
        var tempList2 = TimeCalc.m_timeCalcList.Where(item => (item.DelegateMode == 2) && (item.StartTime >= 0) && (!item.IsOverTime)).ToList();
        for (int i = 0; i < tempList2.Count; i++)
        {
            if (Time.realtimeSinceStartup - tempList2[i].StartTime > tempList2[i].OverTimeDuration)
            {
                tempList2[i].IsOverTime = true;

                if (tempList2[i].m_TimeCalcStringDelegate != null)
                {
                    tempList2[i].m_TimeCalcStringDelegate(tempList2[i].key);
                }
            }
        }

        //Per/s delegate.
        if (Time.realtimeSinceStartup - TimeCalcLastTime > 1.0f)
        {
            var tempList3 = TimeCalc.m_timeCalcList.Where(item => (item.DelegateMode == 3) && (item.StartTime >= 0) && (!item.IsOverTime)).ToList();

            for (int i = 0; i < tempList3.Count; i++)
            {
                if (Time.realtimeSinceStartup - tempList3[i].StartTime > tempList3[i].OverTimeDuration)
                {
                    tempList3[i].IsOverTime = true;
                }

                if (tempList3[i].m_TimeCalcIntDelegate != null)
                {
                    tempList3[i].m_TimeCalcIntDelegate((int)GetCalcTime(tempList3[i].key));
                }
            }

            TimeCalcLastTime = Time.realtimeSinceStartup;
        }

        //Per/frame delegate.
        var tempList4 = TimeCalc.m_timeCalcList.Where(item => (item.DelegateMode == 4) && (item.StartTime >= 0) && (!item.IsOverTime)).ToList();

        for (int i = 0; i < tempList4.Count; i++)
        {
            if (Time.realtimeSinceStartup - tempList4[i].StartTime > tempList4[i].OverTimeDuration)
            {
                tempList4[i].IsOverTime = true;
            }

            if (tempList4[i].m_TimeCalcFloatDelegate != null)
            {
                tempList4[i].m_TimeCalcFloatDelegate(GetCalcTime(tempList4[i].key));
            }
        }
    }

    #endregion
}