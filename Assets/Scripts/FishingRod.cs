using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    public Action<Fish> onCatchFish;
    public Action OnOut;
    public Action<Vector3> onSplash;

    [SerializeField] private Hook hook;
    public List<Fish> fishesOnHook = new List<Fish>();
    private void Awake()
    {
        hook.onCatch = OnCatchFish;
    }

    private void OnCatchFish(Fish fish)
    {
        fishesOnHook.Add(fish);
        onCatchFish?.Invoke(fish);
    }

    private bool isGoingOut = false;
    public void Out()
    {
        if(isGoingOut || this == null) return;
        isGoingOut = true;
        GetComponent<Animator>().Play("out");
    }
    // Update is called once per frame
    public void DestroyMeAnimationEvent()
    {
        isGoingOut = false;
        OnOut?.Invoke();
    }

    public void OnSplashAnimationEvent()
    {
        onSplash?.Invoke(hook.transform.position);
    }
}
