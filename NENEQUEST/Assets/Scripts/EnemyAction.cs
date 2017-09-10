using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;

	protected CharacterStatus status;
	protected HpBar hpBar;
	protected int idx;
	protected SubScoreAction subScoreAction;
	protected SpriteRenderer spriteRenderer;
	protected AudioSource audioSource;
	[SerializeField]
	AudioClip[] audioClip;
	protected Renderer shade;
	protected Collider childCollider;

	protected enum State
	{
		Walking,
		Attacking,
		Damaged,
		Died}

	;

	protected State state = State.Walking;

	public void Initialize (int _idx)
	{
		idx = _idx;

		GameObject objHpBar = transform.FindChild ("HpBar").gameObject;
		hpBar = objHpBar.GetComponent<HpBar> ();
		hpBar.characterStatus = GetComponent<CharacterStatus> ();
		hpBar.cfg = cfg;

		EnemyAnimation enemyAnimation = GetComponent<EnemyAnimation> ();
		enemyAnimation.cfg = cfg;

		audioSource = gameObject.AddComponent<AudioSource> ();
		audioSource.playOnAwake = false;
		audioSource.volume = 0.5f;

		GameObject objCollider = transform.FindChild ("Collider").gameObject;
		childCollider = objCollider.GetComponent<Collider> ();
		subScoreAction = transform.FindChild ("SubScore").gameObject.AddComponent<SubScoreAction> ();
		spriteRenderer = transform.FindChild ("Body").GetComponent<SpriteRenderer> ();
		shade = transform.FindChild ("Shade").GetComponent<Renderer> ();

		status = GetComponent<CharacterStatus> ();
		status.HP = cfg.ENEMY_INITIAL_HP [idx];
		hpBar.Initialize ();
		status.velocity = Vector3.right * cfg.ENEMY_SPEED [idx];
		status.power = cfg.ENEMY_INITIAL_POWER [idx];
		enemyAnimation.Initialize (idx);
	}

	// Update is called once per frame
	void Update ()
	{
		if (isPassed ())
			Destroy (gameObject);

		if (IsWalking ())
			Move ();
	}

	protected virtual void Move ()
	{
	}

	bool isPassed ()
	{
		return transform.position.x < 0;
	}

	bool IsWalking ()
	{
		return state == State.Walking;
	}

	bool IsDamaged ()
	{
		return state == State.Damaged;
	}

	void ChangeState (State s)
	{
		state = s;
	}

	protected IEnumerator InvincibleInterval ()
	{
		yield return new WaitForSeconds (0.1f);
	}

	protected IEnumerator KnockBack (int i)
	{
		ChangeState(State.Damaged);
		float KnockBackPower = i / cfg.ENEMY_MASS [idx];
		Vector3 v;
		float a = 5;
		while (KnockBackPower > 0) {
			v = Vector3.right * KnockBackPower;
			transform.position += v * Time.deltaTime;
			KnockBackPower -= a;
			a += 1;
			yield return null;
		}
		ChangeState(State.Walking);
	}

	protected virtual void Damage (int i)
	{
		StartCoroutine (InvincibleInterval ());
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
			if (IsDamaged ())
				StopCoroutine (KnockBack (i));
			StartCoroutine (KnockBack (i));
		}
	}

	protected virtual void Died ()
	{
		ChangeState(State.Died);
		transform.parent.SendMessage ("AddScore", cfg.ENEMY_SCORE [idx]);
		subScoreAction.Show ();
		childCollider.enabled = false;
		StartCoroutine (Disappear ());
	}

	protected virtual IEnumerator Disappear ()
	{
		Destroy (gameObject, cfg.ENEMY_DISAPPEAR_DURATION [idx]);
		shade.enabled = false;
		while (true) {
			transform.position += Vector3.up * 0.8f * Time.deltaTime;
			Color c = spriteRenderer.color;
			c.a -= 1 / cfg.ENEMY_DISAPPEAR_DURATION [idx] * Time.deltaTime * 2;
			spriteRenderer.color = c;
			yield return null;
		}
	}

	public void OnTriggerStay (Collider collider)
	{
		if (collider.tag == "Player")
			collider.gameObject.transform.parent.SendMessage ("Damage", status.power);
	}

	protected void Sound (int i)
	{
		audioSource.PlayOneShot (audioClip [i]);
	}
}
