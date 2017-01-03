using UnityEngine;
using System.Collections.Generic;

public class UICategoryConfigFile : ScriptableObject
{
    public List<UICategoryConfig> UICategoryList = new List<UICategoryConfig>()
    {
        new UICategoryConfig()
        {
            Name = "FullScreen",
            StartDepth = 1,
            EndDepth = 10,
            MultiPolicy = "Overlay",
            CacheNum = 1,
            ShowingMax = 999
        },
        new UICategoryConfig()
        {
            Name = "Window",
            StartDepth = 11,
            EndDepth = 20,
            MultiPolicy = "Single",
            CacheNum = 3,
            ShowingMax = 1
        },
        new UICategoryConfig()
        {
            Name = "Popup",
            StartDepth = 21,
            EndDepth = 30,
            MultiPolicy = "Overlay",
            CacheNum = 3,
            ShowingMax = 3
        }
    };
}
