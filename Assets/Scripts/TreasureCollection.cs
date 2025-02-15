using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class TreasureCollection : MonoBehaviour
{
    //static instance
    [SerializeField] private List<UITreasureItem> allItems;

    private static TreasureCollection instance;
    public static TreasureCollection Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TreasureCollection>(true);
            return instance;
        }
    }
    
    public Sprite GetSprite(TreasureItem.TreasureType type)
    {
        foreach (var item in allItems)
        {
            if(item.type == type) return item.Sprite;
        }
        return null;
    }
    public void Show()
    {
        var collectedItems = Model.TreasureCollection;
        foreach (var collectedItem in collectedItems)
        {
            
            foreach (var item in allItems)
            {
                if (item.type.ToString() == collectedItem.Key)
                {
                    item.ActivateUI((long)collectedItem.Value);
                }
            }
        }
        gameObject.SetActive(true);
    }
}
