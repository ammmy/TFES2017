using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHandler : MonoBehaviour
{
	void OnTriggerEnter (Collider collider)
	{
		transform.parent.SendMessage ("OnTriggerEnter", collider, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerStay (Collider collider)
	{
		transform.parent.SendMessage ("OnTriggerStay", collider, SendMessageOptions.DontRequireReceiver);
	}
}
