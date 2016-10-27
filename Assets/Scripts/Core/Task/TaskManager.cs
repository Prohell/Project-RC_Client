using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour, IReset
{
    public void Exec(Task task)
    {
        StartCoroutine(Task.Recursion(task.Exec()));
    }

    public void Exec(IEnumerator cor)
    {
        StartCoroutine(Task.Recursion(cor));
    }

    public void Exec(Task task, Action callback)
    {
        StartCoroutine(Task.Recursion(Task.Append(task.Exec(), callback)));
    }

    public void Exec(IEnumerator cor, Action callback)
    {
        StartCoroutine(Task.Recursion(Task.Append(cor, callback)));
    }

    List<Action> _mainThreadTasks = new List<Action>();
    object _tasksLock = new object();

    public void AddMainThreadTasks(Action task)
    {
        lock (_tasksLock)
        {
            _mainThreadTasks.Add(task);
        }
    }

    void Update()
    {
        while (_mainThreadTasks.Count > 0)
        {
            lock (_tasksLock)
            {
                Action task = _mainThreadTasks[0];
                _mainThreadTasks.RemoveAt(0);
                task.Invoke();
            }
        }
    }

    public void OnReset()
    {
        lock (_tasksLock)
        {
            _mainThreadTasks.Clear();
        }
        StopAllCoroutines();
    }
}