using UnityEngine;

public class Mono_Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool m_ApplicationIsQuitting
    {
        get { return m_applicationIsQuitting; }
    }

    private static bool m_applicationIsQuitting;

    private static readonly object temp = new object();



    private static T m_instance;

    public static T GetInstanceWithOutCreate()
    {
        return m_instance;
    }

    /// <summary>
    /// get the instance of this type monobehaviour, will create a component attached to DontDestroyOnLoad if instance is null
    /// </summary>
    public static T Instance
    {
        get
        {
            var editMode = Application.isEditor && !Application.isPlaying;
            if (!editMode && m_ApplicationIsQuitting)
            {
                LogModule.WarningLog("[Singleton] Instance:" + typeof(T) +
                    " already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            if (m_instance == null)
            {
                //lock to assure only instance once.
                lock (temp)
                {
                    //another thread may wait outside the lock while this thread goes into lock and instanced singleton, so check null again when enter into lock.
                    if (m_instance == null)
                    {
                        var tempList = FindObjectsOfType(typeof(T));
                        if (tempList != null && tempList.Length > 0)
                        {
                            if (tempList.Length > 1)
                            {
                                LogModule.ErrorLog("[Singleton] Something went really wrong " +
                                    " - there should never be more than 1 singleton!" +
                                    " Reopenning the scene might fix it.");
                            }
                            m_instance = (T)tempList[0];
                        }

                        if (m_instance == null)
                        {
							
							GameObject main = GameObject.Find ("Main");
							if (main == null) {
								main = new GameObject ("Main");
							}

							m_instance = main.AddComponent<T>();

                            LogModule.DebugLog("[Singleton]Add singleton:" + typeof(T) + " in DontDestroyOnLoad.");
                        }
                        else
                        {
                            LogModule.DebugLog("[Singleton] Using instance already created:" + typeof(T));
                        }
                    }
                }
            }

            return m_instance;
        }
    }

    public void OnDestroy()
    {
        m_instance = null;
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    /// it will create a buggy ghost object that will stay on the Editor scene
    /// even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// 
    /// </summary>
    /// 
    /// Instance will be destroyed manually in our game.
    public void OnApplicationQuit()
    {
        m_applicationIsQuitting = true;
        m_instance = null;
        Destroy(gameObject);
    }

	public void Awake(){
		GameObject main = GameObject.Find ("Main");
		if (main != null) {
			if (main != this.gameObject) {
				Debug.LogError ("Mono_Singleton Gameobject has Created!");
			}
		} else {
			gameObject.name = "Main";
		}

	}
}