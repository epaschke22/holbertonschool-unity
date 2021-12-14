using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorPuzzle : MonoBehaviour
{
    public bool chess1;
    public bool chess2;
    public bool chess3;
    public bool chess4;

    public GameObject projector;

    public void ActivateProjector(int value)
    {
        if (value == 1)
            chess1 = true;
        if (value == 2)
            chess2 = true;
        if (value == 3)
            chess3 = true;
        if (value == 4)
            chess4 = true;

        if (value == -1)
            chess1 = false;
        if (value == -2)
            chess2 = false;
        if (value == -3)
            chess3 = false;
        if (value == -4)
            chess4 = false;

        if (chess1 == true && chess2 == true && chess3 == true && chess4 == true)
        {
            projector.SetActive(true);
        }
        else
        {
            projector.SetActive(false);
        }
    }
}
