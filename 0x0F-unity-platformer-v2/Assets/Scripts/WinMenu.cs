using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Next()
    {
        if (SceneManager.GetActiveScene().name == "Level01")
		{
            SceneManager.LoadScene("Level02");
        }
        else if (SceneManager.GetActiveScene().name == "Level02")
        {
            SceneManager.LoadScene("Level03");
        }
        else
		{
            SceneManager.LoadScene("MainMenu");
        }
    }
}
