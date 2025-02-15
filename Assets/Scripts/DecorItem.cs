using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private DecorType type;
    [SerializeField] private bool isActive;

    public DecorType Type => type;
    public Sprite Sprite => image.sprite;

    private void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        IsActive = Model.IsDecorPurchased(type.ToString());//GetCustomData("decor_active_"+type.ToString()) 
    }
    
    public bool IsActive
    {
        set
        {
            image.color = !value ? new Color(0, 0, 0, 45f / 256f) : Color.white;
        }
    }

    private void OnValidate()
    {
        if (image == null)
        {
            name = type.ToString();
            image = GetComponent<SpriteRenderer>();
            IsActive = isActive;
        }
    }

    public enum DecorType
    {
        None,
        WaterFlower,
        CrabHouse,
        OrangeHouse,
        SeaweedPink,
        SeaweedGreen,
        SeaweedPurple,
        SeaweedStar,
        ArmedBlue,
        ArmedRed,
        ArmedRedBlue
    }
}
