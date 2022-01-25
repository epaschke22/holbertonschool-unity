using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
	public GameObject poof;
	GameObject _poof;

    public void Delete()
	{
        Destroy(gameObject, 2f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Target")
		{
			ScoreTracking.instance.ScoreUpdate(100);
			_poof = Instantiate(poof, transform.position, transform.rotation);
			Destroy(_poof, 2f);
			Destroy(gameObject);
		}
	}
}
