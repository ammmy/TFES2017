using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour {
	public GameObject owner;

	// Use this for initialization
	void Start () {
		transform.parent = owner.transform;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
