using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAction : MonoBehaviour
{
	Vector3 velocity;
	int power;
	GameObject child, shade;
	float offset = 5;

	public void Initialize (Vector3 _velocity, int _power)
	{
		velocity = _velocity;
		power = _power;
		child = transform.FindChild ("Collider").gameObject;
		shade = transform.FindChild ("Shade").gameObject;
		shade.GetComponent<Renderer> ().enabled = true;
	}

	public void Fire ()
	{
		child.GetComponent<Collider> ().enabled = true;
		// child.GetComponent<Renderer> ().enabled = true;
		StartCoroutine (Move ());
	}

	IEnumerator Move ()
	{
		while (true) {
			if (isPassed ())
				Destroy (gameObject);
			transform.position += velocity * Time.deltaTime;
			Vector3 p = shade.transform.position;
			p.y = Mathf.Min (0, transform.position.y);
			shade.transform.position = p;

			transform.localScale -= transform.localScale * Random.Range(-0.1f, 0.3f) * Time.deltaTime;
			yield return null;
		}
	}

	bool isPassed ()
	{
		return transform.position.x < 0 - offset || transform.position.y < 0 - offset;
	}

	public void OnTriggerEnter (Collider collider)
	{
		collider.gameObject.transform.parent.SendMessage ("Damage", power);
		Destroy (gameObject);
	}
}
