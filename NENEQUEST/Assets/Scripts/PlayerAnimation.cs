using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	public Sprite[] arms, legs;
	GameObject objArm, objLeg;
	SpriteRenderer arm, leg;
	Coroutine armC, legC;
	float legAnimationSpeed;

	// Use this for initialization
	void Start ()
	{
		objArm = transform.FindChild ("Body").FindChild ("Arm").gameObject;
		objLeg = transform.FindChild ("Body").FindChild ("Leg").gameObject;
		arm = objArm.GetComponent<SpriteRenderer> ();
		leg = objLeg.GetComponent<SpriteRenderer> ();
	}

	public void Initialize ()
	{
		InitializeArmSprite ();
		InitializeLegAnimation ();
		StarLegAnimation ();
	}

	void InitializeLegAnimation ()
	{
		leg.sprite = legs [cfg.PLAYER_INITIAL_LEG_SPRITE_IDX];
		legAnimationSpeed = cfg.PLAYER_LEG_ANIMATION_SPEED;
	}

	public void InitializeArmSprite ()
	{
		SetArmSprite (cfg.PLAYER_INITIAL_ARM_SPRITE_IDX);
	}

	public void SetArmSprite (int idx)
	{
		arm.sprite = arms [idx];
	}

	public void StarLegAnimation ()
	{
		legC = StartCoroutine (LegAnimation ());
	}

	public void TerminateLegAnimation ()
	{
		StopCoroutine (legC);
		InitializeLegAnimation ();
	}

	IEnumerator LegAnimation ()
	{
		while (true) {
			foreach (Sprite sprite in legs) {
				leg.sprite = sprite;
				yield return new WaitForSeconds (legAnimationSpeed);
			}
		}
	}

	public void Terminate ()
	{
		TerminateLegAnimation ();
	}
}
