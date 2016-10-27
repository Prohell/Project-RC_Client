using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Task will exec as a coroutine
/// </summary>

public abstract class Task
{
    public static IEnumerator Append(IEnumerator main, Action callback)
    {
        yield return main;
        if (null != callback)
        {
            callback.Invoke();
        }
        yield break;
    }

    public static IEnumerator Recursion(IEnumerator main)
    {
        bool gotError = false;
        CorError err = null;
        CorResult result = null;

        bool gotException = false;
        while (!gotError && TryMoveEnumerator(main, out gotException))
        {
            object rsl = main.Current;
            if (rsl is CorError)
            {
                gotError = true;
                err = rsl as CorError;
            }
            else if (rsl is CorResult)
            {
                CorResult r = (rsl as CorResult);
                r.Merge(result);
                result = r;
                yield return null;
            }
            else if (typeof(IEnumerator).IsInstanceOfType(rsl))
            {
                IEnumerator sub = Recursion(rsl as IEnumerator);
                while (!gotError && TryMoveEnumerator(sub, out gotException))
                {
                    object rsl_sub = sub.Current;
                    if (rsl_sub is CorError)
                    {
                        gotError = true;
                        err = rsl_sub as CorError;
                    }
                    else if (rsl_sub is CorResult)
                    {
                        CorResult r = (rsl_sub as CorResult);
                        r.Merge(result);
                        result = r;
                        yield return null;
                    }
                    else
                    {
                        yield return rsl_sub;
                    }
                }
            }
            else
            {
                yield return rsl;
            }
        }

        if (gotException)
        {
            yield return new CorExceptionError();
        }
        else if (gotError)
        {
            Debug.LogError("error in task routine : " + err.errCode);
            Debug.LogError(err.description);
            yield return err;
        }
        else
        {
            yield return result;
        }
    }

    private static bool TryMoveEnumerator(IEnumerator iter, out bool gotException)
    {
        bool ret = false;
        try
        {
            ret = iter.MoveNext();
            gotException = false;
            return ret;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            gotException = true;
            return false;
        }
    }

    public static IEnumerator Wait(float seconds)
    {
        float end = UnityEngine.Time.time + seconds;
        while (UnityEngine.Time.time < end)
        {
            yield return null;
        }
        yield break;
    }

    public static IEnumerator WaitForFrames(int frames)
    {
        for (int i = 0; i < frames; ++i)
        {
            yield return null;
        }
        yield break;
    }

    public abstract IEnumerator Exec();


}
