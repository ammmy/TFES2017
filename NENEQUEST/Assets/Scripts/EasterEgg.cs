using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
	public Sprite sprite;
	public AudioClip[] audioClip;
	bool running = false;
	Coroutine eggC, intervalC;
	int remainCount = 5;
	int comboTime = 3;
	GameObject objAts;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("EasterEgg_0") && !running && remainCount > 0)
			eggC = StartCoroutine (Egg ());
	}

	void DestroyAts ()
	{
		StopCoroutine (eggC);
		Destroy (objAts.gameObject);
		running = false;
	}

	IEnumerator Egg ()
	{
		running = true;
		remainCount--;
		if (intervalC != null)
			StopCoroutine (intervalC);
		
		objAts = new GameObject ("Ats");
		objAts.transform.parent = transform;
		objAts.transform.localPosition = new Vector3 (13, -7, 15);
		objAts.transform.localScale = Vector3.one * 2;
		objAts.AddComponent<SpriteRenderer> ();
		objAts.AddComponent<AudioSource> ();

		int audioIdx = Random.Range (0, 2);
		objAts.GetComponent<SpriteRenderer> ().sprite = sprite;
		objAts.GetComponent<AudioSource> ().PlayOneShot (audioClip [audioIdx]);
		intervalC = StartCoroutine (EggInterval ());
		yield return new WaitForSeconds (audioClip [audioIdx].length);
		DestroyAts ();
	}

	IEnumerator EggInterval ()
	{
		float time = Time.time;
		while (Time.time - time < comboTime) {
			if (Input.GetButtonDown ("EasterEgg_9")) {
				GameObject objAtsSub = new GameObject ("AtsSub");
				objAtsSub.transform.parent = transform;
				objAtsSub.AddComponent<AudioSource> ().PlayOneShot (audioClip [2]);
				DestroyAts ();
				Destroy (objAtsSub.gameObject, audioClip [2].length);
				break;
			}
			yield return null;
		}
	}
}
