using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiTask : Task
{
    List<Task> subtasks = new List<Task>();
    List<IEnumerator> enumerators = new List<IEnumerator>();

    public void AddSubTask(Task subtask)
    {
        subtasks.Add(subtask);
    }

    public void AddSubTask(IEnumerator subtask)
    {
        enumerators.Add(subtask);
    }

    public void AddSubTask(Task subtask, float delay)
    {
        subtasks.Add(new DelayTask(subtask.Exec(), delay));
    }

    public void AddSubTask(IEnumerator subtask, float delay)
    {
        enumerators.Add(new DelayTask(subtask, delay).Exec());
    }

    public override IEnumerator Exec()
    {
        foreach (Task task in subtasks)
        {
            enumerators.Add(task.Exec());
        }
        IEnumerator[] eArray = enumerators.ToArray();
        enumerators.Clear();
        foreach (IEnumerator e in eArray)
        {
            enumerators.Add(Task.Recursion(e));
        }
        while (enumerators.Count > 0)
        {
            IEnumerator[] taskArray = enumerators.ToArray();
            foreach (IEnumerator enumerator in taskArray)
            {
                if (!enumerator.MoveNext())
                {
                    if (typeof(YieldInstruction).IsInstanceOfType(enumerator.Current))
                    {
                        Debug.LogError("Subtype of YieldInsturction is not supported in MultiTask!");
                    }
                    enumerators.Remove(enumerator);
                }
            }
            yield return null;
        }
        yield break;
    }
}
