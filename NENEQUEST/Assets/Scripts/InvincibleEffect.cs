using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleEffect : MonoBehaviour
{
	bool isInvincible = false;
	SpriteRenderer bodyRenderer;
	bool flip = false;

	// Use this for initialization
	void Start ()
	{
		GameObject body = transform.FindChild ("Body").gameObject;
		bodyRenderer = body.GetComponent<SpriteRenderer> ();
		bodyRenderer.enabled = false;
	}

	public void StartEffect ()
	{
		isInvincible = true;
		StartCoroutine (Flash ());
	}

	public void StopEffect ()
	{
		isInvincible = false;
	}

	private IEnumerator Flash ()
	{
		while (isInvincible) {
			bodyRenderer.enabled = flip;
			flip = !flip;
			yield return new WaitForSeconds (0.1f);  
		}
		bodyRenderer.enabled = false;
	}
}
