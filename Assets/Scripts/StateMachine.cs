using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateUnit[] StateUnits;

    

    public void ActivateState(string stateId)
    {
        foreach (var item in StateUnits)
        {
            if(stateId == item.stateId) item.Show();
            else item.Hide();
        }
    }

    
    public void ActivateFirstState()
    {
        for (int i = 0; i < StateUnits.Length; i++)
        {
            var item = StateUnits[i];
            if (i == 0) item.Show();
            else item.Hide();
        }
    }
    public void DeactivateFirstState()
    {
        if (StateUnits.Length > 1)
        {
            StateUnits[0].Hide();
            StateUnits[1].Show();
        }
    }

    public void SwitchFirst(bool isOn)
    {
        if (isOn)
            ActivateFirstState();
        else
            DeactivateFirstState();
    }
    private void OnValidate()
    {
        if (StateUnits.Length == 0)
        {
            StateUnits = GetComponentsInChildren<StateUnit>(true);
            
            for (int i = 0; i < StateUnits.Length; i++)
            {
                if (string.IsNullOrEmpty(StateUnits[i].stateId)) StateUnits[i].stateId = i.ToString();
                
                if(i != 0)
                    StateUnits[i].gameObject.SetActive(false);
            }
        }
    }
    // void OnDisable()
    // {
    //     foreach (var item in StateUnits)
    //     {
    //         item.Hide();
    //     }
    // }
    

}
