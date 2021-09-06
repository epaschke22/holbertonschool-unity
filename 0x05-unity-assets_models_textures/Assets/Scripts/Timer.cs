using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    float timeValue = 0;

    public bool timerOn = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)
		{
            timeValue += Time.deltaTime;

            float minutes = Mathf.FloorToInt(timeValue / 60);
            float seconds = timeValue % 60;

            timerText.text = string.Format("{0}:{1:00.00}", minutes, seconds);
        }
    }

    public void TimerEnd()
	{
        timerOn = false;
        timerText.fontSize = 60;
        timerText.color = Color.green;
    }
}
