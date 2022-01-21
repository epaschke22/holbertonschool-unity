using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class SlingshotBehavior : MonoBehaviour
{
    public Transform slingshotHandle;
    public Transform slingshotSling;
    public Transform cameraHandleParent;
    public Transform cameraSlingParent;
    public Transform slingshotAimParent;
    public GameObject Ammo;
    bool canPull = true;
    public bool gameActive = false;
    public ARPlane PlayArea;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Activate()
	{
        cameraHandleParent.gameObject.SetActive(true);
        cameraSlingParent.gameObject.SetActive(true);
        gameActive = true;
	}

    public void Deactivate()
    {
        cameraHandleParent.gameObject.SetActive(false);
        cameraSlingParent.gameObject.SetActive(false);
        gameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
		{
            if (Input.touchCount > 0 && canPull == true)
            {
                PlayArea = SelectPlane.playArea;
                slingshotHandle.SetParent(PlayArea.transform, true);

                if (Input.touchCount == 0)
				{
                    slingshotHandle.position = cameraHandleParent.position;
                    slingshotHandle.rotation = cameraHandleParent.rotation;
                    slingshotHandle.SetParent(cameraHandleParent, false);
                }
            }
        }
    }
}
