using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public GameObject playerObject;
    Timer timerRef;

    // Start is called before the first frame update
    void Start()
    {
        timerRef = playerObject.GetComponent<Timer>();
    }

	private void OnTriggerExit(Collider other)
	{
        timerRef.timerOn = true;
        Destroy(gameObject);
	}
}
