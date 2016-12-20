using UnityEngine;
/// <summary>
/// FPS
/// by TT
/// 2016-07-04
/// </summary>
public class FPSCounter : MonoBehaviour
{
    const float fpsMeasurePeriod = 0.5f;
    private int m_FpsAccumulator = 0;
    private float m_FpsNextPeriod = 0;
    private int m_CurrentFps;
    const string display = "{0} FPS";


    private void Start()
    {
        m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }

    void Update()
    {
        m_FpsAccumulator++;
        if (Time.realtimeSinceStartup > m_FpsNextPeriod)
        {
            m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
            m_FpsAccumulator = 0;
            m_FpsNextPeriod += fpsMeasurePeriod;
        }
    }

    void OnGUI()
    {
        // measure average frames per second

        GUI.Label(new Rect(Screen.width - 160, Screen.height - 50, 120, 64), string.Format(display, m_CurrentFps));
    }
}
