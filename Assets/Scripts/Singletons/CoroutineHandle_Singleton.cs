using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandle_Singleton : SingletonMonoBehaviour<CoroutineHandle_Singleton>
{
    public void RunCoroutine<T>(Func<T, IEnumerator> coroutineFunc, T arg)
    {
        StartCoroutine(Coroutine(coroutineFunc, arg));
    }

    private IEnumerator Coroutine<T>(Func<T, IEnumerator> coroutineFunc, T arg)
    {
        yield return StartCoroutine(coroutineFunc(arg));
    }
}
