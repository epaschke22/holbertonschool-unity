using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
	public AudioMixer masterMixer;
	public Slider bgmSlider;
	public Slider sfxSlider;
	public Toggle yToggle;
	public static bool isInverted = true;

	private void Start()
	{
		bgmSlider.value = PlayerPrefs.GetFloat("bgmSliderValue", 1);
		sfxSlider.value = PlayerPrefs.GetFloat("sfxSliderValue", 1);
		if (isInverted == true)
		{
			yToggle.isOn = true;
		} 
		else
		{
			yToggle.isOn = false;
		}
	}

	public void Back()
	{
		PlayerPrefs.SetFloat("bgmSliderValue", bgmSlider.value);
		PlayerPrefs.SetFloat("sfxSliderValue", sfxSlider.value);
		SceneManager.LoadScene(MainMenu.previousSceneName);
	}

	public void Apply()
	{
		if (yToggle.isOn == true)
		{
			isInverted = true;
		}
		else
		{
			isInverted = false;
		}
	}

	public void SetBGMVolume(float sliderValue)
	{
		masterMixer.SetFloat("bgmVolume", Mathf.Log10(sliderValue) * 20);
		PlayerPrefs.SetFloat("bgmVolumeValue", Mathf.Log10(sliderValue) * 20);
	}
	public void SetSFXVolume(float sliderValue)
	{
		masterMixer.SetFloat("sfxVolume", Mathf.Log10(sliderValue) * 20);
		PlayerPrefs.SetFloat("sfxVolumeValue", Mathf.Log10(sliderValue) * 20);
	}
}
