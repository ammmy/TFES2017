using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScoreAction : MonoBehaviour
{
	float time;
	SpriteRenderer spriteRenderer;

	public void Show ()
	{
		transform.parent = null;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.enabled = true;
		time = Time.time;
		StartCoroutine (Effect ());
	}

	protected IEnumerator Effect ()
	{
		while (Time.time - time < 1) {
			transform.position += Vector3.up * 0.8f * Time.deltaTime;
			Color c = spriteRenderer.color;
			c.a -= 0.9f * Time.deltaTime;
			spriteRenderer.color = c;
			yield return null;
		}
		Destroy (gameObject);
	}
}
