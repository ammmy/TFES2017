using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Action : EnemyAction
{
	[System.NonSerialized]
	public GameObject objTarget;
	public EnemyManager enemyManager;
	bool awake = true;
	float velocity;
	Vector3 direction;
	GameObject objFire, objMouse;
	Vector3 fireInitialLocalPosition;
	Vector3[] mouseLocalPosition, mouseLocalEulerAngles;
	SpriteRenderer mouseSpriteRenderer;
	Coroutine fireC;

	protected override void Move ()
	{
		if (awake) {
			awake = false;
			objFire = transform.FindChild ("Fire").gameObject;
			fireInitialLocalPosition = objFire.transform.localPosition;

			objMouse = transform.FindChild ("Mouse").gameObject;
			GameObject objMouse_1 = transform.FindChild ("Mouse_1").gameObject;
			mouseLocalPosition = new Vector3[2];
			mouseLocalEulerAngles = new Vector3[2];
			mouseLocalPosition [0] = objMouse.transform.localPosition;
			mouseLocalPosition [1] = objMouse_1.transform.localPosition;
			mouseLocalEulerAngles [0] = objMouse.transform.localEulerAngles;
			mouseLocalEulerAngles [1] = objMouse_1.transform.localEulerAngles;
			mouseSpriteRenderer = objMouse.GetComponent<SpriteRenderer> ();

			velocity = cfg.ENEMY_SPEED [idx];
			direction = Vector3.left * velocity;
			StartCoroutine (ChangeDirection ());
			fireC = StartCoroutine (Fire ());
			Sound (2);
		}

		transform.position += direction * Time.deltaTime;
		AdjustPosition ();
	}

	private IEnumerator Fire ()
	{
		yield return new WaitForSeconds (2f);
		while (true) {
			objMouse.transform.localPosition = mouseLocalPosition [1];
			objMouse.transform.localEulerAngles = mouseLocalEulerAngles [1];
			yield return new WaitForSeconds (1f);
			float rv = Random.Range (5f, 15f);
			enemyManager.enemy2Garbage.Clear ();
			for (int i = 0; i < Random.Range (15, 25); i++) {
				GameObject _objFire = Instantiate (objFire);
				_objFire.transform.parent = transform;
				_objFire.transform.localPosition = fireInitialLocalPosition;
				_objFire.transform.transform.FindChild ("Body").GetComponent<SpriteRenderer> ().enabled = true;
				enemyManager.enemy2Garbage.Add (_objFire);

				FireAction fireAction = _objFire.GetComponent<FireAction> ();
				Vector3 v = (objTarget.transform.position - _objFire.transform.position + new Vector3 (0, 0, Random.Range (-8f, 8f))).normalized * rv;
				fireAction.Initialize (v, cfg.ENEMY_2_FIRE_INITIAL_POWER);

				_objFire.transform.parent = null;
				fireAction.Fire ();
				yield return new WaitForSeconds (0.1f);
			}

			objMouse.transform.localPosition = mouseLocalPosition [0];
			objMouse.transform.localEulerAngles = mouseLocalEulerAngles [0];
			yield return new WaitForSeconds (Random.Range (2f, 5f));
		}
	}

	private IEnumerator ChangeDirection ()
	{
		while (true) {
			yield return new WaitForSeconds (3f);
			direction = new Vector3 (Random.Range (-1f, 1f), 0, Random.Range (-1f, 1f)).normalized * velocity;
		}
	}

	void AdjustPosition ()
	{
		if (transform.position.x < cfg.FIELD_LEN_X * 0.6f) {
			Vector3 p = transform.position;
			p.x = cfg.FIELD_LEN_X * 0.6f;
			transform.position = p;
		} else if (transform.position.x > cfg.FIELD_LEN_X) {
			Vector3 p = transform.position;
			p.x = cfg.FIELD_LEN_X;
			transform.position = p;
		}

		if (transform.position.z < 0) {
			Vector3 p = transform.position;
			p.z = 0;
			transform.position = p;
		} else if (transform.position.z > cfg.FIELD_LEN_Z) {
			Vector3 p = transform.position;
			p.z = cfg.FIELD_LEN_Z;
			transform.position = p;
		}
	}

	protected override void Damage (int i)
	{
		StartCoroutine (InvincibleInterval ());
		if (i < 50)
			i /= 4;
		else if (i < 100)
			i /= 2;

		status.HP -= i;
		hpBar.UpdateState ();
		Sound (0);
		if (status.HP <= 0) {
			status.HP = 0;
			hpBar.UpdateState ();
			hpBar.Hide ();
			Sound (1);
			Died ();
		} else {
			if (state == State.Damaged)
				StopCoroutine (KnockBack (i));
			StartCoroutine (KnockBack (i));
		}
	}

	protected override IEnumerator Disappear ()
	{
		Destroy (gameObject, cfg.ENEMY_DISAPPEAR_DURATION [idx]);
		shade.enabled = false;
		while (true) {
			transform.position += Vector3.up * 0.8f * Time.deltaTime;
			Color c = spriteRenderer.color;
			c.a -= 1 / cfg.ENEMY_DISAPPEAR_DURATION [idx] * Time.deltaTime * 2;
			spriteRenderer.color = c;
			mouseSpriteRenderer.color = c;
			yield return null;
		}
	}

	protected override void Died ()
	{
		state = State.Died;
		childCollider.enabled = false;
		StopCoroutine (fireC);
		GameObject preparent = transform.parent.gameObject;
		transform.parent = null;
		StartCoroutine (Disappear ());
		preparent.SendMessage ("AddScore", cfg.ENEMY_SCORE [idx]);
		preparent.SendMessage ("GameClear", cfg.ENEMY_SCORE [idx]);
		subScoreAction.Show ();
	}
}
