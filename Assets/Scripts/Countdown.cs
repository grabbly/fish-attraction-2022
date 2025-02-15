using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Countdown : MonoBehaviour
{
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onLastSeconds;
    [SerializeField] private UnityEvent onDing;
    [SerializeField] private UnityEvent onEnd;
    [SerializeField] private StringUnityEvent onTick;
    [SerializeField] private FloatUnityEvent onValueChanged;
    private int currentTime = 0;
    private int startingTime = 15;

    public void StartTime()
    {
        startingTime = Model.GameTime;
        StartCoroutine(TimeFlow());
    }

    

    IEnumerator TimeFlow()
    {
        onStart.Invoke();
        currentTime = startingTime;
        while (currentTime > 0)
        {
            if (currentTime == 10)
                onLastSeconds.Invoke();
            onValueChanged.Invoke((float)currentTime/(float)startingTime);
            onTick.Invoke(currentTime.ToString());
            yield return new WaitForSeconds(1);
            currentTime--;
        }
        onValueChanged.Invoke(0);
        onTick.Invoke("0");
        yield return new WaitForSeconds(1);
        onDing.Invoke();
        yield return new WaitForSeconds(2);
        onEnd.Invoke();
    }
}
