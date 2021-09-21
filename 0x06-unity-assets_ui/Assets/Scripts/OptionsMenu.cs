using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public Toggle yToggle;
	public static bool isInverted = true;

	private void Start()
	{
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
}
