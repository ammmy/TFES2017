using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	Config cfg;
	PlayerManager playerManager;
	EnemyManager enemyManager;
	ItemGenerator itemGenerator;
	FieldAnimation fieldAnimation;
	SceneGui sceneGui;
	ScoreGui scoreGui;
	public int score = 0;
	public static bool isDebugging = true;
	bool debug = false;
	bool awake = true;
	int highScoreValue;
	string highScoreTime;
	[SerializeField]
	AudioClip[] mainBGMClip;
	[SerializeField]
	AudioClip GameClearClip;
	AudioSource audioSource;
	bool changeBGM = false;
	int bgmIdx = 0;

	enum State
	{
		Title,
		Playing,
		Pausing,
		Result}

	;

	State state = State.Title;

	// Use this for initialization
	void Awake ()
	{
		cfg = GetComponent<Config> ();
		cfg.Initialize ();

		GameObject objPlayer, objField, objEnemyManager, objItemGenerator;
		objPlayer = GameObject.Find ("Player");
		objField = GameObject.Find ("Field");
		objEnemyManager = GameObject.Find ("EnemyManager");
		objItemGenerator = GameObject.Find ("ItemGenerator");
		sceneGui = GameObject.Find ("SceneGui").GetComponent<SceneGui> ();
		scoreGui = GameObject.Find ("ScoreGui").GetComponent<ScoreGui> ();

		playerManager = objPlayer.GetComponent<PlayerManager> ();
		fieldAnimation = objField.GetComponent<FieldAnimation> ();
		enemyManager = objEnemyManager.GetComponent<EnemyManager> ();
		itemGenerator = objItemGenerator.GetComponent<ItemGenerator> ();

		playerManager.cfg = cfg;
		fieldAnimation.cfg = cfg;
		enemyManager.cfg = cfg;
		itemGenerator.cfg = cfg;

		enemyManager.gameManager = this;
		playerManager.gameManager = this;

		enemyManager.objPlayer = objPlayer;
		highScoreValue = PlayerPrefs.GetInt (cfg.HIGH_SCORE_VALUE_KEY, 0);
		highScoreTime = PlayerPrefs.GetString (cfg.HIGH_SCORE_TIME_KEY, "");
		Debug.Log (highScoreValue);
		Debug.Log (highScoreTime);

		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = mainBGMClip [bgmIdx];
		audioSource.loop = true;
	}

	void Initialize ()
	{
		if (changeBGM) {
			changeBGM = false;
			audioSource.clip = mainBGMClip [bgmIdx = (bgmIdx + 1) % mainBGMClip.Length];
			audioSource.Play();
		}
		sceneGui.HideAll ();
		scoreGui.Hide ();
		if (!awake) {
			playerManager.Terminate ();
			enemyManager.Terminate ();
			itemGenerator.Terminate ();
		} else {
			awake = false;
		}
		score = 0;
		playerManager.Initialize ();
		enemyManager.Initialize ();
		itemGenerator.Initialize ();
		scoreGui.Initialize ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown (cfg.KEY_NAME_ENTER)) {
			if (!(state == State.Title || state == State.Result))
				return;

			state = State.Playing;
			StartGame ();
		}

		if (Input.GetButtonDown (cfg.KEY_NAME_ESC))
			GameOver ();

		if (Input.GetButtonDown (cfg.KEY_NAME_DEBUG))
			SwitchDebugMode ();
	}

	public void AddScore (int i)
	{
		if (IsPlaying ())
			score += i;
	}

	bool IsPlaying ()
	{
		return state == State.Playing;
	}

	void StartGame ()
	{
		Initialize ();
	}

	void GameOver ()
	{
		if (IsPlaying ()) {
			sceneGui.SetGameOver ();
			playerManager.Terminate ();
			itemGenerator.Terminate ();
			ShowResult ();
		}
	}

	void ShowResult ()
	{
		state = State.Result;
		scoreGui.SetDigits (score);
		scoreGui.Show ();
		if (score > highScoreValue) {
			highScoreValue = score;
			highScoreTime = System.DateTime.Now.ToString ();
			PlayerPrefs.SetInt (cfg.HIGH_SCORE_VALUE_KEY, highScoreValue);
			PlayerPrefs.SetString (cfg.HIGH_SCORE_TIME_KEY, highScoreTime);
			scoreGui.ShowHighScore ();
		}
	}

	void GameClear (int i)
	{
		if (IsPlaying ()) {
			score += i;
			audioSource.PlayOneShot (GameClearClip);
			changeBGM = true;
			sceneGui.SetGameClear ();
			enemyManager.Terminate ();
			itemGenerator.Terminate ();
			ShowResult ();
		}
	}

	void SwitchDebugMode ()
	{
		playerManager.Debugging (debug = !debug);
	}
}
