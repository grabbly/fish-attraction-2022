using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour
{

	public static UnityEvent<bool> onSoundOnOff = new UnityEvent<bool>();

	public AudioSource music;
	public Toggle musicToggle;
	
	Dictionary<string,AudioSource> hash = new Dictionary<string, AudioSource> ();


	void Start () {
		foreach (var item in GetComponentsInChildren<AudioSource> ()) {
			hash.Add (item.name, item);
		}
		if (Model.MusicOn) music.Play();
		else music.Stop();
		onSoundOnOff.Invoke(Model.MusicOn);
		musicToggle.isOn = !Model.MusicOn;
	}
	public void ResetPitch(string soundName){
		if (hash.ContainsKey (soundName)) {
			hash [soundName].pitch=1;
		} else {
			print ("Sound "+soundName+" not Found");	
		}
	}
	public void Play(string soundName, bool increasePitch = false){
		if (hash.ContainsKey (soundName)) {
			if (increasePitch)
					hash [soundName].pitch += .01f; 
			hash [soundName].Play ();
		} else {
			print ("Sound "+soundName+" not Found");	
		}
	}
	public void Stop(string soundName){
		if (hash.ContainsKey (soundName)) {
			hash [soundName].Stop ();
		} else {
			print ("Sound "+soundName+" not Found");	
		}
	}
	public AudioSource GetSound(string soundName){
		if (hash.ContainsKey (soundName)) {
			return hash [soundName];
		}else {
			print ("Sound "+soundName+" not Found");	
			return null;
		}
	}
	
	public void MusicOnOff(bool isOff)
	{
		onSoundOnOff.Invoke(!isOff);
		Model.MusicOn = !isOff;
		if (!isOff) music.Play();
		else music.Stop();
	}
}
