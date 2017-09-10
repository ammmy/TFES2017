using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAction : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	Vector3 initialLocalScale;
	Vector3 startLocalPosition;
	Vector3 velocity;
	Vector3[] animationScale, animationAngle;
	int numAnimationScale = 2;
	float startLocalPositionX = 34;
	float endPositionX = -9;

	// Use this for initialization
	void Start ()
	{
		velocity = cfg.CLOUD_INITIAL_SPEED;
		startLocalPosition = transform.localPosition;
		initialLocalScale = transform.localScale;
		startLocalPosition.x = startLocalPositionX;
		animationScale = new Vector3[numAnimationScale];
		animationAngle = new Vector3[numAnimationScale];
		SetRandomAnimationScale ();
		SetRandomAnimationAngle ();
		StartCoroutine (Move ());
		StartCoroutine (AnimateScaleAngle ());
	}

	void ToLeft ()
	{
		transform.position += velocity * Time.deltaTime;
	}

	bool IsPassed ()
	{
		return transform.position.x < endPositionX;
	}

	void SetStartPosition ()
	{
		transform.localPosition = startLocalPosition;
	}

	void SetRandomPosY ()
	{
		Vector3 position = transform.position;
		float y = Random.Range (3f, 8f);
		position.y = y;
		transform.position = position;
	}

	void SetRandomScale ()
	{
		Vector3 scale = initialLocalScale;
		float r = Random.Range (0.8f, 1.2f);
		scale *= r;
		transform.localScale = scale;
	}

	void SetRandomAnimationScale ()
	{
		for (int i = 0; i < numAnimationScale; i++)
			animationScale [i] = transform.localScale * Random.Range (0.8f, 1.2f);
	}

	void SetRandomAnimationAngle ()
	{
		for (int i = 0; i < numAnimationScale; i++)
			animationAngle [i] = new Vector3 (0, 0, Random.Range (-5f, 5f));
	}

	void Initialize ()
	{
		SetStartPosition ();
		SetRandomPosY ();
		SetRandomScale ();
		SetRandomAnimationScale ();
		SetRandomAnimationAngle ();
	}

	IEnumerator AnimateScaleAngle ()
	{
		while (true) {
			for (int i = 0; i < numAnimationScale; i++) {
				transform.localScale = animationScale [i];
				transform.eulerAngles = animationAngle [i];
				yield return new WaitForSeconds (1f);
			}
		}
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
