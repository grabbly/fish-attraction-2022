using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameDecorController : MonoBehaviour
{
    [SerializeField] private 
        List<DecorItem> decorItems;

    // private void OnValidate()
    // {
    //     if(decorItems.Count == 0)
    //         decorItems = FindObjectsOfType<DecorItem>().ToList();
    // }
    private static GameDecorController instance;
    public static GameDecorController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameDecorController>(true);
            return instance;
        }
    }

    public Sprite GetSprite(DecorItem.DecorType type)
    {
        foreach (var decorItem in decorItems)
        {
            if (decorItem.Type == type) return decorItem.Sprite;
        }
        return null;
    }
}
