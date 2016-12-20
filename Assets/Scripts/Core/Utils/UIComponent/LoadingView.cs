using UnityEngine;
using System.Collections;

public class LoadingView : MonoBehaviour
{
    public UIProgressBar Bar;
    public UILabel InfoLabel;
    public UITexture Background;

    public void Set(float progress, string text = null)
    {
        Bar.value = progress;
        if (!string.IsNullOrEmpty(text))
        {
            InfoLabel.text = text;
        }
    }
}
