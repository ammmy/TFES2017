using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesightController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RelayOnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log ("Enter");
		}
	}

	public void RelayOnTriggerStay(Collider collider){
		Debug.Log (LayerMask.LayerToName(collider.gameObject.layer));
		if (LayerMask.LayerToName(collider.gameObject.layer) == "Player") {
			Debug.Log ("Stay");
		}
	}

	public void RelayOnTriggerExit(Collider collider){
		if (collider.gameObject.tag == "Player") {
			Debug.Log ("Exit");
		}
	}

}
