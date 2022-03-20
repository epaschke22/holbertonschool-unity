using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public Canvas pauseMenuUI;
	public AudioMixerSnapshot unpaused;
	public AudioMixerSnapshot paused;

	public void TogglePause()
	{
		if (GameIsPaused) 
			Resume();
		else 
			Pause();
	}

	public void Pause()
	{
		paused.TransitionTo(.001f);
		pauseMenuUI.enabled = true;
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Resume()
	{
		unpaused.TransitionTo(.001f);
		pauseMenuUI.enabled = false;
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Restart()
	{
		Resume();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void Mainmenu()
	{
		Resume();
		SceneManager.LoadScene("MainMenu");
	}

	public void Options()
	{
		Resume();
		MainMenu.previousSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene("Options");
	}
}
