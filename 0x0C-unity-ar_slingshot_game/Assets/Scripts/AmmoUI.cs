using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public GameObject[] ammo;

	public void Reload()
	{
		for (int i = 0; i < ammo.Length; i++)
		{
			ammo[i].SetActive(true);
		}
	}

	public void UpdateAmount(int count)
	{
		for (int i = 0; i < ammo.Length; i++)
		{
			if (i >= count)
				ammo[i].SetActive(false);
		}
	}
}
