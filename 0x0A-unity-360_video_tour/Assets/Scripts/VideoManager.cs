using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class VideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public Animator fade;
    public GameObject[] sceneList;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void ChangeVideo(VideoClip newClip)
	{
        fade.SetTrigger("Start");
        StartCoroutine(WaitForFade(newClip.name));
        videoPlayer.clip = newClip;
	}

    IEnumerator WaitForFade(string clipName)
	{
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject scene in sceneList)
        {
            if (scene.name == clipName)
            {
                scene.SetActive(true);
            }
            else
            {
                scene.SetActive(false);
            }
        }
    }
}
