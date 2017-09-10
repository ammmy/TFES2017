using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Action : EnemyAction
{
	[System.NonSerialized]
	public GameObject objTarget;

	protected override void Move ()
	{
		Vector3 nv = status.velocity;
		Vector3 p = transform.position;
		Vector3 tp = objTarget.transform.position;
		float x = p.x;
		float tx = tp.x;
		if (Mathf.Abs (tx - x) < 10 && tx - x < -1)
			nv = (p - tp + new Vector3 (0, 0, Random.Range (-1f, 1f))).normalized * status.velocity.x;
		transform.position -= nv * Time.deltaTime;
		status.velocity.x += Time.deltaTime * 2.5f;
	}
}
