using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitController : MonoBehaviour
{
    [SerializeField] private UnityEvent onAwake;
    [SerializeField] private UnityEvent onStart;
    private void Awake()
    {
        print(Model.TreasureCollection.Count);
        onAwake.Invoke();
        Model.AddCollectionItem("test");
    }

    private void OnDestroy()
    {
        Model.SaveData();
    }
}
