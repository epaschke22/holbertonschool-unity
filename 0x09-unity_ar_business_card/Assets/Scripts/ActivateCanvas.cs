using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCanvas : MonoBehaviour
{
    public GameObject renderObject;
    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        renderObject.SetActive(true);
    }

    public void RenderCanvus()
	{
        StartCoroutine(Wait(0.1f));
	}
}
