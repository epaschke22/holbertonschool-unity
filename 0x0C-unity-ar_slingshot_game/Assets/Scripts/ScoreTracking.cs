using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracking : MonoBehaviour
{
    public Text scoreText;
    public int scoreValue;
	SlingshotBehavior slingshot;

	private void Start()
	{
		slingshot = gameObject.GetComponent<SlingshotBehavior>();
	}

	public void ScoreUpdate(int value)
	{
		scoreValue += value;
		scoreText.text = scoreValue.ToString();
	}

	public void ScoreReset()
	{
		scoreText.gameObject.SetActive(true);
		scoreValue = 0;
		scoreText.text = scoreValue.ToString();
	}
}
