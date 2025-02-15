using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPage : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCoins;
    [SerializeField] private TMP_Text levelCoins;
    [SerializeField] private FishCounterUI coinsFx;
    [SerializeField] private UnityEvent onClosePage;
    private int _score = 0;
    
    public void Init(int score)
    {
        levelCoins.gameObject.SetActive(true);
        totalCoins.text = (Model.FishPoints - score).ToString();
        levelCoins.text = score.ToString();
        _score = score;
    }
    public void Collect()
    {
        levelCoins.gameObject.SetActive(false);
        // var curScore = 0;
        float closeDelay = 0;
        int step = _score / 20;
        if (step < 1) step = 1;
        int j = -1;
        int curScore = Model.FishPoints - _score;
        coinsFx.onScoreAdded = addedScore =>
        {
            curScore += addedScore;
            totalCoins.text = (curScore).ToString();
        };

        for (int i = 0; i < _score; i+=step)
        {
            j++;
            
            var pointShowDelay = closeDelay = j *.1f;
            
            pointShowDelay.Delay(() =>
            {
                coinsFx.AddScore(step, levelCoins.transform.position);
            });
            
        }

        closeDelay += 1;
        closeDelay.Delay(()=>onClosePage.Invoke());
    }

}
