using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo_Links : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGitHub()
	{
        Application.OpenURL("https://github.com/epaschke22");
    }

    public void LoadLinkedIn()
    {
        Application.OpenURL("https://www.linkedin.com/in/erickson-paschke-79802782/");
    }

    public void LoadEmail()
	{
        Application.OpenURL("mailto:ericksonpaschke@gmail.com?subject=Nice Card!");
    }
}
