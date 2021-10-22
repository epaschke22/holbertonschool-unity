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
	public static float bgmSliderValue = 1f;
	public static float sfxSliderValue = 1f;
	public Toggle yToggle;
	public static bool isInverted = true;

	private void Start()
	{
		bgmSlider.value = bgmSliderValue;
		sfxSlider.value = sfxSliderValue;
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
		bgmSliderValue = bgmSlider.value;
		sfxSliderValue = sfxSlider.value;
		SceneManager.LoadScene(MainMenu.previousSceneName);
	}

	public void Apply()
	{
		bgmSliderValue = bgmSlider.value;
		sfxSliderValue = sfxSlider.value;
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
	}
	public void SetSFXVolume(float sliderValue)
	{
		masterMixer.SetFloat("sfxVolume", Mathf.Log10(sliderValue) * 20);
	}
}
