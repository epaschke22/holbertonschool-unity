using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHook : MonoBehaviour
{
    public GameObject[] fish;

    GameObject currentFish = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "FishingZone" && currentFish == null)
		{
            int randomfish = Random.Range(0, fish.Length);
            currentFish = Instantiate(fish[randomfish], gameObject.transform.position, gameObject.transform.rotation);
			currentFish.transform.parent = gameObject.transform;
		}

        if (other.tag == "BucketZone" && currentFish != null)
		{
            Destroy(currentFish);
            currentFish = null;
		}
	}
}
