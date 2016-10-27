using UnityEngine;
using System.Collections;

public static class MonoBehaviourExtension
{

    public static void RecCoroutine(this MonoBehaviour mono, IEnumerator cor)
    {
        mono.StartCoroutine(Task.Recursion(cor));
    }

    public static void SyncCoroutine(this MonoBehaviour mono, IEnumerator cor)
    {

        while (cor.MoveNext())
        {
            if (cor.Current != null && typeof(IEnumerator).IsInstanceOfType(cor.Current))
            {
                IEnumerator sub = cor.Current as IEnumerator;
                IEnumerator recSub = Task.Recursion(sub);
                while (recSub.MoveNext())
                {
                    //yield return recSub.Current;
                }
            }
            //yield return main.Current;
        }
    }
}
