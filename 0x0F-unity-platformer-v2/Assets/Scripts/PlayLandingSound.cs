using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLandingSound : MonoBehaviour
{
    public AudioSource impactSound;

    void Landing()
	{
        if (impactSound)
            impactSound.Play();
    }
}
