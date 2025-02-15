using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public static Game instance;
    public Camera GameCamera => cam;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject fishingRodPrefab;
    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private TreasureSpawner treasureSpawner;
    [SerializeField] private UnityEvent onGameStart;
    [SerializeField] private UnityEvent onSplash;
    [SerializeField] private IntUnityEvent onEndGame;

    public Action<int, Vector3> onScoreDelta;
    public Action<int> onScoreChanged;
    private FishingRod currentRot;
    private List<Fish> catchedFishes = new List<Fish>();
    private int score = 0;
    
    private void Awake()
    {
        instance = this;
    }


    
    // private static State _state;
    public static State state {private set; get;}
    public enum State
    {
        Start,
        Game,
        End
    }
    
    public void StartGame()
    {
        
        state = State.Game;
        onGameStart.Invoke();
    }
    void Update()
    {
        if(state != State.Game) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (currentRot == null)
            {
                var touch = cam.ScreenToWorldPoint(Input.mousePosition);
                var rodGO = Instantiate(fishingRodPrefab);
                rodGO.transform.position = new Vector3(touch.x + 4.22f, 4.29f, 0);
                currentRot = rodGO.GetComponent<FishingRod>();
                currentRot.OnOut = OnOut;
                currentRot.onCatchFish = OnCatchFish;
                currentRot.onSplash = OnSplash;
            }
            else
            {
                currentRot.Out();
            }
        }
    }

    private void OnSplash(Vector3 pos)
    {
        var splash = Instantiate(splashPrefab);
        splash.transform.position = pos;
        var scoreDelta = 0;
        if (currentRot != null)
        {
            foreach (var fish in catchedFishes)
            {
                onScoreDelta?.Invoke(fish.Score, pos);
                score += fish.Score;
                scoreDelta += fish.Score;
            }

            onScoreChanged?.Invoke(score);
            catchedFishes.Clear();
        }
        onSplash.Invoke();
        treasureSpawner.TrySpawn(scoreDelta, pos);
    }
    private void OnCatchFish(Fish fish)
    {
        catchedFishes.Add(fish);
    }
    public void EndGame()
    {
        
        // Model.ResetGameTime();
        // Model.ResetFishAmount();
        
        state = State.End;
        float delay = 1;
        if (currentRot != null)
        {
            0.5f.Delay(() =>{ currentRot.Out();});
            delay = 2f;
        }
        delay.Delay(() =>
        {
            onEndGame.Invoke(score);
            Model.FishPoints += score;
            treasureSpawner.CollectAll();
        });
    }
    private void OnOut()
    {
        Debug.Log("OUT FISH");
        Destroy(currentRot.gameObject);
    }
}
