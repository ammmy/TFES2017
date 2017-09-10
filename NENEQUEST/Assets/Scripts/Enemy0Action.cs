using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0Action : EnemyAction
{
	protected override void Move ()
	{
		transform.position -= status.velocity * Time.deltaTime;
	}
}
