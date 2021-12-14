using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public bool containerA;
    public bool containerB;

    public GameObject openDoor;
    public GameObject lockedDoor;

    public void ActivateDoor(int value)
	{
        if (value == 1)
            containerA = true;
        if (value == 2)
            containerB = true;
        if (value == -1)
            containerA = false;
        if (value == -2)
            containerB = false;

        if (containerA == true && containerB == true)
		{
            openDoor.SetActive(true);
            lockedDoor.SetActive(false);
        }
        else
		{
            openDoor.SetActive(false);
            lockedDoor.SetActive(true);
        }
	}
}
