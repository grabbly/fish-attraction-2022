using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAnim : MonoBehaviour
{
    private void OnEnable()
    {
        SoundsController.onSoundOnOff.AddListener(OnOn);
        OnOn(Model.MusicOn);
    }

    private void OnDestroy()
    {
        SoundsController.onSoundOnOff.RemoveListener(OnOn);
    }

    private void OnOn(bool isOn)
    {
        //todo cache Animator
        GetComponent<Animator>().ResetTrigger(!isOn ? "on" : "off");
        GetComponent<Animator>().SetTrigger(isOn ? "on" : "off");
    }
}
