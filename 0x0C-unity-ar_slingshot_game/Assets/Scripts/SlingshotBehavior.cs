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
    bool canPull = true;
    public bool gameBegin = true;
    public ARPlane PlayArea = SelectPlane.playArea;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameBegin == true)
		{
            if (Input.touchCount > 0 && canPull == true)
            {
                canPull = false;
                slingshotHandle.SetParent(PlayArea.transform, true);
            }
            else
            {
                canPull = true;
                slingshotHandle.position = cameraHandleParent.position;
                slingshotHandle.rotation = cameraHandleParent.rotation;
                slingshotHandle.SetParent(cameraHandleParent, false);
            }
        }
    }
}
