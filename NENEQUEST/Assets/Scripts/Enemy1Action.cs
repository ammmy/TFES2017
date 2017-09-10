using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Action : EnemyAction
{
	[System.NonSerialized]
	public GameObject objTarget;
	float velocityIncreaseCoefficient = 2.5f;

	protected override void Move ()
	{
		Vector3 velocity = status.velocity;
		Vector3 position = transform.position;
		Vector3 targetPosition = objTarget.transform.position;
		float x = position.x;
		float targetX = targetPosition.x;
		if (Mathf.Abs (targetX - x) < 10 && targetX - x < -1) // TODO
			velocity = (position - targetPosition + Vector3.forward * Random.Range (-1f, 1f)).normalized * status.velocity.x; // TODO
		transform.position -= velocity * Time.deltaTime;
		status.velocity.x += Time.deltaTime * velocityIncreaseCoefficient;
	}
}
