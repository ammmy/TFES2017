using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public PlayerAttacker playerAttacker;
	[System.NonSerialized]
	public PlayerAction playerAction;
	[System.NonSerialized]
	public PlayerAnimation playerAnimation;
	[System.NonSerialized]
	public PlayerController playerController;
	AudioSource switchWeapon;

	GameObject[] weapons;
	Dictionary<string, int> itemIdx;
	int nowWeaponIdx = 0;
	bool awake = true;

	public void Initialize ()
	{
		if (awake) {
			awake = false;
			weapons = new GameObject[cfg.WEAPON_NUM];
			for (int i = 0; i < cfg.WEAPON_NUM; i++) {
				weapons [i] = transform.FindChild ("Weapon_" + i.ToString ()).gameObject;
				weapons [i].SendMessage ("SetConfig", cfg);
				weapons [i].SendMessage ("SetPlayerAnimation", playerAnimation);
				weapons [i].SendMessage ("SetPlayerController", playerController);
				weapons [i].SendMessage ("SetWeaponManager", this);
				weapons [i].SendMessage ("Prepare");
			}

			itemIdx = new Dictionary<string, int> () {
				{ cfg.KEY_NAME_WEAPON_0, cfg.WEAPON_0_IDX },
				{ cfg.KEY_NAME_WEAPON_1, cfg.WEAPON_1_IDX },
				{ cfg.KEY_NAME_WEAPON_2, cfg.WEAPON_2_IDX }
			};
		}

		for (int i = 0; i < cfg.WEAPON_NUM; i++) {
			weapons [i].SendMessage ("Initialize");
			weapons [i].SendMessage ("Hide");
		}

		nowWeaponIdx = cfg.INITIAL_WEAPON_IDX;
		weapons [nowWeaponIdx].SendMessage ("Show");
		playerAttacker.weapon = weapons [nowWeaponIdx];
		switchWeapon = GetComponent<AudioSource> ();
	}

	public void SwitchWeapon (string key)
	{
		int idx = itemIdx [key];
		if (nowWeaponIdx != idx) {
			switchWeapon.Play ();
			weapons [nowWeaponIdx].SendMessage ("Hide");
			nowWeaponIdx = idx;
			playerAttacker.weapon = weapons [nowWeaponIdx];
			weapons [nowWeaponIdx].SendMessage ("Show");
		}
	}

	public void StartAttacking ()
	{
		playerAction.StartAttacking ();
	}

	public void EndAttacking ()
	{
		playerAction.EndAttacking ();
	}
}
