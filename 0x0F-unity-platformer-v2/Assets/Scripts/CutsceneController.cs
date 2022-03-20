using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public float cutsceneLength = 5f;
    public GameObject mainCamera;
    public GameObject timerCanvas;
    public PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cutscene(cutsceneLength));
    }

    IEnumerator Cutscene(float waitTime)
	{
        yield return new WaitForSeconds(waitTime);
        GameStart();
    }

    public void GameStart()
	{
        mainCamera.SetActive(true);
        timerCanvas.SetActive(true);
        playerScript.canMove = true;
        gameObject.SetActive(false);
	}
}
