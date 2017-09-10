using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	[System.NonSerialized]
	public GameManager gameManager;
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public PlayerAttacker playerAttacker;
	[System.NonSerialized]
	public WeaponManager weaponManager;
	[System.NonSerialized]
	public CharacterStatus status;
	Collider childCollider;
	Renderer colliderRenderer;
	AudioSource audioSource;

	InvincibleEffect invincibleEffect;
	HpBar hpBar;

	enum State
	{
		Walking,
		Attacking,
		Damaged,
		Died}

	;

	State state;

	// Use this for initialization
	void Start ()
	{
		childCollider = transform.FindChild ("Collider").GetComponent<Collider> ();
		invincibleEffect = transform.FindChild ("InvincibleMarker").GetComponent<InvincibleEffect> ();
		colliderRenderer = transform.FindChild ("Collider").GetComponent<Renderer> ();

		GameObject objHpBar = transform.FindChild ("HpBar").gameObject;
		hpBar = objHpBar.GetComponent<HpBar> ();
		hpBar.characterStatus = GetComponent<CharacterStatus> ();
		audioSource = GetComponent<AudioSource> ();
	}

	public void Initialize ()
	{
		hpBar.cfg = cfg;
		status.HP = cfg.PLAYER_INITIAL_HP;
		ChangeState (State.Walking);
		transform.position = cfg.PLAYER_INITIAL_POSITION;
		hpBar.Initialize ();
		TerminateInvincibleInterval ();
	}

	void TerminateInvincibleInterval ()
	{
		StopCoroutine (InvincibleInterval ());
		invincibleEffect.stopEffect ();
		childCollider.enabled = true;
	}

	private IEnumerator InvincibleInterval ()
	{
		ChangeState (State.Damaged);
		invincibleEffect.startEffect ();
		childCollider.enabled = false;
		yield return new WaitForSeconds (cfg.PLAYER_INVINCIBLE_TIME);
		invincibleEffect.stopEffect ();
		childCollider.enabled = true;
		ChangeState (State.Walking);
	}

	public bool IsWalking ()
	{
		return state == State.Walking;
	}

	public bool IsAttacking ()
	{
		return state == State.Attacking;
	}

	public bool IsDamaged ()
	{
		return state == State.Damaged;
	}

	void ChangeState (State s)
	{
		state = s;
	}

	public void SwitchWeapon (string key)
	{
		if (!IsAttacking ())
			weaponManager.SwitchWeapon (key);
	}

	public void StartAttacking ()
	{
		ChangeState (State.Attacking);
	}

	public void EndAttacking ()
	{
		ChangeState (State.Walking);
	}

	public void Attack ()
	{
		if (IsWalking ())
			playerAttacker.Attack ();
	}

	void Heal (int i)
	{
		if (state == State.Died)
			return;
		status.HP += i;
		if (status.HP > cfg.PLAYER_INITIAL_HP)
			status.HP = cfg.PLAYER_INITIAL_HP;
		hpBar.UpdateState ();
	}

	void Damage (int i)
	{
		if (state == State.Died)
			return;
		StartCoroutine (InvincibleInterval ());
		status.HP = Mathf.Max (status.HP - i, 0);
		hpBar.UpdateState ();
		// Sound (); // Noisy
		if (status.HP == 0)
			Died ();
	}

	void Died ()
	{
		status.HP = 0;
		ChangeState (State.Died);
		gameManager.gameObject.transform.SendMessage ("GameOver");
	}

	public void Debugging (bool debug)
	{
		colliderRenderer.enabled = debug;
	}
	protected void Sound ()
	{
		audioSource.Play();
	}
}
