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
    public GameObject tryAgainButton;
    public ScoreTracking scoreUI;
    public LineRenderer trajectoryRenderer;
    public int lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();
    GameObject ammoInst;
    int ammoCount = 7;
    public float power = 1f;
    bool canPull = true;
    public bool gameActive = false;
    public ARPlane PlayArea;

    public void Activate()
	{
        gameActive = false;
        cameraHandleParent.gameObject.SetActive(true);
        cameraSlingParent.gameObject.SetActive(true);
        scoreUI.ScoreReset();
        tryAgainButton.SetActive(false);
        DelayInput();
        ammoUI.GetComponent<AmmoUI>().Reload();
        ammoUI.SetActive(true);
        ammoCount = 7;
        ammoInst = Instantiate(Ammo, ammoSpawn);
        slingshotSling.position = cameraSlingParent.position;
        slingshotSling.rotation = cameraSlingParent.rotation;
        slingshotSling.SetParent(cameraSlingParent, true);
        slingshotHandle.position = cameraHandleParent.position;
        slingshotHandle.rotation = cameraHandleParent.rotation;
        slingshotHandle.SetParent(cameraHandleParent, true);
        DelayInput();
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
            if (Input.touchCount > 0 && canPull == true && ammoCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                DrawTrajectory(slingshotAimParent.position, ammoInst.GetComponent<Rigidbody>(), ammoSpawn.position);
                // Handle finger movements based on TouchPhase
                switch (touch.phase)
                {
                    //When a touch has first been detected, change the message and record the starting position
                    case TouchPhase.Began:
                        PlayArea = SelectPlane.playArea;
                        slingshotHandle.SetParent(PlayArea.transform, true);
                        break;

                    //Determine if the touch is a moving touch
                    case TouchPhase.Moved:
                        break;

                    case TouchPhase.Ended:
                        trajectoryRenderer.positionCount = 0;
                        float dist = Vector3.Distance(slingshotAimParent.position, cameraSlingParent.position);
                        ammoCount -= 1;
                        ammoUI.GetComponent<AmmoUI>().UpdateAmount(ammoCount);
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
		else
		{
            gameActive = false;
            tryAgainButton.SetActive(true);
        }
    }

    void DrawTrajectory(Vector3 targetVector, Rigidbody rb, Vector3 startPoint)
	{
        Vector3 velocity = (targetVector / rb.mass) * Time.fixedDeltaTime;
        float flightDuration = (2 * velocity.y) / Physics.gravity.y;
        float stepTime = flightDuration / lineSegmentCount;
        _linePoints.Clear();

		for (int i = 0; i < lineSegmentCount; i++)
		{
            float stepTimePassed = stepTime * i;

            Vector3 moveVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);

            _linePoints.Add(-moveVector + startPoint);

		}

        trajectoryRenderer.positionCount = _linePoints.Count;
        trajectoryRenderer.SetPositions(_linePoints.ToArray());
	}
}
