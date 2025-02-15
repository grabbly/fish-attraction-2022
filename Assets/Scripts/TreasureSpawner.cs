using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] allPrefabs;
    [SerializeField] private Transform treasureCollectionBtn;
    
    private List<UITreasureItem> allItems = new List<UITreasureItem>(); 
    public void TrySpawn(int scoreDelta, Vector3 pos)
    {
        if ( scoreDelta > 0 && Random.value > .3f)
        {
            GivePrize(scoreDelta, pos);
        }
    }

    private void GivePrize(int scoreDelta, Vector3 pos)
    {
        var index = scoreDelta;
        if (Random.value < .5f)
            index = 0;
        else if (index >= allPrefabs.Length)
        {
            index = Random.Range(0, allPrefabs.Length);
        }
        var prefab = allPrefabs[index];
        var go = Instantiate(prefab);
        go.transform.position = pos;

        var treasureItem = go.GetComponent<UITreasureItem>();
        treasureItem.FallDown(OnCollect);
        allItems.Add(treasureItem);
    }

    public void CollectAll()
    {
        var allItemsArray = allItems.ToArray();
        foreach (var item in allItemsArray)
        {
            item.OnMouseDown();
        }
    }
    private void OnCollect(UITreasureItem item)
    {
        if (allItems.Contains(item)) allItems.Remove(item);
        
        Model.AddCollectionItem(item.type.ToString());
        item.transform.DOMove(treasureCollectionBtn.position, .5f).SetEase(Ease.InCubic).OnComplete(() =>
        {
            treasureCollectionBtn.localScale = Vector3.one*1.3f;
            treasureCollectionBtn.DOScale(Vector3.one, .2f);
            Destroy(item.gameObject);
        });
    }
}
