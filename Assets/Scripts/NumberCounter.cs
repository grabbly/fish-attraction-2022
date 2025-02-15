using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private StringUnityEvent onChanged;
    private int curNum = 0;
    public void Reset()
    {
        StopAllCoroutines();
        curNum = 0;
        onChanged.Invoke(curNum.ToString());
    }
    public void CountTo(int targetNum)
    {
        StartCoroutine(CountFlow(targetNum));
    }
    IEnumerator CountFlow(int targetNum)
    {
        yield return new WaitForSeconds(delay);
        var startNum = curNum;
        int step = (targetNum-startNum) / 20;
        if (step < 1) step = 1;
        // for (int i = startNum; i <= targetNum; i++)
        for (int i = startNum; i < targetNum; i+=step)
        {
            yield return new WaitForSeconds(.1f);
            curNum = i;
            onChanged.Invoke(curNum.ToString());
        }
    }
}
