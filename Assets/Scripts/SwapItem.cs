using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapItem : MonoBehaviour
{
    public Action<SwapItem> onClick;
    [SerializeField] private DecorItem.DecorType decorType;
    [SerializeField] private Image image;
    [SerializeField] private SwapPriceItem[] priceItem;
    private List<SwapPriceItem> priceItems;
    public void OnClick()
    {
        onClick?.Invoke(this);
    }
    public List<SwapPriceItemProps> Cost {
        get
        {
            var costList = new List<SwapPriceItemProps>();
            foreach (var item in priceItem)
            {
                if(!item.gameObject.activeSelf) continue;
                
                SwapPriceItemProps props;// = new SwapPriceItemProps();
                props.treasure = item.Type;
                props.price = item.Amount;

            }
            return costList;
        }
    }
    private void Start()
    {
        image.sprite = GameDecorController.Instance.GetSprite(decorType);
    }
    public struct SwapPriceItemProps
    {
        public TreasureItem.TreasureType treasure;
        public int price;
    }
}
