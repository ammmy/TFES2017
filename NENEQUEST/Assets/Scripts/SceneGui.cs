using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGui : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField]
	Renderer titleRenderer, gameOverRenderer, gameClearRenderer;

	// Use this for initialization
	void Start ()
	{
		SetTitle ();
	}

	public void HideAll ()
	{
		titleRenderer.enabled = false;
		gameOverRenderer.enabled = false;
		gameClearRenderer.enabled = false;
	}

	public void SetTitle ()
	{
		HideAll ();
		titleRenderer.enabled = true;
	}

	public void SetGameOver ()
	{
		HideAll ();
		gameOverRenderer.enabled = true;
	}

	public void SetGameClear ()
	{
		HideAll ();
		gameClearRenderer.enabled = true;
	}
}
