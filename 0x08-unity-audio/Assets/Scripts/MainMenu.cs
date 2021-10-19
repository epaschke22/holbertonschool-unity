using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static string previousSceneName = "MainMenu";

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
