using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRod : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
		{
            gameObject.transform.position = new Vector3(0.342f, 0.638f, -0.480f);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
    }
}
