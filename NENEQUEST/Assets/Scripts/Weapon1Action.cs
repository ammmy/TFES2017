using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1Action : WeaponAction
{
	GameObject objBody_0, objBody_2;
	Vector3 localPosition_0, localEulerAngles_0, localPosition_2, localEulerAngles_2;

	public override void Initialize ()
	{
		idx = cfg.WEAPON_1_IDX;
		base.Initialize ();
		objBody_0 = objBody.transform.FindChild ("Body_0").gameObject;
		objBody_2 = objBody.transform.FindChild ("Body_2").gameObject;
		localPosition_0 = objBody_0.transform.localPosition;
		localEulerAngles_0 = objBody_0.transform.localEulerAngles;
		localPosition_2 = objBody_2.transform.localPosition;
		localEulerAngles_2 = objBody_2.transform.localEulerAngles;
	}

	public override IEnumerator Fire ()
	{
		ChangeState (State.Attaking);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_0_IDX);
		objSprite.transform.localPosition = localPosition_0;
		objSprite.transform.localEulerAngles = localEulerAngles_0;
		// childCollider.enabled = colliderRenderer.enabled = true;
		yield return new WaitForSeconds (1.0f);

		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_2_IDX);
		objSprite.transform.localPosition = localPosition_2;
		objSprite.transform.localEulerAngles = localEulerAngles_2;
		childCollider.enabled = true;
		// colliderRenderer.enabled = true;
		yield return new WaitForSeconds (0.1f);

		childCollider.enabled = false;
		// colliderRenderer.enabled = false;
		yield return new WaitForSeconds (0.4f);

		ChangeState (State.Interval);
		playerAnimation.SetArmSprite (cfg.PLAYER_ARM_SPRITE_1_IDX);
		SetInitialPositionRotation ();
		yield return new WaitForSeconds (0.1f);

		ChangeState (State.Waiting);
	}
}
