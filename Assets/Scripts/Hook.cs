using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hook : MonoBehaviour
{
    public Action<Fish> onCatch;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Fish"))
        {
            col.transform.SetParent(transform);
            col.transform.DOLocalMove(Vector3.zero, .2f);
            col.enabled = false;
            var fish = col.gameObject.GetComponent<Fish>();
            fish.enabled = false;
            onCatch?.Invoke(fish); 
        }
    }
}
