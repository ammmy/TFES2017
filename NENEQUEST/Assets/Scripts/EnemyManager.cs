using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	[System.NonSerialized]
	public Config cfg;
	[System.NonSerialized]
	public GameManager gameManager;
	[System.NonSerialized]
	public GameObject objPlayer;

	public GameObject[] objEnemy;
	bool[] flagEnemy;
	float spawnPosX;
	float startTime;
	List<Coroutine> coroutineList = new List<Coroutine> ();
	public List<GameObject> enemy2Garbage;

	enum Phase
	{
		Phase_0 = 0,
		Phase_1,
		Phase_2,
		Phase_3,
		Phase_4,
		Generating,
		Termination,
		Debugging}

	;

	Phase phase = Phase.Termination, prePhase = Phase.Termination;

	public void Initialize ()
	{
		flagEnemy = new bool[cfg.ENEMY_TYPE_NUM];
		phase = prePhase = Phase.Termination;
		ClearGeneratedObject ();
		for (int i = 0; i < cfg.ENEMY_TYPE_NUM; i++)
			flagEnemy [i] = false;
		phase = Phase.Phase_0;
		spawnPosX = cfg.FIELD_LEN_X + 10f;
	}

	void ClearGeneratedObject ()
	{
		foreach (Transform t in transform)
			Destroy (t.gameObject);
		foreach (GameObject obj in enemy2Garbage)
			Destroy (obj);
	}

	public void Terminate ()
	{
		phase = Phase.Termination;
		ClearGeneratedObject ();
		foreach (Coroutine c in coroutineList) {
			if (c != null)
				StopCoroutine (c);
		}
		coroutineList.Clear ();
	}

	// Update is called once per frame
	void Update ()
	{
		switch (phase) {
		case Phase.Debugging:
			Debugging ();
			break;
		case Phase.Generating:
			break;
		case Phase.Termination:
			break;
		default:
			Generate ();
			break;
		}
	}

	void Generate ()
	{
		switch (phase) {
		case Phase.Phase_0:
			coroutineList.Add (StartCoroutine (Phase_0 ()));
			break;
		case Phase.Phase_1:
			coroutineList.Add (StartCoroutine (Phase_1 ()));
			break;
		case Phase.Phase_2:
			coroutineList.Add (StartCoroutine (Phase_2 ()));
			break;
		case Phase.Phase_3:
			coroutineList.Add (StartCoroutine (Phase_3 ()));
			break;
		case Phase.Phase_4:
			coroutineList.Add (StartCoroutine (Phase_4 ()));
			break;
		}
	}

	Vector3 getP (float r, float min, float max)
	{
		if (Random.Range (0f, 1f) > r)
			return AddNoiseZVector (GetPlayerPos (), min, max);
		return GetRandomPos ();
	}

	IEnumerator CreateEnemy0 ()
	{
		int idx = cfg.ENEMY_0_IDX;
		flagEnemy [idx] = false;
		int enemyNum = cfg.ENEMY_NUM [(int)prePhase, idx];
		while (enemyNum > 0) {
			switch (prePhase) {
			case Phase.Phase_0:
				CreateEnemy (idx, getP (0.9f, -1, 1));
				yield return new WaitForSeconds (Random.Range (2.5f, 4f));
				break;
			case Phase.Phase_1:
				CreateEnemy (idx, getP (0.7f, -1, 1));
				yield return new WaitForSeconds (1 + Random.Range (2f, 3.5f));
				break;
			case Phase.Phase_2:
				CreateEnemy (idx, getP (0.5f, -1, 1));
				yield return new WaitForSeconds (1 + Random.Range (1f, 3.5f));
				break;
			case Phase.Phase_3:
				CreateEnemy (idx, getP (0.3f, -1, 1));
				yield return new WaitForSeconds (1 + Random.Range (1f, 2f));
				break;
			}
			enemyNum--;
		}
		flagEnemy [idx] = true;
	}

	IEnumerator CreateEnemy1 ()
	{
		yield return new WaitForSeconds (Random.Range (0.5f, 2f));
		int idx = cfg.ENEMY_1_IDX;
		flagEnemy [idx] = false;
		int enemyNum = cfg.ENEMY_NUM [(int)prePhase, idx];
		while (enemyNum > 0) {
			switch (prePhase) {
			case Phase.Phase_0:
				CreateEnemy (idx, getP (0.9f, -1, 1));
				yield return new WaitForSeconds (Random.Range (4f, 8f));
				break;
			case Phase.Phase_1:
				CreateEnemy (idx, getP (0.8f, -1, 1));
				yield return new WaitForSeconds (Random.Range (3f, 5f));
				break;
			case Phase.Phase_2:
				CreateEnemy (idx, getP (0.8f, -1, 1));
				yield return new WaitForSeconds (Random.Range (2f, 4f));
				break;
			case Phase.Phase_3:
				CreateEnemy (idx, getP (0.7f, -1, 1));
				yield return new WaitForSeconds (Random.Range (1f, 3f));
				break;
			}
			enemyNum--;
		}
		flagEnemy [idx] = true;
	}

	void CreateEnemy2 ()
	{
		int idx = cfg.ENEMY_2_IDX;
		Vector3 p;
		if (Random.Range (0f, 1f) > 0.5f)
			p = AddNoiseZVector (GetPlayerPos (), -1f, 1f);
		else
			p = GetRandomPos ();
		CreateEnemy (idx, p);
	}

	IEnumerator Phase_0 ()
	{
		prePhase = phase;
		phase = Phase.Generating;
		coroutineList.Add (StartCoroutine (CreateEnemy0 ()));
		coroutineList.Add (StartCoroutine (CreateEnemy1 ()));
		while (!(flagEnemy [cfg.ENEMY_0_IDX] && flagEnemy [cfg.ENEMY_1_IDX]))
			yield return null;
		phase = Phase.Phase_1;
	}

	private IEnumerator Phase_1 ()
	{
		prePhase = phase;
		phase = Phase.Generating;
		coroutineList.Add (StartCoroutine (CreateEnemy0 ()));
		coroutineList.Add (StartCoroutine (CreateEnemy1 ()));
		while (!(flagEnemy [cfg.ENEMY_0_IDX] && flagEnemy [cfg.ENEMY_1_IDX]))
			yield return null;
		phase = Phase.Phase_2;
	}

	private IEnumerator Phase_2 ()
	{
		prePhase = phase;
		phase = Phase.Generating;
		coroutineList.Add (StartCoroutine (CreateEnemy0 ()));
		coroutineList.Add (StartCoroutine (CreateEnemy1 ()));
		while (!(flagEnemy [cfg.ENEMY_0_IDX] && flagEnemy [cfg.ENEMY_1_IDX]))
			yield return null;
		phase = Phase.Phase_3;
	}

	private IEnumerator Phase_3 ()
	{
		prePhase = phase;
		phase = Phase.Generating;
		coroutineList.Add (StartCoroutine (CreateEnemy0 ()));
		coroutineList.Add (StartCoroutine (CreateEnemy1 ()));
		while (!(flagEnemy [cfg.ENEMY_0_IDX] && flagEnemy [cfg.ENEMY_1_IDX]))
			yield return null;
		phase = Phase.Phase_4;
	}

	IEnumerator Phase_4 ()
	{
		prePhase = phase;
		phase = Phase.Generating;
		yield return new WaitForSeconds (1);
		CreateEnemy2 ();
		startTime = Time.time;
	}

	public void AddScore (int i)
	{
		gameManager.SendMessage ("AddScore", i);
	}

	public void GameClear ()
	{
		gameManager.SendMessage ("GameClear", (int)Mathf.Max (0, 180 - Time.time + startTime));
	}

	void Debugging ()
	{
		if (Input.GetKeyDown (KeyCode.F))
			CreateEnemy (0, new Vector3 (25, 0, Random.Range (0f, 15f)));
	}

	void CreateEnemy (int i, Vector3 position)
	{
		GameObject enemy = Instantiate (objEnemy [i]);
		enemy.transform.parent = transform;
		enemy.transform.position = position;
		if (i == cfg.ENEMY_0_IDX) {
			Enemy0Action enemyAction = enemy.GetComponent<Enemy0Action> ();
			enemyAction.cfg = cfg;
			enemyAction.Initialize (i);
		} else if (i == cfg.ENEMY_1_IDX) {
			Enemy1Action enemyAction = enemy.GetComponent<Enemy1Action> ();
			enemyAction.cfg = cfg;
			enemyAction.objTarget = objPlayer;
			enemyAction.Initialize (i);
		} else if (i == cfg.ENEMY_2_IDX) {
			Enemy2Action enemyAction = enemy.GetComponent<Enemy2Action> ();
			enemyAction.cfg = cfg;
			enemyAction.objTarget = objPlayer;
			enemyAction.enemyManager = this;
			enemyAction.Initialize (i);
		}
	}

	Vector3 GetPlayerPos ()
	{
		Vector3 p = objPlayer.transform.position;
		p.x = spawnPosX;
		return p;
	}

	float GetRandomZ ()
	{
		return Random.Range (0f, cfg.FIELD_LEN_Z);
	}

	Vector3 AddNoiseZVector (Vector3 v, float min, float max)
	{
		v.z += Random.Range (min, max);
		v.z = Mathf.Clamp (v.z, 0, cfg.FIELD_LEN_Z);
		return v;
	}

	Vector3 GetRandomZVector ()
	{
		return new Vector3 (0, 0, GetRandomZ ());
	}

	Vector3 GetRandomPos ()
	{
		return new Vector3 (spawnPosX, 0, GetRandomZ ());
	}
}
