using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    float timeValue = 0;

    public bool timerOn = false;

    public GameObject winMenu;

    // Start is called before the first frame update
    void Start()
    {
        winMenu.SetActive(false);
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

    public void Win()
	{
        timerOn = false;
        winMenu.SetActive(true);
        winMenu.transform.GetChild(3).GetComponent<Text>().text = timerText.text;
        timerText.enabled = false;
    }
}
