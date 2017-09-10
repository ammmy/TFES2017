using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item0Action : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	AudioSource audioSource;
	int power;
	int idx;
	Vector3 velocity;
	SpriteRenderer spriteRenderer;
	Renderer shade;
	Collider childCollider;

	enum State
	{
		Initializing,
		Walking,
		Acquired}

	;

	State state = State.Initializing;

	public void Initialize ()
	{
		audioSource = GetComponent<AudioSource> ();
		spriteRenderer = transform.FindChild ("Body").GetComponent<SpriteRenderer> ();
		shade = transform.FindChild ("Shade").GetComponent<Renderer> ();
		childCollider = transform.FindChild ("Collider").GetComponent<Collider> ();

		idx = cfg.ITEM_0_IDX;
		velocity = Vector3.left * cfg.ITEM_INITIAL_SPEED [idx];
		power = cfg.ITEM_INITIAL_POWER [idx];
		state = State.Walking;
	}

	// Update is called once per frame
	void Update ()
	{
		if (isPassed ())
			Destroy (gameObject);
		
		if (state == State.Walking)
			Move ();
	}

	void Move ()
	{
		transform.position += velocity * Time.deltaTime;
	}

	void Sound ()
	{
		audioSource.Play ();
	}

	bool isAcquired ()
	{
		return state == State.Acquired;
	}

	bool isPassed ()
	{
		return transform.position.x < 0;
	}

	IEnumerator Disappear ()
	{
		Destroy (gameObject, audioSource.clip.length);
		shade.enabled = false;
		while (true) {
			transform.position += Vector3.up * 0.8f * Time.deltaTime;
			Color c = spriteRenderer.color;
			c.a -= 1 / audioSource.clip.length * Time.deltaTime * 2;
			spriteRenderer.color = c;
			yield return null;
		}
	}

	public void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player") {
			state = State.Acquired;
			childCollider.enabled = false;
			Sound ();
			collider.gameObject.transform.parent.SendMessage ("Heal", power);
			StartCoroutine (Disappear ());
		}
	}
}
