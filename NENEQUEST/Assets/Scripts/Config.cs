using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
	[System.NonSerialized]
	public float FIELD_LEN_X = 25;
	[System.NonSerialized]
	public float FIELD_LEN_Z = 15;

	[System.NonSerialized]
	public Vector3 PLAYER_INITIAL_SPEED = new Vector3 (15 / 2f, 0, 25 / 2f);
	[System.NonSerialized]
	public int PLAYER_INITIAL_HP = 150;
	[System.NonSerialized]
	public Vector3 PLAYER_INITIAL_POSITION = new Vector3 (0, 0, 0);
	[System.NonSerialized]
	public float PLAYER_INVINCIBLE_TIME = 0.5f;
	[System.NonSerialized]
	public float PLAYER_LEG_ANIMATION_SPEED = 0.15f;

	[System.NonSerialized]
	public int PLAYER_INITIAL_ARM_SPRITE_IDX = 1;
	[System.NonSerialized]
	public int PLAYER_INITIAL_LEG_SPRITE_IDX = 0;
	[System.NonSerialized]
	public int PLAYER_ARM_SPRITE_0_IDX = 0;
	[System.NonSerialized]
	public int PLAYER_ARM_SPRITE_1_IDX = 1;
	[System.NonSerialized]
	public int PLAYER_ARM_SPRITE_2_IDX = 2;

	[System.NonSerialized]
	public int ENEMY_TYPE_NUM = 3;
	[System.NonSerialized]
	public int ENEMY_0_IDX = 0;
	[System.NonSerialized]
	public int ENEMY_1_IDX = 1;
	[System.NonSerialized]
	public int ENEMY_2_IDX = 2;
	[System.NonSerialized]
	// public int[,] ENEMY_NUM = { { 1, 1, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 1 } };
	public int[,] ENEMY_NUM = { { 4, 0, 0 }, { 10, 5, 0 }, { 7, 7, 0 }, { 5, 6, 0 }, { 0, 0, 1 } };
	[System.NonSerialized]
	public float[] ENEMY_SPEED = { 8, 2, 1 };
	[System.NonSerialized]
	public int[] ENEMY_INITIAL_HP = { 50, 80, 300 };
	[System.NonSerialized]
	public int[] ENEMY_INITIAL_POWER = { 30, 50, 5 };
	[System.NonSerialized]
	public int ENEMY_2_FIRE_INITIAL_POWER = 20;
	[System.NonSerialized]
	public float[] ENEMY_MASS = { 3, 10, 20 };
	[System.NonSerialized]
	public int[] ENEMY_SCORE = { 1, 3, 20 };
	[System.NonSerialized]
	public float[] ENEMY_ANIMATION_SPEED = { 0.2f, 0.5f, 0.5f };
	[System.NonSerialized]
	public float[] ENEMY_DISAPPEAR_DURATION = { 1f, 1f, 3f };


	[System.NonSerialized]
	public Vector3 CLOUD_INITIAL_SPEED = new Vector3 (-0.5f, 0f, 0f);
	[System.NonSerialized]
	public Vector3 MOUNTAIN_INITIAL_SPEED = new Vector3 (-1f, 0f, 0f);

	[System.NonSerialized]
	public int INITIAL_WEAPON_IDX = 0;
	[System.NonSerialized]
	public int WEAPON_NUM = 3;
	[System.NonSerialized]
	public int WEAPON_0_IDX = 0;
	[System.NonSerialized]
	public int WEAPON_1_IDX = 1;
	[System.NonSerialized]
	public int WEAPON_2_IDX = 2;
	[System.NonSerialized]
	public int[] WEAPON_INITIAL_POWER = { 35, 80, 45 };
	[System.NonSerialized]
	public float[] WEAPON_INITIAL_MASS = { 2, 5, 1 };

	[System.NonSerialized]
	public int ITEM_NUM = 1;
	[System.NonSerialized]
	public int ITEM_0_IDX = 0;
	[System.NonSerialized]
	public float ITEM_0_FIRST_DELAY = 20;
	[System.NonSerialized]
	public int[] ITEM_INITIAL_SPEED = { 3 };
	[System.NonSerialized]
	public int[] ITEM_INITIAL_POWER = { 50 };

	[System.NonSerialized]
	public float HP_BAR_FRAME_LENGTH_UNIT = 0.0028f;
	[System.NonSerialized]
	public float HP_BAR_FRAME_MERGIN = 0.01f;

	[System.NonSerialized]
	public string HIGH_SCORE_VALUE_KEY = "HIGH_SCORE_VALUE";
	[System.NonSerialized]
	public string HIGH_SCORE_TIME_KEY = "HIGH_SCORE_TIME";

	[System.NonSerialized]
	public string KEY_NAME_UP = "Up";
	[System.NonSerialized]
	public string KEY_NAME_DOWN = "Down";
	[System.NonSerialized]
	public string KEY_NAME_RIGHT = "Right";
	[System.NonSerialized]
	public string KEY_NAME_LEFT = "Left";
	[System.NonSerialized]
	public string KEY_NAME_ATTACK = "Attack";
	[System.NonSerialized]
	public string KEY_NAME_WEAPON_0 = "Weapon_0";
	[System.NonSerialized]
	public string KEY_NAME_WEAPON_1 = "Weapon_1";
	[System.NonSerialized]
	public string KEY_NAME_WEAPON_2 = "Weapon_2";
	[System.NonSerialized]
	public string KEY_NAME_PAUSE = "Pause";
	[System.NonSerialized]
	public string KEY_NAME_ENTER = "Enter";
	[System.NonSerialized]
	public string KEY_NAME_ESC = "Esc";
	[System.NonSerialized]
	public string KEY_NAME_DEBUG = "Debug";

	[System.NonSerialized]
	public string[] KEY_NAMES_MOVING;

	[System.NonSerialized]
	public string[] KEY_NAMES_WEAPON;

	public void Initialize ()
	{
		KEY_NAMES_MOVING = new string[] {
			KEY_NAME_UP,
			KEY_NAME_DOWN,
			KEY_NAME_RIGHT,
			KEY_NAME_LEFT
		};

		KEY_NAMES_WEAPON = new string[] {
			KEY_NAME_WEAPON_0,
			KEY_NAME_WEAPON_1,
			KEY_NAME_WEAPON_2
		};
	}
}
