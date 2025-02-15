    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{

    public static UIController instance;
    public Camera uiCamera;
    
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private IntUnityEvent onScoreChanged;
    [SerializeField] private UnityEvent onStartGame;
    [SerializeField] private FishCounterUI scoreCounter;
    [SerializeField] private Sprite[] treasures;

    private void Awake()
    {
        instance = GetComponent<UIController>();
        InvokeRepeating("SaveData",5,5);
    }

    
    private void Start()
    {
        // Model.ResetGameTime();
        // Model.ResetFishAmount();
        Game.instance.onScoreDelta = scoreCounter.AddScore;
        Game.instance.onScoreChanged = score => {onScoreChanged.Invoke(score);};
       // scoreCounter.onScoreAdded
       OnStart();
    }

    public void OnStart()
    {
        onStart.Invoke();
    }
    // private void OnAddScore(int score, Transform startPoint)
    // {
    //     scoreCounter.AddScore(score, startPoint);
    // }
    public Sprite TreasureIcon(int index)
    {
        return treasures[index % treasures.Length];
    }
    public void OnStartPressed()
    {
        onStartGame.Invoke();
    }

    private void SaveData()
    {
        Model.SaveData();
    }

    private void OnDestroy()
    {
        SaveData();
    }
}
