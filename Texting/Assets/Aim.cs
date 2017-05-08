using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
	public GameObject target;

	// Use this for initialization
	void Start () {
		transform.parent = target.transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
