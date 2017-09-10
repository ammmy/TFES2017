using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon0Action : WeaponAction
{
	GameObject objBody_2;
	Vector3 localPosition_2, localEulerAngles_2;

	public override void Initialize ()
	{
		idx = cfg.WEAPON_0_IDX;
		base.Initialize ();
		objBody_2 = objBody.transform.FindChild ("Body_2").gameObject;
		localPosition_2 = objBody_2.transform.localPosition;
		localEulerAngles_2 = objBody_2.transform.localEulerAngles;
	}

	public override IEnumerator Fire ()
	{
		ChangeState (State.Attaking);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_2_IDX);
		objSprite.transform.localPosition = localPosition_2;
		objSprite.transform.localEulerAngles = localEulerAngles_2;
		childCollider.enabled = true;
		// colliderRenderer.enabled = true; // TODO
		yield return new WaitForSeconds (0.1f);

		ChangeState (State.Interval);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_1_IDX);
		SetInitialPositionRotation ();
		childCollider.enabled = false;
		// colliderRenderer.enabled = false; // TODO
		yield return new WaitForSeconds (0.1f);

		ChangeState (State.Waiting);
	}
}
