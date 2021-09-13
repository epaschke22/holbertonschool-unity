using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : MonoBehaviour
{
    public GameObject playerObject;
	Timer timerRef;

	// Start is called before the first frame update
	void Start()
	{
		timerRef = playerObject.GetComponent<Timer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			timerRef.TimerEnd();
		}
	}
}
