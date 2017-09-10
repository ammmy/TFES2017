using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public GameObject weapon;

	public void Attack ()
	{
		weapon.transform.SendMessage ("Attack");
	}
}
