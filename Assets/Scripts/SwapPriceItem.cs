using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwapPriceItem : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image image;
    [SerializeField] private TreasureItem.TreasureType type;

    private void Start()
    {
        image.sprite = TreasureCollection.Instance.GetSprite(type);
        text.text = price.ToString();
    }
    public int Amount => price; 
    public TreasureItem.TreasureType Type => type; 
}
