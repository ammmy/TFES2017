using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField]
	GameObject objFrame, objBack, objFore;
	public Renderer frameRenderer, backRenderer, foreRenderer;
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public CharacterStatus characterStatus;
	float initialHP;
	Vector3 initialLocalScale, initialLocalPosition;
	bool awake = true;

	public void Initialize ()
	{
		if (awake) {
			awake = false;
			initialLocalScale = objFore.transform.localScale;
			initialLocalPosition = objFore.transform.localPosition;
			initialHP = characterStatus.HP;
			initialLocalScale.x = 0;
			initialLocalScale += new Vector3 (initialHP * cfg.HP_BAR_FRAME_LENGTH_UNIT, 0, 0);
			frameRenderer = objFrame.GetComponent<Renderer> ();
			backRenderer = objBack.GetComponent<Renderer> ();
			foreRenderer = objFore.GetComponent<Renderer> ();
		}

		Show ();
		objFrame.transform.localScale = initialLocalScale + new Vector3 (cfg.HP_BAR_FRAME_MERGIN, 0, cfg.HP_BAR_FRAME_MERGIN) * 2;
		objBack.transform.localScale = initialLocalScale;
		objFore.transform.localScale = initialLocalScale;
		objFore.transform.localPosition = initialLocalPosition;
	}

	public void UpdateState ()
	{
		Vector3 s = initialLocalScale;
		s.x *= characterStatus.HP / initialHP;
		objFore.transform.localScale = s;

		Vector3 p = initialLocalPosition;
		p.x -= (initialLocalScale.x - s.x) * 10 / 2;
		objFore.transform.localPosition = p;
	}

	public void Show ()
	{
		changeRenderer (true);
	}

	public void Hide ()
	{
		changeRenderer (false);
	}

	void changeRenderer (bool b)
	{
		frameRenderer.enabled = b;
		backRenderer.enabled = b;
		foreRenderer.enabled = b;
	}
}
