using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldAnimation : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	GameObject[] Cloud, Mountain;
	int numCloud, numMountain;

	// Use this for initialization
	void Start ()
	{
		GameObject objCloud = transform.FindChild ("Cloud").gameObject;
		GameObject objMountain = transform.FindChild ("Mountain").gameObject;
		numCloud = objCloud.transform.childCount;
		numMountain = objMountain.transform.childCount;
		Cloud = new GameObject[numCloud];
		Mountain = new GameObject[numMountain];

		for (int i = 0; i < numCloud; i++) {
			Cloud [i] = objCloud.transform.FindChild ("Body_" + i.ToString ()).gameObject;
			Cloud [i].AddComponent<CloudAction> ().cfg = cfg;
		}
		for (int i = 0; i < numMountain; i++) {
			Mountain [i] = objMountain.transform.FindChild ("Body_" + i.ToString ()).gameObject;
			Mountain [i].AddComponent<MountainAction> ().cfg = cfg;
		}
	}
}
