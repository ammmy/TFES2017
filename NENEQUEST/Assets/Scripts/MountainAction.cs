using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainAction : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	float startPositionX = 2.2f;
	Vector3 velocity;
	float endPositionX = -17.93f;

	// Use this for initialization
	void Start ()
	{
		velocity = cfg.MOUNTAIN_INITIAL_SPEED;
		// StartCoroutine (Move ()); // TODO
	}

	void Initialize ()
	{
		SetStartPosition ();
	}

	void SetStartPosition ()
	{
		Vector3 position = transform.position;
		position.x = startPositionX;
		transform.position = position;
	}

	void ToLeft ()
	{
		transform.position += velocity * Time.deltaTime;
	}

	bool IsPassed ()
	{
		return transform.position.x < endPositionX;
	}

	IEnumerator Move ()
	{
		while (true) {
			ToLeft ();
			if (IsPassed ())
				Initialize ();
			yield return null;
		}
	}
}
