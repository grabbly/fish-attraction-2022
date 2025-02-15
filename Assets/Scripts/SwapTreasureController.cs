using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwapTreasureController : MonoBehaviour
{
    
    // [SerializeField] private UITreasureItem[] treasuresPrefabs;
    [SerializeField] private SwapItem[] items;
    [SerializeField] private UnityEvent OnChanged;

    private void Start()
    {
        items = transform.GetComponentsInChildren<SwapItem>();
        foreach (var item in items)
        {
            item.onClick = OnClick;
        }
    }

    private void OnClick(SwapItem item)
    {
        bool allEnough = true;
        foreach (var priceItem in item.Cost)
        {
            var key = priceItem.treasure.ToString();
            if(!Model.TreasureCollection.ContainsKey(key) || (long)Model.TreasureCollection[key] < priceItem.price)
            {
                allEnough = false;
                break;
            }
        }
        if (allEnough)
        {
            SwapTreasure(item);
        }
    }

    private void SwapTreasure(SwapItem item)
    {
        foreach (var priceItem in item.Cost)
        {
            Model.SetCollectionItem(priceItem.treasure.ToString(), priceItem.price);
        }
        OnChanged.Invoke();
    }
}
