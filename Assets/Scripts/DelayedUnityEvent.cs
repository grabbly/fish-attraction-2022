using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayedUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEvent;
    [SerializeField] private float delay;
    public bool doOnStart;
    void Start()
    {
        if (doOnStart)
            StartDelay();
    }
    public void DoEvent()
    {
        StartDelay();
    }
    public void StartDelay()
    {
        Invoke("Do", delay);

    }

    void Do()
    {
        OnEvent.Invoke();
    }
}