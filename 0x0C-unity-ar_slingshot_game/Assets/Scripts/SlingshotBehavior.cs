using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class SlingshotBehavior : MonoBehaviour
{
    public Image debug1;
    public Image debug2;
    public Image debug3;
    public Image debug4;

    public Transform slingshotHandle;
    public Transform slingshotSling;
    public Transform cameraHandleParent;
    public Transform cameraSlingParent;
    public Transform slingshotAimParent;
    public Transform ammoSpawn;
    public GameObject Ammo;
    public GameObject ammoUI;
    GameObject ammoInst;
    int ammoCount = 7;
    public float power = 1f;
    bool canPull = true;
    public bool gameActive = false;
    public ARPlane PlayArea;

    public void Activate()
	{
        cameraHandleParent.gameObject.SetActive(true);
        cameraSlingParent.gameObject.SetActive(true);
        DelayInput();
        ammoUI.SetActive(true);
        ammoCount = 7;
        ammoInst = Instantiate(Ammo, ammoSpawn);
    }

    public async void DelayInput()
	{
        var end = Time.time + 1f;
        while (Time.time < end)
		{
            await Task.Yield();
		}
        gameActive = true;
    }

    public void Deactivate()
    {
        cameraHandleParent.gameObject.SetActive(false);
        cameraSlingParent.gameObject.SetActive(false);
        gameActive = false;
        ammoUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
		{
            debug4.color = Color.red;
            if (Input.touchCount > 0 && canPull == true)
            {
                Touch touch = Input.GetTouch(0);
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        debug4.color = Color.yellow;
                        PlayArea = SelectPlane.playArea;
                        slingshotHandle.SetParent(PlayArea.transform, true);
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        break;

                    case TouchPhase.Ended:
                        debug4.color = Color.green;
                        float dist = Vector3.Distance(slingshotAimParent.position, cameraSlingParent.position);
                        LaunchAmmo(dist);
                        SlingMotion();
                        break;
                }
            }
        }
    }

    void LaunchAmmo(float dist)
    {
        ammoInst.transform.parent = null;
        ammoInst.transform.LookAt(slingshotAimParent.position);
        ammoInst.GetComponent<Rigidbody>().isKinematic = false;
        ammoInst.GetComponent<Rigidbody>().AddForce(ammoInst.transform.forward * dist * power, ForceMode.Impulse);
        ammoInst.GetComponent<AmmoBehavior>().Delete();
        ammoInst = null;
        ammoCount -= 1;
    }

    public async void SlingMotion()
    {
        slingshotSling.position = slingshotAimParent.position;
        slingshotSling.rotation = slingshotAimParent.rotation;
        slingshotSling.SetParent(slingshotAimParent, true);
        var end = Time.time + 1f;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        slingshotSling.position = cameraSlingParent.position;
        slingshotSling.rotation = cameraSlingParent.rotation;
        slingshotSling.SetParent(cameraSlingParent, true);
        slingshotHandle.position = cameraHandleParent.position;
        slingshotHandle.rotation = cameraHandleParent.rotation;
        slingshotHandle.SetParent(cameraHandleParent, true);
        if (ammoCount > 0)
            ammoInst = Instantiate(Ammo, ammoSpawn);
    }
}
