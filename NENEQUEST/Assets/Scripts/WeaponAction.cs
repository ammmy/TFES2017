using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction : MonoBehaviour
{
	[System.NonSerialized]
	public PlayerAnimation playerAnimation;
	protected Config cfg;
	protected GameObject objCollider;
	protected Collider childCollider;
	protected Renderer colliderRenderer;
	protected int power, idx;
	protected float mass;
	protected GameObject objBody, objSprite;
	SpriteRenderer spriteRenderer;
	WeaponManager weaponManager;
	PlayerController playerController;
	protected Vector3 initialLocalPosition, initialLocalEulerAngles;

	protected enum State
	{
		Waiting,
		Attaking,
		Interval}

	;

	[SerializeField]
	State state;

	public void Prepare ()
	{
		objCollider = transform.FindChild ("Collider").gameObject;
		childCollider = objCollider.GetComponent<Collider> ();
		childCollider.enabled = false;
		colliderRenderer = objCollider.GetComponent<Renderer> ();
		colliderRenderer.enabled = false;
		objBody = transform.FindChild ("Body").gameObject;
		objSprite = objBody.transform.FindChild ("Body").gameObject;
		spriteRenderer = objSprite.GetComponent<SpriteRenderer> ();
		initialLocalPosition = objSprite.transform.localPosition;
		initialLocalEulerAngles = objSprite.transform.localEulerAngles;
	}

	public virtual void Initialize ()
	{
		state = State.Waiting;
		power = cfg.WEAPON_INITIAL_POWER [idx];
		mass = cfg.WEAPON_INITIAL_MASS [idx];
	}

	protected void SetInitialPositionRotation ()
	{
		objSprite.transform.localPosition = initialLocalPosition;
		objSprite.transform.localEulerAngles = initialLocalEulerAngles;
	}

	bool IsWaiting ()
	{
		return state == State.Waiting;
	}

	bool IsAttacking ()
	{
		return state == State.Attaking;
	}

	protected void ChangeState (State s)
	{
		if (!IsAttacking () && s == State.Attaking)
			playerController.ChangeSpeed (1 / cfg.WEAPON_INITIAL_MASS [idx]);
		if (IsAttacking () && s != State.Attaking)
			playerController.RestoreSpeed ();
		state = s;
	}

	void SetConfig (Config c)
	{
		cfg = c;
	}

	void SetWeaponManager (WeaponManager instance)
	{
		weaponManager = instance;
	}

	void SetPlayerAnimation (PlayerAnimation instance)
	{
		playerAnimation = instance;
	}

	void SetPlayerController (PlayerController instance)
	{
		playerController = instance;
	}

	void Attack ()
	{
		if (IsWaiting ())
			StartCoroutine (AttackCoroutine ());
	}

	public void Hide ()
	{
		spriteRenderer.enabled = false;
	}

	public void Show ()
	{
		spriteRenderer.enabled = true;
	}

	public virtual IEnumerator AttackCoroutine ()
	{
		weaponManager.StartAttacking ();
		StartCoroutine (Fire ());
		while (IsAttacking ())
			yield return null;
		weaponManager.EndAttacking ();
	}

	public virtual IEnumerator Fire ()
	{
		yield return null;
	}

	public void OnTriggerEnter (Collider collider)
	{
		collider.gameObject.transform.parent.SendMessage ("Damage", power);
	}
}
