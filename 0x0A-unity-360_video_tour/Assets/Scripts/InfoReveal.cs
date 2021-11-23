using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoReveal : MonoBehaviour
{
    public GameObject textBox;
    bool visible = false;

    public void Toggle()
	{
        if (visible == false)
		{
            textBox.SetActive(true);
            visible = true;
		}
        else
		{
            textBox.SetActive(false);
            visible = false;
        }
	}
    
}
