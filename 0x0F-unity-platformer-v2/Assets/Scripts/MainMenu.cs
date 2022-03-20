using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public static string previousSceneName = "MainMenu";

    public AudioMixer masterMixer;

    void Start()
	{
        masterMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("bgmVolumeValue", Mathf.Log10(1) * 20));
        masterMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolumeValue", Mathf.Log10(1) * 20));
    }
	public void LevelSelect(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Options()
	{
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Options");
    }

    public void Exit()
	{
        Debug.Log("Exited");
        Application.Quit();
	}
}
