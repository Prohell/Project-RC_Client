using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {

    private float m_LastUpdateShowTime = 0f; 

    private float m_UpdateShowDeltaTime = 0.1f;

    private int m_FrameUpdate = 0; 

    private float m_FPS = 0;

    void Awake()
    {
        Application.targetFrameRate = 100;
    }

    // Use this for initialization  
    void Start()
    {
        m_LastUpdateShowTime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame  
    void Update()
    {
        m_FrameUpdate++;
        if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
        {
            m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
            m_FrameUpdate = 0;
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
        }
    }

    void OnGUI()
    {
        if (m_FPS >= 100)
        {
            GUI.color = Color.green;
        }else if(m_FPS < 100&& m_FPS>=60)
        {
            GUI.color = Color.yellow;
        }else if(m_FPS <= 60)
        {
            GUI.color = Color.red;
        }
        GUI.Label(new Rect(Screen.width / 2, 20, 100, 100), "FPS: " + m_FPS);
    }
}
