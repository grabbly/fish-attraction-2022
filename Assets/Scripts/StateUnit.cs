using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateUnit : MonoBehaviour
{
    [SerializeField] private UnityEvent OnShow;
    [SerializeField] private UnityEvent OnHide;
    public string stateId;

    public void Show()
    {
        gameObject.SetActive(true);
        OnShow.Invoke();
    }

    public void Hide()
    {
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            OnHide.Invoke();
        }
        
    }
    
}
