using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	public Canvas pauseMenuUI;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Pause()
	{
		pauseMenuUI.enabled = true;
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void Resume()
	{
		pauseMenuUI.enabled = false;
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void Options()
	{
		SceneManager.LoadScene("Options");
	}
}
