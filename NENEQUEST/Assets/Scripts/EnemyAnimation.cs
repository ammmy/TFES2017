using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	[SerializeField]
	Sprite[] sprites;
	GameObject objSprite;
	SpriteRenderer spriteRenderer;
	float animationSpeed;

	public void Initialize (int idx)
	{
		objSprite = transform.FindChild ("Body").gameObject;
		spriteRenderer = objSprite.GetComponent<SpriteRenderer> ();
		animationSpeed = cfg.ENEMY_ANIMATION_SPEED [idx];
		StartCoroutine (Animate ());
	}

	IEnumerator Animate ()
	{
		while (true) {
			foreach (Sprite sprite in sprites) {
				spriteRenderer.sprite = sprite;
				yield return new WaitForSeconds (animationSpeed);
			}
		}
	}
}
