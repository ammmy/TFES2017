using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesightCollider : MonoBehaviour {
	EyesightController eyesightController;
	
	// Use this for initialization
	void Start () {
		GameObject objColliderTriggerParent = gameObject.transform.parent.gameObject;
		eyesightController = objColliderTriggerParent.GetComponent<EyesightController>();
	}
	
	void OnTriggerEnter(Collider collider){
		eyesightController.RelayOnTriggerEnter(collider);
	}
	
	void OnTriggerStay(Collider collider){
		eyesightController.RelayOnTriggerStay(collider);
	}
	
	void OnTriggerExit(Collider collider){
		eyesightController.RelayOnTriggerExit(collider);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
