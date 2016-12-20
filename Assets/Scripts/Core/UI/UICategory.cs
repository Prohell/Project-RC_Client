using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UICategory
{
    public string Name;

    public int RemainingDepth
    {
        get { return m_allocateList.Count(item => item.Flag == false); }
    }

    public GameObject RootObject;
    public List<UIItem> UIItemList = new List<UIItem>();

    private List<Allocate> m_allocateList = new List<Allocate>();

    public struct Allocate
    {
        public string Key;
        public bool Flag;
    }

    public struct ToShowConfig
    {
        public UIItemConfig ItemConfig;
        public DelegateUtil.TableDelegate ViewDelegate;
        public DelegateUtil.TableDelegate RefreshDelegate;
        public bool IsShowAnim;
    }

    private List<ToShowConfig> m_toShowList = new List<ToShowConfig>();
    private List<UIItem> m_toRemoveList = new List<UIItem>();

    public UICategoryConfig m_Config = new UICategoryConfig();

    private int m_configDepthLength
    {
        get { return m_Config.EndDepth - m_Config.StartDepth + 1; }
    }

    public void Init()
    {
        m_allocateList = Enumerable.Repeat(new Allocate() { Key = "", Flag = false }, m_configDepthLength).ToList();
    }

    /// <summary>
    /// Open or store to to show list.
    /// </summary>
    public bool CanOpenUI
    {
        get
        {
            switch (m_Config.MultiPolicy)
            {
                case "Single":
                    {
                        //Open a new ui will close a old one in Single policy.
                        return true;
                    }
                case "Overlay":
                    {
                        return UIItemList.Count(item => item.IsShowing) < m_Config.ShowingMax;
                    }
                default:
                    {
                        throw new Exception(string.Format("MultiPolicy: {0} not defined.", m_Config.MultiPolicy));
                    }
            }
        }
    }

    public void CheckOpenDepth(string key)
    {
        switch (m_Config.MultiPolicy)
        {
            case "Single":
                {
                    //Open a new ui will close a old one in Single policy.
                    return;
                }
            case "Overlay":
                {
                    int targetIndex = m_allocateList.FindIndex(item => item.Key == key);
                    var list = m_allocateList.Where(
                            item => item.Flag && item.Key != key && m_allocateList.IndexOf(item) > targetIndex).ToList();
                    foreach (var item in list)
                    {
                        if (UIManager.GetInstance().GetItem(item.Key) == null)
                        {
                            Debug.LogError("!!!");
                        }
                    }

                    if (m_allocateList.Any(item => item.Flag && item.Key != key && m_allocateList.IndexOf(item) > targetIndex && UIManager.GetInstance().GetItem(item.Key).IsShowing))
                    {
                        MoveDepthToTop(key);
                    }
                    break;
                }
            default:
                {
                    throw new Exception(string.Format("MultiPolicy: {0} not defined.", m_Config.MultiPolicy));
                }
        }
    }

    private void MoveDepthToTop(string targetKey)
    {
        int allocateNum = m_allocateList.Count(item => item.Key == targetKey);

        if (m_allocateList.Skip(m_configDepthLength - allocateNum).Any(item => item.Flag))
        {
            RearrangeDepth();

            if (m_allocateList.Skip(m_configDepthLength - allocateNum).Any(item => item.Flag))
            {
                LogModule.ErrorLog("Cannot move item index: {0} cause no remaining depth", targetKey);
                return;
            }
        }

        int newIndex = m_allocateList.FindLastIndex(item => item.Flag) + 1;
        int oldIndex = m_allocateList.FindIndex(item => item.Key == targetKey);
        for (int i = 0; i < allocateNum; i++)
        {
            Utils.Swap(m_allocateList, oldIndex + i, newIndex + i);
        }
        UIManager.GetInstance().MoveUIItemDepth(targetKey, m_Config.StartDepth + newIndex);
    }

    public int GetNewItemDepth()
    {
        if (RemainingDepth < 1)
        {
            return -1;
        }

        //Not knowing allocateNum when getting new item depth.
        if (m_allocateList.Last().Flag)
        {
            RearrangeDepth();

            if (m_allocateList.Last().Flag)
            {
                return -1;
            }
        }

        int newItemIndex = m_allocateList.FindLastIndex(item => item.Flag) + 1;
        return newItemIndex + m_Config.StartDepth;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uiItem"></param>
    /// <returns>depth to add ui item</returns>
    public int AddItem(UIItem uiItem, int allocateNum)
    {
        if (m_allocateList.Any(item => item.Key == uiItem.Name))
        {
            LogModule.ErrorLog("Cannot add item: {0} cause same key exist.", uiItem);
            return -1;
        }

        if (RemainingDepth < allocateNum)
        {
            LogModule.ErrorLog("Cannot add item: {0} cause {1} RemainingDepth < {2} AllocateDepth", uiItem.Name, RemainingDepth, allocateNum);
            return -1;
        }

        if (m_allocateList.Skip(m_configDepthLength - allocateNum).Any(item => item.Flag))
        {
            RearrangeDepth();

            if (m_allocateList.Skip(m_configDepthLength - allocateNum).Any(item => item.Flag))
            {
                LogModule.ErrorLog("Cannot add item: {0} cause no remaining depth", uiItem);
                return -1;
            }
        }

        UIItemList.Add(uiItem);

        int newItemIndex = m_allocateList.FindLastIndex(item => item.Flag) + 1;
        for (int i = newItemIndex; i < newItemIndex + allocateNum; i++)
        {
            m_allocateList[i] = new Allocate()
            {
                Key = uiItem.Name,
                Flag = true
            };
        }

        return newItemIndex + m_Config.StartDepth;
    }

    public bool RemoveItem(UIItem uiItem)
    {
        if (!m_allocateList.Any(item => item.Key == uiItem.Name))
        {
            LogModule.ErrorLog("Cannot remove item: {0} cause not exist.", uiItem);
            return false;
        }

        UIItemList.Remove(uiItem);
        if (m_toRemoveList.Contains(uiItem))
        {
            m_toRemoveList.Remove(uiItem);
        }

        for (int i = 0; i < m_allocateList.Count; i++)
        {
            if (m_allocateList[i].Key == uiItem.Name)
            {
                m_allocateList[i] = new Allocate() { Key = "", Flag = false };
            }
        }

        return true;
    }

    public void AddToToShowList(ToShowConfig toShowConfig)
    {
        m_toShowList.Add(toShowConfig);
    }

    private void RearrangeDepth()
    {
        int firstEmptyIndex = m_allocateList.FindIndex(item => item.Flag == false);

        if (firstEmptyIndex >= 0)
        {
            List<Allocate> moved = new List<Allocate>();

            for (int i = firstEmptyIndex; i < m_configDepthLength; i++)
            {
                if (m_allocateList[i].Flag)
                {
                    Utils.Swap(m_allocateList, firstEmptyIndex, i);
                    if (!moved.Contains(m_allocateList[firstEmptyIndex]))
                    {
                        UIManager.GetInstance().MoveUIItemDepth(m_allocateList[firstEmptyIndex].Key, m_Config.StartDepth + firstEmptyIndex);
                    }
                    moved.Add(m_allocateList[firstEmptyIndex]);

                    firstEmptyIndex = m_allocateList.FindIndex(item => item.Flag == false);
                }
            }
        }
    }

    private readonly object lockObject = new object();

    private void TryReleaseCache()
    {
        if (m_toRemoveList.Count > m_Config.CacheNum)
        {
            lock (lockObject)
            {
                try
                {
                    int remainingRemoveNum = m_toRemoveList.Count - m_Config.CacheNum;

                    if (remainingRemoveNum > 0 && m_toRemoveList.Any())
                    {
                        for (int i = 0; i < remainingRemoveNum; i++)
                        {
                            UIManager.GetInstance().DestroyCache(m_toRemoveList[i].Name);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error in release cached item, category: {0}, {1}\n{2}", Name, e.Message, e.StackTrace));
                }
            }
        }
    }

    private readonly object lockObject2 = new object();

    private void TryShowItem()
    {
        //Show item exist in toShowList
        if (m_toShowList.Any())
        {
            lock (lockObject2)
            {
                try
                {
                    int remainingShowNum = m_Config.ShowingMax - UIItemList.Count(item => item.IsShowing);

                    if (remainingShowNum > 0 && m_toShowList.Any())
                    {
                        for (int i = 0; i < remainingShowNum; i++)
                        {
                            UIManager.GetInstance().OpenUIInternal(m_toShowList[i].ItemConfig, m_toShowList[i].ViewDelegate, m_toShowList[i].RefreshDelegate, m_toShowList[i].IsShowAnim);
                            m_toShowList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error in Show cached item, category: {0}, {1}\n{2}", Name, e.Message, e.StackTrace));
                }
            }
        }
    }

    public void OnUIOpen(UIItem item)
    {
        if (m_toRemoveList.Contains(item))
        {
            m_toRemoveList.Remove(item);
        }
    }

    public void OnUIClose(UIItem item)
    {
        if (!m_toRemoveList.Contains(item))
        {
            m_toRemoveList.Add(item);
        }

        TryShowItem();
        TryReleaseCache();
    }
}
