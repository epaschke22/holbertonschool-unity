using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.AI;

public class SelectPlane : MonoBehaviour
{
    public Image debug1;
    public Image debug2;
    public Image debug3;
    public Image debug4;

    public GameObject initialTextPrompt;
    public GameObject scoreText;
    public GameObject startButton;
    public GameObject tryAgainButton;

    public GameObject target;
    public int numberOfTagets;
    public NavMeshSurface _navMeshSurface;
    public static ARPlane playArea;
    public Material playAreaMaterial;
    public Gradient playAreaBorder;
    public Material defaultMaterial;
    public Gradient defaultBorder;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private SlingshotBehavior slingshotBehavior;
    private Vector2 touchPosition;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
        slingshotBehavior = GetComponent<SlingshotBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            if (playArea == null)
            {
                foreach (ARRaycastHit hit in hits)
                {
                    playArea = _arPlaneManager.GetPlane(hit.trackableId);
                }
                playArea.GetComponent<MeshRenderer>().material = playAreaMaterial;
                playArea.GetComponent<LineRenderer>().colorGradient = playAreaBorder;
                startButton.SetActive(true);
                _arPlaneManager.enabled = false;
            }
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        initialTextPrompt.SetActive(false);
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            if (!(plane.trackableId == playArea.trackableId))
            {
                plane.gameObject.SetActive(false);
            }
        }
        _navMeshSurface = playArea.GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
        playArea.gameObject.GetComponent<MeshRenderer>().enabled = false;
        playArea.gameObject.GetComponent<LineRenderer>().enabled = false;
        SpawnAI(numberOfTagets);
    }

    public void SpawnAI(int amount)
	{
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.1f, 0.1f), 0.05f, Random.Range(-0.1f, 0.1f));
            Instantiate(target, playArea.center + randomOffset, Quaternion.Euler(0, 0, 0));
        }
    }

    public void ResetGame()
    {
        slingshotBehavior.Deactivate();
        tryAgainButton.SetActive(false);
        startButton.SetActive(false);
        scoreText.SetActive(false);
        initialTextPrompt.SetActive(true);
        playArea.gameObject.GetComponent<MeshRenderer>().enabled = true;
        playArea.gameObject.GetComponent<LineRenderer>().enabled = true;
        playArea.GetComponent<MeshRenderer>().material = defaultMaterial;
        playArea.GetComponent<LineRenderer>().colorGradient = defaultBorder;
        playArea = null;
        _arPlaneManager.enabled = true;
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(true);
        }
        foreach(GameObject _target in GameObject.FindGameObjectsWithTag("Target")) 
        {
            Destroy(_target);
        }
    }

    public void Quit()
	{
        Application.Quit();
    }
}
