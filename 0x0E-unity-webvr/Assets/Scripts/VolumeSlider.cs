using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
	public AudioMixer mixer;

	public void setVolume(float sliderValue)
	{
		mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
	}
}
