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
    public GameObject startButton;
    public GameObject targetSpawner;
    public NavMeshSurface _navMeshSurface;
    public static ARPlane playArea;
    public Material playAreaMaterial;
    public Gradient playAreaBorder;

    private GameObject spawnedAI;
    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
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
                startButton.SetActive(true);
                _arPlaneManager.enabled = false;
                playArea.GetComponent<MeshRenderer>().material = playAreaMaterial;
                playArea.GetComponent<LineRenderer>().colorGradient = playAreaBorder;
            }
        }
    }

    public void StartGame()
    {
        //_surfaces[0] = playArea.gameObject;
        //BakeAtRuntime();
        _navMeshSurface = playArea.GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
        playArea.gameObject.GetComponent<MeshRenderer>().enabled = false;
        spawnedAI = Instantiate(targetSpawner, playArea.center, Quaternion.Euler(0, 0, 0));
    }

	//private void BakeAtRuntime()
	//{
	//	_navMeshSurface = new NavMeshSurface[_surfaces.Length];
	//	for (int i = 0; i < _navMeshSurface.Length; i++)
	//	{
	//		if (_surfaces[i].gameObject.GetComponent<NavMeshSurface>() == null)
	//		{
	//			_navMeshSurface[i] = _surfaces[i].gameObject.AddComponent<NavMeshSurface>();
	//		}
	//		else
	//		{
	//			_navMeshSurface[i] = _surfaces[i].gameObject.GetComponent<NavMeshSurface>();
	//		}
	//	}
	//	for (int i = 0; i < _navMeshSurface.Length; i++)
	//	{
	//		_navMeshSurface[i].BuildNavMesh();
	//	}
	//}

	public void Reset()
    {
        _arPlaneManager.enabled = true;
        startButton.SetActive(false);
        initialTextPrompt.SetActive(true);
        Destroy(playArea.gameObject);
        playArea = null;
    }

    public void DisablePlanes()
    {
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            if (!(plane.trackableId == playArea.trackableId))
            {
                Destroy(plane.gameObject);
            }
        }

        _arPlaneManager.enabled = false;
    }

    public void Quit()
	{
        Application.Quit();
    }
}
