using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class FishCounterUI : MonoBehaviour
{
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Transform flyTarget;
    public UnityEvent onScoreAddedUnityEvent;
    public Action<int> onScoreAdded;

    public void Fly(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            AddScore(1, transform.position);
        }
    }
    
    public void AddScore(int score, Vector3 startPoint)
    {
        var scoreGO = Instantiate(scorePrefab, transform);
        var pos = startPoint;
        pos.z = scoreGO.transform.position.z;
        scoreGO.transform.position = pos;
        TMP_Text text = scoreGO.GetComponent<TMP_Text>();
        if(text) text.text = "+"+score.ToString();
        scoreGO.transform.DOMove(RandomPointOnRadius(pos, 0.5f), .3f)
            .OnComplete(() =>
            {
                scoreGO.transform.DOMove(flyTarget.position, .3f)
                    .SetEase(Ease.InSine).SetDelay(UnityEngine.Random.value/2f).OnComplete(() =>
                    {
                        flyTarget.DOKill();
                        flyTarget.localScale = Vector3.one * 1.1f;
                        flyTarget.DOScale(1, .3f);
                        onScoreAdded?.Invoke(score);
                        onScoreAddedUnityEvent.Invoke();
                        Destroy(scoreGO);
                    });
            }
        );
    }

    private Vector3 RandomPointOnRadius(Vector3 center, float radius)
    {
        var r = radius * Mathf.Sqrt(UnityEngine.Random.value);
        var theta = UnityEngine.Random.value * 2 * Mathf.PI;
        var x = center.x + r * Mathf.Cos(theta);
        var y = center.y + r * Mathf.Sin(theta);
        return new Vector3(x, y, center.z);
    }
}
