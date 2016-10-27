using UnityEngine;
using System.Collections;

public class DelayTask : Task
{
    IEnumerator _delayedTask;
    float _delay;

    public DelayTask(IEnumerator delayedTask, float delay)
    {
        _delayedTask = delayedTask;
        _delay = delay;
    }

    public override IEnumerator Exec()
    {
        yield return Task.Wait(_delay);
        yield return _delayedTask;
    }
}
