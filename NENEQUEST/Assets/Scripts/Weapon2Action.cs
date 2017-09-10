using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2Action : WeaponAction
{
	GameObject objBody_0, objBody_2, objArrow;
	Vector3 localPosition_0, localEulerAngles_0, localPosition_2, localEulerAngles_2, localPosition_Arrow, localEulerAngles_Arrow;

	public override void Initialize ()
	{
		idx = cfg.WEAPON_2_IDX;
		base.Initialize ();
		objBody_0 = objBody.transform.FindChild ("Body_0").gameObject;
		localPosition_0 = objBody_0.transform.localPosition;
		localEulerAngles_0 = objBody_0.transform.localEulerAngles;
		objBody_2 = objBody.transform.FindChild ("Body_2").gameObject;
		localPosition_2 = objBody_2.transform.localPosition;
		localEulerAngles_2 = objBody_2.transform.localEulerAngles;
		objArrow = objBody.transform.FindChild ("Arrow").gameObject;
		localPosition_Arrow = objArrow.transform.localPosition;
		localEulerAngles_Arrow = objArrow.transform.localEulerAngles;
	}

	public override IEnumerator Fire ()
	{
		ChangeState (State.Attaking);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_0_IDX);
		objSprite.transform.localPosition = localPosition_0;
		objSprite.transform.localEulerAngles = localEulerAngles_0;
		GameObject _objArrow = Instantiate (objArrow);
		_objArrow.transform.parent = objBody.transform;
		_objArrow.transform.localPosition = localPosition_Arrow;
		_objArrow.transform.localEulerAngles = localEulerAngles_Arrow;
		_objArrow.GetComponent<SpriteRenderer> ().enabled = true;
		GameObject objShade = _objArrow.transform.FindChild ("Shade").gameObject;
		Vector3 shadeP = objShade.transform.position;
		shadeP.y = 0;
		objShade.transform.position = shadeP;
		objShade.GetComponent<Renderer> ().enabled = true;
		GameObject _objCollider = Instantiate (objCollider);
		_objCollider.transform.parent = transform;
		_objCollider.transform.localPosition = objCollider.transform.localPosition;
		_objCollider.transform.localEulerAngles = objCollider.transform.localEulerAngles;
		_objCollider.transform.parent = _objArrow.transform;

		ArrowAction arrowAction = _objArrow.AddComponent<ArrowAction> ();
		arrowAction.Initialize (new Vector3 (10, 0, 0), cfg.WEAPON_INITIAL_POWER [idx], _objCollider, cfg.FIELD_LEN_X * 1.5f);
		yield return new WaitForSeconds (0.1f);

		_objArrow.transform.parent = null;
		arrowAction.Fire ();
		ChangeState (State.Interval);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_2_IDX);
		objSprite.transform.localPosition = localPosition_2;
		objSprite.transform.localEulerAngles = localEulerAngles_2;
		yield return new WaitForSeconds (1.0f);

		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_1_IDX);
		SetInitialPositionRotation ();

		ChangeState (State.Waiting);
	}
}
