using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

	public Slider volumeSlider1;
	public Slider volumeSlider2;

	private AudioSource[] aSources;

	// Use this for initialization
	void Start () {
		aSources = GetComponents<AudioSource> ();
	}
	

	public void ChangeVol(int n) {
		if (n == 1)
		{
			for (int j = 0; j < aSources.Length; j++)
			{
				aSources[j].volume = volumeSlider1.value;
			}
			volumeSlider2.value = volumeSlider1.value;
		} 
		else
		{
			for (int j = 0; j < aSources.Length; j++)
			{
				aSources[j].volume = volumeSlider2.value;
			}
			volumeSlider1.value = volumeSlider2.value;
		}
	}
}
