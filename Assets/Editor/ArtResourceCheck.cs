using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ArtResourceCheck :Editor {
    [MenuItem("Art Tools/Check")]
    static void delete()
    {
        GameObject prefab = Selection.activeGameObject;

        if (null == prefab) return;
        //删除空的Animation组件
        Animation[] animations = prefab.GetComponentsInChildren<Animation>(true);
        foreach (Animation animation in animations)
        {
            if (animation.clip == null)
            {
                GameObject.DestroyImmediate(animation, true);
            }
        }
        //删除Animator组件
        Animator[] animators = prefab.GetComponentsInChildren<Animator>(true);
        foreach (Animator animatior in animators)
        {
            GameObject.DestroyImmediate(animatior, true);
        }

        //删除missing的脚本组件
        MonoBehaviour[] monoBehaviours = prefab.GetComponentsInChildren<MonoBehaviour>(true);
        foreach (MonoBehaviour monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour == null)
            {
                Debug.Log("有个missing的脚本");
                SelectMissing();
                //GameObject.DestroyImmediate(monoBehaviour,true);
            }
            else
            {
                Debug.Log("包含脚本的物体"+ monoBehaviour.ToString());
            }
        }
    }
    [MenuItem("Art Tools/SelectMissing")]
    static void SelectMissing()
    {
        Transform[] ts = FindObjectsOfType<Transform>();
        List<GameObject> selection = new List<GameObject>();
        foreach (Transform t in ts)
        {
            Component[] cs = t.gameObject.GetComponents<Component>();
            foreach (Component c in cs)
            {
                if (c == null)
                {
                    selection.Add(t.gameObject);
                }
            }
        }
        Selection.objects = selection.ToArray();
    }
}
