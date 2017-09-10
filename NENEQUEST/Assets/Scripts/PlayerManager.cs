using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	[System.NonSerialized]
	public GameManager gameManager;
	[System.NonSerialized]
	public Config cfg;

	GameObject objWeaponManager;

	PlayerAction playerAction;
	PlayerController playerController;
	PlayerAttacker playerAttacker;
	PlayerAnimation playerAnimation;
	WeaponManager weaponManager;
	CharacterStatus status;
	// AudioSource playerDamaged; // Noisy

	// Use this for initialization
	void Start ()
	{
		objWeaponManager = transform.FindChild ("WeaponManager").gameObject;

		playerAction = GetComponent<PlayerAction> ();
		playerAttacker = GetComponent<PlayerAttacker> ();
		playerController = GetComponent<PlayerController> ();
		playerAnimation = GetComponent<PlayerAnimation> ();
		status = GetComponent<CharacterStatus> ();
		// playerDamaged = GetComponent<AudioSource> (); // Noisy

		weaponManager = objWeaponManager.GetComponent<WeaponManager> ();

		playerAction.cfg = cfg;
		playerAction.playerAttacker = playerAttacker;
		playerAction.weaponManager = weaponManager;
		playerAction.gameManager = gameManager;
		playerAction.status = status;

		playerController.cfg = cfg;
		playerController.playerAction = playerAction;
		playerController.status = status;

		playerAttacker.cfg = cfg;

		weaponManager.cfg = cfg;
		weaponManager.playerAction = playerAction;
		weaponManager.playerAttacker = playerAttacker;
		weaponManager.playerAnimation = playerAnimation;
		weaponManager.playerController = playerController;

		playerAnimation.cfg = cfg;
	}

	public void Initialize ()
	{
		playerAction.Initialize ();
		playerController.Initialize ();
		weaponManager.Initialize ();
		playerAnimation.Initialize ();
	}

	public void Terminate ()
	{
		playerController.Terminate ();
		playerAnimation.Terminate ();
	}

	public void Debugging (bool debug)
	{
		playerAction.Debugging (debug);
	}
}
