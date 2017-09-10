using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public PlayerAction playerAction;
	[System.NonSerialized]
	public CharacterStatus status;

	Dictionary<string, Vector3> keyMoving;
	string PushedKeyMoving = "";

	bool isActive;

	// Use this for initialization
	void Start ()
	{
		keyMoving = new Dictionary<string, Vector3> () {
			{ "Up", new Vector3 (0, 0, 1) },
			{ "Down", new Vector3 (0, 0, -1) },
			{ "Right", new Vector3 (1, 0, 0) },
			{ "Left", new Vector3 (-1, 0, 0) }
		};
	}

	public void Initialize ()
	{
		isActive = true;
		status.velocity = cfg.PLAYER_INITIAL_SPEED;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!isActive)
			return;

		InputCheckMoving ();
		InputCheckItem ();

		if (!(PushedKeyMoving == ""))
			Move (PushedKeyMoving);

		if (Input.GetButtonDown ("Attack") && playerAction.IsWalking ())
			playerAction.Attack ();

		AdjustPosition ();
	}

	// どの移動キーが押されたかを検出
	void InputCheckMoving ()
	{
		if (!Input.anyKey) {
			PushedKeyMoving = "";
			return;
		}

		foreach (string key in cfg.KEY_NAMES_MOVING) {
			if (Input.GetButtonDown (key)) {
				PushedKeyMoving = key;
				break;
			}
			if (Input.GetButton (key))
			if (PushedKeyMoving == "")
				PushedKeyMoving = key;
			if (Input.GetButtonUp (key))
			if (PushedKeyMoving == key)
				PushedKeyMoving = "";
		}
	}

	void InputCheckItem ()
	{
		if (!Input.anyKeyDown)
			return;

		foreach (string key in cfg.KEY_NAMES_WEAPON) {
			if (Input.GetButtonDown (key)) {
				playerAction.SwitchWeapon (key);
				return;
			}
		}
	}

	void Move (string key)
	{
		transform.position += Vector3.Scale (keyMoving [key], status.velocity) * Time.deltaTime;
	}

	public void ChangeSpeed (float r)
	{
		status.velocity *= r;
	}

	public void RestoreSpeed ()
	{
		status.velocity = cfg.PLAYER_INITIAL_SPEED;
	}

	public void Terminate ()
	{
		isActive = false;
	}

	// 画面の範囲外に出ないように
	void AdjustPosition ()
	{
		if (transform.position.x < 0) {
			Vector3 p = transform.position;
			p.x = 0;
			transform.position = p;
		} else if (transform.position.x > cfg.FIELD_LEN_X) {
			Vector3 p = transform.position;
			p.x = cfg.FIELD_LEN_X;
			transform.position = p;
		}

		if (transform.position.z < 0) {
			Vector3 p = transform.position;
			p.z = 0;
			transform.position = p;
		} else if (transform.position.z > cfg.FIELD_LEN_Z) {
			Vector3 p = transform.position;
			p.z = cfg.FIELD_LEN_Z;
			transform.position = p;
		}
	}
}
