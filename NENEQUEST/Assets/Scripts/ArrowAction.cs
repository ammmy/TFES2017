using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAction : MonoBehaviour
{
	Vector3 velocity;
	int power;
	GameObject child;
	float xLimit;

	public void Initialize (Vector3 _velocity, int _power, GameObject _child, float _xLimit)
	{
		velocity = _velocity;
		power = _power;
		child = _child;
		xLimit = _xLimit;
	}

	public void Fire ()
	{
		child.GetComponent<Collider> ().enabled = true;
		// child.GetComponent<Renderer> ().enabled = true; // TODO
		StartCoroutine (Move ());
	}

	IEnumerator Move ()
	{
		while (true) {
			if (isPassed ())
				Destroy (gameObject);
			transform.position += velocity * Time.deltaTime;
			yield return null;
		}
	}

	bool isPassed ()
	{
		return transform.position.x > xLimit;
	}

	public void OnTriggerEnter (Collider collider)
	{
		collider.gameObject.transform.parent.SendMessage ("Damage", power);
		Destroy (gameObject);
	}
}
