using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITreasureItem : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private SpriteRenderer sprite;
    public Sprite Sprite => image.sprite;
    
    public Action<UITreasureItem> onCollect;
    public TreasureItem.TreasureType type;
    private CircleCollider2D collider;
    
    
    public void ActivateUI(long itemAmount)
    {
        if (amount == null) amount = GetComponentInChildren<TMP_Text>();
        amount.color = Color.black;
        amount.text = "x"+itemAmount.ToString();
        image.color = Color.white;
        sprite.gameObject.SetActive(false);
       
    }
    public void FallDown(Action<UITreasureItem> callback)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        onCollect = callback;
        collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = 2f;
        image.gameObject.SetActive(false);
        sprite.gameObject.SetActive(true);
        transform.localScale = Vector3.one * .5f;
        transform.DOMoveY(-3.5f, 3f).OnComplete(() =>
        {
            sprite.GetComponent<Animator>().SetBool("blink",true);
        });
    }

    public void OnMouseDown()
    {
        if (collider != null)
        {
            transform.DOKill();
            onCollect.Invoke(this);
            collider.enabled = false;
        // Destroy(gameObject);
        }
    }
    private void OnValidate()
    {
        image.color = color;
        // amount = GetComponentInChildren<TMP_Text>();
        // if (sprite == null)
        // {
        //     sprite = GetComponentInChildren<SpriteRenderer>();
        //     if(sprite) sprite.sprite = image.sprite;
        // }
        //
        // if (sprite == null)
        // {
        //     var rend = new GameObject("Sprite");
        //     rend.transform.SetParent(transform);
        //     sprite = rend.AddComponent<SpriteRenderer>();
        //     sprite.sprite = image.sprite;
        // }
        //     if (image == null) image = GetComponentInChildren<Image>();
        //
        //     image.color = color;
        //     // name = "item_"+type.ToString();
    }
    
    
}
