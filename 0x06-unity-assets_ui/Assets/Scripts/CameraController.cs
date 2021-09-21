using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public bool isInverted;
    public CinemachineFreeLook mainCam;

    // Start is called before the first frame update
    void Start()
    {
        if (OptionsMenu.isInverted == true)
		{
            mainCam.m_YAxis.m_InvertInput = true;
        }
        else
		{
            mainCam.m_YAxis.m_InvertInput = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
