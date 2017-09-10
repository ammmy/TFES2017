using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGui : MonoBehaviour
{
	public Sprite[] digits;
	public Sprite unit;
	GameObject[] panels;
	int NumberOfDigits = 5;
	SpriteRenderer[] digitsSpriteRenderer;
	SpriteRenderer highScore;

	// Use this for initialization
	void Start ()
	{
		panels = new GameObject[NumberOfDigits + 1];
		digitsSpriteRenderer = new SpriteRenderer[NumberOfDigits + 1];
		for (int i = 0; i < NumberOfDigits; i++) {
			panels [i] = transform.FindChild ("Digit_" + i.ToString ()).gameObject;
			digitsSpriteRenderer [i] = panels [i].GetComponent<SpriteRenderer> ();
		}
		panels [NumberOfDigits] = transform.FindChild ("Unit").gameObject;
		digitsSpriteRenderer [NumberOfDigits] = panels [NumberOfDigits].GetComponent<SpriteRenderer> ();
		highScore = transform.FindChild ("HighScore").GetComponent<SpriteRenderer> ();
		Hide ();
	}

	public void Initialize ()
	{
		HideHighScore ();
	}

	public void Show ()
	{
		SwitchRenderer (true);
	}

	public void Hide ()
	{
		SwitchRenderer (false);
		highScore.enabled = false;
	}

	public void ShowHighScore ()
	{
		highScore.enabled = true;
	}

	public void HideHighScore ()
	{
		highScore.enabled = false;
	}

	void SwitchRenderer (bool b)
	{
		for (int i = 0; i < NumberOfDigits + 1; i++)
			digitsSpriteRenderer [i].enabled = b;
	}

	public void SetDigits (int n)
	{
		int[] eachDigit = new int[NumberOfDigits];
		for (int i = NumberOfDigits - 1; i >= 0; i--) {
			eachDigit [i] = n % 10;
			n /= 10;
		}
		bool space = true;
		for (int i = 0; i < NumberOfDigits; i++) {
			if (eachDigit [i] != 0)
				space = false;
			if (space && i != NumberOfDigits - 1)
				digitsSpriteRenderer [i].sprite = digits [10];
			else
				digitsSpriteRenderer [i].sprite = digits [eachDigit [i]];
		}
	}
}
