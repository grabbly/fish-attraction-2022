using System;
using System.Collections;
using UnityEngine;

public static class Utility
{
    public static void Delay(this float number, Action onComplete)
    {
        var go = new GameObject("delay"+number);
        var delayComp = go.AddComponent<DelayMono>();
        delayComp.StartCoroutine(InvokeRoutine(onComplete, number, delayComp));
    }
    private static IEnumerator InvokeRoutine(System.Action onComplete, float delay, DelayMono mono)
    {
        yield return new WaitForSeconds(delay);
        onComplete();
        GameObject.Destroy(mono.gameObject);
    }
    public class DelayMono : MonoBehaviour { }
}