using System;
using UnityEngine;
using DG.Tweening;

public class UIViewBase : MonoBehaviour
{
    public string Name;

    public static float AnimMin = 0;
    public static float AnimMax = 1;
    public static float Duration = .5f;

    private Transform m_transform;

    public Action OnOpenUIComplete;
    public Action OnCloseUIComplete;

    public void ViewBase_OnOpenUIComplete()
    {
        m_transform.localScale = Vector3.one * AnimMax;

        if (OnOpenUIComplete != null)
        {
            OnOpenUIComplete();
        }
    }

    public void ViewBase_OnCloseUIComplete()
    {
        m_transform.localScale = Vector3.one * AnimMin;

        if (OnCloseUIComplete != null)
        {
            OnCloseUIComplete();
        }
    }

    public void OnOpen()
    {
        if (gameObject.activeInHierarchy)
        {
            m_transform.localScale = Vector3.one * AnimMin;

            m_transform.DOScale(Vector3.one, Duration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                ViewBase_OnOpenUIComplete();
            });
        }
    }

    public void OnClose()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.localScale = Vector3.one * AnimMax;

            m_transform.DOScale(Vector3.zero, Duration).SetEase(Ease.InBack).OnComplete(() =>
            {
                ViewBase_OnCloseUIComplete();
            });
        }
    }

    void OnEnable()
    {
        UIManager.GetInstance().ReportState(Name, "OnEnable");
    }

    void OnDisable()
    {
        UIManager.GetInstance().ReportState(Name, "OnDisable");
    }

    void OnDestroy()
    {
        UIManager.GetInstance().ReportState(Name, "OnDestroy");
    }

    void Start()
    {

    }

    void Awake()
    {
        m_transform = transform;
    }
}
