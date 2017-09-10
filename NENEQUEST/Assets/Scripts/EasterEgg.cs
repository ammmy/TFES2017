using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
	public Sprite sprite;
	public AudioClip[] audioClip;
	bool running = false;
	Coroutine intervalC;
	int remainCount = 5;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("EasterEgg_0") && !running && remainCount > 0)
			StartCoroutine (Egg ());
	}

	IEnumerator Egg ()
	{
		running = true;
		remainCount--;
		GameObject objAts = new GameObject ("Ats");
		objAts.transform.parent = transform;
		objAts.transform.localPosition = new Vector3 (13, -7, 15);
		objAts.transform.localScale = Vector3.one * 2;
		objAts.AddComponent<SpriteRenderer> ().sprite = sprite;
		int audioIdx = Random.Range (0, 2);
		objAts.AddComponent<AudioSource> ().PlayOneShot (audioClip[audioIdx]);
		if (intervalC != null)
			StopCoroutine (intervalC);
		intervalC = StartCoroutine(EggInterval ());
		yield return new WaitForSeconds(audioClip[audioIdx].length);
		Destroy (objAts.gameObject);
		running = false;
	}

	IEnumerator EggInterval ()
	{
		float time = Time.time;
		while (Time.time - time < 3) {
			if (Input.GetButtonDown ("EasterEgg_9")) {
				GameObject objAts = new GameObject ("Ats");
				objAts.transform.parent = transform;
				objAts.AddComponent<AudioSource> ().PlayOneShot (audioClip[2]);
				break;
			}
			yield return null;
		}
	}
}
